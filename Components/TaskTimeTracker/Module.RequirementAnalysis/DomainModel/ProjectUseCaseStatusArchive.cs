using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.DataModel.RequirementAnalysis
{
    [Serializable]
	public class ProjectUseCaseStatusArchiveDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string ProjectUseCaseStatusArchiveId	 = "ProjectUseCaseStatusArchiveId";
			public const string RecordDate						 = "RecordDate";
			public const string UseCase							 = "UseCase";
			public const string Project							 = "Project";
			public const string ProjectUseCaseStatus			 = "ProjectUseCaseStatus";
			public const string ProjectUseCaseStatusId			 = "ProjectUseCaseStatusId";
			public const string Memo							 = "Memo";
			public const string KnowledgeDate					 = "KnowledgeDate";
			public const string AcknowledgedById				 = "AcknowledgedById";
			public const string AcknowledgedBy					 = "AcknowledgedBy";

		}

        public static readonly ProjectUseCaseStatusArchiveDataModel Empty = new ProjectUseCaseStatusArchiveDataModel();

		public int?		ProjectUseCaseStatusArchiveId	{ get; set; }
		public int?		RecordDate						{ get; set; }
		public string	UseCase							{ get; set; }
		public string	Project							{ get; set; }
		public string	ProjectUseCaseStatus			{ get; set; }
		public int?		ProjectUseCaseStatusId			{ get; set; }
		public string	Memo							{ get; set; }
		public int?		KnowledgeDate					{ get; set; }
		public int?		AcknowledgedById				{ get; set; }
		public string	AcknowledgedBy					{ get; set; }
		
	}
}
