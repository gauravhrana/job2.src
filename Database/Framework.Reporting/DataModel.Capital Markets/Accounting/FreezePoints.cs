using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class FreezePointsDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string FreezePointsId = "FreezePointsId";
		}

		public static readonly FreezePointsDataModel Empty = new FreezePointsDataModel();
		[PrimaryKey]
		public int? FreezePointsId { get; set; }

	}
}
