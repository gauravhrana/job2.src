using Framework.Components.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.CapitalMarkets
{
    public class PortfolioXCustodianAccountDataModel : BaseModel
    {
        public class DataColumns
        {
            public const string PortfolioXCustodianAccountId = "PortfolioXCustodianAccountId";

            public const string CustodianAccountId = "CustodianAccountId";
            public const string PortfolioId = "PortfolioId";

            public const string CustodianAccount = "CustodianAccount";
            public const string Portfolio = "Portfolio";

        }

        public static readonly PortfolioXCustodianAccountDataModel Empty = new PortfolioXCustodianAccountDataModel();

        [PrimaryKey, IncludeInSearch]
        public int? PortfolioXCustodianAccountId { get; set; }

        [ForeignKey("CustodianAccount"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? CustodianAccountId { get; set; }

        [ForeignKey("Portfolio"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? PortfolioId { get; set; }

        [ForeignKeyName("CustodianAccount", "CustodianAccountId", "CustodianAccountId", "Name"), OnlyProperty]
        public string CustodianAccount { get; set; }
        [ForeignKeyName("Portfolio", "PortfolioId", "PortfolioId", "Name"), OnlyProperty]
        public string Portfolio { get; set; }

    }
}
