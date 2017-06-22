using Framework.Components.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DayCare
{
	public class CourseDataModel : BaseModel
	{

		public class DataColumns
		{
			public const string CourseId	= "CourseId";

			public const string Name		= "Name";
			public const string Description = "Description";
			public const string Duration	= "Duration";
			public const string Fees		= "Fees";			
			public const string SortOrder	= "SortOrder";
		}

		public static readonly CourseDataModel Empty = new CourseDataModel();

		[PrimaryKey, IncludeInSearch]
		public int? CourseId { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		public decimal? Duration { get; set; }
		public decimal? Fees { get; set; }

		public int? SortOrder { get; set; }
		

	}
}
