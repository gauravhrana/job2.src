using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class TransactionTypeDataModel : BaseModel
    {

        public class DataColumns : BaseColumns
        {
            public const string TransactionTypeId = "TransactionTypeId";
            public const string Name = "Name";
            public const string Description = "Description";
            public const string SortOrder = "SortOrder";
            public const string Code = "Code";
        }

        public static readonly TransactionTypeDataModel Empty = new TransactionTypeDataModel();

        [PrimaryKey, IncludeInSearch]
        public int? TransactionTypeId { get; set; }

        [IncludeInSearch, IncludeInUnique]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SortOrder { get; set; }

        [IncludeInSearch]
        public string Code { get; set; }

    }
}
