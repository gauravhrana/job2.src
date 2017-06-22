//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataModel.Framework.DataAccess;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

//namespace DataModel.TaskTimeTracker.TimeTracking
//{
//	[Table("ScheduleState")]
//	public class ScheduleStateDataModel : StandardDataModel
//	{
//		public class DataColumns : StandardDataColumns
//		{
//			public const string ScheduleStateId = "ScheduleStateId";
//		}

//		public static readonly ScheduleStateDataModel Empty = new ScheduleStateDataModel();

//		[Key]
//		public int? ScheduleStateId { get; set; }

//	}
//}
