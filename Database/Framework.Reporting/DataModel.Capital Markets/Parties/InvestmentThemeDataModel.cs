using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;


namespace DataModel.CapitalMarkets
{
	public partial class InvestmentThemeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string InvestmentThemeId = "InvestmentThemeId";
			
		}

		public static readonly InvestmentThemeDataModel Empty = new InvestmentThemeDataModel();
        [PrimaryKey, IncludeInSearch]
		public int? InvestmentThemeId { get; set; }
		
	}


}