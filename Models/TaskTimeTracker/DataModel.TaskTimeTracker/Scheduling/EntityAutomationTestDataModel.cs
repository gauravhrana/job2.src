using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataModel.TaskTimeTracker
{
    [Serializable]
	public class EntityAutomationTestDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string EntityAutomationTestId = "EntityAutomationTestId";
			public const string QuestionPhrase = "QuestionPhrase";
			public const string QuestionCategoryId = "QuestionCategoryId";
			public const string QuestionCategory = "QuestionCategory";
			public const string SortOrder = "SortOrder";
		}

		public static readonly EntityAutomationTestDataModel Empty = new EntityAutomationTestDataModel();

		[Key]
		public int? EntityAutomationTestId { get; set; }

		public string QuestionPhrase { get; set; }
		public string QuestionCategory { get; set; }
		public int? QuestionCategoryId { get; set; }
		public int? SortOrder { get; set; }

	}
}

