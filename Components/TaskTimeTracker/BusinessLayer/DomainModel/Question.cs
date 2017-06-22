using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class QuestionDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string QuestionId			= "QuestionId";
			public const string QuestionPhrase		= "QuestionPhrase";
			public const string QuestionCategoryId	= "QuestionCategoryId";
			public const string Category			= "Category";
			public const string SortOrder			= "SortOrder";
		}

		public int? QuestionId { get; set; }
		public string QuestionPhrase { get; set; }
		public string Category { get; set; }
		public int? QuestionCategoryId { get; set; }
		public int? SortOrder { get; set; }

		public string ToURLQuery()
		{
			return String.Empty;
		}
	}
}
