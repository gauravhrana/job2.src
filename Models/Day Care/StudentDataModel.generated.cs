using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class StudentDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string StudentId = "StudentId";
			public const string Name = "Name";
			public const string Description = "Description";
			public const string SortOrder = "SortOrder";
		}

		public static readonly StudentDataModel Empty = new StudentDataModel();

	}
}
