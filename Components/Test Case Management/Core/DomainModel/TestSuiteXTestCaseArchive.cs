using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TestCaseManagement
{
    [Serializable]
    public class TestSuiteXTestCaseArchiveDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string TestSuiteXTestCaseArchiveId = "TestSuiteXTestCaseArchiveId";
			public const string TestSuiteId					= "TestSuiteId";
			public const string TestCaseId					= "TestCaseId";
			public const string TestCaseStatusId			= "TestCaseStatusId";
			public const string TestCasePriorityId			= "TestCasePriorityId";
			public const string RecordDate					= "RecordDate";
			public const string TestSuite					= "TestSuite";
			public const string TestCase					= "TestCase";
			public const string TestCaseStatus				= "TestCaseStatus";
			public const string TestCasePriority			= "TestCasePriority";
			public const string TestSuiteXTestCaseId		= "TestSuiteXTestCaseId";
			public const string KnowledgeDate				= "KnowledgeDate"; 
		}

        public static readonly TestSuiteXTestCaseArchiveDataModel Empty = new TestSuiteXTestCaseArchiveDataModel();

		public int? TestSuiteXTestCaseArchiveId { get; set; }
		public int? TestSuiteId					{ get; set; }
		public int? TestCaseId					{ get; set; }
		public int? TestCaseStatusId			{ get; set; }
		public int? TestCasePriorityId			{ get; set; }
		public int? RecordDate					{ get; set; }
		public string TestSuite					{ get; set; }
		public string TestCase					{ get; set; }
		public string TestCaseStatus			{ get; set; }
		public string TestCasePriority			{ get; set; }
		public int? TestSuiteXTestCaseId		{ get; set; }
		public int? KnowledgeDate				{ get; set; }

		public string TestCaseStatusList { get; set; }
	}
}
