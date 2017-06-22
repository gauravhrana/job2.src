using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class CalendarDataModel : StandardModel
	{
        [PrimaryKey, IncludeInSearch]
		public int? CalendarId { get; set; }

	}
}
