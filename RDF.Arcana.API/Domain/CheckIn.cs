using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain
{
    public class CheckIn : BaseEntity
    {
        public int? ClientId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Image { get; set; }
        public string Remarks { get; set; }
        public string BusinessNameOthers { get; set; }
        public string FullNameOthers { get; set; }
        public string BarangayOthers { get; set; }
        public string CityOthers { get; set; }
        public string ProvinceOthers { get; set; }

        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Clients Client { get; set; }
        public virtual User CreatedBy { get; set; }

    }
}
