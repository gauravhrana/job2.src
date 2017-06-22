using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class HolidayDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string HolidayId = "HolidayId";
		}

		public static readonly HolidayDataModel Empty = new HolidayDataModel();

	}
}
