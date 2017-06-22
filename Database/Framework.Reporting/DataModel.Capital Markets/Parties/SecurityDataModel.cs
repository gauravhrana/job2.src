using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class SecurityDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string SecurityId = "SecurityId";
		}

		public static readonly SecurityDataModel Empty = new SecurityDataModel();
		[PrimaryKey]
		public int? SecurityId { get; set; }

	}
}
