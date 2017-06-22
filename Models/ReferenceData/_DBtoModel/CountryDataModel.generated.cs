using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class CountryDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string CountryId = "CountryId";
		}

		public static readonly CountryDataModel Empty = new CountryDataModel();

		public int? CountryId { get; set; }

	}
}
