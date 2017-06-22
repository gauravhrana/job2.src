using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class MediumOfExchangeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string MediumOfExchangeId = "MediumOfExchangeId";
			public const string ExtendedDescription = "ExtendedDescription";
		}

		public static readonly MediumOfExchangeDataModel Empty = new MediumOfExchangeDataModel();

	}
}
