using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class StudentDataModel : StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string StudentId = "StudentId";

		}

		public static readonly StudentDataModel Empty = new StudentDataModel();

		[PrimaryKey]
		public int? StudentId { get; set; }
	}
}
