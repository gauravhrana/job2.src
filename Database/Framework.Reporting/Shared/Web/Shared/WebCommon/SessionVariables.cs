using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;

namespace Shared.WebCommon.UI.Web
{
    public class SessionVariables
    {
		public static class SessionKeys
		{
			public static string ApplicationUserRoles = "ApplicationUserRoles";
			public static string RequestProfile       = "RequestProfile";
			public static string SystemRequestProfile = "SystemRequestProfile";
            public static string Applications = "Applications";
		}

        public static string CurrentApplicationCode
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["CurrentApplicationCode"]);
            }
            set
            {
                HttpContext.Current.Session["CurrentApplicationCode"] = value;
            }
        }

        public static string CurrentApplicationModuleCode
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["CurrentApplicationModuleCode"]);
            }
            set
            {
                HttpContext.Current.Session["CurrentApplicationModuleCode"] = value;
            }
        }

        public static List<ApplicationUserRole> ApplicationUserRoles
        {
            get
            {
                if (HttpContext.Current.Session[SessionKeys.ApplicationUserRoles] == null)
                {
                    ApplicationCommon.SetApplicationUserRoles();
                }
                return (List<ApplicationUserRole>)(HttpContext.Current.Session[SessionKeys.ApplicationUserRoles]);
            }
            set
            {
                HttpContext.Current.Session["ApplicationUserRoles"] = value;
            }
        }

		public static RequestProfile RequestProfile
        {
            get
            {
				if (HttpContext.Current.Session != null)
					return (RequestProfile)HttpContext.Current.Session[SessionKeys.RequestProfile];
				else
					return null;
            }
            set
            {
                HttpContext.Current.Session[SessionKeys.RequestProfile] = value;
            }
        }

		public static RequestProfile SystemRequestProfile
		{
			get
			{
				if (HttpContext.Current.Session != null)
					return (RequestProfile)HttpContext.Current.Session[SessionKeys.SystemRequestProfile];
				else
					return null;
			}
			set
			{
				HttpContext.Current.Session[SessionKeys.SystemRequestProfile] = value;
			}
		}

        public static int Count
        {
            get
            {
                return (int)HttpContext.Current.Session["Count"];
            }
            set
            {
                HttpContext.Current.Session["Count"] = value;
            }
        }

        public static DataTable GridColumnsTable
        {
            get
            {
                if (HttpContext.Current.Session["GridTable"] != null)
                {
                    return (DataTable)HttpContext.Current.Session["GridTable"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["GridTable"] = value;
            }
        }

        public static DataTable AEFLTable
        {
            get
            {
                if (HttpContext.Current.Session["AEFLTable"] != null)
                {
                    return (DataTable)HttpContext.Current.Session["AEFLTable"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["AEFLTable"] = value;
            }
        }

        public static List<UPreference> UserPreferences
        {
            get
            {
                if (HttpContext.Current.Session["UserPreferences"] != null)
                {
                    return (List<UPreference>)(HttpContext.Current.Session["UserPreferences"]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["UserPreferences"] = value;
            }
        }

		public static List<UPreferenceCategory> UserPreferenceCategories
		{
			get
			{
				if (HttpContext.Current.Session["UserPreferenceCategories"] != null)
				{
					return (List<UPreferenceCategory>)(HttpContext.Current.Session["UserPreferenceCategories"]);
				}
				else
				{
					return null;
				}
			}
			set
			{
				HttpContext.Current.Session["UserPreferenceCategories"] = value;
			}
		}

        public static DataTable FieldConfigurations
        {
            get
            {
                if (HttpContext.Current.Session["FieldConfigurations"] != null)
                {
                    return (DataTable)(HttpContext.Current.Session["FieldConfigurations"]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["FieldConfigurations"] = value;
            }
        }

        public static DataTable UserFieldConfigurationModes
        {
            get
            {
                if (HttpContext.Current.Session["UserFieldConfigurationModes"] != null)
                {
                    return (DataTable)HttpContext.Current.Session["UserFieldConfigurationModes"];
                }
                else
                {
					FieldConfigurationUtility.SetUserFieldConfigurationModes();

					return (DataTable)HttpContext.Current.Session["UserFieldConfigurationModes"];
					
                }
            }
            set
            {
                HttpContext.Current.Session["UserFieldConfigurationModes"] = value;
            }
        }

        public static List<UPreference> ApplicationUserPreferences
        {
            get
            {
                if (HttpContext.Current.Session["ApplicationUserPreferences"] != null)
                {
                    return (List<UPreference>)(HttpContext.Current.Session["ApplicationUserPreferences"]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["ApplicationUserPreferences"] = value;
            }
        }

        public static string SortExpression
        {
            get
            {
				if (HttpContext.Current.Session["SortExpression"] != null)
				{
					return Convert.ToString(HttpContext.Current.Session["SortExpression"]);
				}
				else
				{
					return null;
				}
            }
            set
            {
                HttpContext.Current.Session["SortExpression"] = value;
            }
        }

        public static int? SearchControlColumnsModeId
        {
            get
            {
                if (HttpContext.Current.Session["SearchControlColumnsModeId"] == null)
                {
                    var fcModeName = ConfigurationManager.AppSettings["SearchControlColumnsModeName"];
                    HttpContext.Current.Session["SearchControlColumnsModeId"] = FieldConfigurationModeDataManager.GetFCModeIdByName(fcModeName, SessionVariables.RequestProfile);
                }
                return Convert.ToInt32(HttpContext.Current.Session["SearchControlColumnsModeId"]);
            }
            set
            {
                HttpContext.Current.Session["SearchControlColumnsModeId"] = value;
            }
        }

		public static int? DeveloperModeId
		{
			get
			{
				if (HttpContext.Current.Session["DeveloperModeId"] == null)
				{
					var fcModeName = ConfigurationManager.AppSettings["DeveloperModeName"];
					HttpContext.Current.Session["DeveloperModeId"] = FieldConfigurationModeDataManager.GetFCModeIdByName(fcModeName, SessionVariables.RequestProfile);
				}
				return Convert.ToInt32(HttpContext.Current.Session["DeveloperModeId"]);
			}
			set
			{
				HttpContext.Current.Session["DeveloperModeId"] = value;
			}
		}
		
        public static string SortDirection
        {
            get
            {
				if (HttpContext.Current.Session["SortDirection"] != null)
				{
					return Convert.ToString(HttpContext.Current.Session["SortDirection"]);
				}
				else
				{
					return null;
				}
            }
            set
            {
                HttpContext.Current.Session["SortDirection"] = value;
            }
        }

        public static string ActiveTableName
        {
            get
            {
				if (HttpContext.Current.Session["TableName"] != null)
				{
					return Convert.ToString(HttpContext.Current.Session["TableName"]);
				}
				else
				{
					return null;
				}
            }
            set
            {
                HttpContext.Current.Session["TableName"] = value;
            }
        }

        public static string UserTheme
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["UserTheme"]);
            }
            set
            {
                HttpContext.Current.Session["UserTheme"] = value;
            }
        }

        public static bool StartupChecked
        {
            get
            {
                if (HttpContext.Current.Session["StartupChecked"] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(HttpContext.Current.Session["StartupChecked"]);
            }
            set
            {
                HttpContext.Current.Session["StartupChecked"] = value;
            }
        }

        public static bool UserAuthorized
        {
            get
            {
                return Convert.ToBoolean(HttpContext.Current.Session["UserAuthorized"]);
            }
            set
            {
                HttpContext.Current.Session["UserAuthorized"] = value;
            }
        }

        public static string ApplicationUserFullName
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["ApplicationUserFullName"]);
            }
            set
            {
                HttpContext.Current.Session["ApplicationUserFullName"] = value;
            }
        }

        public static string ApplicationUserName
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["ApplicationUserName"]);
            }
            set
            {
                HttpContext.Current.Session["ApplicationUserName"] = value;
            }
        }

		public static string UserMenuCategory
		{
			get
			{
				if (HttpContext.Current.Session["UserMenuCategory"] == null)
				{
                    HttpContext.Current.Session["UserMenuCategory"] = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.UserMenuCategory);
				}
                return Convert.ToString(HttpContext.Current.Session["UserMenuCategory"]);
			}
			set
			{
                HttpContext.Current.Session["UserMenuCategory"] = value;
			}
		}

        public static int UserApplicationMode
        {
            get
            {
                if (HttpContext.Current.Session["UserApplicationModeId"] == null)
                {
                    HttpContext.Current.Session["UserApplicationModeId"] = PerferenceUtility.GetUserPreferenceByKeyAsInt(ApplicationCommon.UserApplicationModeId);
                }
                return Convert.ToInt32(HttpContext.Current.Session["UserApplicationModeId"]);
            }
            set
            {
                HttpContext.Current.Session["UserApplicationModeId"] = value;
            }
        }

		public static DataTable MenuCategoryList 
		{
			get
			{
				if (HttpContext.Current.Session["MenuCategoryList"] == null)
				{
                    HttpContext.Current.Session["MenuCategoryList"] = Shared.UI.Web.MenuHelper.GetMenuCategoryList();
				}				
				return (DataTable)(HttpContext.Current.Session["MenuCategoryList"]);
			}
			set
			{
				HttpContext.Current.Session["MenuCategoryList"] = value;
			}
		}

		public static DataTable ApplicationModeList
		{
			get
			{
				if (HttpContext.Current.Session["ApplicationModeList"] == null)
				{
                    HttpContext.Current.Session["ApplicationModeList"] = Framework.Components.UserPreference.ApplicationModeDataManager.GetList(SessionVariables.RequestProfile);
				}
				return (DataTable)(HttpContext.Current.Session["ApplicationModeList"]);
			}
			set
			{
				HttpContext.Current.Session["ApplicationModeList"] = value;
			}
		}

        public static bool IsTesting
        {
            get
            {
	            try
	            {
					return Convert.ToBoolean(HttpContext.Current.Session["IsTesting"]);
	            }
	            catch (Exception)
	            {
		            return false;
	            }                
            }
            set
            {
                HttpContext.Current.Session["IsTesting"] = value;
            }
        }

        public static int ApplicationMode
        {
            get {
	            return IsTesting ? 0 : 1;
            }
        }

        public static int? TopNCount
        {
            get
            {
                if (HttpContext.Current.Session["TopNCount"] == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToInt32(HttpContext.Current.Session["TopNCount"]);
                }
            }
            set
            {
                HttpContext.Current.Session["TopNCount"] = value;
            }
        }

        public static string DefaultActionLink
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["DefaultActionLink"]);
            }
            set
            {
                HttpContext.Current.Session["DefaultActionLink"] = value;
            }
        }

        public static int DefaultRowCount
        {
            get
            {
                if (HttpContext.Current.Session["DefaultRowCount"] == null)
                {
                    HttpContext.Current.Session["DefaultRowCount"] = PerferenceUtility.GetUserPreferenceByKeyAsInt(ApplicationCommon.DefaultRowCountKey);
                }
                return Convert.ToInt32(HttpContext.Current.Session["DefaultRowCount"]);
            }
            set
            {
                HttpContext.Current.Session["DefaultRowCount"] = value;
            }
        }

        public static string UserDateFormat
        {
            get
            {
                if (HttpContext.Current.Session["UserDateFormat"] == null)
                {
                    HttpContext.Current.Session["UserDateFormat"] = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.UserDateFormat);
                }
                return Convert.ToString(HttpContext.Current.Session["UserDateFormat"]);
            }
            set
            {
                HttpContext.Current.Session["UserDateFormat"] = value;
            }
        }

        public static string UserTimeFormat
        {
            get
            {
                if (HttpContext.Current.Session["UserTimeFormat"] == null)
                {
                    HttpContext.Current.Session["UserTimeFormat"] = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.UserTimeFormat);
                }
                return Convert.ToString(HttpContext.Current.Session["UserTimeFormat"]);
            }
            set
            {
                HttpContext.Current.Session["UserTimeFormat"] = value;
            }
        }

		//public static string DateRangeDateTimeFormat
		//{
		//	get
		//	{
		//		return "dd-MM-yy";
		//	}
		//}

        public static double? UserTimeZoneDifference
        {
            get
            {
                if (HttpContext.Current.Session["UserTimeZoneDifference"] == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToDouble(HttpContext.Current.Session["UserTimeZoneDifference"]);
                }
            }
            set
            {
                HttpContext.Current.Session["UserTimeZoneDifference"] = value;
            }
        }

        public static string AuditHistoryAdvancedModeInterval
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["AuditHistoryAdvancedModeInterval"]);
            }
            set
            {
                HttpContext.Current.Session["AuditHistoryAdvancedModeInterval"] = value;
            }
        }

		public static int ApplicationEntityFieldLabelMode
		{
			get
			{
				return Convert.ToInt32(HttpContext.Current.Session["ApplicationEntityFieldMode"]); 
			}
			set
			{
				HttpContext.Current.Session["ApplicationEntityFieldMode"] = value;
			}
		}

        public static int FieldConfigurationMode
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["FieldConfigurationMode"]);
            }
            set
            {
                HttpContext.Current.Session["FieldConfigurationMode"] = value;
            }
        }

        public static List<MenuDataModel> SiteMenuData
        {
            get
            {
                var objectOfInterest = HttpContext.Current.Session["SiteMenu"];

                if (objectOfInterest == null)
                {
                    var data = new MenuDataModel();
                    objectOfInterest = Framework.Components.Core.MenuDataManager.GetEntityDetails(data, RequestProfile, 0);

                    HttpContext.Current.Session["SiteMenu"] = objectOfInterest;
                }

                return (List<MenuDataModel>)objectOfInterest;
            }
            set
            {
                HttpContext.Current.Session["SiteMenu"] = value;
            }
        }

        public static List<MenuDataModel> UserPreferedMenuData
        {
            get
            {
                if (HttpContext.Current.Session["UserPreferedMenu"] == null)
                {
                    HttpContext.Current.Session["UserPreferedMenu"] = Shared.UI.Web.MenuHelper.GetUserPreferedMenu();
                }

                return (List<MenuDataModel>)(HttpContext.Current.Session["UserPreferedMenu"]);
            }
            set
            {
                HttpContext.Current.Session["UserPreferedMenu"] = value;
            }
        }

		public static string UserMenuCategoryType
		{
			get
			{
				if (HttpContext.Current.Session["UserMenuCategoryType"] == null)
				{
					HttpContext.Current.Session["UserMenuCategoryType"] = Shared.UI.Web.MenuHelper.GetUserPreferedMenuCategory();
				}

				return HttpContext.Current.Session["UserMenuCategoryType"].ToString();
			}
			set
			{
				HttpContext.Current.Session["UserMenuCategoryType"] = value;
			}
		}

		public static int GetSessionInstanceFCMode(string currentEntity)
		{
			if (HttpContext.Current.Session[currentEntity + "SelectedMode"] != null)
				return Convert.ToInt32(HttpContext.Current.Session[currentEntity + "SelectedMode"].ToString());
			else
				return -1;
		}

		public static void SaveSessionInstanceFCMode(int selectedMode, string currentEntity)
		{
			//var currententity = "Schedule";

			if (HttpContext.Current.Session[currentEntity + "SelectedMode"] == null)
				HttpContext.Current.Session.Add(currentEntity + "SelectedMode", selectedMode);
			else
				HttpContext.Current.Session[currentEntity + "SelectedMode"] = selectedMode;
		}

    }
}