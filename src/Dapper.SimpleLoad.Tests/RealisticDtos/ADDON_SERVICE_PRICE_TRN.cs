using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[opp].[ADDON_SERVICE_PRICE_TRN]")]
    public class ADDON_SERVICE_PRICE_TRN
    {
        public ADDON_SERVICE_PRICE_TRN()
        {
            this.ADDON_OFFER_TRN = new HashSet<ADDON_OFFER_TRN>();
        }
        [PrimaryKey]
        public Guid? AddOnServicePriceGUID { get; set; }
        public DateTimeOffset EffectiveDate { get; set; }
        public decimal FloorPrice { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal CeilingPrice { get; set; }
        public decimal WholesaleCost { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
        public int CountryKey { get; set; }
        public int CalculatorVersionKey { get; set; }
        public OppProvisionTypeEnum ProvisionTypeKey { get; set; }
        public int AddOnServiceKey { get; set; }
        public int AddonServiceItemKey { get; set; }
    
        public virtual ICollection<ADDON_OFFER_TRN> ADDON_OFFER_TRN { get; set; }
        public virtual ADDON_SERVICES_LUT ADDON_SERVICES_LUT { get; set; }
        public virtual CALCULATOR_VERSION_LUT CALCULATOR_VERSION_LUT { get; set; }
        public virtual ADDON_SERVICE_ITEM_LUT ADDON_SERVICE_ITEM_LUT { get; set; }
    }
}
