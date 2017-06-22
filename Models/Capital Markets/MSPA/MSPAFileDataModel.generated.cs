using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class MSPAFileDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string MSPAFileId = "MSPAFileId";
			public const string Filename = "Filename";
			public const string DropDate = "DropDate";
			public const string BusinessDate = "BusinessDate";
			public const string MSPAExtractTaskRunId = "MSPAExtractTaskRunId";
			public const string MSPAHoldingTaskRunId = "MSPAHoldingTaskRunId";
			public const string MSPATradeTaskRunId = "MSPATradeTaskRunId";
			public const string MSPASecurityTaskRunId = "MSPASecurityTaskRunId";
		}

		public static readonly MSPAFileDataModel Empty = new MSPAFileDataModel();

	}
}
