
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.ListingFee_To_Cheque_Conversion
{
    [Route("api/listing-to-check"), ApiController]
    public class AddListingFeeToChequeConversion : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddListingFeeToChequeConversion(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddListingFeeToChequeConversionCommand command)
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class AddListingFeeToChequeConversionCommand : IRequest<Result>
        {
            public int ClientId { get; set; }
            public string Bank { get; set; }
            public string ChequeNo { get; set; }
            public DateTime ChequeDate { get; set; }
            public string VoucherNo { get; set; }
            public decimal AmountToConvert { get; set; }
            public int AddedBy { get; set; }
        }

        public class Handler : IRequestHandler<AddListingFeeToChequeConversionCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddListingFeeToChequeConversionCommand request, CancellationToken cancellationToken)
            {
                var listingFees = await _context.ListingFees
                    .Where(lf => lf.Status == Status.Approved &&
                                 lf.ClientId == request.ClientId &&
                                 lf.IsActive &&
                                 lf.Total > 0)
                    .OrderBy(cr => cr.CratedAt)
                    .ToListAsync(cancellationToken);

                if (listingFees is null || !listingFees.Any())
                {
                    return ListingFeeToChequeConversionError.NotFound();
                }

                var listingFeesTotal = listingFees.Sum(t => t.Total);

                if (request.AmountToConvert > listingFeesTotal)
                {
                    return ListingFeeToChequeConversionError.Insufficient();
                }

                decimal amountToConvert = request.AmountToConvert;

                foreach (var fee in listingFees)
                {
                    if (amountToConvert <= 0)
                        break;

                    if (amountToConvert >= fee.Total)
                    {
                        amountToConvert -= fee.Total;
                        fee.Total = 0;
                    }
                    else
                    {
                        fee.Total -= amountToConvert;
                        amountToConvert = 0;
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);


                var cheque = new Cheque
                {
                    ClientId = request.ClientId,
                    ListingFeeId = listingFees.First().Id,
                    Bank = request.Bank,
                    ChequeNo = request.ChequeNo,
                    ChequeDate = request.ChequeDate,
                    VoucherNo = request.VoucherNo,
                    Amount = request.AmountToConvert,
                    RemainingBalanceListing = listingFees.Sum(lf => lf.Total),
                    AddedBy = request.AddedBy
                };
                _context.Cheque.Add(cheque);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();

            }
        }
    }
}
