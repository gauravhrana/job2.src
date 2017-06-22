using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class TrainStationDataModel : StandardModel
	{		
        [PrimaryKey, IncludeInSearch]
        public int? TrainStationId { get; set; }

        [ForeignKey("Country"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? CountryId { get; set; }

        [ForeignKeyName("Country", "CountryId", "CountryId", "Name"), OnlyProperty]
        public string Country { get; set; }


	}
}
