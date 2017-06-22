using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class TaskXDeliverableArtifacDataModel : BaseDataModel
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

		public int? TaskXDeliverableArtifactId { get; set; }
		public int? TaskId { get; set; }
		public int? DeliverableArtifactId { get; set; }
		public int? DeliverableArtifactStatusId { get; set; }

		public string Task { get; set; }
		public string DeliverableArtifact { get; set; }
		public string DeliverableArtifactStatus { get; set; }

		public string ToURLQuery()
		{
			return String.Empty;
		}
	}
}
