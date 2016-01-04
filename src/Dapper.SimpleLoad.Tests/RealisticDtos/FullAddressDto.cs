using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    public class FullAddressDto : AddressDto
    {
        [SimpleSaveIgnore]
        [SimpleLoadIgnore]
        public string CompanyName { get; set; }

        [SimpleSaveIgnore]
        [SimpleLoadIgnore]
        public string PostCode { get; set; }

        [SimpleSaveIgnore]
        [SimpleLoadIgnore]
        public bool Verified { get; set; }

        [SimpleSaveIgnore]
        [SimpleLoadIgnore]
        public string VerifiedId { get; set; }

        [SimpleSaveIgnore]
        [SimpleLoadIgnore]
        public string ValidationIdentity { get; set; }
    }

    /// <summary>
    /// This is just a tagging class to allow SimpleLoad to distinguish the different
    /// addresses on an opportunity.
    /// </summary>
    public class ShippingAddressDto : FullAddressDto
    {
    }
}
