using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[gen].[PHONE_NUMBER_MST]")]
    public class PHONE_NUMBER_MST
    {
        public PHONE_NUMBER_MST()
        {
            this.MERCHANT_MST_PhoneGUID = new HashSet<MERCHANT_MST>();
            this.MERCHANT_MST_FaxNumberGUID = new HashSet<MERCHANT_MST>();
            this.LEGAL_INFO_MST_PhoneGUID = new HashSet<LEGAL_INFO_MST>();
            this.LEGAL_INFO_MST_FaxNumberGUID = new HashSet<LEGAL_INFO_MST>();
        }
        [PrimaryKey]
        public Guid? PhoneGUID { get; set; }
        public GenCountryEnum CountryKey { get; set; }
        public string PhoneNumber { get; set; }
        public int BadNumberCount { get; set; }
        public bool IsDoNotCall { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
        public int PhoneNumberTypeKey { get; set; }
    
        public virtual ICollection<MERCHANT_MST> MERCHANT_MST_PhoneGUID { get; set; }
        public virtual ICollection<MERCHANT_MST> MERCHANT_MST_FaxNumberGUID { get; set; }
        public virtual ICollection<LEGAL_INFO_MST> LEGAL_INFO_MST_PhoneGUID { get; set; }
        public virtual ICollection<LEGAL_INFO_MST> LEGAL_INFO_MST_FaxNumberGUID { get; set; }
    }
}
