using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class CourseInstanceDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string CourseInstanceId = "CourseInstanceId";
			public const string Name = "Name";
			public const string CourseId = "CourseId";
			public const string Course = "Course";
			public const string DepartmentId = "DepartmentId";
			public const string Department = "Department";
			public const string TeacherId = "TeacherId";
			public const string Teacher = "Teacher";
			public const string StartTime = "StartTime";
			public const string EndTime = "EndTime";
		}

		public static readonly CourseInstanceDataModel Empty = new CourseInstanceDataModel();

	}
}
