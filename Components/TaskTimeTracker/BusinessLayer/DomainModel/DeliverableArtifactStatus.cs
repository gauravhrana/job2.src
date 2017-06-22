using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class DeliverableArtifactStatusDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string DeliverableArtifactStatusId = "DeliverableArtifactStatusId";
		}

		public int? DeliverableArtifactStatusId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"DeliverableArtifactStatusId=" + DeliverableArtifactStatusId
		}
	}
}
