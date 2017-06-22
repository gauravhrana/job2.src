using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class DepartmentDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string DepartmentId = "DepartmentId";
		}

		public static readonly DepartmentDataModel Empty = new DepartmentDataModel();

	}
}
