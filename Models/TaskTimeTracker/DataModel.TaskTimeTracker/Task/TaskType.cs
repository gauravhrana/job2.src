//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataModel.Framework.DataAccess;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;


//namespace DataModel.TaskTimeTracker.Task
//{
//    [Table("TaskType")]
//    public class TaskTypeDataModel : StandardDataModel
//    {
//        public class DataColumns : StandardDataColumns
//        {
//            public const string TaskTypeId = "TaskTypeId";
//        }

//        public static readonly TaskTypeDataModel Empty = new TaskTypeDataModel();

//        [Key]
//        public int? TaskTypeId { get; set; }

//    }
//}
