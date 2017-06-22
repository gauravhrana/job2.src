using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.LogAndTrace
{
    [Serializable]
	public class UserLoginDataModel : StandardDataModel
	{

		public int?     UserLoginId         { get; set; }
		public string   UserName            { get; set; }
		public DateTime?     RecordDate     { get; set; }
		public int?     UserLoginStatusId   { get; set; }
		public string   UserLoginStatus     { get; set; }

		public DateTime? RecordDate2		{ get; set; }
		public int?     FromSearchDate      { get; set; }
		public int?     ToSearchDate        { get; set; }

        public string   Application         { get; set; }

        public static readonly UserLoginDataModel Empty = new UserLoginDataModel();

		public class DataColumns : StandardDataColumns
		{
			public const string UserLoginId       = "UserLoginId";
			public const string UserName          = "UserName";
			public const string RecordDate        = "RecordDate";
			public const string UserLoginStatusId = "UserLoginStatusId";
			public const string UserLoginStatus   = "UserLoginStatus";
            public const string Application       = "Application";

			public const string RecordDate2       = "RecordDate2";
			public const string FromSearchDate    = "FromSearchDate";
			public const string ToSearchDate      = "ToSearchDate";
		}

	} 

}
