using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class HolidayDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string HolidayId = "HolidayId";

		}

		public static readonly HolidayDataModel Empty = new HolidayDataModel();

		[PrimaryKey]
		public int? HolidayId { get; set; }

	}
}
