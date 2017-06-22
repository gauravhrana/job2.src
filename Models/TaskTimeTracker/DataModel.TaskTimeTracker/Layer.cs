//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DataModel.Framework.DataAccess;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace DataModel.TaskTimeTracker
//{
//    [Table("Layer")]
//    public class LayerDataModel : StandardDataModel
//    {
//        public class DataColumns : StandardDataColumns
//        {
//            public const string LayerId = "LayerId";
//        }

//        public static readonly LayerDataModel Empty = new LayerDataModel();

//        [Key]
//        public int? LayerId { get; set; }

//    }
//}
