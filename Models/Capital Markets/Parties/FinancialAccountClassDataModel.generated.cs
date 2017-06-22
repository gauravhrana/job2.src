using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class FinancialAccountClassDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string FinancialAccountClassId = "FinancialAccountClassId";
		}

		public static readonly FinancialAccountClassDataModel Empty = new FinancialAccountClassDataModel();

	}
}
