using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{
    [Serializable]
    [Table("NeedXFeature")]
	public class NeedXFeatureDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string NeedXFeatureId	= "NeedXFeatureId";
			public const string NeedId			= "NeedId";			
			public const string FeatureId		= "FeatureId";
			public const string Need			= "Need";
			public const string Feature			= "Feature";
		}

        public static readonly NeedXFeatureDataModel Empty = new NeedXFeatureDataModel();

        [Key]
		public int? NeedXFeatureId { get; set; }
		public int? NeedId { get; set; }
		public int? FeatureId { get; set; }
		public string Need { get; set; }
		public string Feature { get; set; }

	}
}
