using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class FoodTypeDataModel : StandardModel
    {

        public class DataColumns  
        {
            public const string FoodTypeId = "FoodTypeId";
        }

        public static readonly FoodTypeDataModel Empty = new FoodTypeDataModel();

		[PrimaryKey]
        public int? FoodTypeId { get; set; }

    }
}
