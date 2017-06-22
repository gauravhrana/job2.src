using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker
{
    [Table("QuestionCategory")]
	public class QuestionCategoryDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string QuestionCategoryId = "QuestionCategoryId";
		}

        public static readonly QuestionCategoryDataModel Empty = new QuestionCategoryDataModel();
       
        [Key]
		public int? QuestionCategoryId { get; set; }

	}
}
