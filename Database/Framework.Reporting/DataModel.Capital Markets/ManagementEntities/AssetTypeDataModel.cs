using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class AssetTypeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AssetTypeId = "AssetTypeId";
		}

		public static readonly AssetTypeDataModel Empty = new AssetTypeDataModel();

		[PrimaryKey, IncludeInSearch]
		public int? AssetTypeId { get; set; }

	}
}

