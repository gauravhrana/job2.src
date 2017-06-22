using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class AccidentReportDataModel : StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string AccidentReportId = "AccidentReportId";

		}

		public static readonly AccidentReportDataModel Empty = new AccidentReportDataModel();

		[PrimaryKey]
		public int? AccidentReportId { get; set; }
	}
}
