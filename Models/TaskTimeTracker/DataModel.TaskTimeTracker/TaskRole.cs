//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataModel.Framework.DataAccess;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;


//namespace DataModel.TaskTimeTracker
//{
//    [Table("TaskRole")]
//    public class TaskRoleDataModel : StandardDataModel
//    {
//        public class DataColumns : StandardDataColumns
//        {
//            public const string TaskRoleId = "TaskRoleId";
//        }

//        public static readonly TaskRoleDataModel Empty = new TaskRoleDataModel();

//        [Key]
//        public int? TaskRoleId { get; set; }
//    }
//}
