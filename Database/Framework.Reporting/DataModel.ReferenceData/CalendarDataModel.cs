using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class CalendarDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CalendarId = "CalendarId";

		}

		public static readonly CalendarDataModel Empty = new CalendarDataModel();

		[PrimaryKey]
		public int? CalendarId { get; set; }

	}
}
