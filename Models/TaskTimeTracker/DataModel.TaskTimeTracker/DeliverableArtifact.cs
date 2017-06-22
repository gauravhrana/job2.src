//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataModel.Framework.DataAccess;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace DataModel.TaskTimeTracker
//{
//    [Table("DeliverableArtifact")]
//    public class DeliverableArtifactDataModel : StandardDataModel
//    {
//        public class DataColumns : StandardDataColumns
//        {
//            public const string DeliverableArtifactId = "DeliverableArtifactId";
//        }

//        public static readonly DeliverableArtifactDataModel Empty = new DeliverableArtifactDataModel();
        
//        [Key]
//        public int? DeliverableArtifactId { get; set; }

//    }
//}
