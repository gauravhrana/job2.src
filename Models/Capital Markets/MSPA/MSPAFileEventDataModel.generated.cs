using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class MSPAFileEventDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string MSPAFileEventId = "MSPAFileEventId";
			public const string Description = "Description";
			public const string CreatedBy = "CreatedBy";
			public const string CreatedOn = "CreatedOn";
			public const string MSPAFileId = "MSPAFileId";
			public const string MSPAFile = "MSPAFile";
			public const string TradingEventTypeId = "TradingEventTypeId";
			public const string TradingEventType = "TradingEventType";
		}

		public static readonly MSPAFileEventDataModel Empty = new MSPAFileEventDataModel();

	}
}
