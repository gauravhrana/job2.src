using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class ManagementFirmDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ManagementFirmId = "ManagementFirmId";
		}

		public static readonly ManagementFirmDataModel Empty = new ManagementFirmDataModel();

	}
}
