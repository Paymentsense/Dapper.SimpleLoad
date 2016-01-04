using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[app].[LOCATION_MST]")]
    public class LocationDto
    {
        public LocationDto()
        {
            Opportunities = new List<OpportunityDto>();
        }

        [PrimaryKey]
        public Guid? LocationGuid { get; set; }

        [Column("TradingAddressGUID")]
        [ForeignKeyReference(typeof(FullAddressDto))]
        [OneToOne]
        public FullAddressDto TradingAddress { get; set; }

        [OneToMany]
        public IList<OpportunityDto> Opportunities { get; set; }
    }
}
