using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace TestCaseManagement.Components.BusinessLayer.DomainModel.TCM
{
	public class TestSuite : StandardModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TestSuiteId = "TestSuiteId";
		}

		public int? TestSuiteId { get; set; }

		public string ToURLQuery()
		{
			return string.Empty; //"TestSuiteId=" + TestSuiteId
		}
	}
}
