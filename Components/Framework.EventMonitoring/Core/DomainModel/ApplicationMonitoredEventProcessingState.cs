using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.EventMonitoring
{
    [Serializable]
	public class ApplicationMonitoredEventProcessingStateDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ApplicationMonitoredEventProcessingStateId = "ApplicationMonitoredEventProcessingStateId";
			public const string Code = "Code";			
		}

        public static readonly ApplicationMonitoredEventProcessingStateDataModel Empty = new ApplicationMonitoredEventProcessingStateDataModel();

		public int?     ApplicationMonitoredEventProcessingStateId { get; set; }
		public string   Code { get; set; }
		
		
	}
}
