using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class AssetBackedDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AssetBackedId = "AssetBackedId";
		}

		public static readonly AssetBackedDataModel Empty = new AssetBackedDataModel();

	}
}
