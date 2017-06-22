using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class DepartmentDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string DepartmentId = "DepartmentId";
		}

		public static readonly DepartmentDataModel Empty = new DepartmentDataModel();
		[PrimaryKey]
		public int? DepartmentId { get; set; }

	}
}
