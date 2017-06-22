using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class HolidayDataModel : StandardModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? HolidayId { get; set; }

	}
}
