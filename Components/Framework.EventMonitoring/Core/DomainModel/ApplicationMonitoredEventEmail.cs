using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.EventMonitoring
{
    [Serializable]
	public class ApplicationMonitoredEventEmailDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ApplicationMonitoredEventEmailId = "ApplicationMonitoredEventEmailId";
			public const string ApplicationMonitoredEventSourceId = "ApplicationMonitoredEventSourceId";
			public const string UserId = "UserId";
			public const string CorrespondenceLevel = "CorrespondenceLevel";
			public const string Active = "Active";
			public const string ApplicationMonitoredEventSource = "ApplicationMonitoredEventSource";
			public const string User = "User";
		}

        public static readonly ApplicationMonitoredEventEmailDataModel Empty = new ApplicationMonitoredEventEmailDataModel();

		public int? ApplicationMonitoredEventEmailId { get; set; }
		public int? ApplicationMonitoredEventSourceId { get; set; }
		public int? UserId { get; set; }
		public string CorrespondenceLevel { get; set; }
		public bool? Active { get; set; }
		public string ApplicationMonitoredEventSource { get; set; }
		public string User { get; set; }

	}
}
