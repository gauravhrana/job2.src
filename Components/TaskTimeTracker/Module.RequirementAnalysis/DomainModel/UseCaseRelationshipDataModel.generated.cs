using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{

	[Serializable]
	public partial class UseCaseRelationshipDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string UseCaseRelationshipId = "UseCaseRelationshipId";
		}

		public static readonly UseCaseRelationshipDataModel Empty = new UseCaseRelationshipDataModel();

		public int? UseCaseRelationshipId { get; set; }

	}
}
