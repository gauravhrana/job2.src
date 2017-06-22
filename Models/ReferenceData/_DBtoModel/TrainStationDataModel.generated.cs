using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class TrainStationDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string TrainStationId = "TrainStationId";
		}

		public static readonly TrainStationDataModel Empty = new TrainStationDataModel();

		public int? TrainStationId { get; set; }

	}
}
