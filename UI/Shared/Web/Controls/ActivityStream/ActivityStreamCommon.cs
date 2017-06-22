using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.WebCommon.UI.Web;

namespace WebCommon.UI.Web
{
    public class ActivityStreamCommon
    {

        #region constants

        // ActivityStream Keys
        public const string DateRangeKey           = "DateRange";
        public const string BackGroungColorKey     = "BackgroundColor";
        public const string HeightKey              = "Height";
        public const string DataTypeKey            = "DataType";
        public const string WidthKey               = "Width";
        public const string ActivityStreamAuditId  = "ActivityStreamAuditId";
        public const string ActivityStreamPageSize = "ActivityStreamPageSize";
        public const string ActivityStreamTitle    = "ActivityStreamTitle";

        #endregion

        #region public methods

        public static Dictionary<string, string> GetUserPreferencesForActivityStream(string upCategory)
        {
            var settings = new Dictionary<string, string>();

            //Get Current User's Preferences
            if (SessionVariables.UserPreferences == null)
            {
                ApplicationCommon.SetUserPreferences();
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
            };

                
            var userPreferences = SessionVariables.UserPreferences;
            foreach (var upKey in listKeys)
            {
                settings.Add(upKey, ApplicationCommon.GetUserPreferenceByKey(upKey, upCategory));
            }

            return settings;
        }

        #endregion

    }
}