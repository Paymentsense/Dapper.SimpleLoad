using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[app].[PRO_SUPPORT_RULES_LUT]"), ReferenceData]
    public class ProSupportRule
    {
        [PrimaryKey]
        public int? ProSupportRuleKey { get; set; }
        //public RuleType Type { get; set; }
        //public RuleStatementFlag RuleFlag { get; set; }
        //public RuleContext Context { get; set; }
        //public RuleRequirement Requirement { get; set; }
        //public RuleClassification Classification { get; set; }

        //public DocumentCategoryEnum DocumentCategory { get; set; }
        //public HighlightFieldEnum? FieldCategory { get; set; }

        public int? RuleMccCode { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string AdditionalInformation { get; set; }
        public string Reason { get; set; }
        public bool IsNoteRequired { get; set; }

        public bool IsForFirstData { get; set; }
        public bool IsForGlobalPayments { get; set; }
        public bool IsForValitor { get; set; }

        private string _ruleFireReason;
    }
}
