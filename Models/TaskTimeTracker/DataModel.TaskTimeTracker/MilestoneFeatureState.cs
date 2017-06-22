//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataModel.Framework.DataAccess;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace DataModel.TaskTimeTracker
//{
//    [Table("MilestoneFeatureState")]
//    public class MilestoneFeatureStateDataModel : StandardDataModel
//    {
//        public class DataColumns : StandardDataColumns
//        {
//            public const string MilestoneFeatureStateId = "MilestoneFeatureStateId";
//        }

//        public static readonly MilestoneFeatureStateDataModel Empty = new MilestoneFeatureStateDataModel();

//        [Key]
//        public int? MilestoneFeatureStateId { get; set; }

//    }
//}
