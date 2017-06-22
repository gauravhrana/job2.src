using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace TestCaseManagement.Components.BusinessLayer.DomainModel.TCM
{
	public class TestCase : StandardModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TestCaseId = "TestCaseId";
		}

		public int? TestCaseId { get; set; }

		public string ToURLQuery()
		{
			return string.Empty; //"TestCaseId=" + TestCaseId
		}
	}
}
