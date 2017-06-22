using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class MSPAFileEventTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string MSPAFileEventTypeId = "MSPAFileEventTypeId";
		}

		public static readonly MSPAFileEventTypeDataModel Empty = new MSPAFileEventTypeDataModel();

	}
}
