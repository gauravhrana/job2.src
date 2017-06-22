using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class ManagerDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string ManagerId = "ManagerId";
		}

		public static readonly ManagerDataModel Empty = new ManagerDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? ManagerId { get; set; }

	}
}
