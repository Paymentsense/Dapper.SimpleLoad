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
        [ForeignKeyReference(typeof(PostalDistrictNameDto))]
        [Column("PostalDistrictKey")]
        public PostalDistrictNameDto PostalDistrict { get; set; }

        public string PostalLocation { get; set; }
        
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        //  TODO: geo-location (if we care)

        public string TownName { get; set; }

        public int? CountyKey { get; set; }

        public GenCountryEnum CountryKey { get; set; }

        [SimpleSaveIgnore]
        [OneToOne("PostCodeKey")]
        public PostcodeDto PostCode { get; set; }
    }
}
