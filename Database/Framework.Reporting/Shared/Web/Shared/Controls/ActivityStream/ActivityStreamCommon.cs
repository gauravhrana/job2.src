using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.ActivityStream
{
    public class ActivityStreamCommon
    {

        #region constants

        // ActivityStream Keys
        public const string DateRangeKey                   = "DateRange";
        public const string BackGroungColorKey             = "BackgroundColor";
        public const string HeightKey                      = "Height";
        public const string DataTypeKey                    = "DataType";
        public const string WidthKey                       = "Width";
        public const string ActivityStreamAuditId          = "ActivityStreamAuditId";
        public const string ActivityStreamPageSize         = "ActivityStreamPageSize";
        public const string ActivityStreamTitle            = "ActivityStreamTitle";
        public const string ActivityStreamExcludedEntities = "ActivityStreamExcludedEntities";
        public const string ActivityStreamPagingStyle      = "ActivityStreamPagingStyle";
        public const string ActivityStreamGroupInterval    = "ActivityStreamGroupInterval";

        #endregion

        #region public methods

        public static Dictionary<string, string> GetUserPreferencesForActivityStream(string upCategory)
        {
            var settings = new Dictionary<string, string>();

            //Get Current User's Preferences
            if (SessionVariables.UserPreferences == null)
            {
                PerferenceUtility.RefreshUserPreferencesCache();
            }

            var listKeys = new List<string>()
            {
                    DateRangeKey              
                ,   BackGroungColorKey        
                ,   WidthKey                  
                ,   DataTypeKey               
                ,   HeightKey                 
                ,   ActivityStreamAuditId     
                ,   ActivityStreamPageSize
                ,   ActivityStreamTitle
                ,   ActivityStreamPagingStyle
                ,   ActivityStreamGroupInterval
            };

                
            var userPreferences = SessionVariables.UserPreferences;
            foreach (var upKey in listKeys)
            {
                settings.Add(upKey, PerferenceUtility.GetUserPreferenceByKey(upKey, upCategory));
            }

            return settings;
        }

        public static DataTable GetExcludedSystemEntities(string categoryName)
        {
            var upValue = PerferenceUtility.GetUserPreferenceByKey(ActivityStreamExcludedEntities, categoryName, categoryName);

            var data = new UserPreferenceSelectedItemDataModel();
            data.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
            data.ParentKey = upValue;
            var dt = UserPreferenceSelectedItemDataManager.Search(data, SessionVariables.RequestProfile);
            if (dt != null)
            {
                var rowFilter = String.Empty;
                rowFilter = " UserPreferenceKey = '"+ ActivityStreamExcludedEntities +"' ";
                var dv = dt.DefaultView;
                dv.RowFilter = rowFilter;
                return dv.ToTable();
            }
            return dt;
        }

        public static void SetExcludedSystemEntities(List<string> lst, string categoryName)
        {
            var upValue = PerferenceUtility.GetUserPreferenceByKey(ActivityStreamExcludedEntities, categoryName, categoryName);
            var upkId = PerferenceUtility.GetUserPreferenceKey(ActivityStreamExcludedEntities);

            var data = new UserPreferenceSelectedItemDataModel();
            data.UserPreferenceKeyId = upkId;
			UserPreferenceSelectedItemDataManager.Delete(data, SessionVariables.RequestProfile);

            foreach (var strValue in lst)
            {
                data = new UserPreferenceSelectedItemDataModel();
                data.UserPreferenceKeyId = upkId;
                data.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
                data.ParentKey = upValue;
                data.Value = strValue;
                data.SortOrder = 1;
				UserPreferenceSelectedItemDataManager.Create(data, SessionVariables.RequestProfile);
            }
        }

        #endregion

    }
}