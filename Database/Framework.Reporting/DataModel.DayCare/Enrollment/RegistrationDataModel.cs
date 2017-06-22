using Framework.Components.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DayCare
{
	public class RegistrationDataModel : BaseModel
	{

		public class DataColumns
		{
			public const string RegistrationId = "RegistrationId";

			public const string CourseId = "CourseId";
			public const string StudentId = "StudentId";

			public const string EnrollmentDate = "EnrollmentDate";
			
		}

		public static readonly RegistrationDataModel Empty = new RegistrationDataModel();

		[PrimaryKey, IncludeInSearch]
		public int? RegistrationId { get; set; }

		[ForeignKey("Course"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
		public int? CourseId { get; set; }

		[ForeignKey("Student"), IncludeInSearch, JsonConverter(typeof(NullableIntConverter))]
		public int? StudentId { get; set; }
		
		public DateTime? EnrollmentDate { get; set; }
		
	}
}
