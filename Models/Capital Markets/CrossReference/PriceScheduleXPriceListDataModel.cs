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
	public partial class PriceScheduleXPriceListDataModel : BaseModel
    {
        
        [PrimaryKey, IncludeInSearch]
        public int? PriceScheduleXPriceListId { get; set; }

        [ForeignKey("PriceSchedule"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? PriceScheduleId { get; set; }

        [ForeignKey("PriceList"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? PriceListId { get; set; }

        [ForeignKeyName("PriceSchedule", "PriceScheduleId", "PriceScheduleId", "Name"), OnlyProperty]
        public string PriceSchedule { get; set; }
        [ForeignKeyName("PriceList", "PriceListId", "PriceListId", "Name"), OnlyProperty]
        public string PriceList { get; set; }
    }
}
