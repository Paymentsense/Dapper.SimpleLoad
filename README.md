#Dapper.SimpleLoad

Dapper.SimpleLoad simplifies Dapper multi-mapping to load complex objects from a relational database. It's the companion to [Dapper.SimpleSave](https://github.com/Paymentsense/Dapper.SimpleSave).

[![Build status](https://ci.appveyor.com/api/projects/status/me6txem2givlymt4?svg=true)](https://ci.appveyor.com/project/PaymentSense/dapper-simpleload)

##Example: How can Dapper.SimpleLoad help me?

[Dapper](https://github.com/StackExchange/dapper-dot-net) multi-mapping works really well for loading relatively simple objects but, as your requirements expand and the code becomes more complex, it can quickly get out of hand.

Dapper.SimpleLoad allows you to avoid that. In other words, you can **get rid** of things like this gothic horror novel of multi-mapping (an example lifted from real code):

```c#
        private IEnumerable<MerchantMasterDto> GetMerchantByGuid(Guid merchantGuid)
        {
            var results = Execute(connection =>

                connection.Query(@"
SELECT m.*,
     tc.ThompsonCategory AS BusinessCategory,
     c.ContactGUID, 
     c.SalutationKey,
     c.FirstName,
     c.MiddleInitial,
     c.Surname,
     c.IsPrimaryContact,
     c.PreferredContactType,
     e.EmailAddressGUID,
     e.EmailAddress,
     e.ReasonKey,
     p.PhoneGUID,
     p.CountryKey,
     p.PhoneNumber,
     p.BadNumberCount,
     p.IsDoNotCall,
     p.PhoneNumberTypeKey,
     p.PhoneNumberKey,
     mo.PhoneGUID,
     mo.CountryKey,
     mo.PhoneNumber,
     mo.BadNumberCount,
     mo.IsDoNotCall,
     mo.PhoneNumberTypeKey,
     mo.PhoneNumberKey,
     s.SalutationKey,
     s.Description,
     d.MerchantDetailsId,
     d.PSPID,
     d.CCProcessorId,
     d.TerminalModelId,
     d.TerminalAgeId,
     t.TerminalMakeId,
     t.Name,
     t.SortOrder,
     a.AddressGUID,
     a.HouseNumber,
     a.StreetName,
     a.HouseName,
     a.FlatAptSuite,
     a.TownName,
     a.PostCodeKey,
     a.CountyKey,
     a.CountryKey,
     co.CountyName,
     pc.PostCodeKey,
     pd.PostalDistrict,
     pc.PostalLocation,
     pc.Latitude,
     pc.Longitude,
     mp.PhoneGUID,
     mp.CountryKey,
     mp.PhoneNumber,
     mp.BadNumberCount,
     mp.IsDoNotCall,
     mp.PhoneNumberTypeKey,
     cem.CountryKey,
     cem.Alpha2CountryCode,
     cem.NumericCountryCode,
     cem.Name,
     cem.TelephoneCountryCode,
     cem.IsHighRisk,
     cem.RowGUID,
     cem.DisplayName,
     cem.Alpha2CountryCode,                  
     cep.CountryKey,
     cep.Alpha2CountryCode,
     cep.NumericCountryCode,
     cep.Name,
     cep.TelephoneCountryCode,
     cep.IsHighRisk,
     cep.RowGUID,
     cep.DisplayName,
     cep.Alpha2CountryCode,                  
     cemp.CountryKey,
     cemp.Alpha2CountryCode,
     cemp.NumericCountryCode,
     cemp.Name,
     cemp.TelephoneCountryCode,
     cemp.IsHighRisk,
     cemp.RowGUID,
     cemp.DisplayName,
     cemp.Alpha2CountryCode,
     pf.PhoneGUID,
     pf.CountryKey,
     pf.PhoneNumber,
     pf.BadNumberCount,
     pf.IsDoNotCall,
     pf.PhoneNumberTypeKey,
     cemf.CountryKey,
     cemf.Alpha2CountryCode,
     cemf.NumericCountryCode,
     cemf.Name,
     cemf.TelephoneCountryCode,
     cemf.IsHighRisk,
     cemf.RowGUID,
     cemf.DisplayName,
     cemf.Alpha2CountryCode
FROM [cust].[MERCHANT_MST] m
LEFT JOIN [gen].[THOMPSON_CODE_LUT] tc
     ON m.ThompsonCodeKey = tc.ThompsonCodeKey
LEFT JOIN [cust].[MERCHANT_MST_CONTACT_LNK] mc
     ON m.MerchantGuid = mc.MerchantGuid
LEFT JOIN [cust].[CONTACT_MST] c
     ON c.ContactGuid = mc.ContactGuid
LEFT JOIN [gen].[EMAIL_ADDRESS_MST] e
     ON c.EmailAddressGUID = e.EmailAddressGUID
LEFT JOIN [gen].[PHONE_NUMBER_MST] p
     ON c.MainPhoneGUID = p.PhoneGUID
LEFT JOIN [gen].[PHONE_NUMBER_MST] mo
     ON c.MobilePhoneGUID = mo.PhoneGUID
LEFT JOIN [gen].[SALUTATION_ENUM] s
     ON c.SalutationKey = s.SalutationKey
LEFT JOIN [tele].MERCHANT_DETAILS_MST d
     ON m.MerchantDetailsId = d.MerchantDetailsId
LEFT JOIN [tele].TERMINAL_MODEL_LUT t
     ON d.TerminalModelId = t.TerminalModelId
LEFT JOIN [gen].ADDRESS_MST a
     ON m.AddressGUID = a.AddressGUID
LEFT JOIN [gen].[COUNTY_LUT] co
     ON a.CountyKey = co.CountyKey
LEFT JOIN [gen].[POSTCODE_LUT] pc
     ON a.PostCodeKey = pc.PostCodeKey
LEFT JOIN [gen].[POSTCODE_DISTRICT_LUT] pd
     ON pc.PostalDistrictKey = pd.PostalDistrictKey
LEFT JOIN [gen].[PHONE_NUMBER_MST] mp
     ON m.PhoneGUID = mp.PhoneGUID
LEFT JOIN [gen].[COUNTRY_ENUM] cem
     ON cem.CountryKey = mo.CountryKey
LEFT JOIN [gen].[COUNTRY_ENUM] cep
     ON cep.CountryKey = p.CountryKey
LEFT JOIN [gen].[COUNTRY_ENUM] cemp
     ON cemp.CountryKey = mp.CountryKey
LEFT JOIN [gen].[PHONE_NUMBER_MST] pf
     ON m.FaxNumberGUID = pf.PhoneGUID 
LEFT JOIN [gen].[COUNTRY_ENUM] cemf
     ON cemf.CountryKey = pf.CountryKey
WHERE m.MerchantGUID = @MerchantGuid;",
                    new[]
                    {
                        typeof (MerchantMasterDto), typeof (ContactMasterDto), typeof (EmailAddressMasterDto),
                        typeof (PhoneNumberDto), typeof (MobilePhoneNumberDto), typeof (SalutationDto),
                        typeof (MerchantDetailsDto), typeof (TerminalModelDto), typeof(AddressDto), typeof(CountyDto),
                        typeof(PostCodeDetailsDto), typeof(PhoneNumberDto),typeof(CountryDto),typeof(CountryDto),typeof(CountryDto), typeof(FaxNumberDto), typeof(CountryDto)
                    },

                    (objects) =>
                    {
                        var merchantMaster = (MerchantMasterDto)objects[0];
                        var contactMaster = (ContactMasterDto)objects[1];
                        var emailAddress = (EmailAddressMasterDto)objects[2];
                        var mainPhoneNumber = (PhoneNumberDto)objects[3];
                        var mobilePhoneNumber = (MobilePhoneNumberDto)objects[4];
                        var salutation = (SalutationDto)objects[5];
                        var merchantDetails = (MerchantDetailsDto)objects[6];
                        var terminalModel = (TerminalModelDto)objects[7];
                        var address = (AddressDto)objects[8];
                        var county = (CountyDto)objects[9];
                        var postcode = (PostCodeDetailsDto)objects[10];
                        var merchantPhoneNumber = (PhoneNumberDto)objects[11];
                        var mobilePhoneNumberCountry = (CountryDto)objects[12];
                        var mainPhoneNumberCountry = (CountryDto)objects[13];
                        var merchantPhoneNumberCountry = (CountryDto)objects[14];
                        var faxNumber = (FaxNumberDto)objects[15];
                        var faxNumberCountry = (CountryDto)objects[16];

                        if (mainPhoneNumber != null)
                            mainPhoneNumber.Country = mainPhoneNumberCountry;
                        if (mobilePhoneNumber != null)
                            mobilePhoneNumber.Country = mobilePhoneNumberCountry;
                        if (merchantPhoneNumber != null)
                            merchantPhoneNumber.Country = merchantPhoneNumberCountry;

                        if (merchants.ContainsKey(merchantMaster.MerchantGuid.GetValueOrDefault()))
                        {
                            merchantMaster = merchants[merchantMaster.MerchantGuid.GetValueOrDefault()];
                        }
                        else
                        {
                            merchantMaster.Contacts = new List<MerchantContactLinkDto>();
                            merchants.Add(merchantMaster.MerchantGuid.GetValueOrDefault(), merchantMaster);
                        }
                        if (contactMaster != null)
                        {
                            if (merchantMaster.Contacts.All(c => c.Contact.ContactGuid != contactMaster.ContactGuid))
                            {
                                var link = new MerchantContactLinkDto
                                {
                                    Contact = contactMaster,
                                    MerchantGuid = merchantMaster.MerchantGuid
                                };
                                merchantMaster.Contacts.Add(link);

                                if (contactMaster.EmailAddress == null)
                                {
                                    contactMaster.EmailAddress = emailAddress;
                                }

                                if (contactMaster.MainPhone == null)
                                {
                                    contactMaster.MainPhone = mainPhoneNumber;
                                }

                                if (contactMaster.MobilePhone == null)
                                {
                                    contactMaster.MobilePhone = mobilePhoneNumber;
                                }

                                if (contactMaster.Salutation == null)
                                {
                                    contactMaster.Salutation = salutation;
                                }
                            }
                        }

                        if (mainPhoneNumberCountry != null && mainPhoneNumber.Country == null)
                        {
                            mainPhoneNumber.Country = mainPhoneNumberCountry;
                        }

                        if (mobilePhoneNumberCountry != null && mobilePhoneNumber.Country == null)
                        {
                            mobilePhoneNumber.Country = mobilePhoneNumberCountry;
                        }

                        if (merchantPhoneNumberCountry != null && merchantPhoneNumber.Country == null)
                        {
                            merchantPhoneNumber.Country = merchantPhoneNumberCountry;
                        }

                        if (merchantDetails != null && merchantMaster.MerchantDetails == null)
                        {
                            merchantMaster.MerchantDetails = merchantDetails;
                        }

                        if (terminalModel != null && merchantMaster.MerchantDetails != null)
                        {
                            merchantMaster.MerchantDetails.TerminalModel = terminalModel;
                        }

                        if (address != null && merchantMaster.Address == null)
                        {
                            merchantMaster.Address = address;

                            if (county != null)
                            {
                                merchantMaster.Address.County = county;
                            }
                        }

                        if (postcode != null && merchantMaster.Address != null)
                        {
                            merchantMaster.Address.PostCodeDetails = postcode;
                        }

                        if (merchantPhoneNumber != null && merchantMaster.Phone == null)
                        {
                            merchantPhoneNumber.Country = merchantPhoneNumberCountry;
                            merchantMaster.Phone = merchantPhoneNumber;
                        }

                        if (faxNumber != null && merchantMaster.Fax == null)
                        {
                            faxNumber.Country = faxNumberCountry;
                            merchantMaster.Fax = faxNumber;
                        }

                        return merchantMaster;
                    },
                    new { MerchantGuid = merchantGuid },
                    splitOn:
                        "ContactGuid, EmailAddressGuid, PhoneGUID, PhoneGUID, SalutationKey, MerchantDetailsId, TerminalMakeId, AddressGuid, CountryKey, PostCodeKey, PhoneGUID,CountryKey,CountryKey,CountryKey, PhoneGUID,CountryKey"
                    ));
            return results.FirstOrDefault();
        }
```

And replace them with something more readable and maintainable:

```c#
        public MerchantMasterDto GetMerchantByGuid(Guid merchantGuid)
        {
            return Execute(connection => connection.AutoQuery<MerchantMasterDto>(
                new[]
                {
                    typeof (PhoneNumberDto),
                    typeof (CountryDto),
                    typeof (FaxNumberDto),
                        typeof (CountryDto),
                    typeof (MerchantContactLinkDto),
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
                    typeof (PostalDistrictNameDto),
                    typeof (ThompsonCodeDto)
                },
                new { MerchantGuid = merchantGuid }).FirstOrDefault());
        }
```

Dapper.SimpleLoad takes care of both the SQL generation and object wire-up for you. You just have to tell it which objects to load, and it'll do the rest. If you need more control, you can specify a `WHERE` clause condition, or use one of its `CustomQuery` overloads to specify your own SQL.

It provides a Dapper-like API for querying that allows a high degree of customisation (for example, custom where clauses, and full custom SQL when you really need it) whilst minimising the amount of code you need to write and maintain.
