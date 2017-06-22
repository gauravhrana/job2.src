using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.EventMonitoring
{
    [Serializable]
	public class ApplicationMonitoredEventDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ApplicationMonitoredEventId			= "ApplicationMonitoredEventId";
			public const string ApplicationMonitoredEventSourceId	= "ApplicationMonitoredEventSourceId";
			public const string ApplicationMonitoredEventProcessingStateId = "ApplicationMonitoredEventProcessingStateId";
			public const string ReferenceId				= "ReferenceId";
			public const string ReferenceCode			= "ReferenceCode";
			public const string Category				= "Category";
			public const string Message					= "Message";
			public const string IsDuplicate				= "IsDuplicate";
			public const string LastModifiedBy			= "LastModifiedBy";
			public const string LastModifiedOn			= "LastModifiedOn";

			public const string ApplicationMonitoredEventSource				= "ApplicationMonitoredEventSource";
			public const string ApplicationMonitoredEventProcessingState	= "ApplicationMonitoredEventProcessingState";
		}

        public static readonly ApplicationMonitoredEventDataModel Empty = new ApplicationMonitoredEventDataModel();

        public int? ApplicationMonitoredEventId { get; set; }
		public int? ApplicationMonitoredEventSourceId { get; set; }
		public int? ApplicationMonitoredEventProcessingStateId { get; set; }
		public int? ReferenceId { get; set; }
		public string ReferenceCode { get; set; }
		public string Category { get; set; }
		public string Message { get; set; }
		public bool? IsDuplicate { get; set; }
		public string LastModifiedBy { get; set; }
		public DateTime? LastModifiedOn { get; set; }
		public string ApplicationMonitoredEventSource { get; set; }
		public string ApplicationMonitoredEventProcessingState { get; set; }

	}

}