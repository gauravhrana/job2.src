using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class TrainStationDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string TrainStationId = "TrainStationId";
			public const string CountryId = "CountryId";
			public const string Country = "Country";
		}

		public static readonly TrainStationDataModel Empty = new TrainStationDataModel();

	}
}
