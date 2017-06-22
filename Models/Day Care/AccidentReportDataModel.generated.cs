using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class AccidentReportDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AccidentReportId = "AccidentReportId";
		}

		public static readonly AccidentReportDataModel Empty = new AccidentReportDataModel();

	}
}
