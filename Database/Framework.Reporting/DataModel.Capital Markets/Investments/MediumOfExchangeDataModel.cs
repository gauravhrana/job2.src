using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class MediumOfExchangeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string MediumOfExchangeId = "MediumOfExchangeId";
		}

		public static readonly MediumOfExchangeDataModel Empty = new MediumOfExchangeDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? MediumOfExchangeId { get; set; }

	}
}
