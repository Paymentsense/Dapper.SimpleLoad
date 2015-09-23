using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [DataContract]
	[Table("[gen].BUSINESS_LEGAL_TYPE_ENUM"), ReferenceData]
    public enum GenBusinessLegalTypeEnum : int
    {
       [Description("")][EnumMember]None = 0,
       [Description("Private Limited Company")][EnumMember]PrivateLimitedCompany = 1,
       [Description("Public Limited Company")][EnumMember]PublicLimitedCompany = 2,
       [Description("Partnership")][EnumMember]Partnership = 3,
       [Description("Trust")][EnumMember]Trust = 4,
       [Description("Limited Liability Partnership")][EnumMember]LimitedLiabilityPartnership = 5,
       [Description("Sole Trader")][EnumMember]SoleTrader = 6,
        
    }    
}
