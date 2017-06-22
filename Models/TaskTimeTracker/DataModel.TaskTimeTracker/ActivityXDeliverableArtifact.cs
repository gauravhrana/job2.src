using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.TaskTimeTracker
{
    [Table("ActivityXDeliverableArtifact"), Serializable]
	public class ActivityXDeliverableArtifactDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ActivityXDeliverableArtifactId		= "ActivityXDeliverableArtifactId";
			public const string DeliverableArtifactId				= "DeliverableArtifactId";
			public const string ActivityId							= "ActivityId";
			public const string DeliverableArtifactStatusId			= "DeliverableArtifactStatusId";

			public const string DeliverableArtifact					= "DeliverableArtifact";
			public const string Activity							= "Activity";
			public const string DeliverableArtifactStatus			= "DeliverableArtifactStatus";
		}

        public static readonly ActivityXDeliverableArtifactDataModel Empty = new ActivityXDeliverableArtifactDataModel();

        [Key]
		public int? ActivityXDeliverableArtifactId { get; set; }
		public int? DeliverableArtifactId { get; set; }
		public int? ActivityId { get; set; }
		public int? DeliverableArtifactStatusId { get; set; }

		public string DeliverableArtifact { get; set; }
		public string Activity { get; set; }
		public string DeliverableArtifactStatus { get; set; }

	}
}
