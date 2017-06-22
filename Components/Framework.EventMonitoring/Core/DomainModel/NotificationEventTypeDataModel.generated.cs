using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.EventMonitoring
{

	[Serializable]
	public partial class NotificationEventTypeDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string NotificationEventTypeId = "NotificationEventTypeId";
		}

		public static readonly NotificationEventTypeDataModel Empty = new NotificationEventTypeDataModel();

		public int? NotificationEventTypeId { get; set; }

	}
}
