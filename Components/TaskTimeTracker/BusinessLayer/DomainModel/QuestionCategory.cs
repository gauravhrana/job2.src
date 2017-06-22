using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class QuestionCategoryDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string QuestionCategoryId = "QuestionCategoryId";
		}

		public int? QuestionCategoryId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"QuestionCategoryId=" + QuestionCategoryId
		}
	}
}
