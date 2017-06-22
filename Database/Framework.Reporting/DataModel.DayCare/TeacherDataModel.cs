using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class TeacherDataModel : StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string TeacherId = "TeacherId";

		}

		public static readonly TeacherDataModel Empty = new TeacherDataModel();

		[PrimaryKey]
		public int? TeacherId { get; set; }
	}
}
