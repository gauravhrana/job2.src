using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class HolidayDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string HolidayId = "HolidayId";
		}

		public static readonly HolidayDataModel Empty = new HolidayDataModel();

		public int? HolidayId { get; set; }

	}
}
