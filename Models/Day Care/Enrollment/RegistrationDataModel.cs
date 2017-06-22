using Framework.Components.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DayCare
{
	public partial class RegistrationDataModel : BaseModel
	{
		
        [PrimaryKey, IncludeInSearch] 
		public int? RegistrationId { get; set; }
		
		[ForeignKey("Course"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? CourseId { get; set; }

		[ForeignKeyName("Course", "CourseId", "CourseId", "Name"), OnlyProperty]
		public string Course { get; set; }

		[ForeignKey("Student"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? StudentId { get; set; }

		[ForeignKeyName("Student", "StudentId", "StudentId", "Name"), OnlyProperty]
		public string Student { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter)), DateRange("FromSearchEnrollmentDate", "ToSearchEnrollmentDate")]
		public DateTime? EnrollmentDate { get; set; }

        [OnlyProperty, Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter))]
        public DateTime? FromSearchEnrollmentDate { get; set; }

        [OnlyProperty, Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter))]
        public DateTime? ToSearchEnrollmentDate { get; set; }
		
	}
}
