using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class FunctionalityEntityStatusArchiveDataModel : StandardDataModel
    {
		public class DataColumns : BaseDataColumns
        {
            public const string FunctionalityEntityStatusArchiveId = "FunctionalityEntityStatusArchiveId";
            public const string RecordDate = "RecordDate";
            public const string SystemEntityType = "SystemEntityType";
            public const string SystemEntityTypeId = "SystemEntityTypeId";
            public const string Functionality = "Functionality";
            public const string FunctionalityId = "FunctionalityId";
            public const string FunctionalityStatus = "FunctionalityStatus";
            public const string FunctionalityStatusId = "FunctionalityStatusId";
            public const string FunctionalityPriority = "FunctionalityPriority";
            public const string FunctionalityPriorityId = "FunctionalityPriorityId";
            public const string FunctionalityEntityStatusId = "FunctionalityEntityStatusId";
            public const string TargetDate = "TargetDate";
            public const string StartDate = "StartDate";
            public const string AssignedTo = "AssignedTo";
            public const string Memo = "Memo";
            public const string KnowledgeDate = "KnowledgeDate";
            public const string AcknowledgedById = "AcknowledgedById";
            public const string AcknowledgedBy = "AcknowledgedBy";
        }

		public static readonly FunctionalityEntityStatusArchiveDataModel Empty = new FunctionalityEntityStatusArchiveDataModel();

        public int? FunctionalityEntityStatusArchiveId { get; set; }
        public DateTime? RecordDate { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string SystemEntityType { get; set; }
        public string Functionality { get; set; }
        public string FunctionalityStatus { get; set; }
        public string FunctionalityPriority { get; set; }
        public int? FunctionalityPriorityId { get; set; }
        public int? FunctionalityEntityStatusId { get; set; }
        public int? SystemEntityTypeId { get; set; }
        public int? FunctionalityId { get; set; }
        public int? FunctionalityStatusId { get; set; }
        public string AssignedTo { get; set; }
        public string Memo { get; set; }
        public DateTime? KnowledgeDate { get; set; }
        public int? AcknowledgedById { get; set; }
        public string AcknowledgedBy { get; set; }

    }
}
