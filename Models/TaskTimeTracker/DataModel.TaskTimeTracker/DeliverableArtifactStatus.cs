//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataModel.Framework.DataAccess;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace DataModel.TaskTimeTracker
//{
//    [Table("DeliverableArtifactStatus")]
//    public class DeliverableArtifactStatusDataModel : StandardDataModel
//    {
//        public class DataColumns : StandardDataColumns
//        {
//            public const string DeliverableArtifactStatusId = "DeliverableArtifactStatusId";
//        }

//        public static readonly DeliverableArtifactStatusDataModel Empty = new DeliverableArtifactStatusDataModel();

//        [Key]
//        public int? DeliverableArtifactStatusId { get; set; }

//    }
//}
