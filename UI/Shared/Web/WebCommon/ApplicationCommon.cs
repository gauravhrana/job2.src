using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.UI;
using Shared.UI.Web;

namespace Shared.WebCommon.UI.Web
{
    public class ApplicationCommon
    {
        public const string DetailsBorderClassName         = "DetailControlBorder";
        public const string HistoryGridVisibilityKey       = "HistoryGridVisible";
        public const string GridDefaultClickActionKey      = "GridDefaultClickAction";
        public const string DefaultRowCountKey             = "DefaultRowCount";
        public const string GridDetailLinkVisibleKey       = "GridDetailLinkVisible";
        public const string GridDeleteLinkVisibleKey       = "GridDeleteLinkVisible";
        public const string HistoryAdvancedModeGroupingKey = "AuditHistoryAdvancedModeGrouping";
        public const string HistoryAdvancedModeIntervalKey = "AuditHistoryAdvancedModeInterval";
        public const string UserTimeZone                   = "UserTimeZone";
        public const string AutoSearchOn                   = "AutoSearchOn";
        public const string UpdateInfoStyle                = "UpdateInfoStyle";

        public static string GetSuperKey()
        {
            var currentPage = HttpContext.Current.CurrentHandler as Page;
            var sKey = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["SuperKey"]) || currentPage.RouteData.Values["SuperKey"] != null)
            {

                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["SuperKey"]))
                {
                    sKey = HttpContext.Current.Request.QueryString["SuperKey"];
                }
                else
                {
                    sKey = Convert.ToString(currentPage.RouteData.Values["SuperKey"]);
                }
            }
            return sKey;
        }

        public static int GetSetId()
        {
            var currentPage = HttpContext.Current.CurrentHandler as Page;
            var sId = 0;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["SetId"]))
            {
                sId = Convert.ToInt32(HttpContext.Current.Request.QueryString["SetId"]);
            }
            else
            {
                sId = Convert.ToInt32(currentPage.RouteData.Values["SetId"]);
            }
            return sId;
        }

        public static double GetUserTimeZoneDifference()
        {
            if (SessionVariables.UserTimeZoneDifference == null)
            {
                var userTimeZoneId = GetUserPreferenceByKeyAsInt(UserTimeZone);
                var data = new Framework.Components.Core.TimeZone.Data();
                data.TimeZoneId = userTimeZoneId;
                var dt = Framework.Components.Core.TimeZone.GetDetails(data, SessionVariables.AuditId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    SessionVariables.UserTimeZoneDifference = Convert.ToDouble(dt.Rows[0][Framework.Components.Core.TimeZone.DataColumns.TimeDifference]);
                }
            }
            return SessionVariables.UserTimeZoneDifference.Value;
        }

        public static string GetUserTimeZoneDateAsString(object dtm)
        {
            var date = string.Empty;
            if (dtm != DBNull.Value)
            {
                var userTimeZoneDifference = GetUserTimeZoneDifference();
                var appTimeZoneDifference = Convert.ToDouble(ConfigurationManager.AppSettings["UTCTimeZoneDifference"]);

                var dtmNow = Convert.ToDateTime(dtm).AddHours(appTimeZoneDifference * -1);
                dtmNow = dtmNow.AddHours(userTimeZoneDifference);
                date = dtmNow.ToString();
            }
            return date;
        }

        public static int GenerateSuperKey(StringCollection sc, int systemEntityTypeId)
        {
            int superKeyId = 0;

            var data = new Framework.Components.Core.SuperKey.Data();
            data.SortOrder = 1;
            data.SystemEntityTypeId = systemEntityTypeId;
            data.ExpirationDate = Convert.ToInt32(DateTime.Now.AddDays(30).ToString("yyyyMMdd"));
            data.Description = systemEntityTypeId + " : " + data.ExpirationDate;
            data.Name = systemEntityTypeId + " : " + data.ExpirationDate;

            superKeyId = Framework.Components.Core.SuperKey.Create(data, SessionVariables.AuditId);

            foreach (string _str in sc)
            {
                var detailData = new Framework.Components.Core.SuperKeyDetail.Data();
                detailData.SuperKeyId = superKeyId;
                detailData.EntityKey = Convert.ToInt32(_str);
                Framework.Components.Core.SuperKeyDetail.Create(detailData, SessionVariables.AuditId);
            }
            return superKeyId;
        }

        public static string SetDateRange(string dateRange, string key, string procedure)
        {
            var userPreferenceCategoryId = GetUserPreferenceCategory(key);

            //if not then find the default values for "GridDefaultClickAction" Prefrence
            var defDateRange = string.Empty;
            var defDataTypeId = 0;
            var userPreferenceKeyId = 0;

            var dt1 = Framework.Components.UserPreference.UserPreferenceKey.GetList(SessionVariables.AuditId);
            for (var i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["Name"].Equals("DateRange"))
                {

                    // store default values for Preference "GridDefaultClickAction"
                    userPreferenceKeyId = Convert.ToInt32(dt1.Rows[i][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.UserPreferenceKeyId]);
                    defDateRange = Convert.ToString(dt1.Rows[i][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.Value]);
                    defDataTypeId = Convert.ToInt32(dt1.Rows[i][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.DataTypeId]);
                    break;
                }
            }

            //insert new preference for current user using default values
            Framework.Components.UserPreference.UserPreference.Data dt = new Framework.Components.UserPreference.UserPreference.Data();

            dt.ApplicationUserId = SessionVariables.AuditId;
            dt.UserPreferenceKeyId = userPreferenceKeyId;

            dt.DataTypeId = defDataTypeId;
            dt.ApplicationId = Framework.Components.DataAccess.SetupConfiguration.ApplicationId; // for TTT Application
            dt.UserPreferenceCategoryId = userPreferenceCategoryId; // for General Category

            if (procedure == "Create")
            {
                dt.Value = defDateRange;
                Framework.Components.UserPreference.UserPreference.Create(dt, SessionVariables.AuditId);
            }
            else if (procedure == "Update")
            {
                var userPreferences = SessionVariables.UserPreferences;
                var preference = userPreferences.Find(item => item.UserPreferenceKey == "DateRange" && item.UserPreferenceCategory == key);

                dt.UserPreferenceId = preference.Id;
                dt.Value = dateRange;
                Framework.Components.UserPreference.UserPreference.Update(dt, SessionVariables.AuditId);
            }

            dateRange = Convert.ToString(defDateRange);
            SetUserPreferences();

            return dateRange;
        }

        public static bool UserPreferenceExists(string userPreferenceName, string key)
        {
            var userPreferences = SessionVariables.UserPreferences;
            var preference = userPreferences.Find(item => item.UserPreferenceKey == userPreferenceName && item.UserPreferenceCategory == key);

            if (preference != null)
            {
                return true;
            }

            return false;
        }

        public static string SetBackgroundColor(string color, string key, string procedure)
        {
            var userPreferenceCategoryId = GetUserPreferenceCategory(key);

            //if not then find the default values for "GridDefaultClickAction" Prefrence
            var defBackgroundColor = string.Empty;
            var defDataTypeId = 0;
            var userPreferenceKeyId = 0;

            var dt1 = Framework.Components.UserPreference.UserPreferenceKey.GetList(SessionVariables.AuditId);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["Name"].Equals("BackgroundColor"))
                {

                    // store default values for Preference "GridDefaultClickAction"
                    userPreferenceKeyId = Convert.ToInt32(dt1.Rows[i][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.UserPreferenceKeyId]);
                    defBackgroundColor = Convert.ToString(dt1.Rows[i][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.Value]);
                    defDataTypeId = Convert.ToInt32(dt1.Rows[i][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.DataTypeId]);
                    break;
                }
            }

            //insert new preference for current user using default values
            Framework.Components.UserPreference.UserPreference.Data dt = new Framework.Components.UserPreference.UserPreference.Data();

            dt.ApplicationUserId = SessionVariables.AuditId;
            dt.UserPreferenceKeyId = userPreferenceKeyId;

            dt.DataTypeId = defDataTypeId;
			dt.ApplicationId = Framework.Components.DataAccess.SetupConfiguration.ApplicationId; ; // for TTT Application
            dt.UserPreferenceCategoryId = userPreferenceCategoryId; // for General Category

            if (procedure == "Create")
            {
                dt.Value = defBackgroundColor;
                Framework.Components.UserPreference.UserPreference.Create(dt, SessionVariables.AuditId);
            }
            else if (procedure == "Update")
            {
                var userPreferences = SessionVariables.UserPreferences;
                var preference = userPreferences.Find(item => item.UserPreferenceKey == "BackgroundColor" && item.UserPreferenceCategory == key);

                dt.UserPreferenceId = preference.Id;
                dt.Value = color;
                Framework.Components.UserPreference.UserPreference.Update(dt, SessionVariables.AuditId);
            }


            color = Convert.ToString(defBackgroundColor);
            SetUserPreferences();

            return color;
        }

        public static string GetApplicationUserName()
        {
            if (string.IsNullOrEmpty(SessionVariables.ApplicationUserName))
            {
                SetApplicationUserName();
            }
            return SessionVariables.ApplicationUserName;
        }

        public static void SetApplicationUserName()
        {
            var oData = new Framework.Components.ApplicationUser.ApplicationUser.Data();

            oData.ApplicationUserId = SessionVariables.AuditId;

            SessionVariables.ApplicationUserName = Framework.Components.ApplicationUser.ApplicationUser.GetFullName(oData, SessionVariables.AuditId);
        }

        public static void SetApplicationUserRoles()
        {
            var lst = new List<ApplicationUserRole>();

            var dt = Framework.Components.ApplicationUser.ApplicationUserXApplicationRole.GetByApplicationUser(SessionVariables.AuditId, SessionVariables.AuditId);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var role = new ApplicationUserRole();
                    role.Id = Convert.ToInt32(dr[Framework.Components.ApplicationUser.ApplicationUserXApplicationRole.DataColumns.ApplicationUserXApplicationRoleId]);
                    role.ApplicationRole = Convert.ToString(dr[Framework.Components.ApplicationUser.ApplicationUserXApplicationRole.DataColumns.ApplicationRole]);
                    role.ApplicationRoleId = Convert.ToInt32(dr[Framework.Components.ApplicationUser.ApplicationUserXApplicationRole.DataColumns.ApplicationRoleId]);
                    role.ApplicationUserId = Convert.ToInt32(dr[Framework.Components.ApplicationUser.ApplicationUserXApplicationRole.DataColumns.ApplicationUserId]);
                    lst.Add(role);
                }
            }
            SessionVariables.ApplicationUserRoles = lst;
        }

        public static void SetUserPreferences()
        {
            var lst = new List<UPreference>();

            var data = new Framework.Components.UserPreference.UserPreference.Data();

            data.ApplicationUserId = SessionVariables.AuditId;

            var dt = Framework.Components.UserPreference.UserPreference.Search(data, SessionVariables.AuditId);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var preference = new UPreference();
                    preference.ApplicationUserId = Convert.ToInt32(dr[Framework.Components.UserPreference.UserPreference.DataColumns.ApplicationUserId]);
                    preference.DataType = Convert.ToString(dr[Framework.Components.UserPreference.UserPreference.DataColumns.UserPreferenceDataType]);
                    preference.Id = Convert.ToInt32(dr[Framework.Components.UserPreference.UserPreference.DataColumns.UserPreferenceId]);
                    preference.UserPreferenceKey = Convert.ToString(dr[Framework.Components.UserPreference.UserPreference.DataColumns.UserPreferenceKey]);
                    preference.UserPreferenceCategory = Convert.ToString(dr[Framework.Components.UserPreference.UserPreference.DataColumns.UserPreferenceCategory]);
                    preference.value = Convert.ToString(dr[Framework.Components.UserPreference.UserPreference.DataColumns.Value]);
                    lst.Add(preference);
                }
            }
            SessionVariables.UserPreferences = lst;
        }

        public static void UpdateUserPreference(string upCategory, string upKey, string upValue)
        {

            var userPreferenceValue = string.Empty;
            var auditId = SessionVariables.AuditId;
            if (SessionVariables.UserPreferences == null)
            {
                SetUserPreferences();
            }

            var userPreferences = SessionVariables.UserPreferences;
            var preference = userPreferences.Find(item => item.UserPreferenceKey == upKey && item.UserPreferenceCategory == upCategory);

            //check if current user has preference of for the key
            if (preference == null)
            {
                GetUserPreferenceByKey(upKey, upCategory);
                userPreferences = SessionVariables.UserPreferences;
                preference = userPreferences.Find(item => item.UserPreferenceKey == upKey && item.UserPreferenceCategory == upCategory);
            }            

            var data = new Framework.Components.UserPreference.UserPreference.Data();
            data.UserPreferenceId = preference.Id;
            data.Value = upValue;

            Framework.Components.UserPreference.UserPreference.UpdateValueOnly(data, SessionVariables.AuditId);
            SetUserPreferences();
        }

        #region SystemEntities

        public static string[] SystemEntities =
        {
            "AuditAction"                               ,
            "AuditHistory"                              ,
            "ApplicationRole"                           ,
            "Project"                                   ,
            "Question"                                  ,
            "Schedule"                                  ,
            "ScheduleItem"                              ,
            "ScheduleQuestion"                          ,
            "Task"                                      ,
            "TaskFormulation"                           ,
            "UserPreferenceDataType"                    ,
            "UserPreference"                            ,
            "Layer"                                     ,
            "Person"                                    ,
            "UserPreferenceKey"                         ,
            "SystemEntityType"                          ,
            "BatchFile"                                 ,
            "FileType"                                  ,
            "BatchFileStatus"                           ,
            "Feature"                                   ,
            "Need"                                      ,
            "BatchFileSet"                              ,
            "BatchFileHistory"                          ,
            "TaskEntityType"                            ,
            "TaskScheduleType"                          ,
            "TaskEntity = 2600"                         ,
            "TaskSchedule"                              ,
            "TaskRun"                                   ,
            "Milestone"                                 ,
            "Client"                                    ,
            "ApplicationMonitoredEventSource"           ,
            "ApplicationMonitoredEventProcessingState"  ,
            "ApplicationMonitoredEventEmail"            ,
            "ApplicationMonitoredEvent"                 ,
            "NeedsXFeature"                             ,
            "Activity"                                  ,
            "TaskType"                                  ,
            "TaskPriorityType"                          ,
            "TaskPriorityXApplicationUser"              ,
            "ReleaseLog"                                ,
            "ReleaseLogDetails"                         ,
            "Application"                               ,
            "Risk"                                      ,
            "Reward"                                    ,   
            "ProjectTimeLine"                           ,   
            "TaskRiskRewardRankingXPerson"              ,   
            "PersonTitle"                               ,   
            "ProjectXNeeds"								,
			"ApplicationUser"							,
			"SystemEntityCategory"						,
            "TypeOfIssue"                               ,
            "TimeZone"                                  ,
            "Country"
        };

        #endregion 
        
        public static class Columns
        {
            public const string UpdatedDate = "UpdatedDate";
			public const string UpdatedBy = "UpdatedBy";
			public const string LastAction = "LastAction";
        }

		public class AuditConstStrings
		{
			public const string AuditHistory = "AuditHistory";
			public const string Audit = "Audit";
			public const string AuditHistoryId = "AuditHistoryId";
		}

        public static int GetUserPreferenceCategory(string userPreferenceCategory)
        {
            var userPreferenceCategoryId = 0;
            if (!string.IsNullOrEmpty(userPreferenceCategory))
            {
                var data = new Framework.Components.UserPreference.UserPreferenceCategory.Data();

                data.Name = userPreferenceCategory;

                var dt = Framework.Components.UserPreference.UserPreferenceCategory.Search(data, SessionVariables.AuditId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    userPreferenceCategoryId = Convert.ToInt32(dt.Rows[0][Framework.Components.UserPreference.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId]);
                }
            }

            return userPreferenceCategoryId;
        }

        public static int GetUserPreferenceKey(string userPreferenceKey)
        {
            var userPreferenceKeyId = 0;
            if (!string.IsNullOrEmpty(userPreferenceKey))
            {
                var data = new Framework.Components.UserPreference.UserPreferenceKey.Data();

                data.Name = userPreferenceKey;

                var dt = Framework.Components.UserPreference.UserPreferenceKey.Search(data, SessionVariables.AuditId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    userPreferenceKeyId = Convert.ToInt32(dt.Rows[0][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.UserPreferenceKeyId]);
                }
            }

            return userPreferenceKeyId;
        }

        public static string GetUserTheme()
        {
            var strAppTheme = Convert.ToString(HttpContext.Current.Application["Branding"]);
            var strUserTheme = string.Empty;

            // Get theme from Session object if it is already set
            if (!string.IsNullOrEmpty(SessionVariables.UserTheme))
                strUserTheme = SessionVariables.UserTheme;
            else
            {
                // Get Theme from User Preference if not alreday set in session
                //UserPreference table retrieval

                if (SessionVariables.UserPreferences == null)
                {
                    SetUserPreferences();
                }

                var userPreferences = SessionVariables.UserPreferences;
                var preference = userPreferences.Find(item => item.UserPreferenceKey == "ImageTheme");

                if (preference == null || string.IsNullOrEmpty(preference.value))
                {
                    // if user preference for theme is not set then fetch default theme from application object.
                    var strTmpArry = strAppTheme.Split(new char[] { '/' });

                    if (strTmpArry.Length > 0)
                    {
                        strUserTheme = strTmpArry[strTmpArry.Length - 1];
                    }
                }
                else
                {
                    strUserTheme = preference.value;
                }
            }

            var strSubPath = strAppTheme.Substring(0, strAppTheme.LastIndexOf('/') + 1);

            return strSubPath + strUserTheme;
        }

        public static string GetUserPreferenceByKey(string userPreferenceKey, string userPreferenceCategory = "General")
        {
            var userPreferenceValue = string.Empty;
            var auditId = SessionVariables.AuditId;
            if (SessionVariables.UserPreferences == null)
            {
                SetUserPreferences();
            }

            var userPreferences = SessionVariables.UserPreferences;
            var preference = userPreferences.Find(item => item.UserPreferenceKey == userPreferenceKey && item.UserPreferenceCategory == userPreferenceCategory);

            //check if current user has preference of for the key
            if (preference != null)
            {
                //if yes then assign it to a variable
                userPreferenceValue = preference.value;
            }
            else
            {
                var userPreferenceCategoryId = GetUserPreferenceCategory(userPreferenceCategory);

                //if not then find the default values for Prefrence
                var defValue = string.Empty;
                var defDataTypeId = 0;
                var userPreferenceKeyId = 0;

                var dt1 = Framework.Components.UserPreference.UserPreferenceKey.GetList(SessionVariables.AuditId);
                for (var i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i]["Name"].Equals(userPreferenceKey))
                    {
                        // store default values for Preference "GridDefaultClickAction"
                        userPreferenceKeyId = Convert.ToInt32(dt1.Rows[i][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.UserPreferenceKeyId]);
                        defValue = Convert.ToString(dt1.Rows[i][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.Value]);
                        defDataTypeId = Convert.ToInt32(dt1.Rows[i][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.DataTypeId]);
                        break;
                    }
                }

                //insert new preference for current user using default values
                Framework.Components.UserPreference.UserPreference.Data dt = new Framework.Components.UserPreference.UserPreference.Data();

                dt.ApplicationUserId = SessionVariables.AuditId;
                dt.UserPreferenceKeyId = userPreferenceKeyId;
                dt.Value = defValue;
                dt.DataTypeId = defDataTypeId;
				dt.ApplicationId = Framework.Components.DataAccess.SetupConfiguration.ApplicationId; ;
                dt.UserPreferenceCategoryId = userPreferenceCategoryId;

                Framework.Components.UserPreference.UserPreference.Create(dt, SessionVariables.AuditId);

                userPreferenceValue = defValue;
                SetUserPreferences();
            }

            return userPreferenceValue;
        }

        public static bool GetUserPreferenceByKeyAsBoolean(string userPreferenceKey, string userPreferenceCategory = "General")
        {
            var value = GetUserPreferenceByKey(userPreferenceKey, userPreferenceCategory);
            return Convert.ToBoolean(value);
        }

        public static int GetUserPreferenceByKeyAsInt(string userPreferenceKey, string userPreferenceCategory = "General")
        {
            var value = GetUserPreferenceByKey(userPreferenceKey, userPreferenceCategory);
            return Convert.ToInt32(value);
        }

        public static double GetUserPreferenceByKeyAsDouble(string userPreferenceKey, string userPreferenceCategory = "General")
        {
            var value = GetUserPreferenceByKey(userPreferenceKey, userPreferenceCategory);
            return Convert.ToDouble(value);
        }

    }
}