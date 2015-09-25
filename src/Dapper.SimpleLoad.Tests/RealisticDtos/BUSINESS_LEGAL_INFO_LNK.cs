using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[app].[BUSINESS_LEGAL_INFO_LNK]")]
    public class BUSINESS_LEGAL_INFO_LNK
    {
        [PrimaryKey, Column("BusinessLegalInfoGUID")]
        public Guid? BusinessLegalInfoGUID { get; set; }
        public Guid ApplicationGUID { get; set; }
        public Guid BusinessDetailGuid { get; set; }
        public Guid UpdateSessionGUID { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
    
        public virtual APPLICATION_MST APPLICATION_MST { get; set; }
        //public virtual BUSINESS_DETAIL_TRN BUSINESS_DETAIL_TRN { get; set; }
    }
}
