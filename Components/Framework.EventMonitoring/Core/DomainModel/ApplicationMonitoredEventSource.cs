using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.EventMonitoring
{
    [Serializable]
	public class ApplicationMonitoredEventSourceDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ApplicationMonitoredEventSourceId = "ApplicationMonitoredEventSourceId";
			public const string Code							  = "Code";			
		}

        public static readonly ApplicationMonitoredEventSourceDataModel Empty = new ApplicationMonitoredEventSourceDataModel();

		public int? ApplicationMonitoredEventSourceId { get; set; }
		public string Code { get; set; }		

	}
}
