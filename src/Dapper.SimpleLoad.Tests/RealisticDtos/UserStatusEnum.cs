using System.ComponentModel;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("user.USER_STATUS_ENUM")]
    public enum UserStatusEnum
    {
        [Description("Active")]
        Active = 0,

        [Description("Password Locked")]
        PasswordLocked = 1,

        [Description("Account Locked")]
        AccountLocked = 2
    }
}