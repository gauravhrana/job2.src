using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace TestCaseManagement.Components.BusinessLayer.DomainModel.TCM
{
	public class TestRun : StandardModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TestRunId = "TestRunId";
		}

		public int? TestRunId { get; set; }

		public string ToURLQuery()
		{
			return string.Empty; //"TestRunId=" + TestRunId
		}
	}
}
