using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{
	public partial class ProvinceDataModel : StandardModel
	{
		[PrimaryKey, IncludeInSearch] 
		public int? ProvinceId { get; set; }

		[ForeignKey("Country"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? CountryId { get; set; }

		[ForeignKeyName("Country", "CountryId", "CountryId", "Name"), OnlyProperty]
		public string Country { get; set; }

		[ForeignKey("ProvinceType"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? ProvinceTypeId { get; set; }

		[ForeignKeyName("ProvinceType", "ProvinceTypeId", "ProvinceTypeId", "Name"), OnlyProperty]
		public string ProvinceType { get; set; }

	}
}
