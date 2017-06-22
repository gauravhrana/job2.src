using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{
	public class ProjectXNeedDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ProjectXNeedId	= "ProjectXNeedId";
			public const string ProjectId		= "ProjectId";
			public const string NeedId			= "NeedId";

			public const string Project			= "Project";
			public const string Need			= "Need";
		}

		public int? ProjectXNeedId { get; set; }
		public int? ProjectId { get; set; }
		public int? NeedId { get; set; }

		public string Project { get; set; }
		public string Need { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"ProjectId=" + ProjectId
		}
	}
}
