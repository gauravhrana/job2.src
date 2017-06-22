using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class FundDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string FundId = "FundId";
			public const string ManagementFirmId = "ManagementFirmId";
			public const string ManagementFirm = "ManagementFirm";
		}

		public static readonly FundDataModel Empty = new FundDataModel();

	}
}
