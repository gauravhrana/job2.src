using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class AssetTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AssetTypeId = "AssetTypeId";
		}

		public static readonly AssetTypeDataModel Empty = new AssetTypeDataModel();

	}
}
