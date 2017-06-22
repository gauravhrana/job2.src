using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class MealDetailDataModel : StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string MealDetailId = "MealDetailId";

		}

		public static readonly MealDetailDataModel Empty = new MealDetailDataModel();

		[PrimaryKey]
		public int? MealDetailId { get; set; }
	}
}
