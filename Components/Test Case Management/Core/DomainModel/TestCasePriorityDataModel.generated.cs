using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TestCaseManagement
{

	[Serializable]
	public partial class TestCasePriorityDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string TestCasePriorityId = "TestCasePriorityId";
		}

		public static readonly TestCasePriorityDataModel Empty = new TestCasePriorityDataModel();

		public int? TestCasePriorityId { get; set; }

	}
}
