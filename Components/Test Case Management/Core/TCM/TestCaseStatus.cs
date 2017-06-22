using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace TestCaseManagement.Components.BusinessLayer.DomainModel.TCM
{
	public class TestCaseStatus : StandardModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TestCaseStatusId = "TestCaseStatusId";
		}

		public int? TestCaseStatusId { get; set; }

		public string ToURLQuery()
		{
			return string.Empty; //"TestCaseStatusId=" + TestCaseStatusId
		}
	}
}
