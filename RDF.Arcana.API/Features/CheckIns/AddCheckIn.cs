using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.CheckIns
{
    [Route("api/add-check-in"), ApiController]
    public class AddCheckIn : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddCheckIn(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Upload([FromForm] AddCheckInCommand command)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.CreatedBy = userId;
                }
                var result = await _mediator.Send(command);
                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class AddCheckInCommand : IRequest<Result>
        {
            public int? ClientId { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public IFormFile Image { get; set; }
            public string Remarks { get; set; }
            public int CreatedBy { get; set; }

            
                public string BusinessName { get; set; }
                public string FullName { get; set; }
                public string Barangay { get; set; }
                public string City { get; set; }
                public string Province { get; set; }
            
        }

        public class Handler : IRequestHandler<AddCheckInCommand, Result>
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

            public async Task<Result> Handle(AddCheckInCommand request, CancellationToken cancellationToken)
            {
                string imageUpload = string.Empty;

                var cdoCluster = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == request.CreatedBy);

                //CDO and Admin has only Access
                if (cdoCluster.UserRolesId != 6 && cdoCluster.UserRolesId != 1)
                {
                    return CheckInErrors.Unauthorized();
                }

                if (request.Image != null && request.Image.Length > 0)
                {
                    await using var stream = request.Image.OpenReadStream(); 

                    var attachmentsParams = new ImageUploadParams
                    {
                        File = new FileDescription(request.Image.FileName, stream), 
                        PublicId = request.Image.FileName 
                    };

                    var imageUploadResult = await _cloudinary.UploadAsync(attachmentsParams); 

                    imageUpload = imageUploadResult.SecureUrl.ToString();
                }

                else
                {
                    return CheckInErrors.NoImageUploaded();
                }

                if (request.ClientId is null)
                {
                    
                        var checkIn = new CheckIn
                        {
                            ClientId = null, 
                            Latitude = request.Latitude,
                            Longitude = request.Longitude,
                            Image = imageUpload,
                            Remarks = request.Remarks,
                            CreatedById = request.CreatedBy,
                            CreatedDate = DateTime.Now,
                            BusinessNameOthers = request.BusinessName,
                            FullNameOthers = request.FullName,
                            BarangayOthers = request.Barangay,
                            CityOthers = request.City,
                            ProvinceOthers = request.Province
                        };

                        await _context.CheckIns.AddAsync(checkIn, cancellationToken);
                    

                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    var checkIn = new CheckIn
                    {
                        ClientId = request.ClientId,
                        Latitude = request.Latitude,
                        Longitude = request.Longitude,
                        Image = imageUpload,
                        Remarks = request.Remarks,
                        CreatedById = request.CreatedBy,
                        CreatedDate = DateTime.Now,
                    };

                    await _context.CheckIns.AddAsync(checkIn, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Result.Success();
            }
        }
    }
}
