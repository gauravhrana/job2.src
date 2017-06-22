using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class NeedItemDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string NeedItemId = "NeedItemId";
		}

		public static readonly NeedItemDataModel Empty = new NeedItemDataModel();

	}
}
