using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class CashDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string CashId = "CashId";
		}

		public static readonly CashDataModel Empty = new CashDataModel();

	}
}
