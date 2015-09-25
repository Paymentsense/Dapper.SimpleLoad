using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[gen].[SALES_CHANNEL_LNK]")]
    public class SALES_CHANNEL_LNK
    {
        public SALES_CHANNEL_LNK()
        {
            this.OFFER_TRN = new HashSet<OFFER_TRN>();
            this.APPLICATION_MST = new HashSet<APPLICATION_MST>();
        }
        [PrimaryKey]
        public Guid? SalesChannelGUID { get; set; }
        public int DepartmentKey { get; set; }
        public GenCountryEnum CountryKey { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
    
        //public virtual DEPARTMENT_LUT DEPARTMENT_LUT { get; set; }
        public virtual ICollection<OFFER_TRN> OFFER_TRN { get; set; }
        public virtual ICollection<APPLICATION_MST> APPLICATION_MST { get; set; }
    }
}
