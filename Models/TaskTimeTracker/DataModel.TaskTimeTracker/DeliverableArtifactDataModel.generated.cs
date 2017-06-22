using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{

	[Serializable]
	public partial class DeliverableArtifactDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string DeliverableArtifactId = "DeliverableArtifactId";
		}

		public static readonly DeliverableArtifactDataModel Empty = new DeliverableArtifactDataModel();

		public int? DeliverableArtifactId { get; set; }

	}
}
