using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[cust].MERCHANT_MST")]
    public class MerchantMasterDto
    {
        [PrimaryKey]
        public Guid? MerchantGuid { get; set; }
        
        public long? V1MerchantId { get; set; }
        public string LocatorId { get; set; }
        public int ThompsonCodeKey { get; set; }
        public CustNumberEmployeesEnum NumberEmployeesKey { get; set; }
        public GenBusinessLegalTypeEnum BusinessLegalTypeKey { get; set; }
        public int CurrentTradingBankKey { get; set; }
        public int CallRestrictedReasonKey { get; set; }
        public int EmailRestrictedReasonKey { get; set; }
        
        [OneToOne()]
        [Column("AddressGuid")]
        [ForeignKeyReference(typeof(AddressDto))]
        public AddressDto Address { get; set; }

        [ManyToOne("PhoneGUID"), Column("PhoneGUID")]
        public PhoneNumberDto Phone { get; set; }

        [ManyToOne("PhoneGUID"), Column("FaxNumberGUID")]
        public FaxNumberDto Fax { get; set; }

        public Guid EmailAddressGUID { get; set; }
        public string WebsiteURL { get; set; }
        public string CreditPreScreenFlag { get; set; }
        public string ExperianBusinessURN { get; set; }
        public string ExperianLocationURN { get; set; }
        public DateTime? ExperianLastUpdate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public long AnnualTurnover { get; set; }

        public int OwnershipUserKey { get; set; }


        public string BusinessName { get; set; }
        public bool IsVatExempt { get; set; }
        public bool? DoesEcommerce { get; set; }


        public MerchantEntityType EntityType { get; set; }

        [SimpleSaveIgnore]
        [ManyToMany("[cust].[MERCHANT_MST_CONTACT_LNK]")]
        public IList<ContactMasterDto> Contacts { get; set; }

        [SimpleSaveIgnore]
        public string BusinessCategory { get; set; }

        [OneToOne]
        [Column("MerchantDetailsId")]
        [ManyToOne]
        public MerchantDetailsDto MerchantDetails { get; set; }

        public bool IsDoNotCall { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }
}
