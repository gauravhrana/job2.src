using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class CityDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CityId = "CityId";

		}

		public static readonly CityDataModel Empty = new CityDataModel();

		[PrimaryKey]
		public int? CityId { get; set; }

	}
}
