using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[gen].[POSTCODE_LUT]")]
    [ReferenceData]
    public class PostCodeDetailsDto
    {
        [PrimaryKey]
        public int? PostCodeKey { get; set; }

        [ManyToOne("PostalDistrictKey")]
        [Column("PostalDistrictKey")]
        public PostalDistrictNameDto PostalDistrict { get; set; }

        public string PostalLocation { get; set; }
        
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
