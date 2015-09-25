using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[app].[LEGAL_INFO_MST]")]
    public class LEGAL_INFO_MST
    {
        [PrimaryKey]
        public int? LegalInfoKey { get; set; }
        [ForeignKeyReference(typeof(APPLICATION_MST))]
        public Guid ApplicationGUID { get; set; }
        public int? MerchantLegalInfoKey { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public Guid AddressGUID { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public Guid ContactGUID { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public DateTime? CompanyStartDate { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public DateTime? CompanyRegistrationDate { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public string CompanyRegistrationNumber { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public string RegisteredCharityNumber { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public string VATNumber { get; set; }
        public int AdvertisingFlagKey { get; set; }
        public bool? HasBusinessBankAccount { get; set; }
        public DateTime? BusinessBankAccountOpened { get; set; }
        public decimal? CurrentOverdraftLimit { get; set; }
        public int? SelfProprietorshipInYears { get; set; }
        public int? IndustryExperienceInYears { get; set; }
        public decimal? PersonalInvestmentAmount { get; set; }
        public int ChangeProcessorReasonKey { get; set; }
        public int BankKey { get; set; }
        public int? TimeWithCurrentAcquirerMonths { get; set; }
        public string CommentToUnderwriting { get; set; }
        public bool SalesDeclaration { get; set; }
        public DateTime? DateOfSalesVisit { get; set; }
        public string PersonVisited { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public Guid MerchantGUID { get; set; }
        public string BacsName { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public GenBusinessLegalTypeEnum BusinessLegalTypeKey { get; set; }
        public Guid PhoneGUID { get; set; }
        public Guid FaxNumberGUID { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public string RegisteredName { get; set; }
        public string BusinessDescription { get; set; }
    
        [OneToMany, Column("AddressGUID")]
        public virtual ADDRESS_MST ADDRESS_MST { get; set; }
        [OneToMany, Column("PhoneGUID")]
        public virtual PHONE_NUMBER_MST PHONE_NUMBER_MST_PhoneGUID { get; set; }
        [OneToMany, Column("PhoneGUID")]
        public virtual PHONE_NUMBER_MST PHONE_NUMBER_MST_FaxNumberGUID { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public virtual CONTACT_MST CONTACT_MST { get; set; }
        [SimpleLoadIgnore]
        [SimpleSaveIgnore]
        public virtual MERCHANT_MST MERCHANT_MST { get; set; }

        
    }
}
