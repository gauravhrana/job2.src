using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Competency
{

	[Serializable]
	public partial class CompetencyDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string CompetencyId = "CompetencyId";
		}

		public static readonly CompetencyDataModel Empty = new CompetencyDataModel();

		public int? CompetencyId { get; set; }

	}
}
