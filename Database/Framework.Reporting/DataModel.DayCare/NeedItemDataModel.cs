using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class NeedItemDataModel : StandardModel
    {

        public class DataColumns 
        {
            public const string NeedItemId = "NeedItemId";
        }

        public static readonly NeedItemDataModel Empty = new NeedItemDataModel();

		[PrimaryKey]
        public int? NeedItemId { get; set; }

    }
}
