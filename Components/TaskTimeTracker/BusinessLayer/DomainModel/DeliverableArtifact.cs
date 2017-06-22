using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class DeliverableArtifacDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string DeliverableArtifactId = "DeliverableArtifactId";
		}

		public int? DeliverableArtifactId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"DeliverableArtifactId=" + DeliverableArtifactId
		}
	}
}
