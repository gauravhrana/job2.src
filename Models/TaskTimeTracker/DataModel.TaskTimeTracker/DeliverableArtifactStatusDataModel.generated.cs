using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{

	[Serializable]
	public partial class DeliverableArtifactStatusDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string DeliverableArtifactStatusId = "DeliverableArtifactStatusId";
		}

		public static readonly DeliverableArtifactStatusDataModel Empty = new DeliverableArtifactStatusDataModel();

		public int? DeliverableArtifactStatusId { get; set; }

	}
}
