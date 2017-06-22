using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PrismSample.Infrastructure
{
    public class UPreferenceCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UPreference
    {
        public int Id
        {
            set;
            get;
        }

        public int UserPreferenceKeyId
        {
            get;
            set;
        }

        public int UserPreferenceCategoryId
        {
            get;
            set;
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
    }

    public class UserPerferenceUtility
    {

        #region Static Variables

        private static List<UPreferenceCategory> UserPreferenceCategories;
        private static List<UPreference> UserPreferences;

        #endregion

        #region User Preferenc Category Methods

        public static void RefreshUserPreferenceCategoriesCache()
        {
            var data = new UserPreferenceCategoryDataModel();
            var list = UserPreferenceCategoryDataManager.GetEntityDetails(data, ApplicationCommon.ReqProfile, 0);

            var result = list.Select(item => new UPreferenceCategory()
            {
                Name = item.Name
                                ,
                Id = item.UserPreferenceCategoryId.Value
            })
                            .ToList();

            UserPreferenceCategories = result;
        }

        public static int CreateUserPreferenceCategoryIfNotExists(string upCategoryName, string upDescription)
        {
            if (string.IsNullOrEmpty(upCategoryName))
                return 0;

            //Check if UserPreferenceCategory exists
            if (UserPreferenceCategories == null)
            {
                RefreshUserPreferenceCategoriesCache();
            }

            var userPreferenceCategories = UserPreferenceCategories;

            var preferenceCategory = userPreferenceCategories.FirstOrDefault(item => item.Name.Equals(upCategoryName, StringComparison.OrdinalIgnoreCase));

            if (preferenceCategory != null)
            {
                return preferenceCategory.Id;
            }

            //Create new UserPreferenceCategory for the given upCategoryName
            var data = new UserPreferenceCategoryDataModel();
            data.Name = upCategoryName;
            data.Description = upDescription;
            data.SortOrder = 1;

            // not sure if SystemAuditId if this is corect?
            var upCategoryId = UserPreferenceCategoryDataManager.Create(data, ApplicationCommon.ReqProfile);

            userPreferenceCategories.Add(new UPreferenceCategory()
            {
                    Name = upCategoryName
                ,   Id = upCategoryId
            });

            UserPreferenceCategories = userPreferenceCategories;

            return upCategoryId;
        }

        #endregion

        #region User Preference Methods

        public static void RefreshUserPreferencesCache()
        {
            //Log4Net.LogInfo("SetUserPreferences Start");

            var lst = new List<UPreference>();

            var data = new UserPreferenceDataModel();

            data.ApplicationUserId = ApplicationCommon.ReqProfile.AuditId;
            data.ApplicationId = ApplicationCommon.ReqProfile.ApplicationId;

            var listUP = UserPreferenceDataManager.GetEntityDetails(data, ApplicationCommon.ReqProfile, 0);

            if (listUP != null && listUP.Count > 0)
            {
                foreach (var upItem in listUP)
                {
                    var preference                      = new UPreference();

                    preference.ApplicationUserId        = upItem.ApplicationUserId.Value;
                    preference.UserPreferenceKeyId      = upItem.UserPreferenceKeyId.Value;
                    preference.UserPreferenceCategoryId = upItem.UserPreferenceCategoryId.Value;
                    preference.DataType                 = upItem.UserPreferenceDataType;
                    preference.Id                       = upItem.UserPreferenceId.Value;
                    preference.UserPreferenceKey        = upItem.UserPreferenceKey;
                    preference.UserPreferenceCategory   = upItem.UserPreferenceCategory;
                    preference.value                    = upItem.Value;

                    lst.Add(preference);
                }
            }

            UserPreferences = lst;
        }

        public static string ResetUserPreferenceByKey(string userPreferenceKey, string userPreferenceCategory = "General")
        {
            var finalValue = string.Empty;
            var userPreferenceCategoryId = 0;

            if (!string.IsNullOrEmpty(userPreferenceCategory))
            {
                userPreferenceCategoryId = CreateUserPreferenceCategoryIfNotExists(userPreferenceCategory, userPreferenceCategory);
            }

            var dataUPKey = new UserPreferenceKeyDataModel();
            dataUPKey.Name = userPreferenceKey;

            var upKeys = UserPreferenceKeyDataManager.GetEntityDetails(dataUPKey, ApplicationCommon.ReqProfile);
            if (upKeys.Count > 0)
            {
                finalValue = upKeys[0].Value;
            }
            else
            {
                throw new Exception("UPKey '" + userPreferenceKey + "' does not exist in current application");
            }

            UpdateUserPreference(userPreferenceCategory, userPreferenceKey, finalValue);

            return finalValue;
        }

        public static string GetUserPreferenceByKey(string userPreferenceKey, string userPreferenceCategory = "General", string upValue = "")
        {
            var userPreferenceValue = String.Empty;

            if (UserPreferences == null)
            {
                RefreshUserPreferencesCache();
            }

            var userPreferences = UserPreferences;

            var preference = userPreferences.Find(item => item.UserPreferenceKey.Equals(userPreferenceKey, StringComparison.OrdinalIgnoreCase)
                                                                    && item.UserPreferenceCategory.Equals(userPreferenceCategory, StringComparison.OrdinalIgnoreCase));

            //check if current user has preference of for the key
            if (preference != null)
            {
                //if yes then assign it to a variable
                userPreferenceValue = preference.value;
            }
            else
            {
                // get info from database
                var userPreferenceCategoryId = 0;
                if (!string.IsNullOrEmpty(userPreferenceCategory))
                {
                    userPreferenceCategoryId = CreateUserPreferenceCategoryIfNotExists(userPreferenceCategory, userPreferenceCategory);
                }

                //if not then find the default values for Prefrence
                var defValue = String.Empty;
                var defDataTypeId = 0;
                var userPreferenceKeyId = 0;

                var dataUPKey = new UserPreferenceKeyDataModel();
                dataUPKey.Name = userPreferenceKey;

                var upKeys = UserPreferenceKeyDataManager.GetEntityDetails(dataUPKey, ApplicationCommon.ReqProfile);
                if (upKeys.Count > 0)
                {
                    userPreferenceKeyId = upKeys[0].UserPreferenceKeyId.Value;

                    if (String.IsNullOrEmpty(upValue))
                    {
                        defValue = upKeys[0].Value;
                    }
                    else
                    {
                        defValue = upValue;
                    }

                    defDataTypeId = upKeys[0].DataTypeId.Value;
                }
                else
                {
                    throw new Exception("UPKey '" + userPreferenceKey + "' does not exist in current application");
                }

                // insert new preference for current user using default values
                var dt = new UserPreferenceDataModel();

                dt.ApplicationUserId = ApplicationCommon.ReqProfile.AuditId;
                dt.UserPreferenceKeyId = userPreferenceKeyId;
                dt.Value = defValue;
                dt.DataTypeId = defDataTypeId;
                dt.ApplicationId = ApplicationCommon.ReqProfile.ApplicationId;
                dt.UserPreferenceCategoryId = userPreferenceCategoryId;

                var upId = UserPreferenceDataManager.Create(dt, ApplicationCommon.SystemRequestProfile);

                userPreferenceValue = defValue;

                // Add the item back to cache as to avoid reloading whole list of UPs again.
                var upreferenceItem = new UPreference();
                upreferenceItem.ApplicationUserId = ApplicationCommon.ReqProfile.AuditId;
                upreferenceItem.DataType = string.Empty;
                upreferenceItem.Id = upId;
                upreferenceItem.UserPreferenceKey = userPreferenceKey;
                upreferenceItem.UserPreferenceCategory = userPreferenceCategory;
                upreferenceItem.value = defValue;

                userPreferences.Add(upreferenceItem);

            }

            return userPreferenceValue;
        }

        public static bool GetUserPreferenceByKeyAsBoolean(string userPreferenceKey, string userPreferenceCategory = "General")
        {
            var value = GetUserPreferenceByKey(userPreferenceKey, userPreferenceCategory);

            bool answer;

            bool.TryParse(value, out answer);

            return answer;
        }

        public static int GetUserPreferenceByKeyAsInt(string userPreferenceKey, string userPreferenceCategory = "General")
        {
            var value = GetUserPreferenceByKey(userPreferenceKey, userPreferenceCategory);

            int answer;

            int.TryParse(value, out answer);

            return answer;
        }

        public static double GetUserPreferenceByKeyAsDouble(string userPreferenceKey, string userPreferenceCategory = "General")
        {
            var value = GetUserPreferenceByKey(userPreferenceKey, userPreferenceCategory);

            double answer;

            double.TryParse(value, out answer);

            return answer;
        }

        public static void UpdateUserPreference(string upCategory, string upKey, string upValue)
        {

            if (UserPreferences == null)
            {
                RefreshUserPreferencesCache();
            }

            var userPreferences = UserPreferences;
            var preference = userPreferences.Find(item => item.UserPreferenceKey.Equals(upKey, StringComparison.CurrentCultureIgnoreCase)
                                            && item.UserPreferenceCategory.Equals(upCategory, StringComparison.CurrentCultureIgnoreCase));

            //check if current user has preference of for the key
            if (preference == null)
            {
                GetUserPreferenceByKey(upKey, upCategory);
                userPreferences = UserPreferences;
                preference = userPreferences.Find(item => item.UserPreferenceKey.Equals(upKey, StringComparison.CurrentCultureIgnoreCase)
                                            && item.UserPreferenceCategory.Equals(upCategory, StringComparison.CurrentCultureIgnoreCase));
            }

            //compare with cache if value is actually updated, if not do not call the update funtion to avoid unnecessary db call.
            // also if value is actually update, reflect the change directly to cache so we can avoid bringing back whole set of UP from DB.
            if (preference.value != upValue)
            {
                var data = new UserPreferenceDataModel();
                data.UserPreferenceId = preference.Id;
                data.Value = upValue;

                UserPreferenceDataManager.UpdateValueOnly(data, ApplicationCommon.SystemRequestProfile);

                //update it back to cache so we do not need to reload all the UPs again
                preference.value = upValue;
            }
        }

        #endregion

        public static int GetUserPreferenceKey(string userPreferenceKey)
        {
            var userPreferenceKeyId = 0;

            if (!String.IsNullOrEmpty(userPreferenceKey))
            {
                var data = new UserPreferenceKeyDataModel();

                data.Name = userPreferenceKey;

                var dt = UserPreferenceKeyDataManager.Search(data, ApplicationCommon.SystemRequestProfile);

                if (dt != null && dt.Rows.Count > 0)
                {
                    userPreferenceKeyId = Convert.ToInt32(dt.Rows[0][UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId]);
                }
            }

            return userPreferenceKeyId;
        }

    }
}
