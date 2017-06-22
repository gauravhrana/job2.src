using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class SearchKeyDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string SearchKeyId = "SearchKeyId";
            public const string View = "View";
        }

		public static readonly SearchKeyDataModel Empty = new SearchKeyDataModel();

        public int? SearchKeyId { get; set; }
        public string View { get; set; }
    }
}
