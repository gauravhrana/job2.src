using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class SecurityTypeGroupDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string SecurityTypeGroupId = "SecurityTypeGroupId";
		}

		public static readonly SecurityTypeGroupDataModel Empty = new SecurityTypeGroupDataModel();
		[PrimaryKey]
		public int? SecurityTypeGroupId { get; set; }

	}
}
