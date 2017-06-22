using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;


namespace DataModel.TaskTimeTracker.Task
{
	public class TaskNoteDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TaskNoteId = "TaskNoteId";
		}

		public int? TaskNoteId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"TaskNoteId=" + TaskNoteId
		}
	}
}
