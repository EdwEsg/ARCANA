using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.CheckIns
{
    public class CheckInErrors
    {
        public static Error NotFound() => new("BusinessName.NotFound", "Business Name not found");
        public static Error Unauthorized() => new("Unauthorized", "You do not have access!");
        public static Error NoImageUploaded() => new("NoImageFound", "No Image Uploaded");
        public static Error IdNotFound() => new("Id.NotFound", "Id not found");

        public static Error AlreadyOut() => new("Already.Out", "You are already logged out");
    }
}
