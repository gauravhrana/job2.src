using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.Task
{

	[Serializable]
	public partial class TaskNoteDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string TaskNoteId = "TaskNoteId";
		}

		public static readonly TaskNoteDataModel Empty = new TaskNoteDataModel();

		public int? TaskNoteId { get; set; }

	}
}
