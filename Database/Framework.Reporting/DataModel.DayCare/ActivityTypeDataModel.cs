using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class ActivityTypeDataModel : StandardModel
    {

        public class DataColumns  
        {
            public const string ActivityTypeId = "ActivityTypeId";
        }

        public static readonly ActivityTypeDataModel Empty = new ActivityTypeDataModel();

		[PrimaryKey]
        public int? ActivityTypeId { get; set; }

    }
}
