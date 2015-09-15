using System;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[gen].EMAIL_ADDRESS_MST")]
    public class EmailAddressMasterDto
    {
        [PrimaryKey]
        public Guid? EmailAddressGuid { get; set; }

        public string EmailAddress { get; set; }

        public int ReasonKey { get; set; }

        public DateTimeOffset UpdateDate { get; set; }

        public Guid UpdateSessionGuid { get; set; }
    }
}
