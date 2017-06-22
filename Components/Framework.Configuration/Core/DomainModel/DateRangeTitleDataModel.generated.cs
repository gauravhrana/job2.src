using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{

	[Serializable]
	public partial class DateRangeTitleDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string DateRangeTitleId = "DateRangeTitleId";
		}

		public static readonly DateRangeTitleDataModel Empty = new DateRangeTitleDataModel();

		public int? DateRangeTitleId { get; set; }

	}
}
