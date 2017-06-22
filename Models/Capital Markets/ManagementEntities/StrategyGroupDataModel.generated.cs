using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class StrategyGroupDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string StrategyGroupId = "StrategyGroupId";
			public const string FundId = "FundId";
			public const string Fund = "Fund";
			public const string ClassificationId = "ClassificationId";
			public const string PortfolioId = "PortfolioId";
			public const string ParentStrategyGroupId = "ParentStrategyGroupId";
			public const string DefaultUSecurityId = "DefaultUSecurityId";
			public const string ActiveYN = "ActiveYN";
			public const string OpenDateId = "OpenDateId";
			public const string CloseDateId = "CloseDateId";
			public const string ClosedYN = "ClosedYN";
			public const string ThemeId = "ThemeId";
			public const string StrategyGroupCode = "StrategyGroupCode";
			public const string StrategyGroupDesc = "StrategyGroupDesc";
		}

		public static readonly StrategyGroupDataModel Empty = new StrategyGroupDataModel();

	}
}
