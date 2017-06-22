using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class MealTypeDataModel : StandardModel
    {

        public class DataColumns  
        {
            public const string MealTypeId = "MealTypeId";
        }

        public static readonly MealTypeDataModel Empty = new MealTypeDataModel();

		[PrimaryKey]
        public int? MealTypeId { get; set; }

    }
}
