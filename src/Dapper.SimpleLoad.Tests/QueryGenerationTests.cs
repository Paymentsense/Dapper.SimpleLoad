using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper.SimpleLoad.Tests.RealisticDtos;
using NUnit.Framework;

namespace Dapper.SimpleLoad.Tests
{
    [TestFixture]
    public class QueryGenerationTests : BaseTests
    {
        [Test]
        public void generates_correct_query_for_complex_types()
        {
            //var logger = 
            //SimpleSaveExtensions.Logger = //    TODO

            try
            {
                using (var connection = new SqlConnection())
                {
                    connection
                        .AutoQuery
                        <PhoneNumberDto, CountryDto, CurrencyCodeDto>(new
                            {
                                PhoneNumberKey = 1
                            });
                }
            }
            catch (InvalidOperationException ioe)
            {
                CheckException(ioe);
            }

            //  TODO: check the script
        }

        [Test]
        public void generates_correct_query_with_custom_where_condition()
        {
            try
            {
                using (var connection = new SqlConnection())
                {
                    var results = connection.AutoQuery<PhoneNumberDto, CountryDto>(
                        new[] {"n", "c"},
                        "[c].[TelephoneCountryCode] = @CountryCode AND [n].[PhoneNumber] = @Number",
                        new
                        {
                            CountryCode = "44",
                            Number = "7779123456"
                        });
                }
            }
            catch (InvalidOperationException ioe)
            {
                CheckException(ioe);
            }
        }

        [Test]
        public void generates_correct_query_for_many_to_many_relationship()
        {
            try
            {
                using (var connection = new SqlConnection())
                {
                    var results = connection.AutoQuery<UserWithDepartmentsDto, SimpleDepartmentDto>(
                        new
                        {
                            UserKey = 1
                        });
                }
            }
            catch (InvalidOperationException ioe)
            {
                CheckException(ioe);
            }
        }

        [Test]
        public void generates_correct_query_for_complex_merchant_dto()
        {
            try
            {
                using (var connection = new SqlConnection())
                {
                    var results = connection.AutoQuery<MerchantMasterDto>(
                        new[]
                        {
                            typeof (PhoneNumberDto),
                                typeof (CountryDto),
                            typeof (FaxNumberDto),
                                typeof (CountryDto),
                            typeof (ContactMasterDto),
                                typeof (EmailAddressMasterDto),
                                typeof (PhoneNumberDto),
                                    typeof (CountryDto),
                                typeof (MobilePhoneNumberDto),
                                    typeof (CountryDto),
                                typeof (SalutationDto),
                            typeof (MerchantDetailsDto),
                                typeof (TerminalModelDto),
                            typeof (AddressDto),
                                typeof (CountyDto),
                                typeof (PostCodeDetailsDto),
                                typeof (PostalDistrictNameDto)
                        },
                        new {MerchantGuid = Guid.Empty}).FirstOrDefault();
                }
            }
            catch (InvalidOperationException ioe)
            {
                CheckException(ioe);
            }
        }

        [Test]
        public void generates_correct_query_for_complex_application_dto()
        {
            try
            {
                using (var connection = new SqlConnection())
                {
                    var results = connection.AutoQuery<APPLICATION_MST>(
                    new[]
                    {
                        typeof (LEGAL_INFO_MST),
                        typeof (SALES_CHANNEL_LNK),
                        typeof (LOCATION_MST),
                        typeof (BUSINESS_LEGAL_INFO_LNK),
                        typeof (SHAREHOLDER_MST),
                        typeof (PRINCIPAL_MST),
                        typeof (MERCHANT_MST)
                    },
                    new
                    {
                        ApplicationGuid = Guid.Empty
                    }).FirstOrDefault();
                }
            }
            catch (InvalidOperationException ioe)
            {
                CheckException(ioe);
            }
        }

        [Test]
        public void generates_join_using_specified_column_for_many_to_one_with_foreign_key_target_column()
        {
            try
            {
                using (var connection = new SqlConnection())
                {
                    var results = connection.AutoQuery<SimpleUserWithSipDao, SipAccountMstDao>(
                        new {UserKey = 1}).FirstOrDefault();
                }
            }
            catch (InvalidOperationException ioe)
            {
                CheckException(ioe);
            }
        }

        [Test]
        public void generates_correct_query_using_forward_referencing_foreign_key_attribute()
        {
            try
            {
                using (var connection = new SqlConnection())
                {
                    var results = connection.AutoQuery<LocationDto>(
                        new []
                        {
                            typeof(FullAddressDto),
                                typeof(CountyDto),
                                typeof(PostCodeDetailsDto),
                                    typeof(PostcodeDto),
                                    typeof(PostalDistrictNameDto),
                            typeof(OpportunityDto),
                                typeof(FullAddressDto),
                                    typeof(CountyDto),
                                    typeof(PostCodeDetailsDto),
                                        typeof(PostalDistrictNameDto),
                                        typeof(PostcodeDto),
                                typeof(ShippingAddressDto),
                                    typeof(CountyDto),
                                    typeof(PostCodeDetailsDto),
                                        typeof(PostalDistrictNameDto),
                                        typeof(PostcodeDto),
                        },
                        new {LocationGuid = Guid.Empty});
                }
            }
            catch (InvalidOperationException ioe)
            {
                CheckException(ioe);
            }
        }

        [Test]
        public void Generates_correct_query_for_count()
        {
            try
            {
                using (var connection = new SqlConnection())
                {
                    connection
                        .AutoQuery
                        <PhoneNumberDto, CountryDto, CurrencyCodeDto>(new
                        {
                            PhoneNumberKey = 1
                        }, 1);
                }
            }
            catch (InvalidOperationException ioe)
            {
                CheckException(ioe);
            }
        }

        //  TODO: test this generates a query with the right alias:

        /*
    [Table("[app].[PRO_SUPPORT_RULES_LUT]"), ReferenceData]
    public class ProSupportRule
    {
        [PrimaryKey]
        public int? ProSupportRuleKey { get; set; }
        public RuleType Type { get; set; }
        public RuleStatementFlag RuleFlag { get; set; }
        public RuleContext Context { get; set; }
        public RuleRequirement Requirement { get; set; }
        public RuleClassification Classification { get; set; }

        public DocumentCategoryEnum DocumentCategory { get; set; }
        public HighlightFieldEnum? FieldCategory { get; set; }

        public int? RuleMccCode { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string AdditionalInformation { get; set; }
        public string Reason { get; set; }
        public bool IsNoteRequired { get; set; }

        public bool IsForFirstData { get; set; }
        public bool IsForGlobalPayments { get; set; }
        public bool IsForValitor { get; set; }

        private string _ruleFireReason;

        [SimpleSaveIgnore, SimpleLoadIgnore]
        public bool HasFired { get; set; }

        [SimpleSaveIgnore, SimpleLoadIgnore]
        public string RuleFireReason
        {
            get
            {
                if (string.IsNullOrEmpty(_ruleFireReason))
                {
                    _ruleFireReason = RuleFlag.GetDescriptions();
                }

                return _ruleFireReason;
            }
        }
    }
          
         public IEnumerable<ProSupportRule> GetAllRules(ApplicationDetailsDto application)
        {
            var whereClause = "";

            switch (application.Acquirer)
            {
                case AppAcquirerEnum.FDMS:
                    whereClause = "r.[IsForFirstData] = @isForFirstData";
                    break;
                case AppAcquirerEnum.Global:
                    whereClause = "r.[IsForGlobalPayments] = @isForGlobalPayments";
                    break;
                case AppAcquirerEnum.Valitor:
                    whereClause = "r.[IsForValitor] = @isForValitor";
                    break;
            }

            var allRules = Execute(conn => conn.AutoQuery<ProSupportRule>(
                new [] { "r" },
                whereClause,
                new 
                {
                    isForFirstData = application.Acquirer == AppAcquirerEnum.FDMS,
                    isForGlobalPayments = application.Acquirer == AppAcquirerEnum.Global,
                    isForValitor = application.Acquirer == AppAcquirerEnum.Valitor
                }));

            return allRules;
        }*/

        [Test]
        public void autoquery_with_custom_where_mapping_to_single_type_uses_supplied_alias_and_where_condition()
        {
            try
            {
                using (var connection = new SqlConnection())
                {
                    connection
                        .AutoQuery
                        <ProSupportRule>(new [] { "r" },
                        "r.[IsForFirstData] = @isForFirstData",
                        new 
                        {
                            isForFirstData = false,
                            isForGlobalPayments = false,
                            isForValitor = false
                        });
                }
            }
            catch (InvalidOperationException ioe)
            {
                CheckException(ioe);
            }

        }
    }
}
