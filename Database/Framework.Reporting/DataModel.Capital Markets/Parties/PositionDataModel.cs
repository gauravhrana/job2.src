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
    [Table("Position")]
    public class PositionDataModel : BaseModel
    {
        public class DataColumns
        {
            public const string PositionId                       = "PositionId";
            public const string CustodianCode                    = "CustodianCode";
            public const string StrategyCode                     = "StrategyCode";
            public const string AccountCode                      = "AccountCode";
            public const string InvestmentCode                   = "InvestmentCode";
            public const string Quantity                         = "Quantity";            
            public const string CostBasis                        = "CostBasis";
            public const string MarketValue                      = "MarketValue";
            public const string StartMarketValue                 = "StartMarketValue";
            public const string DeltaAdjustedExposure            = "DeltaAdjustedExposure";
            public const string StartDeltaAdjustedExposure       = "StartDeltaAdjustedExposure";
            public const string RealizedPnL                      = "RealizedPnL";
            public const string UnrealizedPnL                    = "UnrealizedPnL";
            public const string PeriodDate                       = "PeriodDate";
            public const string Mark                             = "Mark";
            
        }

        public static readonly PositionDataModel Empty = new PositionDataModel();

        [PrimaryKey, IncludeInSearch]        
        public int? PositionId                           { get; set; }
        
        public string CustodianCode                 { get; set; }
        public string StrategyCode                  { get; set; }        
        public string AccountCode                   { get; set; }
        public string InvestmentCode                { get; set; }
        public decimal? Quantity                    { get; set; }
        public decimal? CostBasis                   { get; set; }
        public decimal? MarketValue                 { get; set; }
        public decimal? StartMarketValue            { get; set; }
        public decimal? DeltaAdjustedExposure       { get; set; }
        public decimal? StartDeltaAdjustedExposure  { get; set; }
        public decimal? RealizedPnL                 { get; set; }
        public decimal? UnrealizedPnL               { get; set; }
        public DateTime? PeriodDate                 { get; set; }
        public int Mark                             { get; set; }


    }
}
