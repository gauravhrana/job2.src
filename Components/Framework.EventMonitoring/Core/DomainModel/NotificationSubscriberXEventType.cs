using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.EventMonitoring
{
    [Serializable]
	public class NotificationSubscriberXEventTypeDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string NotificationSubscriberXEventTypeId	= "NotificationSubscriberXEventTypeId";
			public const string NotificationEventTypeId				= "NotificationEventTypeId";
			public const string NotificationSubscriberId			= "NotificationSubscriberId";
			public const string NotificationEventType				= "NotificationEventType";
			public const string NotificationSubscriber				= "NotificationSubscriber";			
			public const string CreatedTimeId						= "CreatedTimeId";
		}

        public static readonly NotificationSubscriberXEventTypeDataModel Empty = new NotificationSubscriberXEventTypeDataModel();

		public int?     NotificationSubscriberXEventTypeId  { get; set; }
		public int?     NotificationEventTypeId				{ get; set; }
		public string   NotificationEventType				{ get; set; }
		public string   NotificationSubscriber			    { get; set; }
		public int?     NotificationSubscriberId			{ get; set; }		
		public int?     CreatedTimeId				        { get; set; }
	
	}
}
