using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[app].[SHAREHOLDER_MST]")]
    public class SHAREHOLDER_MST
    {
        [PrimaryKey]
        public Guid? ShareholderGUID { get; set; }
        [ForeignKeyReference(typeof(APPLICATION_MST))]
        public Guid ApplicationGUID { get; set; }
        public bool IsCompany { get; set; }
        public int? SalutationKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? NationalityKey { get; set; }
        public bool IsBeneficialOwner { get; set; }
        public string CompanyName { get; set; }
        public string RegisteredNumber { get; set; }
        public int PercentageSharesHeld { get; set; }
        public Guid AddressGUID { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
    
        [SimpleLoadIgnore, SimpleSaveIgnore]
        public virtual APPLICATION_MST APPLICATION_MST { get; set; }
        [SimpleLoadIgnore, SimpleSaveIgnore]
        public virtual ADDRESS_MST ADDRESS_MST { get; set; }
    }
}
