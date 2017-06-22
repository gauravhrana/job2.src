using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class LockdownPoolsDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string LockdownPoolsId = "LockdownPoolsId";
		}

		public static readonly LockdownPoolsDataModel Empty = new LockdownPoolsDataModel();
		[PrimaryKey]
		public int? LockdownPoolsId { get; set; }

	}
}
