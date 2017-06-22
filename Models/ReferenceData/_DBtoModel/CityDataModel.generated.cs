using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class CityDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string CityId = "CityId";
		}

		public static readonly CityDataModel Empty = new CityDataModel();

		public int? CityId { get; set; }

	}
}
