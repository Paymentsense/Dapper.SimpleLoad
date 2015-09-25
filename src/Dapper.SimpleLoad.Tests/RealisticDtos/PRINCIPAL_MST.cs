using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[app].[PRINCIPAL_MST]")]
    public class PRINCIPAL_MST : CONTACT_MST
    {
        public PRINCIPAL_MST()
        {
            //this.EXPERIAN_ID_CHECK_TRN = new HashSet<EXPERIAN_ID_CHECK_TRN>();
            //this.ADDRESS_OWNERSHIP_LENGTH_LNK = new HashSet<ADDRESS_OWNERSHIP_LENGTH_LNK>();
        }
    
        [ForeignKeyReference(typeof(APPLICATION_MST))]
        public Guid ApplicationGUID { get; set; }
        public bool IsPrimaryPrincipal { get; set; }
        public int GenderKey { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int NationalityCountryKey { get; set; }
        public bool HasValidEIDCheck { get; set; }
        public int ContactRoleKey { get; set; }
        public DateTime? DateRoleTaken { get; set; }
        public int? SharesHeldPercent { get; set; }
        public DateTimeOffset UpdateDate1 { get; set; }
        public Guid UpdateSessionGUID1 { get; set; }
        public bool IsBeneficialOwner { get; set; }
        public int BusinessAffiliateKey { get; set; }
    
        [SimpleLoadIgnore, SimpleSaveIgnore]
        public virtual APPLICATION_MST APPLICATION_MST { get; set; }
        //public virtual ICollection<EXPERIAN_ID_CHECK_TRN> EXPERIAN_ID_CHECK_TRN { get; set; }
        //public virtual ICollection<ADDRESS_OWNERSHIP_LENGTH_LNK> ADDRESS_OWNERSHIP_LENGTH_LNK { get; set; }
    }
}
