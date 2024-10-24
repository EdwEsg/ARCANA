﻿using System.Security.Claims;
using System.Transactions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Sales_Management.Sales_Transactions;
using static RDF.Arcana.API.Features.Sales_Management.Payment_Transaction.AddNewPaymentTransaction.AddNewPaymentTransactionCommand;

namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction;

[Route("api/payment"), ApiController]
public class AddNewPaymentTransaction : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> AddPayment([FromForm] AddNewPaymentTransactionCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }
            var result = await Mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    public class AddNewPaymentTransactionCommand : IRequest<Result>
    {
        public List<int> TransactionId { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public IFormFile Receipt { get; set; } //receipt
        public string ReceiptNo { get; set; }
        public int AddedBy { get; set; }
        public class Payment
        {
            public string PaymentMethod { get; set; }
            public decimal PaymentAmount { get; set; }
            public decimal TotalAmountReceived { get; set; }
            public string Payee { get; set; }
            public DateTime ChequeDate { get; set; }
            public string BankName { get; set; }
            public string ChequeNo { get; set; }
            public DateTime DateReceived { get; set; }
            public decimal ChequeAmount { get; set; }
            public string AccountName { get; set; }
            public string AccountNo { get; set; }
            public int OnlinePlatform { get; set; }
            public string ReferenceNo { get; set; }
            public string WithholdingNo { get; set; }
            public IFormFile WithholdingAttachment { get; set; }
            public int? OthersPayment { get; set; }

        }
    
    }
    
    public class Handler : IRequestHandler<AddNewPaymentTransactionCommand, Result>
    {
        private readonly ArcanaDbContext _context;
        private readonly Cloudinary _cloudinary;

        public Handler(ArcanaDbContext context, IOptions<CloudinaryOptions> options)
        {
            _context = context;

            var account = new Account(
                options.Value.Cloudname,
                options.Value.ApiKey,
                options.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<Result> Handle(AddNewPaymentTransactionCommand request, CancellationToken cancellationToken)
        {
            decimal totalAmount = 0;
            string receiptUpload = string.Empty;

            //Get the sum of the total amount due of the transactions selected
            foreach (var transactionId in request.TransactionId)
            {
                var transactions = await _context.Transactions
                    .Include(sales => sales.TransactionSales)
                    .FirstOrDefaultAsync(tr => tr.Id == transactionId);

                if(transactions is not null)
                {
                    totalAmount += transactions.TransactionSales.TotalAmountDue;
                }
                else if (transactions.Status == Status.Paid)
                {
                    return TransactionErrors.AlreadyPaid();
                }
                else
                {
                    return TransactionErrors.NotFound();
                }
            }


            //Create New Payment Records
            if (request.Receipt != null && request.Receipt.Length > 0)
            {
                await using var stream = request.Receipt.OpenReadStream(); // Open the uploaded file stream

                var attachmentsParams = new ImageUploadParams
                {
                    File = new FileDescription(request.Receipt.FileName, stream), // Create file description
                    PublicId = request.Receipt.FileName // Use the file name for Cloudinary ID
                };

                var receiptUploadResult = await _cloudinary.UploadAsync(attachmentsParams); // Upload file to Cloudinary

                // Store the uploaded file URL 
                receiptUpload = receiptUploadResult.SecureUrl.ToString();
            }

            var paymentRecord = new PaymentRecords
            {
                AddedBy = request.AddedBy,
                ModifiedBy = request.AddedBy,
                Status = Status.ForClearing,
                ClientId = _context.Transactions.FirstOrDefault(tr => tr.Id == request.TransactionId.FirstOrDefault())?.ClientId, 
                Receipt = receiptUpload,
                ReceiptNo = request.ReceiptNo
                
            };

            await _context.PaymentRecords.AddAsync(paymentRecord, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            // Order transactions by total amount due (descending)
            var orderedTransactions = request.TransactionId
                .OrderByDescending(tid => _context.Transactions
                    .FirstOrDefault(t => t.Id == tid)?.TransactionSales.RemainingBalance ?? 0)
                .ToList();

            //To track the number of transactions
            int totalTransactions = orderedTransactions.Count;
            int currentIteration = 0;

            var originalPaymentAmounts = request.Payments.ToDictionary(p => p, p => p.PaymentAmount);

            foreach (int transactionId in orderedTransactions)
            {
                currentIteration++;


                var transaction = await _context.Transactions
                    .Include(t => t.TransactionSales)
                    .FirstOrDefaultAsync(t => t.Id == transactionId, cancellationToken);

                if (transaction is null)
                {
                    return TransactionErrors.NotFound();
                }


                // Order payments by payment amount by their order 
                var orderedPayments = request.Payments
                    .Where(p => p.PaymentAmount > 0)
                    .ToList();

                foreach (var payment in orderedPayments) 
                {
                    decimal origPaymentAmount = originalPaymentAmounts[payment];

                    decimal amountToPay = transaction.TransactionSales.RemainingBalance;

                    if (payment.PaymentAmount == 0)
                    {
                        continue;
                    }

                    // If nothing more to pay for this transaction, move to the next
                    if (amountToPay <= 0 || payment.PaymentAmount <= 0)  
                    {
                        break;
                    }

                    decimal excessAmount = amountToPay - payment.PaymentAmount;
                    
                    if (excessAmount < 0)
                    {
                        excessAmount = 0;
                    }

                    decimal paymentAmount = payment.PaymentAmount;












                    if (payment.PaymentMethod == PaymentMethods.Cheque)
                    {

                        var transactionSales = await _context.TransactionSales
                            .FirstOrDefaultAsync(ts => ts.TransactionId == transaction.Id, cancellationToken);

                        if (transactionSales == null)
                        {
                            return TransactionErrors.NotFound();
                        }

                        decimal totalAmountDue = transactionSales.TotalAmountDue;
                        decimal remainingBalance = transactionSales.RemainingBalance;
                        excessAmount = 0;

                        if (payment.PaymentAmount > remainingBalance)
                        {
                            excessAmount = payment.PaymentAmount - remainingBalance;
                            payment.PaymentAmount = remainingBalance;
                        }


                        var paymentTransaction = new PaymentTransaction
                        {
                            TransactionId = transaction.Id,
                            AddedBy = request.AddedBy,
                            PaymentRecordId = paymentRecord.Id,
                            PaymentMethod = payment.PaymentMethod,
                            PaymentAmount = origPaymentAmount,
                            TotalAmountReceived = payment.PaymentAmount,
                            Payee = payment.Payee,
                            ChequeDate = payment.ChequeDate,
                            BankName = payment.BankName,
                            ChequeNo = payment.ChequeNo,
                            DateReceived = DateTime.Now,
                            ChequeAmount = payment.ChequeAmount,
                            AccountName = payment.AccountName,
                            AccountNo = payment.AccountNo,
                            Status = Status.ForClearing,
                            OnlinePlatform = payment.OnlinePlatform,
                            ReferenceNo = payment.ChequeNo
                        };

                        await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        transactionSales.RemainingBalance -= payment.PaymentAmount;
                        transactionSales.RemainingBalance = transactionSales.RemainingBalance < 0 ? 0 : transactionSales.RemainingBalance;
                        transactionSales.UpdatedAt = DateTime.Now;

                        if (transactionSales.RemainingBalance == 0)
                        {
                            transaction.Status = Status.Paid;
                        }

                        await _context.SaveChangesAsync(cancellationToken);

                        if (currentIteration == totalTransactions)
                        {
                            if (paymentAmount > 0 && paymentAmount > remainingBalance)
                            {
                                var advancePayment = new AdvancePayment
                                {
                                    ClientId = transaction.ClientId,
                                    PaymentMethod = payment.PaymentMethod,
                                    AdvancePaymentAmount = excessAmount,
                                    RemainingBalance = excessAmount,
                                    Payee = payment.Payee,
                                    ChequeDate = payment.ChequeDate,
                                    BankName = payment.BankName,
                                    ChequeNo = payment.ChequeNo,
                                    DateReceived = payment.DateReceived,
                                    ChequeAmount = origPaymentAmount,
                                    AccountName = payment.AccountName,
                                    AccountNo = payment.AccountNo,
                                    AddedBy = request.AddedBy,
                                    Origin = Origin.Excess,
                                    PaymentTransactionId = paymentTransaction.Id
                                };

                                await _context.AdvancePayments.AddAsync(advancePayment, cancellationToken);
                                await _context.SaveChangesAsync(cancellationToken);
                            }
                        }

                        payment.PaymentAmount = excessAmount;
                    }








                    if (payment.PaymentMethod == PaymentMethods.ListingFee)
                    {
                        // Get client IDs for each transaction
                        var transactionClientIds = await _context.Transactions
                            .Where(t => request.TransactionId.Contains(t.Id))
                            .Select(t => t.ClientId)
                            .Distinct()
                            .ToListAsync(cancellationToken);

                        // Get listing fees for each client
                        var listingFees = await _context.ListingFees
                            .Where(x =>
                                transactionClientIds.Contains(x.ClientId) &&
                                x.IsActive &&
                                x.Status == Status.Approved &&
                                x.Total > 0)
                            .OrderBy(x => x.CratedAt) 
                            .ToListAsync(cancellationToken);

                        // Sum the payment amount for ListingFee
                        var amountToPayListingFee = request.Payments
                            .Where(pm => pm.PaymentMethod == PaymentMethods.ListingFee)
                            .Sum(pa => pa.PaymentAmount);

                        foreach (var currentTransactionId in orderedTransactions)
                        {
                            var currentTransaction = await _context.Transactions
                                .Include(t => t.TransactionSales)
                                .FirstOrDefaultAsync(t => t.Id == currentTransactionId, cancellationToken);

                            if (currentTransaction == null)
                            {
                                continue;
                            }

                            while (currentTransaction.TransactionSales.RemainingBalance > 0 && amountToPayListingFee > 0)
                            {
                                var listingFee = listingFees.FirstOrDefault(x => x.ClientId == currentTransaction.ClientId && x.Total > 0);
                                if (listingFee == null)
                                {
                                    break; // No more listing fees available
                                }

                                decimal paymentAmountForTransaction = Math.Min(currentTransaction.TransactionSales.RemainingBalance, amountToPayListingFee);
                                paymentAmountForTransaction = Math.Min(paymentAmountForTransaction, listingFee.Total);


                                // Create payment transaction
                                var paymentTransaction = new PaymentTransaction
                                {
                                    TransactionId = currentTransaction.Id,
                                    AddedBy = request.AddedBy,
                                    PaymentRecordId = paymentRecord.Id,
                                    PaymentMethod = payment.PaymentMethod,
                                    PaymentAmount = origPaymentAmount,
                                    TotalAmountReceived = paymentAmountForTransaction,
                                    Payee = payment.Payee,
                                    ChequeDate = payment.ChequeDate,
                                    BankName = payment.BankName,
                                    ChequeNo = payment.ChequeNo,
                                    DateReceived = DateTime.Now,
                                    ChequeAmount = payment.ChequeAmount,
                                    AccountName = payment.AccountName,
                                    AccountNo = payment.AccountNo,
                                    Status = Status.ForClearing,
                                    OnlinePlatform = payment.OnlinePlatform,
                                    ReferenceNo = payment.ReferenceNo
                                };

                                await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);

                                amountToPayListingFee -= paymentAmountForTransaction;
                                currentTransaction.TransactionSales.RemainingBalance -= paymentAmountForTransaction;
                                listingFee.Total -= paymentAmountForTransaction;

                                if (listingFee.Total <= 0)
                                {
                                    listingFee.Total = 0;
                                    listingFees.Remove(listingFee);
                                }

                                currentTransaction.Status = currentTransaction.TransactionSales.RemainingBalance <= 0 ? Status.Paid : Status.Pending;

                                await _context.SaveChangesAsync(cancellationToken);

                                if (currentTransaction.TransactionSales.RemainingBalance <= 0)
                                {
                                    break;
                                }
                            }

                            if (amountToPayListingFee <= 0)
                            {
                                break; // No more funds available
                            }
                        }

                        payment.PaymentAmount = amountToPayListingFee;
                        await _context.SaveChangesAsync(cancellationToken);
                    }














                    if (payment.PaymentMethod == PaymentMethods.Others)
                    {
                        // Get client IDs for each transaction
                        var transactionClientIds = await _context.Transactions
                            .Where(t => request.TransactionId.Contains(t.Id))
                            .Select(t => t.ClientId)
                            .Distinct()
                            .ToListAsync(cancellationToken);

                        // Get other for each client
                        var others = await _context.ExpensesRequests
                            .Where(x =>
                                transactionClientIds.Contains(x.ClientId) &&
                                x.Status == Status.Approved &&
                                x.RemainingBalance > 0 &&
                                x.OtherExpenseId == payment.OthersPayment)
                            .OrderBy(x => x.CreatedAt)
                            .ToListAsync(cancellationToken);

                        // Sum the payment amount for Others
                        var amountToPayOthers = request.Payments
                            .Where(pm => pm.PaymentMethod == PaymentMethods.Others && others.Any(o => pm.OthersPayment == o.OtherExpenseId))
                            .Sum(pa => pa.PaymentAmount);

                        foreach (var currentTransactionId in orderedTransactions)
                        {
                            var currentTransaction = await _context.Transactions
                                .Include(t => t.TransactionSales)
                                .FirstOrDefaultAsync(t => t.Id == currentTransactionId, cancellationToken);

                            if (currentTransaction == null)
                            {
                                continue;
                            }

                            while (currentTransaction.TransactionSales.RemainingBalance > 0 && amountToPayOthers > 0)
                            {
                                var other = others.FirstOrDefault(x => x.ClientId == currentTransaction.ClientId && x.RemainingBalance > 0);
                                if (other == null)
                                {
                                    break; // No more others available
                                }

                                decimal paymentAmountForTransaction = Math.Min(currentTransaction.TransactionSales.RemainingBalance, amountToPayOthers);
                                paymentAmountForTransaction = Math.Min(paymentAmountForTransaction, other.RemainingBalance);

                                

                                // Create payment transaction
                                var paymentTransaction = new PaymentTransaction
                                {
                                    TransactionId = currentTransaction.Id,
                                    AddedBy = request.AddedBy,
                                    PaymentRecordId = paymentRecord.Id,
                                    PaymentMethod = payment.PaymentMethod,
                                    PaymentAmount = origPaymentAmount,
                                    TotalAmountReceived = paymentAmountForTransaction,
                                    Payee = payment.Payee,
                                    ChequeDate = payment.ChequeDate,
                                    BankName = payment.BankName,
                                    ChequeNo = payment.ChequeNo,
                                    DateReceived = DateTime.Now,
                                    ChequeAmount = payment.ChequeAmount,
                                    AccountName = payment.AccountName,
                                    AccountNo = payment.AccountNo,
                                    Status = Status.ForClearing,
                                    OnlinePlatform = payment.OnlinePlatform,
                                    ReferenceNo = payment.ReferenceNo,
                                    ExpensesRequestId = payment.OthersPayment ?? null
                                };

                                await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);

                                amountToPayOthers -= paymentAmountForTransaction;
                                currentTransaction.TransactionSales.RemainingBalance -= paymentAmountForTransaction;
                                other.RemainingBalance -= paymentAmountForTransaction;

                                if (other.RemainingBalance <= 0)
                                {
                                    other.RemainingBalance = 0;
                                    others.Remove(other);
                                }

                                currentTransaction.Status = currentTransaction.TransactionSales.RemainingBalance <= 0 ? Status.Paid : Status.Pending;

                                await _context.SaveChangesAsync(cancellationToken);

                                if (currentTransaction.TransactionSales.RemainingBalance <= 0)
                                {
                                    break;
                                }
                            }

                            if (amountToPayOthers <= 0)
                            {
                                break; // No more funds available
                            }
                        }

                        payment.PaymentAmount = amountToPayOthers;
                        await _context.SaveChangesAsync(cancellationToken);
                    }














                    if (payment.PaymentMethod == PaymentMethods.AdvancePayment)
                    {
                        // Get client IDs for each transaction
                        var transactionClientIds = await _context.Transactions
                            .Where(t => request.TransactionId.Contains(t.Id))
                            .Select(t => t.ClientId)
                            .Distinct()
                            .ToListAsync(cancellationToken);

                        // Get advance payments for each client
                        var advancePayments = await _context.AdvancePayments
                            .Where(x =>
                                transactionClientIds.Contains(x.ClientId) &&
                                x.IsActive &&
                                x.Status != Status.Voided &&
                                x.RemainingBalance > 0)
                            .OrderBy(x => x.CreatedAt) // Assuming FIFO, order by CreatedAt or another appropriate property
                            .ToListAsync(cancellationToken);

                        // Sum the payment amount for AdvancePayment
                        var amountToPayAdvancePayment = request.Payments
                            .Where(pm => pm.PaymentMethod == PaymentMethods.AdvancePayment)
                            .Sum(pa => pa.PaymentAmount);

                        foreach (var currentTransactionId in orderedTransactions)
                        {
                            var currentTransaction = await _context.Transactions
                                .Include(t => t.TransactionSales)
                                .FirstOrDefaultAsync(t => t.Id == currentTransactionId, cancellationToken);

                            if (currentTransaction == null)
                            {
                                continue;
                            }

                            while (currentTransaction.TransactionSales.RemainingBalance > 0 && amountToPayAdvancePayment > 0)
                            {
                                var advancePayment = advancePayments.FirstOrDefault(x => x.ClientId == currentTransaction.ClientId && x.RemainingBalance > 0);
                                if (advancePayment == null)
                                {
                                    break; // No more advance payments available
                                }

                                decimal paymentAmountForTransaction = Math.Min(currentTransaction.TransactionSales.RemainingBalance, amountToPayAdvancePayment);
                                paymentAmountForTransaction = Math.Min(paymentAmountForTransaction, advancePayment.RemainingBalance);


                                // Create payment transaction
                                var paymentTransaction = new PaymentTransaction
                                {
                                    TransactionId = currentTransaction.Id,
                                    AddedBy = request.AddedBy,
                                    PaymentRecordId = paymentRecord.Id,
                                    PaymentMethod = payment.PaymentMethod,
                                    PaymentAmount = origPaymentAmount,
                                    TotalAmountReceived = paymentAmountForTransaction,
                                    Payee = payment.Payee,
                                    ChequeDate = payment.ChequeDate,
                                    BankName = payment.BankName,
                                    ChequeNo = payment.ChequeNo,
                                    DateReceived = DateTime.Now,
                                    ChequeAmount = payment.ChequeAmount,
                                    AccountName = payment.AccountName,
                                    AccountNo = payment.AccountNo,
                                    Status = Status.ForClearing,
                                    OnlinePlatform = payment.OnlinePlatform,
                                    ReferenceNo = payment.ReferenceNo
                                };

                                await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);

                                amountToPayAdvancePayment -= paymentAmountForTransaction;
                                currentTransaction.TransactionSales.RemainingBalance -= paymentAmountForTransaction;
                                advancePayment.RemainingBalance -= paymentAmountForTransaction;

                                if (advancePayment.RemainingBalance <= 0)
                                {
                                    advancePayment.RemainingBalance = 0;
                                    advancePayments.Remove(advancePayment);
                                }

                                currentTransaction.Status = currentTransaction.TransactionSales.RemainingBalance <= 0 ? Status.Paid : Status.Pending;

                                await _context.SaveChangesAsync(cancellationToken);

                                if (currentTransaction.TransactionSales.RemainingBalance <= 0)
                                {
                                    break;
                                }
                            }

                            if (amountToPayAdvancePayment <= 0)
                            {
                                break; // No more funds available
                            }
                        }

                        payment.PaymentAmount = amountToPayAdvancePayment;
                        await _context.SaveChangesAsync(cancellationToken);
                    }













                    if (payment.PaymentMethod == PaymentMethods.Withholding)
                    {

                        decimal remainingToPay = amountToPay;
                        Payment currentPayment = null;

                        foreach (var paymentItem in orderedPayments)
                        {
                            if(paymentItem.PaymentAmount == 0)
                            {
                                continue;
                            }

                            string withholdingAttachmentUrl = null;
                            string withholdingNumber = null;

                            if (paymentItem.PaymentMethod != PaymentMethods.Withholding)
                            {
                                break;
                            }

                            if (paymentItem.PaymentAmount <= 0 || remainingToPay <= 0)
                            {
                                continue;
                            }

                            if(paymentItem.WithholdingAttachment != null)
                            {
                                if (payment.WithholdingAttachment.Length > 0)
                                {
                                    await using var stream = payment.WithholdingAttachment.OpenReadStream(); // Open the uploaded file stream

                                    var attachmentsParams = new ImageUploadParams
                                    {
                                        File = new FileDescription(payment.WithholdingAttachment.FileName, stream), // Create file description
                                        PublicId = payment.WithholdingAttachment.FileName // Use the file name for Cloudinary ID
                                    };

                                    var attachmentsUploadResult = await _cloudinary.UploadAsync(attachmentsParams); // Upload file to Cloudinary

                                    // Store the uploaded file URL and withholding number
                                    withholdingAttachmentUrl = attachmentsUploadResult.SecureUrl.ToString();
                                    withholdingNumber = payment.WithholdingNo;
                                }
                            }
                            else
                            {
                                return TransactionErrors.NullWithholding();
                            }


                            currentPayment = paymentItem;
                            decimal currentPaymentAmount = currentPayment.PaymentAmount;

                            // Calculate the remaining amount to pay for this transaction
                            decimal paymentToApply = currentPaymentAmount <= remainingToPay ? currentPaymentAmount : remainingToPay;
                            remainingToPay -= paymentToApply;


                            var paymentTransaction = new PaymentTransaction
                            {
                                TransactionId = transaction.Id,
                                AddedBy = request.AddedBy,
                                PaymentRecordId = paymentRecord.Id,
                                PaymentMethod = currentPayment.PaymentMethod,
                                PaymentAmount = origPaymentAmount,
                                TotalAmountReceived = paymentToApply,
                                Payee = currentPayment.Payee,
                                ChequeDate = currentPayment.ChequeDate,
                                BankName = currentPayment.BankName,
                                ChequeNo = currentPayment.ChequeNo,
                                DateReceived = DateTime.Now,
                                ChequeAmount = currentPayment.ChequeAmount,
                                AccountName = currentPayment.AccountName,
                                AccountNo = currentPayment.AccountNo,
                                Status = Status.ForFiling,
                                OnlinePlatform = currentPayment.OnlinePlatform,
                                ReferenceNo = transaction.InvoiceNo,
                                WithholdingAttachment = withholdingAttachmentUrl,
                                WithholdingNo = withholdingNumber
                            };

                            await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);

                            // Update the remaining balance of the transaction
                            transaction.TransactionSales.RemainingBalance = remainingToPay;
                            transaction.Status = remainingToPay <= 0 ? Status.Paid : Status.Pending;

                            // Adjust the payment amount for any remaining balance
                            currentPayment.PaymentAmount -= paymentToApply;
                            excessAmount = currentPayment.PaymentAmount;

                            await _context.SaveChangesAsync(cancellationToken);

                            if (remainingToPay <= 0)
                            {
                                break;
                            }
                        }

                        if (currentPayment != null && currentPayment.PaymentMethod == payment.PaymentMethod)
                        {
                            payment.PaymentAmount = excessAmount;
                        }

                        await _context.SaveChangesAsync(cancellationToken);
                    }















                    if (payment.PaymentMethod == PaymentMethods.Online)
                    {

                        decimal remainingToPay = amountToPay;
                        Payment currentPayment = null;

                        foreach (var paymentItem in orderedPayments)
                        {
                            if (paymentItem.PaymentAmount == 0)
                            {
                                continue;
                            }

                            if (paymentItem.PaymentMethod != PaymentMethods.Online)
                            {
                                break;
                            }

                            if (paymentItem.PaymentAmount <= 0 || remainingToPay <= 0)
                            {
                                continue;
                            }

                            
                            currentPayment = paymentItem;
                            decimal currentPaymentAmount = currentPayment.PaymentAmount;

                            // Calculate the remaining amount to pay for this transaction
                            decimal paymentToApply = currentPaymentAmount <= remainingToPay ? currentPaymentAmount : remainingToPay;
                            remainingToPay -= paymentToApply;


                            var paymentTransaction = new PaymentTransaction
                            {
                                TransactionId = transaction.Id,
                                AddedBy = request.AddedBy,
                                PaymentRecordId = paymentRecord.Id,
                                PaymentMethod = currentPayment.PaymentMethod,
                                PaymentAmount = origPaymentAmount,
                                TotalAmountReceived = paymentToApply,
                                Payee = currentPayment.Payee,
                                ChequeDate = currentPayment.ChequeDate,
                                BankName = currentPayment.BankName,
                                ChequeNo = currentPayment.ChequeNo,
                                DateReceived = DateTime.Now,
                                ChequeAmount = currentPayment.ChequeAmount,
                                AccountName = currentPayment.AccountName,
                                AccountNo = currentPayment.AccountNo,
                                Status = Status.ForClearing,
                                OnlinePlatform = currentPayment.OnlinePlatform,
                                ReferenceNo = transaction.InvoiceNo,
                            };

                            await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);

                            // Update the remaining balance of the transaction
                            transaction.TransactionSales.RemainingBalance = remainingToPay;
                            transaction.Status = remainingToPay <= 0 ? Status.Paid : Status.Pending;

                            // Adjust the payment amount for any remaining balance
                            currentPayment.PaymentAmount -= paymentToApply;
                            excessAmount = currentPayment.PaymentAmount;

                            await _context.SaveChangesAsync(cancellationToken);

                            if (remainingToPay <= 0)
                            {
                                break;
                            }
                        }

                        if (currentPayment != null && currentPayment.PaymentMethod == payment.PaymentMethod)
                        {
                            payment.PaymentAmount = excessAmount;
                        }

                        await _context.SaveChangesAsync(cancellationToken);
                    }














                    if (payment.PaymentMethod == PaymentMethods.Cash)
                    {

                        decimal remainingToPay = amountToPay;
                        Payment currentPayment = null;

                        foreach (var paymentItem in orderedPayments)
                        {
                            if (paymentItem.PaymentAmount == 0)
                            {
                                continue;
                            }

                            if (paymentItem.PaymentMethod != PaymentMethods.Cash)
                            {
                                break;
                            }

                            if (paymentItem.PaymentAmount <= 0 || remainingToPay <= 0)
                            {
                                continue;
                            }


                            currentPayment = paymentItem;
                            decimal currentPaymentAmount = currentPayment.PaymentAmount;

                            // Calculate the remaining amount to pay for this transaction
                            decimal paymentToApply = currentPaymentAmount <= remainingToPay ? currentPaymentAmount : remainingToPay;
                            remainingToPay -= paymentToApply;


                            var paymentTransaction = new PaymentTransaction
                            {
                                TransactionId = transaction.Id,
                                AddedBy = request.AddedBy,
                                PaymentRecordId = paymentRecord.Id,
                                PaymentMethod = currentPayment.PaymentMethod,
                                PaymentAmount = origPaymentAmount,
                                TotalAmountReceived = paymentToApply,
                                Payee = currentPayment.Payee,
                                ChequeDate = currentPayment.ChequeDate,
                                BankName = currentPayment.BankName,
                                ChequeNo = currentPayment.ChequeNo,
                                DateReceived = DateTime.Now,
                                ChequeAmount = currentPayment.ChequeAmount,
                                AccountName = currentPayment.AccountName,
                                AccountNo = currentPayment.AccountNo,
                                Status = Status.ForClearing,
                                OnlinePlatform = currentPayment.OnlinePlatform,
                                ReferenceNo = transaction.InvoiceNo,
                            };

                            await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);

                            // Update the remaining balance of the transaction
                            transaction.TransactionSales.RemainingBalance = remainingToPay;
                            transaction.Status = remainingToPay <= 0 ? Status.Paid : Status.Pending;

                            // Adjust the payment amount for any remaining balance
                            currentPayment.PaymentAmount -= paymentToApply;
                            excessAmount = currentPayment.PaymentAmount;

                            await _context.SaveChangesAsync(cancellationToken);

                            if (remainingToPay <= 0)
                            {
                                break;
                            }
                        }

                        if (currentPayment != null && currentPayment.PaymentMethod == payment.PaymentMethod)
                        {
                            payment.PaymentAmount = excessAmount;
                        }

                        await _context.SaveChangesAsync(cancellationToken);
                    }
















                }
            }

            

            return Result.Success();
        }
    }
}