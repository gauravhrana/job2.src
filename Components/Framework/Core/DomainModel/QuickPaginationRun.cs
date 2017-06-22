using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class QuickPaginationRunDataModel : BaseDataModel
    {
        public class DataColumns : BaseDataColumns
        {
            public const string QuickPaginationRunId = "QuickPaginationRunId";
            public const string ApplicationUserId    = "ApplicationUserId";
            public const string SystemEntityTypeId   = "SystemEntityTypeId";
            public const string SortClause           = "SortClause";
            public const string WhereClause          = "WhereClause";
            public const string ExpirationTime       = "ExpirationTime";
            public const string SystemEntityType     = "SystemEntityType";
            public const string ApplicationUserName  = "ApplicationUserName";
        }

		public static readonly QuickPaginationRunDataModel Empty = new QuickPaginationRunDataModel();

        public int?     QuickPaginationRunId    { get; set; }
        public int?     ApplicationUserId       { get; set; }
        public int?     SystemEntityTypeId      { get; set; }
        public string   SortClause              { get; set; }
        public string   WhereClause             { get; set; }
        public int?     ExpirationTime          { get; set; }
        public string   SystemEntityType        { get; set; }
        public string   ApplicationUserName     { get; set; }
 
    }
}
