using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class RegistrationDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string RegistrationId = "RegistrationId";
			public const string CourseId = "CourseId";
			public const string Course = "Course";
			public const string StudentId = "StudentId";
			public const string Student = "Student";
			public const string EnrollmentDate = "EnrollmentDate";
			public const string FromSearchEnrollmentDate = "FromSearchEnrollmentDate";
			public const string ToSearchEnrollmentDate = "ToSearchEnrollmentDate";
		}

		public static readonly RegistrationDataModel Empty = new RegistrationDataModel();

	}
}
