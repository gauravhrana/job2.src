using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class SickReportDataModel : StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string SickReportId = "SickReportId";

		}

		public static readonly SickReportDataModel Empty = new SickReportDataModel();

		[PrimaryKey]
		public int? SickReportId { get; set; }
	}
}
