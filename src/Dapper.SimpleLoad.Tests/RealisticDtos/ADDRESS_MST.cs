using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[gen].[ADDRESS_MST]")]
    public class ADDRESS_MST
    {
        [PrimaryKey]
        public Guid? AddressGUID { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string HouseName { get; set; }
        public string FlatAptSuite { get; set; }
        public string TownName { get; set; }
        public int PostCodeKey { get; set; }
        public int CountyKey { get; set; }
        public GenAddressTypesEnum AddressTypeKey { get; set; }
        public DateTimeOffset? AddressConfirmedDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
    
        public virtual ICollection<MERCHANT_MST> MERCHANT_MST { get; set; }
        //public virtual POSTCODE_LUT POSTCODE_LUT { get; set; }
        //public virtual COUNTY_LUT COUNTY_LUT { get; set; }
    }
}
