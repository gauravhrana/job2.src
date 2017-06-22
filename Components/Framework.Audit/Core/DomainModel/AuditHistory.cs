using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Audit
{
    [Serializable]
    public class AuditHistory : BaseDataModel
    {
        public class DataColumns : BaseDataColumns
        {
            public const string AuditHistoryId    = "AuditHistoryId";
            public const string SystemEntityId    = "SystemEntityId";
            public const string EntityKey         = "EntityKey";
            public const string AuditActionId     = "AuditActionId";            
            public const string CreatedByPersonId = "CreatedByPersonId";
            public const string PersonId          = "PersonId";			
            public const string TimeInterval      = "TimeInterval";
			public const string UpdatedRange	  = "UpdatedRange";
            public const string SystemEntity      = "SystemEntity";
            public const string AuditAction       = "AuditAction";
            public const string Person            = "Person";
            public const string ToSearchDate      = "ToSearchDate";
            public const string FromSearchDate    = "FromSearchDate";
            public const string TypeOfIssue       = "TypeOfIssue";
            public const string DataViewMode      = "DataViewMode";			
        }

        public static readonly AuditHistory Empty = new AuditHistory();

        public int?			AuditHistoryId		 { get; set; }
		public int?			SystemEntityId		 { get; set; }
		public int?			EntityKey			 { get; set; }
		public int?			AuditActionId		 { get; set; }
		public int?			TraceId				 { get; set; }		
		public int?			CreatedByPersonId	 { get; set; }
		public int?			PersonId			 { get; set; }
		public float?		TimeInterval		 { get; set; }
		public string		SystemEntity		 { get; set; }
		public string		AuditAction			 { get; set; }
		public string		Person				 { get; set; }
		public DateTime?	ToSearchDate		 { get; set; }
		public DateTime?	FromSearchDate		 { get; set; }
		public string		TypeOfIssue			 { get; set; }
        public string       DataViewMode         { get; set; }

    }
}
