using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class FiscalCalenderDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string FiscalCalenderId = "FiscalCalenderId";
		}

		public static readonly FiscalCalenderDataModel Empty = new FiscalCalenderDataModel();

	}
}
