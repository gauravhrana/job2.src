using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class TuitionDataModel : StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string TuitionId = "TuitionId";

		}

		public static readonly TuitionDataModel Empty = new TuitionDataModel();

		[PrimaryKey]
		public int? TuitionId { get; set; }
	}
}
