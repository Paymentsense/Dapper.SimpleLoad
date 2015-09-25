using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[app].[LOCATION_MST]")]
    public class LOCATION_MST
    {
        [PrimaryKey]
        public Guid? LocationGUID { get; set; }
        public Guid ApplicationGUID { get; set; }
        public Guid OpportunityGUID { get; set; }
        public string BusinessName { get; set; }
        public string LocationReference { get; set; }
        public Guid TradingAddressGUID { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
        //public AppPremisesOwnershipTypeEnum PremisesOwnershipTypeKey { get; set; }
        //public AppPremisesTypeEnum PremisesTypeKey { get; set; }
        public bool SiteVisit { get; set; }

        [Column("ApplicationGUID")]
        public virtual APPLICATION_MST APPLICATION_MST { get; set; }
        //[Column("AddressGUID")]
        //public virtual ADDRESS_OWNERSHIP_LENGTH_LNK ADDRESS_OWNERSHIP_LENGTH_LNK { get; set; }
    }
}
