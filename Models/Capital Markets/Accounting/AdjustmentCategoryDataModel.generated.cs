using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class AdjustmentCategoryDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AdjustmentCategoryId = "AdjustmentCategoryId";
			public const string Code = "Code";
		}

		public static readonly AdjustmentCategoryDataModel Empty = new AdjustmentCategoryDataModel();

	}
}
