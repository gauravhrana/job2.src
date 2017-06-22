using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class InvestmentThemeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string InvestmentThemeId = "InvestmentThemeId";
		}

		public static readonly InvestmentThemeDataModel Empty = new InvestmentThemeDataModel();

	}
}
