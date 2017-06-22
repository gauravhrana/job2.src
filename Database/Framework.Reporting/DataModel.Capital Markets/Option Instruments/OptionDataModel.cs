using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class OptionDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string OptionId = "OptionId";
		}

		public static readonly OptionDataModel Empty = new OptionDataModel();
		[PrimaryKey]
		public int? OptionId { get; set; }

	}
}
