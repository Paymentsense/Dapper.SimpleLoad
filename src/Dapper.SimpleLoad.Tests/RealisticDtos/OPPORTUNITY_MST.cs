using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[opp].[OPPORTUNITY_MST]")]
    public class OPPORTUNITY_MST
    {
        public OPPORTUNITY_MST()
        {
            //this.COMMISSION_TRN = new HashSet<COMMISSION_TRN>();
            //this.OPPORTUNITY_STATUS_TRN = new HashSet<OPPORTUNITY_STATUS_TRN>();
            //this.OFFER_TRN = new HashSet<OFFER_TRN>();
            this.LOCATION_MST = new HashSet<LOCATION_MST>();
        }
        [PrimaryKey]
        public Guid? OpportunityGUID { get; set; }
        public Guid MerchantGUID { get; set; }
        public Guid CurrentOfferGUID { get; set; }
        public Guid PartnerGUID { get; set; }
        public string OpportunityLocatorId { get; set; }
        public Guid OpportunityStatusGUID { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
        public bool IsOverridden { get; set; }
        public Guid OverriddenSessionGUID { get; set; }
        public Guid BillingToAddressGUID { get; set; }
        public int MCCKey { get; set; }
        public Guid ContactGUID { get; set; }
        public int BillingAddressSelectionKey { get; set; }
        public Guid? ShippingAddressGUID { get; set; }
        public int ShippingAddressSelectionKey { get; set; }
        public bool? AccountDataCompromise { get; set; }
        public DateTimeOffset? AccountHeldSince { get; set; }
        public string AcquirerLocationMID { get; set; }
        public bool AdvancedPaymentsTaken { get; set; }
        public int? AdvancePaymentDaysToDelivery { get; set; }
        public int? AdvancePaymentPercentOfSales { get; set; }
        public string AMEXAccountNumber { get; set; }
        public bool? AreAutomaticRenewalsPerformed { get; set; }
        public bool? AreWarrantyPaymentsTaken { get; set; }
        //  TODO: get rid of this!
        [SimpleSaveIgnore, SimpleLoadIgnore]
        public bool? AutomaticRenewalsPerformed
        {
            get { return AreAutomaticRenewalsPerformed; }
            set { AreAutomaticRenewalsPerformed = value; }
        }
        public decimal? AverageGoodsReturnedPercent { get; set; }
        public int? B2BPaymentsPercent { get; set; }
        public string BacsName { get; set; }
        public string BankName { get; set; }
        public int BusinessCategoryKey { get; set; }
        public double? CommercialCardRate { get; set; }
        public int? Delivery0to7DaysPercent { get; set; }
        public int? Delivery15to30DaysPercent { get; set; }
        public int? Delivery30PlusDaysPercent { get; set; }
        public int? Delivery8to14DaysPercent { get; set; }
        public string DepositAccount { get; set; }
        public string DepositAccountSortCode { get; set; }
        public DateTimeOffset? DepositAccountVerifiedDate { get; set; }
        public int? DepositDaysToDelivery { get; set; }
        public string DepositIBAN { get; set; }
        public int? DepositPercentOfTransaction { get; set; }
        public bool DepositRequired { get; set; }
        public int? DepositsRequiredPercent { get; set; }
        public string EcommWebURL { get; set; }
        public string FeesAccount { get; set; }
        public string FeesAccountSortCode { get; set; }
        public DateTimeOffset? FeesAccountVerifiedDate { get; set; }
        public string FeesIBAN { get; set; }
        public int? LocationNumber { get; set; }
        public int? LocationTurnoverMonthly { get; set; }
        public double? MaximumCardTransactionValue { get; set; }
        public int? MembershipCtoPercent { get; set; }
        public int? MembershipsAverageLengthMonths { get; set; }
        public int? MembershipsPaymentPercentOfSales { get; set; }
        public bool? MembershipTaken { get; set; }
        public double? MinimumCardTransactionValue { get; set; }
        public DateTime? PremisesOpenDate { get; set; }
        public double? PremiumCardRate { get; set; }
        public string ProductsAndServicesSold { get; set; }
        public int RefundDaysKey { get; set; }
        public int? SalesCardPresentPercent { get; set; }
        public int? SalesInternetPercent { get; set; }
        public int? SalesMailOrderPrecent { get; set; }
        public int SalesMethodKey { get; set; }
        public int SettlementCurrencyCodeKey { get; set; }
        public decimal? StatementDeliveryFeeMonthly { get; set; }
        public int StatementDeliveryKey { get; set; }
        public bool? StoresCardDetails { get; set; }
        public int SwitcherCurrentBankKey { get; set; }
        public decimal? TotalAnnualSales { get; set; }
        public string TradingName { get; set; }
        public int? TransactionCurrencyCodeKey { get; set; }
        public bool? TransactsThroughOthers { get; set; }
        public int? WarrantyAverageLength { get; set; }
        public int? WarrantyCtoPercent { get; set; }
        public bool? WarrantyTaken { get; set; }
        public int UpdateUserKey { get; set; }
    
        //[SimpleSaveIgnore, SimpleLoadIgnore]
        //public virtual ICollection<COMMISSION_TRN> COMMISSION_TRN { get; set; }
        //[SimpleSaveIgnore, SimpleLoadIgnore]
        //public virtual OPPORTUNITY_STATUS_TRN OPPORTUNITY_STATUS_TRN_CURRENT { get; set; }
        //[SimpleSaveIgnore, SimpleLoadIgnore]
        //public virtual ICollection<OPPORTUNITY_STATUS_TRN> OPPORTUNITY_STATUS_TRN { get; set; }
        //[SimpleSaveIgnore, SimpleLoadIgnore]
        public virtual ICollection<OFFER_TRN> OFFER_TRN { get; set; }
        [SimpleSaveIgnore, SimpleLoadIgnore]
        public virtual OFFER_TRN OFFER_TRN_CURRENT { get; set; }
        [SimpleSaveIgnore, SimpleLoadIgnore]
        public virtual MERCHANT_MST MERCHANT_MST { get; set; }
        [SimpleSaveIgnore, SimpleLoadIgnore]
        public virtual ICollection<LOCATION_MST> LOCATION_MST { get; set; }
        [SimpleSaveIgnore, SimpleLoadIgnore]
        public virtual CONTACT_MST CONTACT_MST { get; set; }
        [SimpleSaveIgnore, SimpleLoadIgnore]
        public virtual ADDRESS_MST ADDRESS_MST { get; set; }
        [SimpleSaveIgnore, SimpleLoadIgnore]
        public virtual ADDRESS_MST ADDRESS_MST1 { get; set; }
        //[SimpleSaveIgnore, SimpleLoadIgnore]
        //public virtual MCC_CODE_LUT MCC_CODE_LUT { get; set; }
    }
}
