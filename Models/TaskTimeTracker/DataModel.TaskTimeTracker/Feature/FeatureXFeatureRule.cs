using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker.Feature
{
    [Serializable]
    [Table("FeatureXFeatureRule")]
	public class FeatureXFeatureRuleDataModel : BaseDataModel
    {
		public class DataColumns : BaseDataColumns
        {
            public const string FeatureXFeatureRuleId = "FeatureXFeatureRuleId";
            public const string FeatureRuleId = "FeatureRuleId";
            public const string FeatureId = "FeatureId";
            public const string FeatureRuleStatusId = "FeatureRuleStatusId";
            public const string FeatureRule = "FeatureRule";
            public const string Feature = "Feature";
            public const string FeatureRuleStatus = "FeatureRuleStatus";
        }

        public static readonly FeatureXFeatureRuleDataModel Empty = new FeatureXFeatureRuleDataModel();

        [Key]
        public int? FeatureXFeatureRuleId { get; set; }
        public int? FeatureRuleId { get; set; }
        public int? FeatureId { get; set; }
        public int? FeatureRuleStatusId { get; set; }
        public int[] FeatureRuleStatusIds { get; set; }
        public string FeatureRule { get; set; }
        public string Feature { get; set; }
        public string FeatureRuleStatus { get; set; }

           
    }
}
