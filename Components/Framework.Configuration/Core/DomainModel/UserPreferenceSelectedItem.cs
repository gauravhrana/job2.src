using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.UserPreference
{

    [Serializable]
    public class UserPreferenceSelectedItemDataModel : BaseDataModel
    {

        public int?		UserPreferenceSelectedItemId	{ get; set; }
		public int?		UserPreferenceKeyId				{ get; set; }
		public string	Value							{ get; set; }
		public string	ParentKey						{ get; set; }
        public int?		SortOrder						{ get; set;}
		public int?		ApplicationUserId				{ get; set; }

		public string	ApplicationUser					{ get; set; }
		public string	UserPreferenceKey				{ get; set; }

        public class DataColumns : BaseDataColumns
        {
            public const string UserPreferenceSelectedItemId         = "UserPreferenceSelectedItemId";
            public const string UserPreferenceKeyId                  = "UserPreferenceKeyId";
            public const string ApplicationUserId                    = "ApplicationUserId";
            public const string ParentKey                            = "ParentKey";
            public const string Value                                = "Value";
            public const string ApplicationUser                      = "ApplicationUser";
            public const string UserPreferenceKey                    = "UserPreferenceKey";
            public const string SortOrder                            = "SortOrder";
        }

		public static readonly UserPreferenceSelectedItemDataModel Empty = new UserPreferenceSelectedItemDataModel();

    }

}
