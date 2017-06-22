using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.EventMonitoring
{
    [Serializable]
	public class NotificationRegistrarDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string NotificationRegistrarId		= "NotificationRegistrarId";
			public const string NotificationEventTypeId		= "NotificationEventTypeId";			
			public const string NotificationPublisherId		= "NotificationPublisherId";
			public const string Message						= "Message";
			public const string PublishDateId				= "PublishDateId";
			public const string PublishTimeId				= "PublishTimeId";
			public const string NotificationEventType		= "NotificationEventType";
		}

        public static readonly NotificationRegistrarDataModel Empty = new NotificationRegistrarDataModel();

		public int? NotificationRegistrarId { get; set; }
		public int? NotificationEventTypeId { get; set; }		
		public int? NotificationPublisherId { get; set; }
		public string Message				{ get; set; }
		public int PublishDateId			{ get; set; }
		public int PublishTimeId			{ get; set; }
		public string NotificationEventType { get; set; }

	}
}
