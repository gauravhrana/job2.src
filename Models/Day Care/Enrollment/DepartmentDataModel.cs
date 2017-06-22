using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class DepartmentDataModel : StandardModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? DepartmentId { get; set; }

	}
}
