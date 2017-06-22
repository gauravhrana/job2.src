using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;


namespace DataModel.CapitalMarkets
{

	public partial class ForwardCashDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string ForwardCashId = "ForwardCashId";
		}

		public static readonly ForwardCashDataModel Empty = new ForwardCashDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? ForwardCashId { get; set; }

	}
}
