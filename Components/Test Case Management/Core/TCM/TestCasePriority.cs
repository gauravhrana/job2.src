using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace TestCaseManagement.Components.BusinessLayer.DomainModel.TCM
{
	public class TestCasePriority : StandardModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TestCasePriorityId = "TestCasePriorityId";
		}

		public int? TestCasePriorityId { get; set; }

		public string ToURLQuery()
		{
			return string.Empty; //"TestCasePriorityId=" + TestCasePriorityId
		}
	}
}
