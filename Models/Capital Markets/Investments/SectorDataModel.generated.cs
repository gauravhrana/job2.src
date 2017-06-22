using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class SectorDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string SectorId = "SectorId";
			public const string Code = "Code";
		}

		public static readonly SectorDataModel Empty = new SectorDataModel();

	}
}
