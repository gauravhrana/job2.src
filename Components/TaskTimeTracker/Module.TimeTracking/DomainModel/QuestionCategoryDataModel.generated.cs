using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.TimeTracking
{

	[Serializable]
	public partial class QuestionCategoryDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string QuestionCategoryId = "QuestionCategoryId";
		}

		public static readonly QuestionCategoryDataModel Empty = new QuestionCategoryDataModel();

		public int? QuestionCategoryId { get; set; }

	}
}
