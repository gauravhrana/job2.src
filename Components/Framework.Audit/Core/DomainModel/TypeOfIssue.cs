using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Audit
{
    [Serializable]
    public class TypeOfIssueDataModel : StandardDataModel
    {

        public class DataColumns : StandardDataColumns
        {
            public const string TypeOfIssueId = "TypeOfIssueId";
            public const string Category = "Category";
        }

        public static readonly TypeOfIssueDataModel Empty = new TypeOfIssueDataModel();

        public int?     TypeOfIssueId   { get; set; }
        public string   Category        { get; set; }

    }
}
