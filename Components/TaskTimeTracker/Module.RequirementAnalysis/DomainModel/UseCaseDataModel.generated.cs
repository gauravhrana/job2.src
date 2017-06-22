using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{

	[Serializable]
	public partial class UseCaseDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string UseCaseId = "UseCaseId";
		}

		public static readonly UseCaseDataModel Empty = new UseCaseDataModel();

		public int? UseCaseId { get; set; }

	}
}
