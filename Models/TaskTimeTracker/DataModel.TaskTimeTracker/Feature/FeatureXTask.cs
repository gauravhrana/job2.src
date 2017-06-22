using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker.Feature
{
    [Serializable]
    [Table("FeatureXTask")]
	public class FeatureXTaskDataModel : BaseDataModel
	{
		public class DataColumns : BaseDataColumns
		{
			public const string FeatureXTaskId		 = "FeatureXTaskId";
			public const string TaskId				 = "TaskId";
			public const string FeatureId			 = "FeatureId";
			public const string Task				 = "Task";
			public const string Feature				 = "Feature";
		}

        public static readonly FeatureXTaskDataModel Empty = new FeatureXTaskDataModel();

        [Key]
		public int?		FeatureXTaskId		{ get; set; }
		public int?		TaskId				{ get; set; }
		public int?		FeatureId			{ get; set; }
		public string	Task				{ get; set; }
		public string	Feature				{ get; set; }
		
	}
}
