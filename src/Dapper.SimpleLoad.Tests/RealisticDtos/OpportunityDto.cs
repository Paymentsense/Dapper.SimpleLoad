using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[opp].[OPPORTUNITY_MST]")]
    public class OpportunityDto
    {
        [PrimaryKey]
        public Guid? OpportunityGuid { get; set; }

        [ForeignKeyReference(typeof(LocationDto))]
        public Guid? LocationGuid { get; set; }

        [OneToOne]
        [ForeignKeyReference(typeof(FullAddressDto))]
        [Column("BillingToAddressGUID")]
        public FullAddressDto BillingToAddress { get; set; }

        [OneToOne]
        [ForeignKeyReference(typeof(ShippingAddressDto))]
        [Column("ShippingAddressGUID")]
        public ShippingAddressDto ShippingAddress { get; set; }
    }
}
