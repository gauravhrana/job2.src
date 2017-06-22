using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class CountryDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CountryId = "CountryId";

		}

		public static readonly CountryDataModel Empty = new CountryDataModel();

		[PrimaryKey]
		public int? CountryId { get; set; }

	}
}
