//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataModel.Framework.DataAccess;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace DataModel.TaskTimeTracker.Feature
//{
//    [Table("FeatureGroup")]
//    public class FeatureGroupDataModel : StandardDataModel
//    {
//        public class DataColumns : StandardDataColumns
//        {
//            public const string FeatureGroupId = "FeatureGroupId";
//        }

//        public static readonly FeatureGroupDataModel Empty = new FeatureGroupDataModel();

//        [Key]
//        public int? FeatureGroupId { get; set; }

//    }
//}
