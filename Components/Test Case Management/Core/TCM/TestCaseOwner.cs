using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace TestCaseManagement.Components.BusinessLayer.DomainModel.TCM
{
	public class TestCaseOwner : StandardModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string TestCaseOwnerId = "TestCaseOwnerId";
		}

		public int? TestCaseOwnerId { get; set; }

		public string ToURLQuery()
		{
			return string.Empty; //"TestCaseOwnerId=" + TestCaseOwnerId
		}
	}
}
