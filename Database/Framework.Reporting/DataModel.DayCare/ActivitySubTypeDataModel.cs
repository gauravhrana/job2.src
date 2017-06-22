using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class ActivitySubTypeDataModel:StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string ActivitySubTypeId = "ActivitySubTypeId";

		}

		public static readonly ActivitySubTypeDataModel Empty = new ActivitySubTypeDataModel();

		[PrimaryKey]
		public int? ActivitySubTypeId { get; set; }
	}
}
