using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class SecurityClassDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string SecurityClassId = "SecurityClassId";
		}

		public static readonly SecurityClassDataModel Empty = new SecurityClassDataModel();

		[PrimaryKey, IncludeInSearch]
		public int? SecurityClassId { get; set; }

	}
}
