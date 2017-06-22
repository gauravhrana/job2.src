using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class CreditDealDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string CreditDealId = "CreditDealId";
		}

		public static readonly CreditDealDataModel Empty = new CreditDealDataModel();

	}
}
