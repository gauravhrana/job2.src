using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess; 

namespace DataModel.CapitalMarkets
{

	public partial class TestingDataModel : TestDataModel
	{

		public class DataColumns : TestColumns
		{
			public const string TestingId = "TestingId";
		}

		public static readonly TestingDataModel Empty = new TestingDataModel();

	}
}
