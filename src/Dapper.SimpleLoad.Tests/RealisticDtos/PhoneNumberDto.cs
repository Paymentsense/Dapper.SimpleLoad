﻿using System;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[gen].[PHONE_NUMBER_MST]")]
    public class PhoneNumberDto
    {
        [PrimaryKey]
        public int? PhoneNumberKey { get; set; }

        public Guid? PhoneGuid { get; set; }

        [ManyToOne]
        [Column("CountryKey")]
        public CountryDto Country { get; set; }

        public string PhoneNumber { get; set; }

        public int BadNumberCount { get; set; }

        public bool IsDoNotCall { get; set; }

        [ManyToOne]
        [Column("PhoneNumberTypeKey")]
        public PhoneNumberTypeEnum PhoneNumberType { get; set; }

        public override string ToString()
        {
            return string.Format("+{0} {1}", Country.TelephoneCountryCode, PhoneNumber);
        }
    }

    public class MobilePhoneNumberDto : PhoneNumberDto
    {
    }

    public class FaxNumberDto : PhoneNumberDto
    {
    }
}
