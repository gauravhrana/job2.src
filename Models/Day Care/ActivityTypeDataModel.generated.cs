using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class ActivityTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ActivityTypeId = "ActivityTypeId";
		}

		public static readonly ActivityTypeDataModel Empty = new ActivityTypeDataModel();

	}
}
