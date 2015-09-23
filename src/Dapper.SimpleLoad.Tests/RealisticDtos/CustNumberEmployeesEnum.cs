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
	[Table("[cust].NUMBER_EMPLOYEES_ENUM"), ReferenceData]
    public enum CustNumberEmployeesEnum : int
    {
       [Description("None")][EnumMember]None = 0,
       [Description("1to5")][EnumMember]From1to5 = 1,
       [Description("6to10")][EnumMember]From6to10 = 2,
       [Description("11to25")][EnumMember]From11to25 = 3,
       [Description("26to50")][EnumMember]From26to50 = 4,
       [Description("51to100")][EnumMember]From51to100 = 5,
       [Description("101to500")][EnumMember]From101to500 = 6,
       [Description("501to1000")][EnumMember]From501to1000 = 7,
       [Description("Over1000")][EnumMember]Over1000 = 8,
        
    }    
}
