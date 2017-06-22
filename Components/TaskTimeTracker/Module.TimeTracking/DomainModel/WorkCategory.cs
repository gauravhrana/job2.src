using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.Module.TimeTracking.DomainModel
{ 
    [Serializable]
	public class WorkCategoryDataModel: StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string Category = "Category";
			public const string Week = "Week";
			public const string Value = "Value";
		}

		public static readonly WorkCategoryDataModel Empty = new WorkCategoryDataModel();

		public string Category { get; set; }
		public int Week { get; set; }
		public int Value { get; set; }

	}
}
	

