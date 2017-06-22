using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class TeacherDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string TeacherId = "TeacherId";
			public const string Name = "Name";
			public const string Description = "Description";
			public const string SortOrder = "SortOrder";
		}

		public static readonly TeacherDataModel Empty = new TeacherDataModel();

	}
}
