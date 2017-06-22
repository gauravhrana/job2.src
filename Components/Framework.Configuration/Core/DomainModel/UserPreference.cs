using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.UserPreference
{

    [Serializable]
    public class UserPreferenceDataModel : BaseDataModel
    {

        public int?		UserPreferenceId			{ get; set; }
		public int?		UserPreferenceKeyId			{ get; set; }
		public string	Value						{ get; set; }
		public int?		DataTypeId					{ get; set; }		
		public int?		UserPreferenceCategoryId	{ get; set; }
		public int?		ApplicationUserId			{ get; set; }
		public string	Application					{ get; set; }
		public string	UserPreferenceCategory		{ get; set; }
		public string	UserPreferenceDataType		{ get; set; }
		public string	ApplicationUser				{ get; set; }
		public string	UserPreferenceKey			{ get; set; } 

        public class DataColumns : BaseDataColumns
        {
            public const string UserPreferenceId         = "UserPreferenceId";
            public const string UserPreferenceKeyId      = "UserPreferenceKeyId";
            public const string Value                    = "Value";
            public const string DataTypeId               = "DataTypeId";
            public const string UserPreferenceCategoryId = "UserPreferenceCategoryId";
            public const string ApplicationUserId        = "ApplicationUserId";

            public const string Application              = "Application";
            public const string UserPreferenceCategory   = "UserPreferenceCategory";
            public const string UserPreferenceDataType   = "UserPreferenceDataType";
            public const string ApplicationUser          = "ApplicationUser";
            public const string UserPreferenceKey        = "UserPreferenceKey";
        }

		public static readonly UserPreferenceDataModel Empty = new UserPreferenceDataModel();

    }

}
