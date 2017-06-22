using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class TrainStationDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string TrainStationId = "TrainStationId";

		}

		public static readonly TrainStationDataModel Empty = new TrainStationDataModel();

		[PrimaryKey]
		public int? TrainStationId { get; set; }

	}
}
