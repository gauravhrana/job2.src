using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class AirportDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string AirportId = "AirportId";
		}

		public static readonly AirportDataModel Empty = new AirportDataModel();

		public int? AirportId { get; set; }

	}
}
