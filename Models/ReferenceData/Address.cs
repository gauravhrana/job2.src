using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using Newtonsoft.Json;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{
	public partial class AddressDataModel : BaseModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? AddressId			{ get; set; }
		

		[ForeignKey("City"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? CityId				{ get; set; }

		[ForeignKeyName("City", "CityId", "CityId", "Name"), OnlyProperty]
		public string City				{ get; set; }

		[ForeignKey("State"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? StateId				{ get; set; }

		[ForeignKeyName("State", "StateId", "StateId", "Name"), OnlyProperty]
		public string State				{ get; set; }

		[ForeignKey("Country"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? CountryId			{ get; set; }

		[ForeignKeyName("Country", "CountryId", "CountryId", "Name"), OnlyProperty]
		public string Country { get; set; }


		public string Address1			{ get; set; }
		public string Address2			{ get; set; }
		public string PostalCode		{ get; set; }
	}
}
