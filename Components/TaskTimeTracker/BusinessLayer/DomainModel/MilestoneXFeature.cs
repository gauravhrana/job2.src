using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;


namespace DataModel.TaskTimeTracker
{
	public class MilestoneXFeatureDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string MilestoneXFeatureId			= "MilestoneXFeatureId";
			public const string FeatureId					= "FeatureId";
			public const string MilestoneId					= "MilestoneId";
			public const string MilestoneFeatureStateId		= "MilestoneFeatureStateId";
			public const string Memo						= "Memo";
			public const string Feature						= "Feature";
			public const string Milestone					= "Milestone";
			public const string MilestoneFeatureState		= "MilestoneFeatureState";
		}

		public int? MilestoneXFeatureId			{ get; set; }
		public int? FeatureId					{ get; set; }
		public int? MilestoneId					{ get; set; }
		public int? MilestoneFeatureStateId		{ get; set; }
		public string Memo						{ get; set; }

		public string Feature					{ get; set; }
		public string Milestone					{ get; set; }
		public string MilestoneFeatureState		{ get; set; }

		public string ToURLQuery()
		{
			return String.Empty; 
		}
	}
}
