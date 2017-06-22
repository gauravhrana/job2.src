//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace TaskTimeTracker.Components.BusinessLayer
//{
//    public static class DefaultDataRules
//    {

//        public static int? CheckAndGetApplicationId(string primaryKeyValue)
//        {
//            int? appId = null;

//            // this indicates that application is in DEBUG mode to DB procedure, so it will generate next value as per the developer's range.
//            #if DEBUG               
//            appId = -999999;
//            if (!string.IsNullOrEmpty(primaryKeyValue))
//            {
//                appId = Convert.ToInt32(primaryKeyValue);
//                if (appId > 0)
//                {
//                    appId = appId * (-1);
//                }
//            }
//            else
//            {
//                appId = null;
//            }
//            #else
//                if (!string.IsNullOrEmpty(primaryKeyValue))
//                {
//                    appId = Convert.ToInt32(primaryKeyValue);
//                }
//                else
//                {
//                    appId = null;
//                }
//            #endif

//            return appId;
//        }

//        public static string CheckAndGetRequestedBy(string name, string requestedBy)
//        {
//            if (string.IsNullOrEmpty(requestedBy) && !(string.IsNullOrEmpty(name)))
//            {
//                return name;
//            }
//            else if (!string.IsNullOrEmpty(requestedBy))
//            {
//                return requestedBy;
//            }
//            return "";

//        }

//        public static string CheckAndGetPrimaryDeveloper(string name, string primaryDeveloper)
//        {
//            if (string.IsNullOrEmpty(primaryDeveloper) && !(string.IsNullOrEmpty(name)))
//            {
//                return name;
//            }
//            else if (!string.IsNullOrEmpty(primaryDeveloper))
//            {
//                return primaryDeveloper;
//            }
//            return "";

//        }

//        public static string CheckAndGetDescription(string name, string description)
//        {
//            if (string.IsNullOrEmpty(description) && !(string.IsNullOrEmpty(name)))
//            {
//                return name;
//            }
//            else if (!string.IsNullOrEmpty(description))
//            {
//                return description;
//            }
//            return "";

//        }

//        public static int CheckAndGetSortOrder(string sortorder)
//        {
//            int _sortorder = 1;

//            int.TryParse(sortorder, out _sortorder);
//            if (_sortorder <= 0)
//                _sortorder = 1;
//            return _sortorder;
//        }

//        public static int CheckAndGetRequestedDate(string requestedDate)
//        {
//            int _requestedDate = 1;

//            int.TryParse(requestedDate, out _requestedDate);
//            if (_requestedDate <= 0)
//                _requestedDate = 1;
//            return _requestedDate;
//        }

//    }
//}
