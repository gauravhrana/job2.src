using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Components.DataAccess;
using Newtonsoft.Json;

namespace DataModel.DayCare
{
	public partial class CourseInstanceDataModel : BaseModel
	{
		/* https://en.wikipedia.org/wiki/Class_(education) */

		[PrimaryKey, IncludeInSearch]
		public int? CourseInstanceId { get; set; }

		[IncludeInSearch]
		public string Name { get; set; }

		[ForeignKey("Course"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? CourseId { get; set; }

		[ForeignKeyName("Course", "CourseId", "CourseId", "Name"), OnlyProperty]
		public string Course { get; set; }

		[ForeignKey("Department"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? DepartmentId { get; set; }

		[ForeignKeyName("Department", "DepartmentId", "DepartmentId", "Name"), OnlyProperty]
		public string Department { get; set; }

		[ForeignKey("Teacher"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? TeacherId { get; set; }

		[ForeignKeyName("Teacher", "TeacherId", "TeacherId", "Name"), OnlyProperty]
		public string Teacher { get; set; }

		[DateTimeType(DateTimeTypeEnum.Time), JsonConverter(typeof(NullableDateConverter))]
		public DateTime? StartTime { get; set; }

		[DateTimeType(DateTimeTypeEnum.Time), JsonConverter(typeof(NullableDateConverter))]
		public DateTime? EndTime { get; set; }

	}
}
