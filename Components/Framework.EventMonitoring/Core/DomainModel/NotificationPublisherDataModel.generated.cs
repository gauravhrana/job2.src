using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.EventMonitoring
{

	[Serializable]
	public partial class NotificationPublisherDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string NotificationPublisherId = "NotificationPublisherId";
		}

		public static readonly NotificationPublisherDataModel Empty = new NotificationPublisherDataModel();

		public int? NotificationPublisherId { get; set; }

	}
}
