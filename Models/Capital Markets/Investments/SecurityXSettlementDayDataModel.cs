using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class SecurityXSettlementDayDataModel : BaseModel
    {
        
        [PrimaryKey, IncludeInSearch]
        public int? SecurityXSettlementDayId { get; set; }

        public int? SettlementDay { get; set; }

        [ForeignKey("Security"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? SecurityId { get; set; }

        [ForeignKeyName("Security", "SecurityId", "SecurityId", "Name"), OnlyProperty]
        public string Security { get; set; }
    }
}
