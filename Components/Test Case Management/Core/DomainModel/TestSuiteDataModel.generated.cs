using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TestCaseManagement
{

	[Serializable]
	public partial class TestSuiteDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string TestSuiteId = "TestSuiteId";
		}

		public static readonly TestSuiteDataModel Empty = new TestSuiteDataModel();

		public int? TestSuiteId { get; set; }

	}
}
