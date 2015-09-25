using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[gen].[EMAIL_ADDRESS_MST]")]
    public class EMAIL_ADDRESS_MST
    {
        public EMAIL_ADDRESS_MST()
        {
            this.MERCHANT_MST = new HashSet<MERCHANT_MST>();
            this.CONTACT_MST = new HashSet<CONTACT_MST>();
        }
        [PrimaryKey]
        public Guid? EmailAddressGUID { get; set; }
        public string EmailAddress { get; set; }
        public int ReasonKey { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
    
        public virtual ICollection<MERCHANT_MST> MERCHANT_MST { get; set; }
        public virtual ICollection<CONTACT_MST> CONTACT_MST { get; set; }
    }
}
