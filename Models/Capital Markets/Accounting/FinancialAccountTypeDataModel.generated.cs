using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class FinancialAccountTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string FinancialAccountTypeId = "FinancialAccountTypeId";
		}

		public static readonly FinancialAccountTypeDataModel Empty = new FinancialAccountTypeDataModel();

	}
}
