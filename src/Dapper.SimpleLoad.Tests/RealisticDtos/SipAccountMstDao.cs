using System;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("dial.SIP_ACCOUNT_MST")]
    public class SipAccountMstDao
    {
        [PrimaryKey]
        public Guid? PhoneGuid { get; set; }
        public string SipUserId { get; set; }
        public string SipPassword { get; set; }
        public string SipProxyUserId { get; set; }
        public string SipProxyPassword { get; set; }
        public int SipAccountTypeKey { get; set; }
        public int RecStatusKey { get; set; }
        public Guid VoicemailPhoneGuid { get; set; }
        public Guid UserGuid { get; set; }

        [SimpleSaveIgnore,SimpleLoadIgnore]
        public string PhoneNumber { get; set; }

        [Column("PhoneGuid")]
        [OneToOne]
        [ForeignKeyReference(typeof(PhoneNumberDto))]
        public PhoneNumberDto DiallerNumber { get; set; }
    }
}