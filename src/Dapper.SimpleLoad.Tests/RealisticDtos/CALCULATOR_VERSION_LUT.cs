using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[opp].[CALCULATOR_VERSION_LUT]")]
    public class CALCULATOR_VERSION_LUT
    {
        public CALCULATOR_VERSION_LUT()
        {
            //this.ACQUIRER_RATES_TRN = new HashSet<ACQUIRER_RATES_TRN>();
            //this.OFFER_CONSTRAINTS_LNK = new HashSet<OFFER_CONSTRAINTS_LNK>();
            this.OFFER_TRN = new HashSet<OFFER_TRN>();
            //this.OFFER_DEFAULTS_TRN = new HashSet<OFFER_DEFAULTS_TRN>();
            this.ADDON_SERVICE_PRICE_TRN = new HashSet<ADDON_SERVICE_PRICE_TRN>();
        }
        [PrimaryKey]
        public int? CalculatorVersionKey { get; set; }
        public int VersionNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public Guid RowGUID { get; set; }
        public GenRecStatusEnum RecStatusKey { get; set; }
    
        //public virtual ICollection<ACQUIRER_RATES_TRN> ACQUIRER_RATES_TRN { get; set; }
        //public virtual ICollection<OFFER_CONSTRAINTS_LNK> OFFER_CONSTRAINTS_LNK { get; set; }
        public virtual ICollection<OFFER_TRN> OFFER_TRN { get; set; }
        //public virtual ICollection<OFFER_DEFAULTS_TRN> OFFER_DEFAULTS_TRN { get; set; }
        public virtual ICollection<ADDON_SERVICE_PRICE_TRN> ADDON_SERVICE_PRICE_TRN { get; set; }
    }
}
