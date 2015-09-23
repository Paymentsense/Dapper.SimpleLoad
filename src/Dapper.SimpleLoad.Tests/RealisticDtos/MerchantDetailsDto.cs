using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[tele].[MERCHANT_DETAILS_MST]")]
    public class MerchantDetailsDto
    {
        [PrimaryKey]
        public int? MerchantDetailsId { get; set; }

        public int? PspId { get; set; }

        public int? CCProcessorId { get; set; }

        public int? TerminalModelId { get; set; }

        public int? TerminalAgeId { get; set; }

        [SimpleSaveIgnore]
        [ManyToOne]
        [Column("TerminalModelId")]
        public TerminalModelDto TerminalModel { get; set; }
    }
}
