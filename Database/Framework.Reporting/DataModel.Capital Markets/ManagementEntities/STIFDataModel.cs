using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class STIFDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string STIFId = "STIFId";
		}

		public static readonly STIFDataModel Empty = new STIFDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? STIFId { get; set; }

	}
}
