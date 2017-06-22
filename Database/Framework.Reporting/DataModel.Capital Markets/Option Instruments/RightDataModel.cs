using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class RightDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string RightId = "RightId";
		}

		public static readonly RightDataModel Empty = new RightDataModel();
		[PrimaryKey]
		public int? RightId { get; set; }

	}
}
