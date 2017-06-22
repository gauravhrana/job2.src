using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class AirportDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AirportId = "AirportId";

		}

		public static readonly AirportDataModel Empty = new AirportDataModel();

		[PrimaryKey]
		public int? AirportId { get; set; }

	}
}
