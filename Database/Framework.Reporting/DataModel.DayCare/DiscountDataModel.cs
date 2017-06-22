using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class DiscountDataModel : StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string DiscountId = "DiscountId";

		}

		public static readonly DiscountDataModel Empty = new DiscountDataModel();

		[PrimaryKey]
		public int? DiscountId { get; set; }
	}
}
