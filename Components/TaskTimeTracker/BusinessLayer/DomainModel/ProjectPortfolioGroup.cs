using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;


namespace DataModel.TaskTimeTracker
{
    public class ProjectPortfolioGroupDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string ProjectPortfolioGroupId = "PortfolioGroupId";
            public const string DateCreated             = "DateCreated";
            public const string DateModified            = "DateModified";
            public const string CreatedByAuditId        = "CreatedByAuditId";
            public const string ModifiedByAuditId = "ModifiedByAuditId";
        }

        public int? ProjectPortfolioGroupId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int? CreatedByAuditId { get; set; }
        public int? ModifiedByAuditId { get; set; }

        public string ToURLQuery()
        {
            return String.Empty; //"ProjectPortfolioGroupId=" + ProjectPortfolioGroupId
        }
    }
}
