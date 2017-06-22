using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataModel.TaskTimeTracker.TimeTracking
{
    [Serializable]
	[Table("ScheduleQuestion")]
	public class ScheduleQuestionDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ScheduleQuestionId	= "ScheduleQuestionId";
			public const string ScheduleId			= "ScheduleId";
			public const string QuestionId			= "QuestionId";
			public const string Answer				= "Answer";
			public const string FromSearchDate		= "FromSearchDate";
			public const string ToSearchDate		= "ToSearchDate";
			public const string UpdatedRange		= "UpdatedRange";
			public const string QuestionPhrase		= "QuestionPhrase";
		}

		public static readonly ScheduleQuestionDataModel Empty = new ScheduleQuestionDataModel();

		[Key]
		public int? ScheduleQuestionId	{ get; set; }
		public int? ScheduleId			{ get; set; }
		public int? QuestionId			{ get; set; }
		public string Answer			{ get; set; }
		public DateTime? FromSearchDate { get; set; }
		public DateTime? ToSearchDate   { get; set; }
		public string QuestionPhrase	{ get; set; }

		
	}
}
