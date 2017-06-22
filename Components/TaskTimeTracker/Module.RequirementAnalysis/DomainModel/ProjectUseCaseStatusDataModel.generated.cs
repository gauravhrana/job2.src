using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{

	[Serializable]
	public partial class ProjectUseCaseStatusDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ProjectUseCaseStatusId = "ProjectUseCaseStatusId";
		}

		public static readonly ProjectUseCaseStatusDataModel Empty = new ProjectUseCaseStatusDataModel();

		public int? ProjectUseCaseStatusId { get; set; }

	}
}
