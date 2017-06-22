using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class FunctionalityXFunctionalityActiveStatusDataModel : StandardDataModel
    {
		public class DataColumns : BaseDataColumns
        {
            public const string FunctionalityXFunctionalityActiveStatusId = "FunctionalityXFunctionalityActiveStatusId";
            public const string FunctionalityId = "FunctionalityId";
            public const string FunctionalityActiveStatusId = "FunctionalityActiveStatusId";
            public const string AcknowledgedBy = "AcknowledgedBy";
            public const string Memo = "Memo";
            public const string KnowledgeDate = "KnowledgeDate";
            public const string Functionality = "Functionality";
            public const string FunctionalityActiveStatus = "FunctionalityActiveStatus";
            public const string FunctionalityPriority = "FunctionalityPriority";

        }

		public static readonly FunctionalityXFunctionalityActiveStatusDataModel Empty = new FunctionalityXFunctionalityActiveStatusDataModel();

        public int? FunctionalityXFunctionalityActiveStatusId { get; set; }
        public int? FunctionalityId { get; set; }
        public int? FunctionalityActiveStatusId { get; set; }
        public string AcknowledgedBy { get; set; }
        public string Memo { get; set; }
        public DateTime? KnowledgeDate { get; set; }
        public string Functionality { get; set; }
        public string FunctionalityPriority { get; set; }
        public string FunctionalityActiveStatus { get; set; }

    }
}
