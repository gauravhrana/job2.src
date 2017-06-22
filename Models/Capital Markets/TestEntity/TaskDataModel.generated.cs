using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess; 

namespace DataModel.CapitalMarkets
{

	public partial class TaskDataModel : TestDataModel
	{

		public class DataColumns : TestColumns
		{
			public const string TaskId = "TaskId";
		}

		public static readonly TaskDataModel Empty = new TaskDataModel();

	}
}
