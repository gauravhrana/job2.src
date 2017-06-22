using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class MealDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string MealId = "MealId";

		}

		public static readonly MealDataModel Empty = new MealDataModel();

		[PrimaryKey]
		public int? MealId { get; set; }
	}
}
