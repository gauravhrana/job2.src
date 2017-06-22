using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker
{
    [Serializable]
    [Table("Question")]
	public class QuestionDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string QuestionId			= "QuestionId";
			public const string QuestionPhrase		= "QuestionPhrase";
			public const string QuestionCategoryId	= "QuestionCategoryId";
            public const string QuestionCategory    = "QuestionCategory";
			public const string SortOrder			= "SortOrder";
		}

        public static readonly QuestionDataModel Empty = new QuestionDataModel();

		[Key]
		public int? QuestionId			{ get; set; }

		public string QuestionPhrase	{ get; set; }
        public string QuestionCategory  { get; set; }
		public int? QuestionCategoryId	{ get; set; }
		public int? SortOrder			{ get; set; }

	}
}
