using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;
using Newtonsoft.Json;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[user].USER_MST")]
    [ReferenceData]
    public class SimpleUserWithSipDao
    {
        [PrimaryKey]
        public int? UserKey { get; set; }
        public Guid UserGUID { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }

        [Column("UserGUID")]
        [ManyToOne("UserGuid")]
        [SimpleSaveIgnore]
        [JsonIgnore]
        public SipAccountMstDao SipAccount { get; set; } // Here, we're basing this on UserGUID
    }
}
