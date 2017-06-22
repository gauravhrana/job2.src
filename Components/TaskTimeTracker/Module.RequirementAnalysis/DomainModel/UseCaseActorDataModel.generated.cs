using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.RequirementAnalysis
{

	[Serializable]
	public partial class UseCaseActorDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string UseCaseActorId = "UseCaseActorId";
		}

		public static readonly UseCaseActorDataModel Empty = new UseCaseActorDataModel();

		public int? UseCaseActorId { get; set; }

	}
}
