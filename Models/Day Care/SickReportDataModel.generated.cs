using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class SickReportDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string SickReportId = "SickReportId";
		}

		public static readonly SickReportDataModel Empty = new SickReportDataModel();

	}
}
