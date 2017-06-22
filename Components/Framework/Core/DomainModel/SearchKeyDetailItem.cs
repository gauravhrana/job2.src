using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class SearchKeyDetailItemDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string SearchKeyDetailItemId = "SearchKeyDetailItemId";
            public const string SearchKeyDetailId     = "SearchKeyDetailId";
            public const string Value                 = "Value";
            public const string SortOrder             = "SortOrder";
        }

		public static readonly SearchKeyDetailItemDataModel Empty = new SearchKeyDetailItemDataModel();

        public int?     SearchKeyDetailItemId   { get; set; }
        public int?     SearchKeyDetailId       { get; set; }
        public string   Value                   { get; set; }
        public int?     SortOrder               { get; set; }

    }
}
