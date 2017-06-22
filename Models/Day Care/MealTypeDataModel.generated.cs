using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class MealTypeDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string MealTypeId = "MealTypeId";
		}

		public static readonly MealTypeDataModel Empty = new MealTypeDataModel();

	}
}
