using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.LogAndTrace
{

    [Serializable]
    public class UserLoginHistoryDataModel : StandardDataModel
    {

        public int?         UserLoginHistoryId  { get; set; }
        public string       UserName            { get; set; }
        public int?         UserId              { get; set; }
        public string       URL                 { get; set; }
        public string       ServerName          { get; set; }
        public DateTime?    DateVisited         { get; set; }
		
        public DateTime?    FromSearchDate      { get; set; }
		public DateTime?	ToSearchDate		{ get; set; }

        public string       Application         { get; set; }

        public static readonly UserLoginHistoryDataModel Empty = new UserLoginHistoryDataModel();

        public class DataColumns : StandardDataColumns
        {
            public const string UserLoginHistoryId = "UserLoginHistoryId";
            public const string UserName           = "UserName";
            public const string URL                = "URL";
            public const string UserId             = "UserId";
            public const string ServerName         = "ServerName";
            public const string DateVisited        = "DateVisited";
			public const string DateRangeValue	   = "DateRangeValue";
            public const string Application        = "Application";

            public const string FromSearchDate     = "FromSearchDate";
            public const string ToSearchDate       = "ToSearchDate";
            public const string GroupBy            = "GroupBy";
        }

    }

}
 