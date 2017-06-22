using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess; 

namespace DataModel.CapitalMarkets
{

	public partial class EntityDataModel : TestDataModel
	{

		public class DataColumns : TestColumns
		{
			public const string EntityId = "EntityId";
		}

		public static readonly EntityDataModel Empty = new EntityDataModel();

	}
}
