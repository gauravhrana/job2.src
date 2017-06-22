using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class CalendarDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string CalendarId = "CalendarId";
		}

		public static readonly CalendarDataModel Empty = new CalendarDataModel();

		public int? CalendarId { get; set; }

	}
}
