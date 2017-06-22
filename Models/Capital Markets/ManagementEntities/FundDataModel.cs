using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class FundDataModel : StandardModel
	{

		[PrimaryKey, IncludeInSearch]
		public int? FundId { get; set; }

		[ForeignKey("ManagementFirm"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? ManagementFirmId { get; set; }

		[ForeignKeyName("ManagementFirm", "ManagementFirmId", "ManagementFirmId", "Name"), OnlyProperty]
		public string ManagementFirm { get; set; }


	}
}
