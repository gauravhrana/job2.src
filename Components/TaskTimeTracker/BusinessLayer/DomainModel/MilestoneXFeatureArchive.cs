using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class MilestoneXFeatureArchiveDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string MilestoneXFeatureArchiveId = "MilestoneXFeatureArchiveId";
			public const string RecordDate = "RecordDate";
			public const string Milestone = "Milestone";
			public const string Feature = "Feature";
			public const string MilestoneFeatureState = "MilestoneFeatureState";
			public const string MilestoneXFeatureId = "MilestoneXFeatureId";
			public const string Memo = "Memo";
			public const string KnowledgeDate = "KnowledgeDate";
			public const string AcknowledgedById = "AcknowledgedById";
			public const string AcknowledgedBy = "AcknowledgedBy";
		}

		public int? MilestoneXFeatureArchiveId { get; set; }
		public int? RecordDate { get; set; }
		public string Milestone { get; set; }
		public string Feature { get; set; }
		public string MilestoneFeatureState { get; set; }
		public int? MilestoneXFeatureId { get; set; }
		public string Memo { get; set; }
		public DateTime KnowledgeDate { get; set; }
		public int? AcknowledgedById { get; set; }
		public string AcknowledgedBy { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; 
		}
	}
}
