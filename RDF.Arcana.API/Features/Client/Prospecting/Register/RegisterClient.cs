using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.New_Doamin;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Direct;

[Route("api/Registration")]
[ApiController]

public class RegisterClient : ControllerBase
{
    private readonly IMediator _mediator;

    public RegisterClient(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class RegisterClientCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public string BusinessAdress { get; set; }
        public string AuthrizedRepreesentative { get; set; }
        public string AuthrizedRepreesentativePosition { get; set; }
        public int Cluster { get; set; }

        public class Handler : IRequestHandler<RegisterClientCommand, Unit>
        {
            private readonly DataContext _conntext;

            public Handler(DataContext conntext)
            {
                _conntext = conntext;
            }

            public async Task<Unit> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
            {
                var existingClient = await _conntext.Clients
                    .Where(x => x.RegistrationStatus == "Released")
                    .FirstOrDefaultAsync(client => client.Id == request.ClientId);

                if (existingClient == null)
                {
                    throw new ClientIsNotFound();
                }

                existingClient.BusinessAddress = request.BusinessAdress;
                existingClient.RepresentativeName = request.AuthrizedRepreesentative;
                existingClient.RepresentativePosition = request.AuthrizedRepreesentativePosition;
                existingClient.Cluster = request.Cluster;

                await _conntext.SaveChangesAsync(cancellationToken);

                return Unit.Value;

            }
        }
    }

    [HttpPut("RegisterClient/{id}")]
    public async Task<IActionResult> Register([FromBody]RegisterClientCommand request, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            request.ClientId = id;
            await _mediator.Send(request, cancellationToken);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Status = StatusCodes.Status404NotFound;
            response.Messages.Add(ex.Message);

            return Conflict(response);
        }
    }
}