using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{
	public class ProjectTimeLineDataModel : BaseDataModel
	{
		public class DataColumns 
		{
			public const string ProjectTimeLineId = "ProjectTimeLineId";
			public const string ProjectId = "ProjectId";
			public const string Project = "Project";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
		}
		
			public int? ProjectTimeLineId { get; set; }
			public int? ProjectId { get; set; }
			public string Project { get; set; }
			public int? StartDate { get; set; }
			public int? EndDate { get; set; }

			public string ToURLQuery()
			{
				return String.Empty; //"NeedId=" + NeedId
			}
	}
}
