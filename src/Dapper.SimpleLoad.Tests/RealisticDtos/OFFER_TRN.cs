using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[opp].[OFFER_TRN]")]
    public class OFFER_TRN
    {
        public OFFER_TRN()
        {
            this.ADDON_OFFER_TRN = new HashSet<ADDON_OFFER_TRN>();
            this.OPPORTUNITY_MST_ISCURRENT = new HashSet<OPPORTUNITY_MST>();
            //this.EQUIPMENT_OFFER_TRN = new HashSet<EQUIPMENT_OFFER_TRN>();
        }
        [PrimaryKey]
        public Guid? OfferGUID { get; set; }
        public Guid GatewayOfferGUID { get; set; }
        public Guid OpportunityGUID { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
        public int CalculatorVersionKey { get; set; }
        public Guid SalesChannelGUID { get; set; }
        public string OfferReference { get; set; }
    
        public virtual SALES_CHANNEL_LNK SALES_CHANNEL_LNK { get; set; }
        public virtual ICollection<ADDON_OFFER_TRN> ADDON_OFFER_TRN { get; set; }
        //public virtual GATEWAY_OFFER_TRN GATEWAY_OFFER_TRN { get; set; }
        public virtual OPPORTUNITY_MST OPPORTUNITY_MST { get; set; }
        public virtual ICollection<OPPORTUNITY_MST> OPPORTUNITY_MST_ISCURRENT { get; set; }
        //public virtual ICollection<EQUIPMENT_OFFER_TRN> EQUIPMENT_OFFER_TRN { get; set; }
        public virtual CALCULATOR_VERSION_LUT CALCULATOR_VERSION_LUT { get; set; }
    }
}
