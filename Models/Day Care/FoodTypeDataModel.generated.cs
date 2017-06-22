using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class FoodTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string FoodTypeId = "FoodTypeId";
		}

		public static readonly FoodTypeDataModel Empty = new FoodTypeDataModel();

	}
}
