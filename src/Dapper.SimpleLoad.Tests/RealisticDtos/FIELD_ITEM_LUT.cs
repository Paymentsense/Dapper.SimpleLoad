using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[opp].[FIELD_ITEM_LUT]")]
    public class FIELD_ITEM_LUT
    {
        public FIELD_ITEM_LUT()
        {
            //this.OFFER_DEFAULTS_TRN1 = new HashSet<OFFER_DEFAULTS_TRN>();
        }
        [PrimaryKey]
        public int? FieldItemKey { get; set; }
        public string Name { get; set; }
        //public UsrResourceEnum OverrideResourceKey { get; set; }
        public Guid BaseRowGUID { get; set; }
        public GenRecStatusEnum RecStatusKey { get; set; }
        public string Description { get; set; }
        public int? DisplayOrder { get; set; }
    
        //public virtual ICollection<OFFER_DEFAULTS_TRN> OFFER_DEFAULTS_TRN1 { get; set; }
        //public virtual DEBIT_PRIORITY_LUT DEBIT_PRIORITY_LUT { get; set; }
    }
}
