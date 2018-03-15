using System;
using System.Text.RegularExpressions;
using Dapper.SimpleLoad.Tests.RealisticDtos;
using NUnit.Framework;
using Dapper.SimpleLoad.Tests.Mocks;

namespace Dapper.SimpleLoad.Tests
{
    [TestFixture]
    public class QueryGenerationTests : BaseTests
    {
        [Test]
        public void generates_correct_query_for_complex_types()
        {
            using (var connection = new MockDbConnection())
            {
                try
                {
                    connection
                        .AutoQuery
                        <PhoneNumberDto, CountryDto, CurrencyCodeDto>(new
                        {
                            PhoneNumberKey = 1
                        });
                }
                catch (MockDbQueryExecutedException)
                {
                    Assert.AreEqual
                    (
                        NormalizeWhitespace(@"SELECT  [ta0_pnd].[PhoneGuid], 
    [ta0_pnd].[PhoneNumber], 
    [ta0_pnd].[BadNumberCount], 
    [ta0_pnd].[IsDoNotCall], 
    [ta1_cd].[CountryKey], 
    [ta1_cd].[Alpha3CountryCode], 
    [ta1_cd].[NumericCountryCode], 
    [ta1_cd].[Name], 
    [ta1_cd].[TelephoneCountryCode], 
    [ta1_cd].[IsFraudWatch], 
    [ta1_cd].[RowGUID], 
    [ta1_cd].[DisplayName], 
    [ta1_cd].[Description], 
    [ta1_cd].[TelephoneValidationRegex], 
    [ta1_cd].[TelephoneValidationMessage], 
    [ta1_cd].[Alpha2CountryCode], 
    [ta2_ccd].[CurrencyCodeKey], 
    [ta2_ccd].[Name], 
    [ta2_ccd].[Description], 
    [ta2_ccd].[IWSLCurrencyCode], 
    [ta2_ccd].[CurrencyBaseFraction], 
    [ta2_ccd].[RowGUID], 
    [ta2_ccd].[Unit], 
    [ta2_ccd].[SubUnit]
FROM [gen].[PHONE_NUMBER_MST] AS ta0_pnd
LEFT OUTER JOIN [gen].[COUNTRY_ENUM] AS ta1_cd
    ON ta1_cd.[CountryKey] = ta0_pnd.[CountryKey]
LEFT OUTER JOIN [gen].[CURRENCY_CODE_ENUM] AS ta2_ccd
    ON ta2_ccd.[CurrencyCodeKey] = ta1_cd.[CurrencyCodeKey]
WHERE ta0_pnd.[PhoneNumberKey] = @PhoneNumberKey
;"),
                        NormalizeWhitespace(connection.FirstCommandText)
                    );
                }
            }
        }

        [Test]
        public void generates_correct_query_with_custom_where_condition()
        {
            using (var connection = new MockDbConnection())
            {
                try
                {
                    connection.AutoQuery<PhoneNumberDto, CountryDto>(
                        new[] { "n", "c" },
                        "[c].[TelephoneCountryCode] = @CountryCode AND [n].[PhoneNumber] = @Number",
                        new
                        {
                            CountryCode = "44",
                            Number = "7779123456"
                        });
                }
                catch (MockDbQueryExecutedException)
                {
                    Assert.AreEqual
                    (
                        NormalizeWhitespace(@"SELECT  [n].[PhoneGuid], 
    [n].[PhoneNumber], 
    [n].[BadNumberCount], 
    [n].[IsDoNotCall], 
    [c].[CountryKey], 
    [c].[Alpha3CountryCode], 
    [c].[NumericCountryCode], 
    [c].[Name], 
    [c].[TelephoneCountryCode], 
    [c].[IsFraudWatch], 
    [c].[RowGUID], 
    [c].[DisplayName], 
    [c].[Description], 
    [c].[TelephoneValidationRegex], 
    [c].[TelephoneValidationMessage], 
    [c].[Alpha2CountryCode]
FROM [gen].[PHONE_NUMBER_MST] AS n
LEFT OUTER JOIN [gen].[COUNTRY_ENUM] AS c
    ON c.[CountryKey] = n.[CountryKey]
WHERE [c].[TelephoneCountryCode] = @CountryCode AND [n].[PhoneNumber] = @Number;"),
                        NormalizeWhitespace(connection.FirstCommandText)
                    );
                }
            }
        }

        [Test]
        public void generates_correct_query_for_many_to_many_relationship()
        {
            using (var connection = new MockDbConnection())
            {
                try
                {
                    connection.AutoQuery<UserWithDepartmentsDto, SimpleDepartmentDto>(
                        new
                        {
                            UserKey = 1
                        });
                }
                catch (MockDbQueryExecutedException)
                {
                    Assert.AreEqual
                    (
                        NormalizeWhitespace(@"SELECT  [ta0_uwdd].[UserKey], 
    [ta0_uwdd].[FirstName], 
    [ta0_uwdd].[LastName], 
    [ta1_sdd].[DepartmentKey], 
    [ta1_sdd].[Name]
FROM [user].USER_MST AS ta0_uwdd
LEFT OUTER JOIN [user].USER_DEPARTMENT_LNK AS ia0_uuserdepartmentlnk
    ON ta0_uwdd.[UserKey] = ia0_uuserdepartmentlnk.[UserKey]
LEFT OUTER JOIN [user].DEPARTMENT_MST AS ta1_sdd
    ON ta1_sdd.[DepartmentKey] = ia0_uuserdepartmentlnk.[DepartmentKey]
WHERE ta0_uwdd.[UserKey] = @UserKey
;"),
                        NormalizeWhitespace(connection.FirstCommandText)
                    );
                }
            }
        }

        [Test]
        public void generates_correct_query_for_complex_merchant_dto()
        {
            using (var connection = new MockDbConnection())
            {
                try
                {
                    connection.AutoQuery<MerchantMasterDto>(
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
                        new { MerchantGuid = Guid.Empty });
                }
                catch (MockDbQueryExecutedException)
                {
                    Assert.AreEqual
                    (
                        NormalizeWhitespace(@"SELECT  [ta0_mmd].[MerchantGuid], 
    [ta0_mmd].[V1MerchantId],
    [ta0_mmd].[LocatorId],
    [ta0_mmd].[ThompsonCodeKey],
    [ta0_mmd].[NumberEmployeesKey],
    [ta0_mmd].[BusinessLegalTypeKey],
    [ta0_mmd].[CurrentTradingBankKey],
    [ta0_mmd].[CallRestrictedReasonKey],
    [ta0_mmd].[EmailRestrictedReasonKey],
    [ta0_mmd].[EmailAddressGUID],
    [ta0_mmd].[WebsiteURL],
    [ta0_mmd].[CreditPreScreenFlag],
    [ta0_mmd].[ExperianBusinessURN],
    [ta0_mmd].[ExperianLocationURN],
    [ta0_mmd].[ExperianLastUpdate],
    [ta0_mmd].[UpdateSessionGUID],
    [ta0_mmd].[UpdateDate],
    [ta0_mmd].[AnnualTurnover],
    [ta0_mmd].[OwnershipUserKey],
    [ta0_mmd].[BusinessName],
    [ta0_mmd].[IsVatExempt],
    [ta0_mmd].[DoesEcommerce],
    [ta0_mmd].[EntityType],
    [ta0_mmd].[IsDoNotCall],
    [ta0_mmd].[CreatedDate],
    [ta1_pnd].[PhoneGuid],
    [ta1_pnd].[PhoneNumber],
    [ta1_pnd].[BadNumberCount],
    [ta1_pnd].[IsDoNotCall],
    [ta2_cd].[CountryKey],
    [ta2_cd].[Alpha3CountryCode],
    [ta2_cd].[NumericCountryCode],
    [ta2_cd].[Name],
    [ta2_cd].[TelephoneCountryCode],
    [ta2_cd].[IsFraudWatch],
    [ta2_cd].[RowGUID],
    [ta2_cd].[DisplayName],
    [ta2_cd].[Description],
    [ta2_cd].[TelephoneValidationRegex],
    [ta2_cd].[TelephoneValidationMessage],
    [ta2_cd].[Alpha2CountryCode],
    [ta3_fnd].[PhoneGuid],
    [ta3_fnd].[PhoneNumber],
    [ta3_fnd].[BadNumberCount],
    [ta3_fnd].[IsDoNotCall],
    [ta4_cd].[CountryKey],
    [ta4_cd].[Alpha3CountryCode],
    [ta4_cd].[NumericCountryCode],
    [ta4_cd].[Name],
    [ta4_cd].[TelephoneCountryCode],
    [ta4_cd].[IsFraudWatch],
    [ta4_cd].[RowGUID],
    [ta4_cd].[DisplayName],
    [ta4_cd].[Description],
    [ta4_cd].[TelephoneValidationRegex],
    [ta4_cd].[TelephoneValidationMessage],
    [ta4_cd].[Alpha2CountryCode],
    [ta5_cmd].[ContactGuid],
    [ta5_cmd].[UpdateDate],
    [ta5_cmd].[UpdateSessionGuid],
    [ta5_cmd].[SalutationKey],
    [ta5_cmd].[FirstName],
    [ta5_cmd].[MiddleInitial],
    [ta5_cmd].[Surname],
    [ta5_cmd].[IsPrimaryContact],
    [ta5_cmd].[PreferredContactType],
    [ta6_eamd].[EmailAddressGuid],
    [ta6_eamd].[EmailAddress],
    [ta6_eamd].[ReasonKey],
    [ta6_eamd].[UpdateDate],
    [ta6_eamd].[UpdateSessionGuid],
    [ta7_pnd].[PhoneGuid],
    [ta7_pnd].[PhoneNumber],
    [ta7_pnd].[BadNumberCount],
    [ta7_pnd].[IsDoNotCall],
    [ta8_cd].[CountryKey],
    [ta8_cd].[Alpha3CountryCode],
    [ta8_cd].[NumericCountryCode],
    [ta8_cd].[Name],
    [ta8_cd].[TelephoneCountryCode],
    [ta8_cd].[IsFraudWatch],
    [ta8_cd].[RowGUID],
    [ta8_cd].[DisplayName],
    [ta8_cd].[Description],
    [ta8_cd].[TelephoneValidationRegex],
    [ta8_cd].[TelephoneValidationMessage],
    [ta8_cd].[Alpha2CountryCode],
    [ta9_mpnd].[PhoneGuid],
    [ta9_mpnd].[PhoneNumber],
    [ta9_mpnd].[BadNumberCount],
    [ta9_mpnd].[IsDoNotCall],
    [ta10_cd].[CountryKey],
    [ta10_cd].[Alpha3CountryCode],
    [ta10_cd].[NumericCountryCode],
    [ta10_cd].[Name],
    [ta10_cd].[TelephoneCountryCode],
    [ta10_cd].[IsFraudWatch],
    [ta10_cd].[RowGUID],
    [ta10_cd].[DisplayName],
    [ta10_cd].[Description],
    [ta10_cd].[TelephoneValidationRegex],
    [ta10_cd].[TelephoneValidationMessage],
    [ta10_cd].[Alpha2CountryCode],
    [ta11_sd].[SalutationKey],
    [ta11_sd].[Description],
    [ta12_mdd].[MerchantDetailsId],
    [ta12_mdd].[PspId],
    [ta12_mdd].[CCProcessorId],
    [ta12_mdd].[TerminalModelId],
    [ta12_mdd].[TerminalAgeId],
    [ta13_tmd].[TerminalModelId],
    [ta13_tmd].[TerminalMakeId],
    [ta13_tmd].[Name],
    [ta13_tmd].[SortOrder],
    [ta14_ad].[AddressGuid],
    [ta14_ad].[HouseNumber],
    [ta14_ad].[StreetName],
    [ta14_ad].[HouseName],
    [ta14_ad].[FlatAptSuite],
    [ta14_ad].[TownName],
    [ta14_ad].[CountryKey],
    [ta14_ad].[AddressTypeKey],
    [ta14_ad].[AddressConfirmedDate],
    [ta15_cd].[CountyKey],
    [ta15_cd].[CountyName],
    [ta16_pcdd].[PostCodeKey],
    [ta16_pcdd].[PostalLocation],
    [ta16_pcdd].[Latitude],
    [ta16_pcdd].[Longitude],
    [ta16_pcdd].[TownName],
    [ta16_pcdd].[CountyKey],
    [ta16_pcdd].[CountryKey],
    [ta17_pdnd].[PostalDistrictKey],
    [ta17_pdnd].[PostalDistrict]
FROM [cust].MERCHANT_MST AS ta0_mmd
LEFT OUTER JOIN [gen].[PHONE_NUMBER_MST] AS ta1_pnd
    ON ta1_pnd.[PhoneGUID] = ta0_mmd.[PhoneGUID]
LEFT OUTER JOIN [gen].[COUNTRY_ENUM] AS ta2_cd
    ON ta2_cd.[CountryKey] = ta1_pnd.[CountryKey]
LEFT OUTER JOIN [gen].[PHONE_NUMBER_MST] AS ta3_fnd
    ON ta3_fnd.[PhoneGUID] = ta0_mmd.[FaxNumberGUID]
LEFT OUTER JOIN [gen].[COUNTRY_ENUM] AS ta4_cd
    ON ta4_cd.[CountryKey] = ta3_fnd.[CountryKey]
LEFT OUTER JOIN [cust].[MERCHANT_MST_CONTACT_LNK] AS ia0_cmerchantmstcontactlnk
    ON ta0_mmd.[MerchantGuid] = ia0_cmerchantmstcontactlnk.[MerchantGuid]
LEFT OUTER JOIN [cust].CONTACT_MST AS ta5_cmd
    ON ta5_cmd.[ContactGuid] = ia0_cmerchantmstcontactlnk.[ContactGuid]
LEFT OUTER JOIN [gen].EMAIL_ADDRESS_MST AS ta6_eamd
    ON ta6_eamd.[EmailAddressGuid] = ta5_cmd.[EmailAddressGuid]
LEFT OUTER JOIN [gen].[PHONE_NUMBER_MST] AS ta7_pnd
    ON ta7_pnd.[PhoneGUID] = ta5_cmd.[MainPhoneGuid]
LEFT OUTER JOIN [gen].[COUNTRY_ENUM] AS ta8_cd
    ON ta8_cd.[CountryKey] = ta7_pnd.[CountryKey]
LEFT OUTER JOIN [gen].[PHONE_NUMBER_MST] AS ta9_mpnd
    ON ta9_mpnd.[PhoneGUID] = ta5_cmd.[MobilePhoneGuid]
LEFT OUTER JOIN [gen].[COUNTRY_ENUM] AS ta10_cd
    ON ta10_cd.[CountryKey] = ta9_mpnd.[CountryKey]
LEFT OUTER JOIN [gen].SALUTATION_ENUM AS ta11_sd
    ON ta11_sd.[SalutationKey] = ta5_cmd.[SalutationKey]
LEFT OUTER JOIN [tele].[MERCHANT_DETAILS_MST] AS ta12_mdd
    ON ta12_mdd.[MerchantDetailsId] = ta0_mmd.[MerchantDetailsId]
LEFT OUTER JOIN [tele].[TERMINAL_MODEL_LUT] AS ta13_tmd
    ON ta13_tmd.[TerminalModelId] = ta12_mdd.[TerminalModelId]
LEFT OUTER JOIN [gen].[ADDRESS_MST] AS ta14_ad
    ON ta14_ad.[AddressGuid] = ta0_mmd.[AddressGuid]
LEFT OUTER JOIN [gen].COUNTY_LUT AS ta15_cd
    ON ta15_cd.[CountyKey] = ta14_ad.[CountyKey]
LEFT OUTER JOIN [gen].[POSTCODE_LUT] AS ta16_pcdd
    ON ta16_pcdd.[PostCodeKey] = ta14_ad.[PostCodeKey]
LEFT OUTER JOIN [gen].[POSTCODE_DISTRICT_LUT] AS ta17_pdnd
    ON ta17_pdnd.[PostalDistrictKey] = ta16_pcdd.[PostalDistrictKey]
WHERE ta0_mmd.[MerchantGuid] = @MerchantGuid
;"),
                        NormalizeWhitespace(connection.FirstCommandText)
                    );
                }
            }
        }

        [Test]
        public void generates_correct_query_for_complex_application_dto()
        {
            using (var connection = new MockDbConnection())
            {
                try
                {
                    connection.AutoQuery<APPLICATION_MST>(
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
                        });
                }
                catch (MockDbQueryExecutedException)
                {
                    Assert.AreEqual
                    (
                        NormalizeWhitespace(@"SELECT  [ta0_applicationmst].[ApplicationGUID], 
    [ta0_applicationmst].[MerchantGUID], 
    [ta0_applicationmst].[ApplicationSignedDate], 
    [ta0_applicationmst].[UpdateDate], 
    [ta0_applicationmst].[UpdateSessionGUID], 
    [ta0_applicationmst].[V1ApplicationId], 
    [ta0_applicationmst].[SalesChannelGUID], 
    [ta0_applicationmst].[CountryKey], 
    [ta0_applicationmst].[ApplicationReference], 
    [ta0_applicationmst].[IsValidated], 
    [ta1_legalinfomst].[LegalInfoKey], 
    [ta1_legalinfomst].[ApplicationGUID], 
    [ta1_legalinfomst].[MerchantLegalInfoKey], 
    [ta1_legalinfomst].[AdvertisingFlagKey], 
    [ta1_legalinfomst].[HasBusinessBankAccount], 
    [ta1_legalinfomst].[BusinessBankAccountOpened], 
    [ta1_legalinfomst].[CurrentOverdraftLimit], 
    [ta1_legalinfomst].[SelfProprietorshipInYears], 
    [ta1_legalinfomst].[IndustryExperienceInYears], 
    [ta1_legalinfomst].[PersonalInvestmentAmount], 
    [ta1_legalinfomst].[ChangeProcessorReasonKey], 
    [ta1_legalinfomst].[BankKey], 
    [ta1_legalinfomst].[TimeWithCurrentAcquirerMonths], 
    [ta1_legalinfomst].[CommentToUnderwriting], 
    [ta1_legalinfomst].[SalesDeclaration], 
    [ta1_legalinfomst].[DateOfSalesVisit], 
    [ta1_legalinfomst].[PersonVisited], 
    [ta1_legalinfomst].[BacsName], 
    [ta1_legalinfomst].[PhoneGUID], 
    [ta1_legalinfomst].[FaxNumberGUID], 
    [ta1_legalinfomst].[BusinessDescription], 
    [ta2_saleschannellnk].[SalesChannelGUID], 
    [ta2_saleschannellnk].[DepartmentKey], 
    [ta2_saleschannellnk].[CountryKey], 
    [ta2_saleschannellnk].[UpdateDate], 
    [ta2_saleschannellnk].[UpdateSessionGUID], 
    [ta3_locationmst].[LocationGUID], 
    [ta3_locationmst].[ApplicationGUID], 
    [ta3_locationmst].[OpportunityGUID], 
    [ta3_locationmst].[BusinessName], 
    [ta3_locationmst].[LocationReference], 
    [ta3_locationmst].[TradingAddressGUID], 
    [ta3_locationmst].[UpdateDate], 
    [ta3_locationmst].[UpdateSessionGUID], 
    [ta3_locationmst].[SiteVisit], 
    [ta3_locationmst].[ApplicationGUID] AS [APPLICATION_MST], 
    [ta4_businesslegalinfolnk].[BusinessLegalInfoGUID], 
    [ta4_businesslegalinfolnk].[ApplicationGUID], 
    [ta4_businesslegalinfolnk].[BusinessDetailGuid], 
    [ta4_businesslegalinfolnk].[UpdateSessionGUID], 
    [ta4_businesslegalinfolnk].[UpdateDate], 
    [ta4_businesslegalinfolnk].[APPLICATION_MST], 
    [ta5_shareholdermst].[ShareholderGUID], 
    [ta5_shareholdermst].[ApplicationGUID], 
    [ta5_shareholdermst].[IsCompany], 
    [ta5_shareholdermst].[SalutationKey], 
    [ta5_shareholdermst].[FirstName], 
    [ta5_shareholdermst].[LastName], 
    [ta5_shareholdermst].[DateOfBirth], 
    [ta5_shareholdermst].[NationalityKey], 
    [ta5_shareholdermst].[IsBeneficialOwner], 
    [ta5_shareholdermst].[CompanyName], 
    [ta5_shareholdermst].[RegisteredNumber], 
    [ta5_shareholdermst].[PercentageSharesHeld], 
    [ta5_shareholdermst].[AddressGUID], 
    [ta5_shareholdermst].[UpdateDate], 
    [ta5_shareholdermst].[UpdateSessionGUID], 
    [ta6_principalmst].[ApplicationGUID], 
    [ta6_principalmst].[IsPrimaryPrincipal], 
    [ta6_principalmst].[GenderKey], 
    [ta6_principalmst].[DateOfBirth], 
    [ta6_principalmst].[NationalityCountryKey], 
    [ta6_principalmst].[HasValidEIDCheck], 
    [ta6_principalmst].[ContactRoleKey], 
    [ta6_principalmst].[DateRoleTaken], 
    [ta6_principalmst].[SharesHeldPercent], 
    [ta6_principalmst].[UpdateDate1], 
    [ta6_principalmst].[UpdateSessionGUID1], 
    [ta6_principalmst].[IsBeneficialOwner], 
    [ta6_principalmst].[BusinessAffiliateKey], 
    [ta6_principalmst].[ContactGUID], 
    [ta6_principalmst].[UpdateDate], 
    [ta6_principalmst].[UpdateSessionGUID], 
    [ta6_principalmst].[SalutationKey], 
    [ta6_principalmst].[FirstName], 
    [ta6_principalmst].[MiddleInitial], 
    [ta6_principalmst].[Surname], 
    [ta6_principalmst].[EmailAddressGUID], 
    [ta6_principalmst].[MainPhoneGUID], 
    [ta6_principalmst].[MobilePhoneGUID], 
    [ta6_principalmst].[IsPrimaryContact], 
    [ta6_principalmst].[PreferredContactType], 
    [ta6_principalmst].[EMAIL_ADDRESS_MST], 
    [ta6_principalmst].[PHONE_NUMBER_MST_MainPhoneGUID], 
    [ta6_principalmst].[PHONE_NUMBER_MST_MobilePhoneGUID], 
    [ta7_merchantmst].[MerchantGUID], 
    [ta7_merchantmst].[V1MerchantId], 
    [ta7_merchantmst].[LocatorId], 
    [ta7_merchantmst].[ThompsonCodeKey], 
    [ta7_merchantmst].[CurrentTradingBankKey], 
    [ta7_merchantmst].[CallRestrictedReasonKey], 
    [ta7_merchantmst].[EmailRestrictedReasonKey], 
    [ta7_merchantmst].[AddressGUID], 
    [ta7_merchantmst].[PhoneGUID], 
    [ta7_merchantmst].[EmailAddressGUID], 
    [ta7_merchantmst].[WebsiteURL], 
    [ta7_merchantmst].[CreditPreScreenFlag], 
    [ta7_merchantmst].[ExperianBusinessURN], 
    [ta7_merchantmst].[ExperianLocationURN], 
    [ta7_merchantmst].[ExperianLastUpdate], 
    [ta7_merchantmst].[UpdateSessionGUID], 
    [ta7_merchantmst].[AnnualTurnover], 
    [ta7_merchantmst].[FaxNumberGUID], 
    [ta7_merchantmst].[UpdateDate], 
    [ta7_merchantmst].[NumberEmployeesKey], 
    [ta7_merchantmst].[BusinessLegalTypeKey], 
    [ta7_merchantmst].[OwnershipUserKey], 
    [ta7_merchantmst].[IsVATExempt], 
    [ta7_merchantmst].[DoesEcommerce], 
    [ta7_merchantmst].[DelphiScore], 
    [ta7_merchantmst].[BusinessName], 
    [ta7_merchantmst].[DeDupeBusinessName], 
    [ta7_merchantmst].[EntityType], 
    [ta7_merchantmst].[DataSourceKey], 
    [ta7_merchantmst].[ExistingProviderKey], 
    [ta7_merchantmst].[MerchantDetailsId], 
    [ta7_merchantmst].[IsDoNotCall], 
    [ta7_merchantmst].[ADDRESS_MST], 
    [ta7_merchantmst].[EMAIL_ADDRESS_MST], 
    [ta7_merchantmst].[PHONE_NUMBER_MST_PhoneGUID], 
    [ta7_merchantmst].[PHONE_NUMBER_MST_FaxNumberGUID]
FROM [app].[APPLICATION_MST] AS ta0_applicationmst
LEFT OUTER JOIN [app].[LEGAL_INFO_MST] AS ta1_legalinfomst
    ON ta1_legalinfomst.[ApplicationGUID] = ta0_applicationmst.[ApplicationGUID]
LEFT OUTER JOIN [gen].[SALES_CHANNEL_LNK] AS ta2_saleschannellnk
    ON ta2_saleschannellnk.[SalesChannelGUID] = ta0_applicationmst.[SalesChannelGUID]
LEFT OUTER JOIN [app].[LOCATION_MST] AS ta3_locationmst
    ON ta3_locationmst.[ApplicationGUID] = ta0_applicationmst.[ApplicationGUID]
LEFT OUTER JOIN [app].[BUSINESS_LEGAL_INFO_LNK] AS ta4_businesslegalinfolnk
    ON ta4_businesslegalinfolnk.[ApplicationGUID] = ta0_applicationmst.[ApplicationGUID]
LEFT OUTER JOIN [app].[SHAREHOLDER_MST] AS ta5_shareholdermst
    ON ta5_shareholdermst.[ApplicationGUID] = ta0_applicationmst.[ApplicationGUID]
LEFT OUTER JOIN [app].[PRINCIPAL_MST] AS ta6_principalmst
    ON ta6_principalmst.[ApplicationGUID] = ta0_applicationmst.[ApplicationGUID]
LEFT OUTER JOIN [cust].[MERCHANT_MST] AS ta7_merchantmst
    ON ta7_merchantmst.[MerchantGUID] = ta0_applicationmst.[MerchantGUID]
WHERE ta0_applicationmst.[ApplicationGuid] = @ApplicationGuid
;"),
                        NormalizeWhitespace(connection.FirstCommandText)
                    );
                }
            }
        }

        [Test]
        public void generates_join_using_specified_column_for_many_to_one_with_foreign_key_target_column()
        {
            using (var connection = new MockDbConnection())
            {
                try
                {
                    connection.AutoQuery<SimpleUserWithSipDao, SipAccountMstDao>(
                        new { UserKey = 1 });
                }
                catch (MockDbQueryExecutedException)
                {
                    Assert.AreEqual
                    (
                        NormalizeWhitespace(@"SELECT  [ta0_suwsd].[UserKey], 
    [ta0_suwsd].[UserGUID], 
    [ta0_suwsd].[Username], 
    [ta0_suwsd].[EmailAddress], 
    [ta1_samd].[PhoneGuid], 
    [ta1_samd].[SipUserId], 
    [ta1_samd].[SipPassword], 
    [ta1_samd].[SipProxyUserId], 
    [ta1_samd].[SipProxyPassword], 
    [ta1_samd].[SipAccountTypeKey], 
    [ta1_samd].[RecStatusKey], 
    [ta1_samd].[VoicemailPhoneGuid], 
    [ta1_samd].[UserGuid]
FROM [user].USER_MST AS ta0_suwsd
LEFT OUTER JOIN dial.SIP_ACCOUNT_MST AS ta1_samd
    ON ta1_samd.[UserGuid] = ta0_suwsd.[UserGUID]
WHERE ta0_suwsd.[UserKey] = @UserKey
;"),
                        NormalizeWhitespace(connection.FirstCommandText)
                    );
                }
            }
        }

        [Test]
        public void generates_correct_query_using_forward_referencing_foreign_key_attribute()
        {
            using (var connection = new MockDbConnection())
            {
                try
                {
                    connection.AutoQuery<LocationDto>(
                        new[]
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
                        new { LocationGuid = Guid.Empty });
                }
                catch (MockDbQueryExecutedException)
                {
                    Assert.AreEqual
                    (
                        NormalizeWhitespace(
                            @"SELECT  [ta0_ld].[LocationGuid], 
    [ta1_fad].[AddressGuid], 
    [ta1_fad].[HouseNumber], 
    [ta1_fad].[StreetName], 
    [ta1_fad].[HouseName], 
    [ta1_fad].[FlatAptSuite], 
    [ta1_fad].[TownName], 
    [ta1_fad].[CountryKey], 
    [ta1_fad].[AddressTypeKey], 
    [ta1_fad].[AddressConfirmedDate], 
    [ta2_cd].[CountyKey], 
    [ta2_cd].[CountyName], 
    [ta3_pcdd].[PostCodeKey], 
    [ta3_pcdd].[PostalLocation], 
    [ta3_pcdd].[Latitude], 
    [ta3_pcdd].[Longitude], 
    [ta3_pcdd].[TownName], 
    [ta3_pcdd].[CountyKey], 
    [ta3_pcdd].[CountryKey], 
    [ta4_pd].[PostCodeKey], 
    [ta4_pd].[PostCode], 
    [ta5_pdnd].[PostalDistrictKey], 
    [ta5_pdnd].[PostalDistrict], 
    [ta6_od].[OpportunityGuid], 
    [ta6_od].[LocationGuid], 
    [ta7_fad].[AddressGuid], 
    [ta7_fad].[HouseNumber], 
    [ta7_fad].[StreetName], 
    [ta7_fad].[HouseName], 
    [ta7_fad].[FlatAptSuite], 
    [ta7_fad].[TownName], 
    [ta7_fad].[CountryKey], 
    [ta7_fad].[AddressTypeKey], 
    [ta7_fad].[AddressConfirmedDate], 
    [ta8_cd].[CountyKey], 
    [ta8_cd].[CountyName], 
    [ta9_pcdd].[PostCodeKey], 
    [ta9_pcdd].[PostalLocation], 
    [ta9_pcdd].[Latitude], 
    [ta9_pcdd].[Longitude], 
    [ta9_pcdd].[TownName], 
    [ta9_pcdd].[CountyKey], 
    [ta9_pcdd].[CountryKey], 
    [ta10_pdnd].[PostalDistrictKey], 
    [ta10_pdnd].[PostalDistrict], 
    [ta11_pd].[PostCodeKey], 
    [ta11_pd].[PostCode], 
    [ta12_sad].[AddressGuid], 
    [ta12_sad].[HouseNumber], 
    [ta12_sad].[StreetName], 
    [ta12_sad].[HouseName], 
    [ta12_sad].[FlatAptSuite], 
    [ta12_sad].[TownName], 
    [ta12_sad].[CountryKey], 
    [ta12_sad].[AddressTypeKey], 
    [ta12_sad].[AddressConfirmedDate], 
    [ta13_cd].[CountyKey], 
    [ta13_cd].[CountyName], 
    [ta14_pcdd].[PostCodeKey], 
    [ta14_pcdd].[PostalLocation], 
    [ta14_pcdd].[Latitude], 
    [ta14_pcdd].[Longitude], 
    [ta14_pcdd].[TownName], 
    [ta14_pcdd].[CountyKey], 
    [ta14_pcdd].[CountryKey], 
    [ta15_pdnd].[PostalDistrictKey], 
    [ta15_pdnd].[PostalDistrict], 
    [ta16_pd].[PostCodeKey], 
    [ta16_pd].[PostCode]
FROM [app].[LOCATION_MST] AS ta0_ld
LEFT OUTER JOIN [gen].[ADDRESS_MST] AS ta1_fad
    ON ta1_fad.[AddressGuid] = ta0_ld.[TradingAddressGUID]
LEFT OUTER JOIN [gen].COUNTY_LUT AS ta2_cd
    ON ta2_cd.[CountyKey] = ta1_fad.[CountyKey]
LEFT OUTER JOIN [gen].[POSTCODE_LUT] AS ta3_pcdd
    ON ta3_pcdd.[PostCodeKey] = ta1_fad.[PostCodeKey]
LEFT OUTER JOIN [gen].[POSTCODE_IDXVIEW] AS ta4_pd
    ON ta4_pd.[PostCodeKey] = ta3_pcdd.[PostCodeKey]
LEFT OUTER JOIN [gen].[POSTCODE_DISTRICT_LUT] AS ta5_pdnd
    ON ta5_pdnd.[PostalDistrictKey] = ta3_pcdd.[PostalDistrictKey]
LEFT OUTER JOIN [opp].[OPPORTUNITY_MST] AS ta6_od
    ON ta6_od.[LocationGuid] = ta0_ld.[LocationGuid]
LEFT OUTER JOIN [gen].[ADDRESS_MST] AS ta7_fad
    ON ta7_fad.[AddressGuid] = ta6_od.[BillingToAddressGUID]
LEFT OUTER JOIN [gen].COUNTY_LUT AS ta8_cd
    ON ta8_cd.[CountyKey] = ta7_fad.[CountyKey]
LEFT OUTER JOIN [gen].[POSTCODE_LUT] AS ta9_pcdd
    ON ta9_pcdd.[PostCodeKey] = ta7_fad.[PostCodeKey]
LEFT OUTER JOIN [gen].[POSTCODE_DISTRICT_LUT] AS ta10_pdnd
    ON ta10_pdnd.[PostalDistrictKey] = ta9_pcdd.[PostalDistrictKey]
LEFT OUTER JOIN [gen].[POSTCODE_IDXVIEW] AS ta11_pd
    ON ta11_pd.[PostCodeKey] = ta9_pcdd.[PostCodeKey]
LEFT OUTER JOIN [gen].[ADDRESS_MST] AS ta12_sad
    ON ta12_sad.[AddressGuid] = ta6_od.[ShippingAddressGUID]
LEFT OUTER JOIN [gen].COUNTY_LUT AS ta13_cd
    ON ta13_cd.[CountyKey] = ta12_sad.[CountyKey]
LEFT OUTER JOIN [gen].[POSTCODE_LUT] AS ta14_pcdd
    ON ta14_pcdd.[PostCodeKey] = ta12_sad.[PostCodeKey]
LEFT OUTER JOIN [gen].[POSTCODE_DISTRICT_LUT] AS ta15_pdnd
    ON ta15_pdnd.[PostalDistrictKey] = ta14_pcdd.[PostalDistrictKey]
LEFT OUTER JOIN [gen].[POSTCODE_IDXVIEW] AS ta16_pd
    ON ta16_pd.[PostCodeKey] = ta14_pcdd.[PostCodeKey]
WHERE ta0_ld.[LocationGuid] = @LocationGuid
;"),
                        NormalizeWhitespace(connection.FirstCommandText)
                    );
                }
            }
        }

        [Test]
        public void Generates_correct_query_for_count()
        {
            using (var connection = new MockDbConnection())
            {
                try
                {
                    connection
                        .AutoQuery
                        <PhoneNumberDto, CountryDto, CurrencyCodeDto>(new
                        {
                            PhoneNumberKey = 1
                        }, 1);
                }
                catch (MockDbQueryExecutedException)
                {
                    Assert.AreEqual
                    (
                        NormalizeWhitespace(
                            @"SELECT TOP (1)  [ta0_pnd].[PhoneGuid], 
    [ta0_pnd].[PhoneNumber], 
    [ta0_pnd].[BadNumberCount], 
    [ta0_pnd].[IsDoNotCall], 
    [ta1_cd].[CountryKey], 
    [ta1_cd].[Alpha3CountryCode], 
    [ta1_cd].[NumericCountryCode], 
    [ta1_cd].[Name], 
    [ta1_cd].[TelephoneCountryCode], 
    [ta1_cd].[IsFraudWatch], 
    [ta1_cd].[RowGUID], 
    [ta1_cd].[DisplayName], 
    [ta1_cd].[Description], 
    [ta1_cd].[TelephoneValidationRegex], 
    [ta1_cd].[TelephoneValidationMessage], 
    [ta1_cd].[Alpha2CountryCode], 
    [ta2_ccd].[CurrencyCodeKey], 
    [ta2_ccd].[Name], 
    [ta2_ccd].[Description], 
    [ta2_ccd].[IWSLCurrencyCode], 
    [ta2_ccd].[CurrencyBaseFraction], 
    [ta2_ccd].[RowGUID], 
    [ta2_ccd].[Unit], 
    [ta2_ccd].[SubUnit]
FROM [gen].[PHONE_NUMBER_MST] AS ta0_pnd
LEFT OUTER JOIN [gen].[COUNTRY_ENUM] AS ta1_cd
    ON ta1_cd.[CountryKey] = ta0_pnd.[CountryKey]
LEFT OUTER JOIN [gen].[CURRENCY_CODE_ENUM] AS ta2_ccd
    ON ta2_ccd.[CurrencyCodeKey] = ta1_cd.[CurrencyCodeKey]
WHERE ta0_pnd.[PhoneNumberKey] = @PhoneNumberKey
;"),
                        NormalizeWhitespace(connection.FirstCommandText)
                    );
                }
            }
        }

        [Test]
        public void autoquery_with_custom_where_mapping_to_single_type_uses_supplied_alias_and_where_condition()
        {
            using (var connection = new MockDbConnection())
            {
                try
                {
                    connection
                        .AutoQuery
                        <ProSupportRule>(new[] {"r"},
                            "r.[IsForFirstData] = @isForFirstData",
                            new
                            {
                                isForFirstData = false,
                                isForGlobalPayments = false,
                                isForValitor = false
                            });
                }
                catch (MockDbQueryExecutedException)
                {
                    Assert.AreEqual
                    (
                        NormalizeWhitespace(
                            @"SELECT  [r].[ProSupportRuleKey], 
    [r].[RuleMccCode], 
    [r].[Title], 
    [r].[Details], 
    [r].[AdditionalInformation], 
    [r].[Reason], 
    [r].[IsNoteRequired], 
    [r].[IsForFirstData], 
    [r].[IsForGlobalPayments], 
    [r].[IsForValitor]
FROM [app].[PRO_SUPPORT_RULES_LUT] AS r
WHERE r.[IsForFirstData] = @isForFirstData;"),
                        NormalizeWhitespace(connection.FirstCommandText)
                    );
                }
            }
        }

        [Test]
        public void generates_correct_sql_when_paginating_results()
        {
            using (var connection = new MockDbConnection())
            {
                try
                {
                    connection
                        .AutoQuery
                        <TeamDto>(new Type[0],
                            new[] {"t"},
                            null,
                            new
                            {
                            },
                            10,
                            100,
                            "t.[Name]");
                }
                catch (MockDbQueryExecutedException)
                {
                    Assert.AreEqual
                    (
                        NormalizeWhitespace("SELECT [t].[TeamKey], [t].[Name] FROM (SELECT * FROM [user].TEAM_MST AS t ORDER BY t.[Name] OFFSET 100 ROWS FETCH NEXT 10 ROWS ONLY) AS t ;"), 
                        NormalizeWhitespace(connection.FirstCommandText)
                    );
                }
            }
        }

        private string NormalizeWhitespace(string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }
    }
}
