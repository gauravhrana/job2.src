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
    public partial class CountryXReligionDataModel : BaseModel
    {

        [PrimaryKey, IncludeInSearch]
        public int? CountryXReligionId { get; set; }        

        [ForeignKey("Country"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? CountryId { get; set; }

        [ForeignKey("Religion"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
        public int? ReligionId { get; set; }        

        [ForeignKeyName("Country", "CountryId", "CountryId", "Name"), OnlyProperty]
        public string Country { get; set; }

        [ForeignKeyName("Religion", "ReligionId", "ReligionId", "Name"), OnlyProperty]
        public string Religion { get; set; }
    }
}
