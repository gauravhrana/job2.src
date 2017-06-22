using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Shared.WebCommon.UI.Web
{
    public class SessionVariables
    {

        public static List<ApplicationUserRole> ApplicationUserRoles
        {
            get
            {
                if (HttpContext.Current.Session["ApplicationUserRoles"] != null)
                {
                    return (List<ApplicationUserRole>)(HttpContext.Current.Session["ApplicationUserRoles"]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["ApplicationUserRoles"] = value;
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

        public static string SortExpression
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["SortExpression"]);
            }
            set
            {
                HttpContext.Current.Session["SortExpression"] = value;
            }
        }

        public static string SortDirection
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["SortDirection"]);
            }
            set
            {
                HttpContext.Current.Session["SortDirection"] = value;
            }
        }

        public static string TableName
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["TableName"]);
            }
            set
            {
                HttpContext.Current.Session["TableName"] = value;
            }
        }

        public static int? CurrentPageIndex
        {
            get
            {
                if (HttpContext.Current.Session["CurrentPageIndex"] == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToInt32(HttpContext.Current.Session["CurrentPageIndex"]);
                }
            }
            set
            {
                HttpContext.Current.Session["TopNCount"] = value;
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

        public static int AuditId
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["AuditId"]);
            }
            set
            {
                HttpContext.Current.Session["AuditId"] = value;
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

        public static bool IsTesting
        {
            get
            {
                return Convert.ToBoolean(HttpContext.Current.Session["IsTesting"]);
            }
            set
            {
                HttpContext.Current.Session["IsTesting"] = value;
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
                    HttpContext.Current.Session["DefaultRowCount"] = ApplicationCommon.GetUserPreferenceByKeyAsInt(ApplicationCommon.DefaultRowCountKey);
                }
                return Convert.ToInt32(HttpContext.Current.Session["DefaultRowCount"]);
            }
            set
            {
                HttpContext.Current.Session["DefaultRowCount"] = value;
            }
        }

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

        public static DataTable SiteMenu
        {
            get
            {
                if (HttpContext.Current.Session["SiteMenu"] == null)
                {
                    return null;
                }
                else
                {
                    return (DataTable)(HttpContext.Current.Session["SiteMenu"]);
                }
            }
            set
            {
                HttpContext.Current.Session["SiteMenu"] = value;
            }
        }

    }
}