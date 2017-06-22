using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class CalendarDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string CalendarId = "CalendarId";
		}

		public static readonly CalendarDataModel Empty = new CalendarDataModel();

	}
}
