using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class DiscountDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string DiscountId = "DiscountId";
		}

		public static readonly DiscountDataModel Empty = new DiscountDataModel();

	}
}
