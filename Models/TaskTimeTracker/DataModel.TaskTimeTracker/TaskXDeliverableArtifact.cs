using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker
{
    [Serializable]
    [Table("TaskXDeliverableArtifact")]
	public class TaskXDeliverableArtifactDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string TaskId						= "TaskId";
			public const string TaskXDeliverableArtifactId	= "TaskXDeliverableArtifactId";
			public const string DeliverableArtifactId		= "DeliverableArtifactId";
			public const string DeliverableArtifactStatusId = "DeliverableArtifactStatusId";
			public const string DeliverableArtifact			= "DeliverableArtifact";
			public const string Task						= "Task";
			public const string DeliverableArtifactStatus	= "DeliverableArtifactStatus";
		}

        public static readonly TaskXDeliverableArtifactDataModel Empty = new TaskXDeliverableArtifactDataModel();
        
        [Key]
		public int? TaskXDeliverableArtifactId { get; set; }
		public int? TaskId { get; set; }
		public int? DeliverableArtifactId { get; set; }
		public int? DeliverableArtifactStatusId { get; set; }

		public string Task { get; set; }
		public string DeliverableArtifact { get; set; }
		public string DeliverableArtifactStatus { get; set; }

	}
}
