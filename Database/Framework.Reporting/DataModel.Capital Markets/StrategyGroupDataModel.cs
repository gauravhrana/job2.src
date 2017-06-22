using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using Newtonsoft.Json;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	
	public class StrategyGroupDataModel : BaseModel
	{
		public class DataColumns 
		{ 
			public const string StrategyGroupId			= "StrategyGroupId";
			public const string ClassificationId		= "ClassificationId ";
			public const string PortfolioId				= "PortfolioId ";
			public const string ParentStrategyGroupId	= "ParentStrategyGroupId ";
			public const string DefaultUSecurityId		= "DefaultUSecurityId ";
			public const string ActiveYN				= "ActiveYN ";
			public const string OpenDateId				= "OpenDateId ";
			public const string CloseDateId				= "CloseDateId ";
			public const string ClosedYN				= "ClosedYN ";
			public const string ThemeId					= "ThemeId";
			public const string StrategyGroupCode		= "StrategyGroupCode  ";
            public const string StrategyGroupDesc       = "StrategyGroupDesc ";

            public const string FundId                  = "FundId";
            public const string Fund                    = "Fund";		
			
		}
		 
		public static readonly StrategyGroupDataModel Empty = new StrategyGroupDataModel();

		[PrimaryKey, IncludeInSearch]
        public int? StrategyGroupId { get; set; }

        [ForeignKey("Fund"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? FundId { get; set; }

        [ForeignKeyName("Fund", "FundId", "FundId", "Name"), OnlyProperty]
        public string Fund { get; set; }

        [JsonConverter(typeof(NullableIntConverter))]
		public int? ClassificationId { get; set; }
		public int? PortfolioId { get; set; }
		public int? ParentStrategyGroupId { get; set; }
		public int? DefaultUSecurityId { get; set; }
		public int? ActiveYN { get; set; }
		public int? OpenDateId { get; set; }
		public int? CloseDateId { get; set; }
		public int? ClosedYN { get; set; }
		public int? ThemeId { get; set; }

		public string StrategyGroupCode { get; set; }
		public string StrategyGroupDesc { get; set; }

	}
}
