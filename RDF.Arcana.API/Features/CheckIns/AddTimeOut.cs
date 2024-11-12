using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.CheckIns
{
    [Route("api/add-time-out"), ApiController]
    public class AddTimeOut : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddTimeOut(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Add([FromRoute] int id)
        {
            try
            {
                var command = new AddTimeOutCommand
                {
                    Id = id
                };

                var result = await _mediator.Send(command);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class AddTimeOutCommand : IRequest<Result>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<AddTimeOutCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddTimeOutCommand request, CancellationToken cancellationToken)
            {
                var checkIn = await _context.CheckIns.FindAsync(request.Id);

                if (checkIn == null)
                {
                    return CheckInErrors.IdNotFound();
                }

                if (checkIn.TimeOut != null)
                {
                    return CheckInErrors.AlreadyOut();
                }

                checkIn.TimeOut = DateTime.Now;
                await _context.SaveChangesAsync();

                return Result.Success();
            }
        }
    }
}
