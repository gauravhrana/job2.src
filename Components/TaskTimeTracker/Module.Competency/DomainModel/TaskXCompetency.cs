using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Competency
{
    [Serializable]
	public class TaskXCompetencyDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string TaskXCompetencyId = "TaskXCompetencyId";
			public const string TaskId = "TaskId";
			public const string CompetencyId = "CompetencyId";

			public const string Task = "Task";
			public const string Competency = "Competency";
		}

        public static readonly TaskXCompetencyDataModel Empty = new TaskXCompetencyDataModel();

		public int? TaskXCompetencyId { get; set; }
		public int? TaskId { get; set; }
		public int? CompetencyId { get; set; }

		public string Task { get; set; }
		public string Competency { get; set; }

	}
}
