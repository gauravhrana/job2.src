using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class EventTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string EventTypeId = "EventTypeId";
		}

		public static readonly EventTypeDataModel Empty = new EventTypeDataModel();

	}
}
