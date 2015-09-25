using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[opp].[ADDON_SERVICES_LUT]")]
    public class ADDON_SERVICES_LUT
    {
        public ADDON_SERVICES_LUT()
        {
            //this.OFFER_CONSTRAINTS_LNK = new HashSet<OFFER_CONSTRAINTS_LNK>();
            this.ADDON_SERVICE_PRICE_TRN = new HashSet<ADDON_SERVICE_PRICE_TRN>();
        }
        [PrimaryKey]
        public int? AddOnServiceKey { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public Guid RowGUID { get; set; }
        public GenRecStatusEnum RecStatusKey { get; set; }
        public string Description { get; set; }
        public int? DisplayOrder { get; set; }
        //public OppAddonServiceTypeEnum AddOnServiceTypeKey { get; set; }
    
        //public virtual ICollection<OFFER_CONSTRAINTS_LNK> OFFER_CONSTRAINTS_LNK { get; set; }
        public virtual ICollection<ADDON_SERVICE_PRICE_TRN> ADDON_SERVICE_PRICE_TRN { get; set; }
    }
}
