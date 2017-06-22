using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess; 

namespace DataModel.CapitalMarkets
{

	public partial class EntityTestDataModel : TestDataModel
	{

		public class DataColumns : TestColumns
		{
			public const string EntityTestId = "EntityTestId";
		}

		public static readonly EntityTestDataModel Empty = new EntityTestDataModel();

	}
}
