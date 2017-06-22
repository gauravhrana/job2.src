using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class MilestoneDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string MilestoneId = "MilestoneId";
			public const string ProjectId = "ProjectId";
			public const string Project = "Project";
		}

		public int? MilestoneId { get; set; }
		public int? ProjectId { get; set; }
		public string Project { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"MilestoneId=" + MilestoneId
		}
	}
}
