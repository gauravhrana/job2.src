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
    public partial class HolidayXCountryDataModel : BaseModel
    {

        [PrimaryKey, IncludeInSearch]
        public int? HolidayXCountryId { get; set; }

        [ForeignKey("Holiday"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? HolidayId { get; set; }

        [ForeignKey("Country"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? CountryId { get; set; }

        [ForeignKeyName("Holiday", "HolidayId", "HolidayId", "Name"), OnlyProperty]
        public string Holiday { get; set; }

        [ForeignKeyName("Country", "CountryId", "CountryId", "Name"), OnlyProperty]
        public string Country { get; set; }
    }
}
