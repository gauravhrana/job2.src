using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class EventSubTypeDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string EventSubTypeId = "EventSubTypeId";
			public const string EventTypeId = "EventTypeId";
			public const string EventType = "EventType";
			public const string PersonId = "PersonId";
			public const string Person = "Person";
			public const string EventKey = "EventKey";
			public const string SortOrder = "SortOrder";
			public const string Value = "Value";
		}

		public static readonly EventSubTypeDataModel Empty = new EventSubTypeDataModel();

	}
}
