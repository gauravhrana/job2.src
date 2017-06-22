using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.EventMonitoring
{

	[Serializable]
	public partial class NotificationSubscriberDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string NotificationSubscriberId = "NotificationSubscriberId";
		}

		public static readonly NotificationSubscriberDataModel Empty = new NotificationSubscriberDataModel();

		public int? NotificationSubscriberId { get; set; }

	}
}
