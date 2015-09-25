using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[opp].[ADDON_SERVICE_ITEM_LUT]")]
    public class ADDON_SERVICE_ITEM_LUT : FIELD_ITEM_LUT
    {
        public ADDON_SERVICE_ITEM_LUT()
        {
            //this.OFFER_CONSTRAINTS_LNK = new HashSet<OFFER_CONSTRAINTS_LNK>();
            this.ADDON_SERVICE_PRICE_TRN = new HashSet<ADDON_SERVICE_PRICE_TRN>();
        }
    
        public Guid RowGUID { get; set; }
    
        //public virtual ICollection<OFFER_CONSTRAINTS_LNK> OFFER_CONSTRAINTS_LNK { get; set; }
        public virtual ICollection<ADDON_SERVICE_PRICE_TRN> ADDON_SERVICE_PRICE_TRN { get; set; }
    }
}
