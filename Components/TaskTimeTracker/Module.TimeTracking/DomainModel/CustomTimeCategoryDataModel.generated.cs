using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.TimeTracking
{

	[Serializable]
	public partial class CustomTimeCategoryDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string CustomTimeCategoryId = "CustomTimeCategoryId";
		}

		public static readonly CustomTimeCategoryDataModel Empty = new CustomTimeCategoryDataModel();

		public int? CustomTimeCategoryId { get; set; }

	}
}
