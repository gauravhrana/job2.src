using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Framework.Components
{
    public static class DefaultDataRules
    {              

        public static int? CheckAndGetEntityId(string primaryKeyValue, bool isTesting)
        {
            int? appId = null;

            // this indicates that application is in DEBUG mode to DB procedure, so it will generate next value as per the developer's range.
			#if DEBUG        
            // when primaryKey Value is not null and is passed by user we check whether it is positive or negative. 
            // If it is positive we make it negative.
            if (!string.IsNullOrEmpty(primaryKeyValue))
            {
                appId = Convert.ToInt32(primaryKeyValue);
                if (appId > 0)
                {
                    appId = appId * (-1);
                }
            }
            else
            {
                // when primaryKey Value is null we check whether Session Testing flag. 
                // if its true then we set the value to indeicate to mode to DB procedure, so it will generate next value as per the developer's range.
                if (isTesting)
                {
                    appId = -999999;
                }
            }
			#else
																																																						// when primary key value is not null, we simply convert it to int. i.e. user can pass poitive as well as negative number.
            if (!string.IsNullOrEmpty(primaryKeyValue))
            {
                appId = Convert.ToInt32(primaryKeyValue);
            }
            else
            {
                appId = null;
            }
			#endif

            return appId;
        }

        private static int GetNextValidId(DataTable dt, int tempId, string entity)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[entity + "Id"].ToString().Equals(tempId.ToString()))
                {
                    tempId -= 1;
                    return GetNextValidId(dt, tempId, entity);
                }
            }

            return tempId;
        }

        public static string CheckAndGetRequestedBy(string name, string requestedBy)
        {
            if (string.IsNullOrEmpty(requestedBy) && !(string.IsNullOrEmpty(name)))
            {
                return name;
            }
            else if (!string.IsNullOrEmpty(requestedBy))
            {
                return requestedBy;
            }
            return "";

        }

        public static string CheckAndGetPrimaryDeveloper(string name, string primaryDeveloper)
        {
            if (string.IsNullOrEmpty(primaryDeveloper) && !(string.IsNullOrEmpty(name)))
            {
                return name;
            }
            else if (!string.IsNullOrEmpty(primaryDeveloper))
            {
                return primaryDeveloper;
            }
            return "";

        }

        public static string CheckAndGetDescription(string name, string description)
        {
            if (string.IsNullOrEmpty(description) && !(string.IsNullOrEmpty(name)))
            {
                return name;
            }
            else if (!string.IsNullOrEmpty(description))
            {
                return description;
            }
            return "";

        }

        public static int CheckAndGetSortOrder(string sortorder)
        {
            var _sortorder = 1;

            int.TryParse(sortorder, out _sortorder);
            if (_sortorder <= 0)
                _sortorder = 1;
            return _sortorder;
        }

        public static int CheckAndGetRequestedDate(string requestedDate)
        {
            var _requestedDate = 1;

            int.TryParse(requestedDate, out _requestedDate);
            if (_requestedDate <= 0)
                _requestedDate = 1;
            return _requestedDate;
        }

    }
}
