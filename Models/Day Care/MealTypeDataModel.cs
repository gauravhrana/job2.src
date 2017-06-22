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
		[PrimaryKey, IncludeInSearch]
        public int? MealTypeId { get; set; }

    }
}

