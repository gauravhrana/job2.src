using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class CreditContractDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string CreditContractId = "CreditContractId";
		}

		public static readonly CreditContractDataModel Empty = new CreditContractDataModel();

	}
}
