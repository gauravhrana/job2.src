using Framework.Components.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace DataModel.ReferenceData
{
    public partial class TimeZoneXCountryDataModel : BaseModel
    {

        [PrimaryKey, IncludeInSearch]
        public int? TimeZoneXCountryId { get; set; }

        [ForeignKey("TimeZone"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? TimeZoneId { get; set; }

        [ForeignKey("Country"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? CountryId { get; set; }

        [ForeignKeyName("TimeZone", "TimeZoneId", "TimeZoneId", "Name"), OnlyProperty]
        public string TimeZone { get; set; }

        [ForeignKeyName("Country", "CountryId", "CountryId", "Name"), OnlyProperty]
        public string Country { get; set; }
    }
}
