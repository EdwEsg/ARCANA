﻿using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

using System.Security.Claims;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
{
    [Route("api/clearing-transaction")]
    public class AddClearingTransaction : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddClearingTransaction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("cleared")]
        public async Task<IActionResult> Add([FromBody] AddClearingTransactionCommand command)
        {
            try
            {

                if (User.Identity is ClaimsIdentity identity
                    && IdentityHelper.TryGetUserId(identity, out var userId))
                {
                    command.AddedBy = userId;
                }

                var result = await _mediator.Send(command);

                return result.IsFailure ? BadRequest(result) : Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class AddClearingTransactionCommand : IRequest<Result>
        {
            public int PaymentRecordId { get; set; }
            public string PaymentMethod { get; set; }
            public string ATag { get; set; }
            public int AddedBy { get; set; }
            public int? ModifiedBy { get; set; }

        }

        public class Handler : IRequestHandler<AddClearingTransactionCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddClearingTransactionCommand request, CancellationToken cancellationToken)
            {
                var paymentTransactions = await _context.PaymentTransactions
					.Where(pt => pt.PaymentMethod == request.PaymentMethod && 
                                 pt.PaymentRecordId == request.PaymentRecordId)
					.ToListAsync(cancellationToken);

                foreach (var paymentTransaction in paymentTransactions)
				{
                    var exisitngClearedPayment = await _context.ClearedPayments
                        .FirstOrDefaultAsync(pt => pt.PaymentTransactionId == paymentTransaction.Id, cancellationToken);

                    if(exisitngClearedPayment is not null)
					{
						exisitngClearedPayment.Status = Status.ForFiling;
                        exisitngClearedPayment.ATag = request.ATag;
                        paymentTransaction.Status = Status.ForFiling;
					}
                    else
                    {
						paymentTransaction.Status = Status.ForFiling;


						var clearingTransaction = new ClearedPayments
						{
							PaymentTransactionId = paymentTransaction.Id,
							ATag = request.ATag,
							AddedBy = request.AddedBy,
							ModifiedBy = request.ModifiedBy,
							Status = Status.ForFiling
						};
						_context.ClearedPayments.Add(clearingTransaction);
					}
                    await _context.SaveChangesAsync(cancellationToken);
				}

                //For conformity of paymentRecord Status, but I get Status in paymentTransactions, please disregard this codeblock
                var paymentRecord = _context.PaymentRecords.FirstOrDefault(pr => pr.Id == request.PaymentRecordId);
                paymentRecord.Status = Status.ForFiling;
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();

            }
        }
    }
}
