using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Components.EventMonitoring
{
	public partial class NotificationPublisherXNotificationEventType
	{
		public class DataColumns : DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns
		{
			public const string NotificationPublisherXNotificationEventTypeId = "NotificationPublisherXNotificationEventTypeId";
			public const string NotificationEventTypeId                       = "NotificationEventTypeId";
			public const string NotificationPublisherId                       = "NotificationPublisherId";
					}

		public class Data : DataModel.Framework.DataAccess.BaseDataModel
		{
			public int? NotificationPublisherXNotificationEventTypeId  { get; set; }
			public int? NotificationEventTypeId                        { get; set; }
			public int? NotificationPublisherId                        { get; set; }
			
		
			public string ToSQLParameter(string dataColumnName)
			{
				var returnValue = "NULL";

				switch (dataColumnName)
				{

					case DataColumns.NotificationPublisherXNotificationEventTypeId:
						if (NotificationPublisherXNotificationEventTypeId != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.NotificationPublisherXNotificationEventTypeId, NotificationPublisherXNotificationEventTypeId);


						}

						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.NotificationPublisherXNotificationEventTypeId);

						}
						break;

					case DataColumns.NotificationEventTypeId:
						if (NotificationEventTypeId != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.NotificationEventTypeId, NotificationEventTypeId);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.NotificationEventTypeId);

						}
						break;

					case DataColumns.NotificationPublisherId:
						if (NotificationPublisherId != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.NotificationPublisherId, NotificationPublisherId);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.NotificationPublisherId);

						}
						break;
					case DataColumns.CreatedDateId:
						if (CreatedDateId != null)
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.CreatedDateId, CreatedDateId);

						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.CreatedDateId);

						}
						break;

				}
				return returnValue;
			}

		}

	}
}
