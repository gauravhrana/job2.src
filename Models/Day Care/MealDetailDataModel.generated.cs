using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class MealDetailDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string MealDetailId = "MealDetailId";
		}

		public static readonly MealDetailDataModel Empty = new MealDetailDataModel();

	}
}
