using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference.DomainModel
{
	public class DateRangeTitle : StandardModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string DateRangeTitleId = "DateRangeTitleId";
		}

		public int? DateRangeTitleId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty;
		}
	}
}
