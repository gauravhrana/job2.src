using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{

	[Serializable]
	public partial class UseCasePackageDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string UseCasePackageId = "UseCasePackageId";
		}

		public static readonly UseCasePackageDataModel Empty = new UseCasePackageDataModel();

		public int? UseCasePackageId { get; set; }

	}
}
