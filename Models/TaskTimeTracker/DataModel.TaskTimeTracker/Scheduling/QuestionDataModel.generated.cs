using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{

	public partial class QuestionDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string QuestionId = "QuestionId";
		}

		public static readonly QuestionDataModel Empty = new QuestionDataModel();

		public int? QuestionId { get; set; }

	}
}
