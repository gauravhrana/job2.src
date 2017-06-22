using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using Newtonsoft.Json;
using Framework.Components.DataAccess;

namespace DataModel.Framework.Core
{
    public partial class TimeZoneXCountryDataModel : BaseModel
    {
        [PrimaryKey, IncludeInSearch]
        public int? TimeZoneXCountryId { get; set; }

        [ForeignKey("TimeZone"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? TimeZoneId { get; set; }

        [ForeignKeyName("TimeZone", "TimeZoneId", "TimeZoneId", "Name"), OnlyProperty]
        public string TimeZone { get; set; }        

        [ForeignKey("Country"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? CountryId { get; set; }

        [ForeignKeyName("Country", "CountryId", "CountryId", "Name"), OnlyProperty]
        public string Country { get; set; }

    }
}
