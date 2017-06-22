using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class ActivitySubTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ActivitySubTypeId = "ActivitySubTypeId";
		}

		public static readonly ActivitySubTypeDataModel Empty = new ActivitySubTypeDataModel();

	}
}
