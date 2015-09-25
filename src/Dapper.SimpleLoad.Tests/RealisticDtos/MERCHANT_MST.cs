using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[cust].[MERCHANT_MST]")]
    public class MERCHANT_MST
    {
        public MERCHANT_MST()
        {
            this.MERCHANT_MST_CONTACT_LNK = new HashSet<MERCHANT_MST_CONTACT_LNK>();
            this.APPLICATION_MST = new HashSet<APPLICATION_MST>();
            //this.ADDRESS_OWNERSHIP_LENGTH_LNK = new HashSet<ADDRESS_OWNERSHIP_LENGTH_LNK>();
            this.LEGAL_INFO_MST = new HashSet<LEGAL_INFO_MST>();
            this.OPPORTUNITY_MST = new HashSet<OPPORTUNITY_MST>();
        }
        [PrimaryKey]
        public Guid? MerchantGUID { get; set; }
        public long? V1MerchantId { get; set; }
        public string LocatorId { get; set; }
        public int ThompsonCodeKey { get; set; }
        public int CurrentTradingBankKey { get; set; }
        public int CallRestrictedReasonKey { get; set; }
        public int EmailRestrictedReasonKey { get; set; }
        public Guid AddressGUID { get; set; }
        public Guid PhoneGUID { get; set; }
        public Guid EmailAddressGUID { get; set; }
        public string WebsiteURL { get; set; }
        public string CreditPreScreenFlag { get; set; }
        public string ExperianBusinessURN { get; set; }
        public string ExperianLocationURN { get; set; }
        public DateTime? ExperianLastUpdate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
        public long AnnualTurnover { get; set; }
        public Guid FaxNumberGUID { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public int NumberEmployeesKey { get; set; }
        public int BusinessLegalTypeKey { get; set; }
        public int OwnershipUserKey { get; set; }
        public bool IsVATExempt { get; set; }
        public bool? DoesEcommerce { get; set; }
        public short? DelphiScore { get; set; }
        public string BusinessName { get; set; }
        public string DeDupeBusinessName { get; set; }
        public byte EntityType { get; set; }
        public int? DataSourceKey { get; set; }
        public int? ExistingProviderKey { get; set; }
        public int? MerchantDetailsId { get; set; }
        public bool IsDoNotCall { get; set; }
    
        public virtual ADDRESS_MST ADDRESS_MST { get; set; }
        public virtual ICollection<MERCHANT_MST_CONTACT_LNK> MERCHANT_MST_CONTACT_LNK { get; set; }
        public virtual EMAIL_ADDRESS_MST EMAIL_ADDRESS_MST { get; set; }
        public virtual PHONE_NUMBER_MST PHONE_NUMBER_MST_PhoneGUID { get; set; }
        public virtual PHONE_NUMBER_MST PHONE_NUMBER_MST_FaxNumberGUID { get; set; }
        public virtual ICollection<APPLICATION_MST> APPLICATION_MST { get; set; }
        //public virtual ICollection<ADDRESS_OWNERSHIP_LENGTH_LNK> ADDRESS_OWNERSHIP_LENGTH_LNK { get; set; }
        public virtual ICollection<LEGAL_INFO_MST> LEGAL_INFO_MST { get; set; }
        public virtual ICollection<OPPORTUNITY_MST> OPPORTUNITY_MST { get; set; }
    }
}
