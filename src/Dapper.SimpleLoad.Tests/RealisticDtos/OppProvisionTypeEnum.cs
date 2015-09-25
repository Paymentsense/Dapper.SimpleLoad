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
	[Table("[opp].PROVISION_TYPE_ENUM"), ReferenceData]
    public enum OppProvisionTypeEnum : int
    {
       [Description("None")][EnumMember]None = 0,
       [Description("Not Required")][EnumMember]NotRequired = 1,
       [Description("Add New")][EnumMember]AddNew = 2,
       [Description("Transfer Existing")][EnumMember]TransferExisting = 3,
        
    }
}
