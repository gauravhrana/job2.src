using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class EventTypeDataModel : StandardModel
    {

        public class DataColumns  
        {
            public const string EventTypeId = "EventTypeId";
        }

        public static readonly EventTypeDataModel Empty = new EventTypeDataModel();

		[PrimaryKey]
        public int? EventTypeId { get; set; }

    }
}
