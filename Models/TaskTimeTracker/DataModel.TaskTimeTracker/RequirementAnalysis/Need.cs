//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataModel.Framework.DataAccess;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace DataModel.TaskTimeTracker.RequirementAnalysis
//{
//    [Table("Need")]
//    public class NeedDataModel : StandardDataModel
//    {
//        public class DataColumns : StandardDataColumns
//        {
//            public const string NeedId = "NeedId";
//        }

//        public static readonly NeedDataModel Empty = new NeedDataModel();

//        [Key]
//        public int? NeedId { get; set; }

//    }
//}
