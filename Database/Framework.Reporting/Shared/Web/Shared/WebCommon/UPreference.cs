using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Shared.WebCommon.UI.Web
{
	public class UPreferenceCategory
	{
		public int		Id				{ get; set; }
		public string	Name			{ get; set; }
	}

    public class UPreference
    {
        public int Id
        {
            set;
            get;
        }

        public string UserPreferenceCategory
        {
            get;
            set;
        }

        public string UserPreferenceKey
        {
            get;
            set;
        }

        public string value
        {
            get;
            set;
        }

        public string DataType
        {
            get;
            set;
        }

        public int ApplicationUserId
        {
            get;
            set;
        }

        public static Boolean IsWildCardSearchPrefixChecked
        {
            get
            {
                return PerferenceUtility.GetUserPreferenceByKeyAsBoolean("WildCardSearchPrefix", "SearchSettings");
            }
            set
            {
                PerferenceUtility.UpdateUserPreference("SearchSettings", "WildCardSearchPrefix", value.ToString());
            }
        }

        public static Boolean IsWildCardSearchPostfixChecked
        {
            get
            {
                return PerferenceUtility.GetUserPreferenceByKeyAsBoolean("WildCardSearchPostfix", "SearchSettings");
            }
            set
            {
                PerferenceUtility.UpdateUserPreference("SearchSettings", "WildCardSearchPostfix", value.ToString());
            }
        }

        public static string GetWildCardSearchPrefix(string category)
        {
            return PerferenceUtility.GetUserPreferenceByKey("WildCardSearchPrefix", category);
        }

        public static string GetWildCardSearchPostfix(string category)
        {
            return PerferenceUtility.GetUserPreferenceByKey("WildCardSearchPostfix", category);
        }

    }
}