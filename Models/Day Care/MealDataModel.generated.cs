using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class MealDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string MealId = "MealId";
		}

		public static readonly MealDataModel Empty = new MealDataModel();

	}
}
