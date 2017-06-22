//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataModel.Framework.DataAccess;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace DataModel.TaskTimeTracker.Feature
//{
//    [Table("Feature")]
//    public class FeatureDataModel : StandardDataModel
//    {
//        public class DataColumns : StandardDataColumns
//        {
//            public const string FeatureId = "FeatureId";
//        }

//        public static readonly FeatureDataModel Empty = new FeatureDataModel();

//        [Key]
//        public int? FeatureId { get; set; }

//    }
//}
