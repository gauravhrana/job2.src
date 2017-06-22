using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class SectorDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string SectorId	= "SectorId";
			public const string Code		= "Code";
		}

		public static readonly SectorDataModel Empty = new SectorDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? SectorId { get; set; }
		public string Code { get; set; }

	}
}
