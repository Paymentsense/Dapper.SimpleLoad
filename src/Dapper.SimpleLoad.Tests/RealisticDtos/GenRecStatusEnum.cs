using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    public enum GenRecStatusEnum : int
    {
        [Description("Unknown")] [EnumMember] Unknown = 0,
        [Description("Active - Show In Lists")] [EnumMember] ActiveShowInLists = 1,
        [Description("Active - Do Not Show In Lists")] [EnumMember] ActiveDoNotShowInLists = 2,
        [Description("In-Active")] [EnumMember] InActive = 3,
    }
}
