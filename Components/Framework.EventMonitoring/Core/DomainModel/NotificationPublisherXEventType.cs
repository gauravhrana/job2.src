using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;


namespace DataModel.Framework.EventMonitoring
{
    [Serializable]
	public class NotificationPublisherXEventTypeDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string NotificationPublisherXEventTypeId	= "NotificationPublisherXEventTypeId";
			public const string NotificationEventTypeId				= "NotificationEventTypeId";
			public const string NotificationPublisherId				= "NotificationPublisherId";
			public const string NotificationEventType				= "NotificationEventType";
			public const string NotificationPublisher				= "NotificationPublisher";			
			public const string CreatedTimeId						= "CreatedTimeId";
		}

        public static readonly NotificationPublisherXEventTypeDataModel Empty = new NotificationPublisherXEventTypeDataModel();

		public int? NotificationPublisherXEventTypeId { get; set; }
		public int? NotificationEventTypeId { get; set; }
		public string NotificationEventType { get; set; }
		public string NotificationPublisher { get; set; }
		public int? NotificationPublisherId { get; set; }		
		public int? CreatedTimeId { get; set; }			

	}
}
