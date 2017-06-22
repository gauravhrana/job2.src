using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    public class SearchKeyDetailDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string SearchKeyDetailId = "SearchKeyDetailId";
            public const string SearchKeyId       = "SearchKeyId";
            public const string SearchParameter   = "SearchParameter";
            public const string SortOrder         = "SortOrder";
            public const string SearchKey         = "SearchKey";
        }

		public static readonly SearchKeyDetailDataModel Empty = new SearchKeyDetailDataModel();

        public int?     SearchKeyDetailId   { get; set; }
        public int?     SearchKeyId         { get; set; }
        public string   SearchParameter     { get; set; }
        public int?     SortOrder           { get; set; }
        public string   SearchKey           { get; set; }	     

    }
}
