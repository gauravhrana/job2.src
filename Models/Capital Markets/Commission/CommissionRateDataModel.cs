using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class CommissionRateDataModel : BaseModel
    {

        [PrimaryKey, IncludeInSearch] 
        public int? CommissionRateId { get; set; }

        public decimal? ClearingRate { get; set; }
        public decimal? ExecutionRate { get; set; }    

        [ForeignKey("Broker"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? BrokerId { get; set; }

        [ForeignKeyName("Broker", "BrokerId", "BrokerId", "Name"), OnlyProperty]
        public string Broker { get; set; }

        [ForeignKey("Exchange"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? ExchangeId { get; set; }

        [ForeignKeyName("Exchange", "ExchangeId", "ExchangeId", "Name"), OnlyProperty]
        public string Exchange { get; set; }

    }
}
