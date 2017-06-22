using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class SleepDataModel : StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string SleepId = "SleepId";

		}

		public static readonly SleepDataModel Empty = new SleepDataModel();

		[PrimaryKey]
		public int? SleepId { get; set; }
	}
}
