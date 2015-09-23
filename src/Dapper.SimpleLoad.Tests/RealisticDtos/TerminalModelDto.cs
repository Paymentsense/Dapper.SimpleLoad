using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[tele].[TERMINAL_MODEL_LUT]")]
    [ReferenceData]
    public class TerminalModelDto
    {
        [PrimaryKey]
        public int? TerminalModelId { get; set; }

        public int TerminalMakeId { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }
    }
}
