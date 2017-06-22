using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class AssetBackedDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AssetBackedId = "AssetBackedId";
		}

		public static readonly AssetBackedDataModel Empty = new AssetBackedDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? AssetBackedId { get; set; }

	}
}
