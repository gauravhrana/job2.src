using Framework.Components.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DayCare
{
	public partial class CourseDataModel : BaseModel
	{	
		[PrimaryKey, IncludeInSearch]  
		public int? CourseId { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		public decimal? Duration { get; set; }
		public decimal? Fees { get; set; }

		public int? SortOrder { get; set; }

	}
}
