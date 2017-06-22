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
    public partial class FundXPortfolioDataModel : BaseModel
    {
        
        [PrimaryKey, IncludeInSearch]
        public int? FundXPortfolioId { get; set; }

        [ForeignKey("Fund"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? FundId { get; set; }

        [ForeignKey("Portfolio"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? PortfolioId { get; set; }

        [ForeignKeyName("Fund", "FundId", "FundId", "Name"), OnlyProperty]
        public string Fund { get; set; }
        [ForeignKeyName("Portfolio", "PortfolioId", "PortfolioId", "Name"), OnlyProperty]
        public string Portfolio { get; set; }
    }
}
