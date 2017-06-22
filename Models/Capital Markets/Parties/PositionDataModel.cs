using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class PositionDataModel : BaseModel
    {
        [PrimaryKey, IncludeInSearch]        
        public int? PositionId                      { get; set; }
        public string InvestmentCode                { get; set; }
        public DateTime? PeriodDate                 { get; set; }
        public string CustodianCode                 { get; set; }
        public string StrategyCode                  { get; set; }        
        public string AccountCode                   { get; set; }        
        public decimal? Quantity                    { get; set; }
        public decimal? CostBasis                   { get; set; }
        public decimal? MarketValue                 { get; set; }
        public decimal? StartMarketValue            { get; set; }
        public decimal? DeltaAdjustedExposure       { get; set; }
        public decimal? StartDeltaAdjustedExposure  { get; set; }
        public decimal? RealizedPnL                 { get; set; }
        public decimal? UnrealizedPnL               { get; set; }        
        public int Mark                             { get; set; }


    }
}
