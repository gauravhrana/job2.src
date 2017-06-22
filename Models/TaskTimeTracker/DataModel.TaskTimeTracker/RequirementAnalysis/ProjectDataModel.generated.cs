using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{

	[Serializable]
	public partial class ProjectDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ProjectId = "ProjectId";
		}

		public static readonly ProjectDataModel Empty = new ProjectDataModel();

		public int? ProjectId { get; set; }

	}
}
