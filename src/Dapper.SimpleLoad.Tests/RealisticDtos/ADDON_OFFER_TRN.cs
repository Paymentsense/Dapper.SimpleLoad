using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[opp].[ADDON_OFFER_TRN]")]
    public class ADDON_OFFER_TRN
    {
        [PrimaryKey]
        public Guid? AddOnOfferGUID { get; set; }
        public Guid OfferGUID { get; set; }
        public GenRecStatusEnum RecStatusKey { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
        public Guid BasedOnAddOnServicePriceGUID { get; set; }
        public decimal FloorPrice { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal CeilingPrice { get; set; }
        public decimal WholesaleCost { get; set; }
        public decimal ActualPrice { get; set; }
    
        public virtual OFFER_TRN OFFER_TRN { get; set; }
        public virtual ADDON_SERVICE_PRICE_TRN ADDON_SERVICE_PRICE_TRN { get; set; }
    }
}
