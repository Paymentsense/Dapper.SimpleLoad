using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[cust].[CONTACT_MST]")]
    public class CONTACT_MST
    {
        public CONTACT_MST()
        {
            this.LEGAL_INFO_MST = new HashSet<LEGAL_INFO_MST>();
            this.MERCHANT_MST_CONTACT_LNK = new HashSet<MERCHANT_MST_CONTACT_LNK>();
            this.OPPORTUNITY_MST = new HashSet<OPPORTUNITY_MST>();
            this.PRINCIPAL_MST = new HashSet<PRINCIPAL_MST>();
        }
        [PrimaryKey]
        public Guid? ContactGUID { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
        public int SalutationKey { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string Surname { get; set; }
        public Guid EmailAddressGUID { get; set; }
        public Guid MainPhoneGUID { get; set; }
        public Guid MobilePhoneGUID { get; set; }
        public bool IsPrimaryContact { get; set; }
        public Nullable<byte> PreferredContactType { get; set; }
    
        public virtual ICollection<LEGAL_INFO_MST> LEGAL_INFO_MST { get; set; }
        public virtual EMAIL_ADDRESS_MST EMAIL_ADDRESS_MST { get; set; }
        public virtual ICollection<MERCHANT_MST_CONTACT_LNK> MERCHANT_MST_CONTACT_LNK { get; set; }
        public virtual PHONE_NUMBER_MST PHONE_NUMBER_MST_MainPhoneGUID { get; set; }
        public virtual PHONE_NUMBER_MST PHONE_NUMBER_MST_MobilePhoneGUID { get; set; }
        public virtual ICollection<OPPORTUNITY_MST> OPPORTUNITY_MST { get; set; }
        public virtual ICollection<PRINCIPAL_MST> PRINCIPAL_MST { get; set; }
    }
}
