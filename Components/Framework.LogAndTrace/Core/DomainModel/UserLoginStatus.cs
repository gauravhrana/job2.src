using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.LogAndTrace
{

    [Serializable]
    public class UserLoginStatusDataModel : StandardDataModel
    {

        public int?     UserLoginStatusId   { get; set; }
        public string   UserLoginStatusCode { get; set; }

        public string   Application         { get; set; }

        public static readonly UserLoginStatusDataModel Empty = new UserLoginStatusDataModel();

		public class DataColumns : StandardDataColumns
		{
			public const string UserLoginStatusId   = "UserLoginStatusId";
            public const string UserLoginStatusCode = "UserLoginStatusCode";
            public const string Application         = "Application";
		}

	}

}
 