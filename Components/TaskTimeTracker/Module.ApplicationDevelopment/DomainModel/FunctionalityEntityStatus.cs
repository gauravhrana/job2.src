using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class FunctionalityEntityStatusDataModel : StandardDataModel
    {
		public class DataColumns : BaseDataColumns
        {
            public const string FunctionalityEntityStatusId = "FunctionalityEntityStatusId";
			public const string SystemEntityTypeId = "SystemEntityTypeId";
            public const string FunctionalityId = "FunctionalityId";
            public const string FunctionalityStatusId = "FunctionalityStatusId";
            public const string FunctionalityPriorityId = "FunctionalityPriorityId";
            public const string AssignedTo = "AssignedTo";
            public const string Memo = "Memo";
            public const string TargetDate = "TargetDate";
            public const string StartDate = "StartDate";
            public const string TargetDate2 = "TargetDate2";
            public const string StartDate2 = "StartDate2";
            public const string SystemEntityType = "SystemEntityType";
            public const string Functionality = "Functionality";
            public const string FunctionalityStatus = "FunctionalityStatus";
            public const string FunctionalityPriority = "FunctionalityPriority";
            public const string FunctionalityActiveStatus = "FunctionalityActiveStatus";
            public const string FunctionalityActiveStatusId = "FunctionalityActiveStatusId";
        }

		public static readonly FunctionalityEntityStatusDataModel Empty = new FunctionalityEntityStatusDataModel();

        public int? FunctionalityEntityStatusId { get; set; }
        public int? FunctionalityId { get; set; }
        public int? FunctionalityStatusId { get; set; }
        public int? FunctionalityActiveStatusId { get; set; }
        public int[] FunctionalityStatusIds { get; set; }
        public int? FunctionalityPriorityId { get; set; }
        public int? SystemEntityTypeId { get; set; }
        public string AssignedTo { get; set; }
        public string Memo { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? TargetDateR2 { get; set; }
        public DateTime? StartDateR2 { get; set; }
        public string SystemEntityType { get; set; }
        public string Functionality { get; set; }
        public string FunctionalityStatus { get; set; }
        public string FunctionalityPriority { get; set; }

    }
}
