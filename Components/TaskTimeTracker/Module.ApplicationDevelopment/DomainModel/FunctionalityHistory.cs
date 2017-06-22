using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
    public class FunctionalityHistoryDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string FunctionalityId = "FunctionalityId";
            public const string FunctionalityHistoryId = "FunctionalityHistoryId";
            public const string FunctionalityActiveStatusId = "FunctionalityActiveStatusId";
            public const string FunctionalityActiveStatus = "FunctionalityActiveStatus";
            public const string FunctionalityPriorityId = "FunctionalityPriorityId";
            public const string FunctionalityPriority = "FunctionalityPriority";
            public const string Memo = "Memo";
            public const string AcknowledgedBy = "AcknowledgedBy";
            public const string KnowledgeDate = "KnowledgeDate";
        }

		public static readonly FunctionalityHistoryDataModel Empty = new FunctionalityHistoryDataModel();

        public int? FunctionalityHistoryId { get; set; }
        public int? FunctionalityId { get; set; }
        public int? FunctionalityActiveStatusId { get; set; }
        public string FunctionalityActiveStatus { get; set; }
        public int? FunctionalityPriorityId { get; set; }
        public string FunctionalityPriority { get; set; }
        public string Memo { get; set; }
        public string AcknowledgedBy { get; set; }
        public DateTime KnowledgeDate { get; set; }

    }
}
