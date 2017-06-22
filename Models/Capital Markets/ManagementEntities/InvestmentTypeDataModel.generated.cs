using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class InvestmentTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string InvestmentTypeId = "InvestmentTypeId";
		}

		public static readonly InvestmentTypeDataModel Empty = new InvestmentTypeDataModel();

	}
}
