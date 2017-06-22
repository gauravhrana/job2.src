using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;


namespace DataModel.CapitalMarkets
{

	public partial class BondDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string BondId = "BondId";
		}

		public static readonly BondDataModel Empty = new BondDataModel();
		[PrimaryKey,IncludeInSearch]
		public int? BondId { get; set; }

	}
}
