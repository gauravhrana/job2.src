using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class SecurityTypeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string SecurityTypeId = "SecurityTypeId";
		}

		public static readonly SecurityTypeDataModel Empty = new SecurityTypeDataModel();
		[PrimaryKey]
		public int? SecurityTypeId { get; set; }

	}
}
