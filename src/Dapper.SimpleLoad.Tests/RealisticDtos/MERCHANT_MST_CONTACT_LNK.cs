using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[cust].[MERCHANT_MST_CONTACT_LNK]")]
    public class MERCHANT_MST_CONTACT_LNK
    {
        [PrimaryKey]
        public Guid? MerchantContactLnkGUID { get; set; }
        public Guid MerchantGUID { get; set; }
        public Guid ContactGUID { get; set; }
        //public GenContactRoleEnum ContactRoleKey { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
    
        public virtual MERCHANT_MST MERCHANT_MST { get; set; }
        public virtual CONTACT_MST CONTACT_MST { get; set; }
    }
}
