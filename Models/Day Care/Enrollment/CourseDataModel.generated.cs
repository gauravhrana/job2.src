using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class CourseDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string CourseId = "CourseId";
			public const string Name = "Name";
			public const string Description = "Description";
			public const string Duration = "Duration";
			public const string Fees = "Fees";
			public const string SortOrder = "SortOrder";
		}

		public static readonly CourseDataModel Empty = new CourseDataModel();

	}
}
