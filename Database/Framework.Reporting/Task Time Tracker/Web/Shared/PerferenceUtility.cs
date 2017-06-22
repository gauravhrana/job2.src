﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using UserPreferenceCategory = DataModel.Framework.Configuration;
using DataModel.Framework.Configuration;

namespace Shared.WebCommon.UI.Web
{
	public class PerferenceUtility
	{

		public static int CreateUserPreferenceCategoryIfNotExists(string upCategoryName, string upDescription)
		{
			if (string.IsNullOrEmpty(upCategoryName))
				return 0;

			//Check if UserPreferenceCategory exists
			if (SessionVariables.UserPreferenceCategories == null)
			{
				RefreshUserPreferenceCategoriesCache();
			}

			var userPreferenceCategories = SessionVariables.UserPreferenceCategories;

			var preferenceCategory = userPreferenceCategories.FirstOrDefault(item => item.Name.Equals(upCategoryName, StringComparison.OrdinalIgnoreCase));

			if (preferenceCategory != null)
			{
				return preferenceCategory.Id;
			}

			//Create new UserPreferenceCategory for the given upCategoryName
			var data         = new UserPreferenceCategoryDataModel();
			data.Name        = upCategoryName;
			data.Description = upDescription;
			data.SortOrder   = 1;

			// not sure if SystemAuditId if this is corect?
			var upCategoryId = UserPreferenceCategoryDataManager.Create(data, SessionVariables.SystemRequestProfile);

			userPreferenceCategories.Add(new UPreferenceCategory()
						{
								Name			=	upCategoryName
							,	Id				=	upCategoryId
						});

			SessionVariables.UserPreferenceCategories = userPreferenceCategories;

			return upCategoryId;
		}

		public static int CreateUserPreferenceCategoryByApplicationIfNotExists(string upCategoryName, string upDescription, int application)
		{
			var userPreferenceCategories = SessionVariables.UserPreferenceCategories;
			var upCategoryId = 0;

			var dataUPCat = new UserPreferenceCategoryDataModel();
			dataUPCat.Name = upCategoryName;
			dataUPCat.ApplicationId = application;

				var dt1 = UserPreferenceCategoryDataManager.Search(dataUPCat, SessionVariables.SystemRequestProfile);

				if (dt1.Rows.Count == 0)
				{

					//Create new UserPreferenceCategory for the given upCategoryName
					var data = new UserPreferenceCategoryDataModel();
					data.Name = upCategoryName;
					data.Description = upDescription;
					data.SortOrder = 1;
					data.ApplicationId = application;

					// not sure if SystemAuditId if this is corect?
					upCategoryId = UserPreferenceCategoryDataManager.Create(data, SessionVariables.SystemRequestProfile);

					userPreferenceCategories.Add(new UPreferenceCategory()
					{
						Name = upCategoryName
						,
						Id = upCategoryId
					});

					SessionVariables.UserPreferenceCategories = userPreferenceCategories;

					return upCategoryId;
				}
				else
				{
					return Convert.ToInt32(dt1.Rows[0][UserPreferenceCategoryDataModel.DataColumns.UserPreferenceCategoryId]);
				}
		}

		#region UserPreference

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

            var dtUPKey = UserPreferenceKeyDataManager.Search(dataUPKey, SessionVariables.SystemRequestProfile);
            if (dtUPKey.Rows.Count > 0)
            {
                finalValue = Convert.ToString(dtUPKey.Rows[0][UserPreferenceCategory.UserPreferenceKeyDataModel.DataColumns.Value]);
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

			if (SessionVariables.UserPreferences == null)
			{
				RefreshUserPreferencesCache();
			}

			var userPreferences = SessionVariables.UserPreferences;

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
				var defValue            = String.Empty;
				var defDataTypeId       = 0;
				var userPreferenceKeyId = 0;

				var dataUPKey = new UserPreferenceKeyDataModel();
				dataUPKey.Name = userPreferenceKey;

				var dt1 = UserPreferenceKeyDataManager.Search(dataUPKey, SessionVariables.SystemRequestProfile);
                if (dt1.Rows.Count > 0)
                {
                    userPreferenceKeyId = Convert.ToInt32(dt1.Rows[0][UserPreferenceCategory.UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId]);

                    if (String.IsNullOrEmpty(upValue))
                    {
                        defValue = Convert.ToString(dt1.Rows[0][UserPreferenceCategory.UserPreferenceKeyDataModel.DataColumns.Value]);
                    }
                    else
                    {
                        defValue = upValue;
                    }

                    defDataTypeId = Convert.ToInt32(dt1.Rows[0][UserPreferenceCategory.UserPreferenceKeyDataModel.DataColumns.DataTypeId]);
                }
                else
                {
                    throw new Exception("UPKey '" + userPreferenceKey + "' does not exist in current application");
                }

				// insert new preference for current user using default values
				var dt                      = new UserPreferenceDataModel();

				dt.ApplicationUserId        = SessionVariables.RequestProfile.AuditId;
				dt.UserPreferenceKeyId      = userPreferenceKeyId;
				dt.Value                    = defValue;
				dt.DataTypeId               = defDataTypeId;
				dt.ApplicationId            = SessionVariables.RequestProfile.ApplicationId;
				dt.UserPreferenceCategoryId = userPreferenceCategoryId;

				var upId                    = UserPreferenceDataManager.Create(dt, SessionVariables.SystemRequestProfile);

				userPreferenceValue                    = defValue;

				// Add the item back to cache as to avoid reloading whole list of UPs again.
				var upreferenceItem                    = new UPreference();
				upreferenceItem.ApplicationUserId      = SessionVariables.RequestProfile.AuditId;
				upreferenceItem.DataType               = string.Empty;
				upreferenceItem.Id                     = upId;
				upreferenceItem.UserPreferenceKey      = userPreferenceKey;
				upreferenceItem.UserPreferenceCategory = userPreferenceCategory;
				upreferenceItem.value                  = defValue;

				userPreferences.Add(upreferenceItem);

			}

			return userPreferenceValue;
		}

		public static TabOrientation GetUserPreferenceByKeyAsTabOrientation(string userPreferenceKey, string userPreferenceCategory = "General")
		{
			var value = GetUserPreferenceByKey(userPreferenceKey, userPreferenceCategory);
			return (TabOrientation)Enum.Parse(typeof(TabOrientation), value);
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

		public static int GetUserPreferenceKey(string userPreferenceKey)
		{
			var userPreferenceKeyId = 0;

			if (!String.IsNullOrEmpty(userPreferenceKey))
			{
				var data = new UserPreferenceCategory.UserPreferenceKeyDataModel();

				data.Name = userPreferenceKey;

				var dt = UserPreferenceKeyDataManager.Search(data, SessionVariables.SystemRequestProfile);

				if (dt != null && dt.Rows.Count > 0)
				{
					userPreferenceKeyId = Convert.ToInt32(dt.Rows[0][UserPreferenceCategory.UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId]);
				}
			}

			return userPreferenceKeyId;
		}

		#endregion

		public static double GetUserTimeZoneDifference()
		{
			if (SessionVariables.UserTimeZoneDifference == null)
			{
				var userTimeZoneId = GetUserPreferenceByKeyAsInt(ApplicationCommon.UserTimeZone);
				var data = new TimeZoneDataModel();
				data.TimeZoneId = userTimeZoneId;

				var dt = TimeZoneDataManger.GetDetails(data, SessionVariables.SystemRequestProfile);
				
				if (dt != null && dt.Rows.Count > 0)
				{
					SessionVariables.UserTimeZoneDifference = Convert.ToDouble(dt.Rows[0][TimeZoneDataModel.DataColumns.TimeDifference]);
				}
			}

			return SessionVariables.UserTimeZoneDifference.Value;
		}

		public static string GetUserTimeZoneDateAsString(object dtm)
		{
			var date = String.Empty;

			if (dtm != DBNull.Value)
			{
				var userTimeZoneDifference = GetUserTimeZoneDifference();
				var appTimeZoneDifference  = Convert.ToDouble(ConfigurationManager.AppSettings["UTCTimeZoneDifference"]);

				var dtmNow                 = Convert.ToDateTime(dtm).AddHours(appTimeZoneDifference * -1);
				dtmNow                     = dtmNow.AddHours(userTimeZoneDifference);
				date                       = dtmNow.ToString();
			}
			
			return date;
		}

		public static bool UserPreferenceExists(string userPreferenceName, string key)
		{
			var userPreferences = SessionVariables.UserPreferences;
			var preference = userPreferences.Find(item => item.UserPreferenceKey.Equals(userPreferenceName, StringComparison.CurrentCultureIgnoreCase) 
													&& item.UserPreferenceCategory.Equals(key, StringComparison.CurrentCultureIgnoreCase));

			if (preference != null)
			{
				return true;
			}

			return false;
		}

		private static List<UPreference> GetUserPreferenceList(DataTable dt)
		{
			var lst = new List<UPreference>();

			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					var preference						= new UPreference();
					preference.ApplicationUserId		= Convert.ToInt32(dr[UserPreferenceDataModel.DataColumns.ApplicationUserId]);
					preference.DataType					= Convert.ToString(dr[UserPreferenceDataModel.DataColumns.UserPreferenceDataType]);
					preference.Id						= Convert.ToInt32(dr[UserPreferenceDataModel.DataColumns.UserPreferenceId]);
					preference.UserPreferenceKey		= Convert.ToString(dr[UserPreferenceDataModel.DataColumns.UserPreferenceKey]);
					preference.UserPreferenceCategory	= Convert.ToString(dr[UserPreferenceDataModel.DataColumns.UserPreferenceCategory]);
					preference.value					= Convert.ToString(dr[UserPreferenceDataModel.DataColumns.Value]);
					lst.Add(preference);
				}
			}

			return lst;
		}

		public static void UpdateUserPreference(string upCategory, string upKey, string upValue)
		{
			//var userPreferenceValue = String.Empty;
			//var auditId = SessionVariables.RequestProfile.AuditId;

			if (SessionVariables.UserPreferences == null)
			{
				RefreshUserPreferencesCache();
			}

			var userPreferences = SessionVariables.UserPreferences;
			var preference = userPreferences.Find(item => item.UserPreferenceKey.Equals(upKey, StringComparison.CurrentCultureIgnoreCase) 
											&& item.UserPreferenceCategory.Equals(upCategory, StringComparison.CurrentCultureIgnoreCase));

			//check if current user has preference of for the key
			if (preference == null)
			{
				GetUserPreferenceByKey(upKey, upCategory);
				userPreferences = SessionVariables.UserPreferences;
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

				UserPreferenceDataManager.UpdateValueOnly(data, SessionVariables.SystemRequestProfile);
                    
                //update it back to cache so we do not need to reload all the UPs again
                preference.value = upValue;
            }
			
		}

		public static void UpdateApplicationUserPreference(string upCategory, string upKey, string upValue)
		{
			//var userPreferenceValue = String.Empty;
			//var auditId = ApplicationCommon.SystemAuditId;

			if (SessionVariables.ApplicationUserPreferences == null)
			{
				RefreshApplicationUserPreferencesCache();
			}

			var userPreferences = SessionVariables.ApplicationUserPreferences;

			//TODO: Review
			if (userPreferences == null)
			{
				return;
			}

			var preference = userPreferences.Find(item => item.UserPreferenceKey.Equals(upKey, StringComparison.CurrentCultureIgnoreCase) 
												&& item.UserPreferenceCategory.Equals(upCategory, StringComparison.CurrentCultureIgnoreCase));

			//check if current user has preference of for the key
			if (preference == null)
			{
				GetApplicationUserPreferenceByKey(upKey, upCategory);
				userPreferences = SessionVariables.ApplicationUserPreferences;
				preference = userPreferences.Find(item => item.UserPreferenceKey.Equals(upKey, StringComparison.CurrentCultureIgnoreCase) && item.UserPreferenceCategory.Equals(upCategory, StringComparison.CurrentCultureIgnoreCase));
			}

            //compare with cache if value is actually updated, if not do not call the update funtion to avoid unnecessary db call.
            // also if value is actually update, reflect the change directly to cache so we can avoid bringing back whole set of UP from DB.
            if (preference.value != upValue)
            {
                var data = new UserPreferenceDataModel();
                data.UserPreferenceId = preference.Id;
                data.Value = upValue;

				UserPreferenceDataManager.UpdateValueOnly(data, SessionVariables.SystemRequestProfile);

                //update it back to cache so we do not need to reload all the UPs again
                preference.value = upValue;
            }

		}

        public static void UpdateApplicationInstancePreference(string upCategory, string upKey, string upValue)
        {
            //var userPreferenceValue = String.Empty;
            //var auditId = ApplicationCommon.SystemAuditId;

            if (ApplicationVariables.ApplicationInstancePreferences == null)
            {
                RefreshApplicationInstancePreferences();
            }

            var userPreferences = ApplicationVariables.ApplicationInstancePreferences;
			var preference = userPreferences.Find(item => item.UserPreferenceKey.Equals(upKey, StringComparison.CurrentCultureIgnoreCase) && item.UserPreferenceCategory.Equals(upCategory, StringComparison.CurrentCultureIgnoreCase));

            //check if current user has preference of for the key
            if (preference == null)
            {
                GetApplicationInstancePreferenceByKey(upKey, upCategory);
                userPreferences = ApplicationVariables.ApplicationInstancePreferences;
				preference = userPreferences.Find(item => item.UserPreferenceKey.Equals(upKey, StringComparison.CurrentCultureIgnoreCase) && item.UserPreferenceCategory.Equals(upCategory, StringComparison.CurrentCultureIgnoreCase));
            }

            //compare with cache if value is actually updated, if not do not call the update funtion to avoid unnecessary db call.
            // also if value is actually update, reflect the change directly to cache so we can avoid bringing back whole set of UP from DB.
            if (preference.value != upValue)
            {
                var data = new UserPreferenceDataModel();
                data.UserPreferenceId = preference.Id;
                data.Value = upValue;

				UserPreferenceDataManager.UpdateValueOnly(data, SessionVariables.SystemRequestProfile);

                //update it back to cache so we do not need to reload all the UPs again
                preference.value = upValue;
            }
        }
		
		public static string GetUserTheme()
		{
			var strAppTheme = Convert.ToString(HttpContext.Current.Application["Branding"]);
			var strUserTheme = String.Empty;

			// Get theme from Session object if it is already set
			if (!String.IsNullOrEmpty(SessionVariables.UserTheme))
				strUserTheme = SessionVariables.UserTheme;
			else
			{
				// Get Theme from User Preference if not alreday set in session
				//UserPreference table retrieval

				if (SessionVariables.UserPreferences == null)
				{
					RefreshUserPreferencesCache();
				}

				var userPreferences = SessionVariables.UserPreferences;
				var preference = userPreferences.Find(item => item.UserPreferenceKey == "ImageTheme");

				if (preference == null || string.IsNullOrEmpty(preference.value))
				{
					// if user preference for theme is not set then fetch default theme from application object.
					var strTmpArry = strAppTheme.Split(new[] { '/' });

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

		public static void RefreshUserPreferenceCategoriesCache()
		{
			var data = new UserPreferenceCategoryDataModel();
			var list = UserPreferenceCategoryDataManager.GetEntityDetails(data, SessionVariables.SystemRequestProfile, 0);

			var result = list.Select(item => new UPreferenceCategory()
							{
									Name			= item.Name
								,	Id				= item.UserPreferenceCategoryId.Value
							})
							.ToList();

			SessionVariables.UserPreferenceCategories = result;
		}

        public static void RefreshUserPreferencesCache()
        {
            //Log4Net.LogInfo("SetUserPreferences Start");

            var lst = new List<UPreference>();

            var data = new UserPreferenceDataModel();

            data.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
			data.ApplicationId     = SessionVariables.RequestProfile.ApplicationId;

            var dt = UserPreferenceDataManager.Search(data, SessionVariables.SystemRequestProfile);

            lst = GetUserPreferenceList(dt);

            SessionVariables.UserPreferences = lst;

            //Log4Net.LogInfo("SetUserPreferences End");
        }

        public static void RefreshApplicationUserPreferencesCache()
        {
            var lst = new List<UPreference>();

            var data = new UserPreferenceDataModel();

			data.ApplicationUserId = SessionVariables.SystemRequestProfile.AuditId;

            var dt = UserPreferenceDataManager.Search(data, SessionVariables.SystemRequestProfile);

            lst = GetUserPreferenceList(dt);

            SessionVariables.ApplicationUserPreferences = lst;
        }

        public static void RefreshApplicationInstancePreferences()
        {
            var lst = new List<UPreference>();

            var data = new UserPreferenceDataModel();

            data.ApplicationUserId = ApplicationCommon.ApplicationInstanceId;
            data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

            var dt = UserPreferenceDataManager.Search(data, SessionVariables.SystemRequestProfile);

            lst = GetUserPreferenceList(dt);

            ApplicationVariables.ApplicationInstancePreferences = lst;
        }

        public static void RefreshApplicationUserPreferences(int userId)
        {
            var lst = new List<UPreference>();

            var data = new UserPreferenceDataModel();

            data.ApplicationUserId = userId;
            data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

            var dt = UserPreferenceDataManager.Search(data, SessionVariables.SystemRequestProfile);

            lst = GetUserPreferenceList(dt);

            SessionVariables.ApplicationUserPreferences = lst;
        }

		public static string GetApplicationUserPreferenceByKey(string userPreferenceKey, string userPreferenceCategory = "General", string upValue = "")
		{
			var userPreferenceValue = String.Empty;
			
			if (SessionVariables.ApplicationUserPreferences == null || SessionVariables.ApplicationUserPreferences.Count == 0)
			{
				RefreshApplicationUserPreferencesCache();
			}

			var userPreferences = SessionVariables.ApplicationUserPreferences;

			var preference = userPreferences.FirstOrDefault(item => item.UserPreferenceKey.Equals(userPreferenceKey, StringComparison.OrdinalIgnoreCase)
																	&& item.UserPreferenceCategory.Equals(userPreferenceCategory, StringComparison.OrdinalIgnoreCase));
	
			// check if current user has preference of for the key
			if (preference != null)
			{
				// if yes then assign it to a variable
				userPreferenceValue = preference.value;
			}
			else
			{
                var userPreferenceCategoryId = 0;

                if (!string.IsNullOrEmpty(userPreferenceCategory))
                {
                    userPreferenceCategoryId = CreateUserPreferenceCategoryIfNotExists(userPreferenceCategory, userPreferenceCategory);
                }

				//if not then find the default values for Prefrence
				var defValue = String.Empty;
				var defDataTypeId = 0;
				var userPreferenceKeyId = 0;

				var dt1 = UserPreferenceKeyDataManager.GetList(SessionVariables.SystemRequestProfile);
				var drs = dt1.Select(StandardDataModel.StandardDataColumns.Name + " = '" + userPreferenceKey + "'");

				if (drs.Length > 0)
				{
					userPreferenceKeyId = Convert.ToInt32(drs[0][UserPreferenceCategory.UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId]);
					
					if (string.IsNullOrEmpty(upValue))
					{
						defValue = Convert.ToString(drs[0][UserPreferenceCategory.UserPreferenceKeyDataModel.DataColumns.Value]);
					}
					else
					{
						defValue = upValue;
					}

					defDataTypeId = Convert.ToInt32(drs[0][UserPreferenceCategory.UserPreferenceKeyDataModel.DataColumns.DataTypeId]);
				}

				//insert new preference for current user using default values
				var dt                                 = new UserPreferenceDataModel();

				dt.ApplicationUserId                   = SessionVariables.SystemRequestProfile.AuditId;
				dt.UserPreferenceKeyId                 = userPreferenceKeyId;
				dt.Value                               = defValue;
				dt.DataTypeId                          = defDataTypeId;
				dt.ApplicationId                       = SessionVariables.RequestProfile.ApplicationId;
				dt.UserPreferenceCategoryId            = userPreferenceCategoryId;

				var upId                               = UserPreferenceDataManager.Create(dt, SessionVariables.SystemRequestProfile);
				userPreferenceValue                    = defValue;

                // Add the item back to cache as to avoid reloading whole list of UPs again.
                var upreferenceItem                    = new UPreference();
                upreferenceItem.ApplicationUserId      = SessionVariables.RequestProfile.AuditId;
                upreferenceItem.DataType               = string.Empty;
                upreferenceItem.Id                     = upId;
                upreferenceItem.UserPreferenceKey      = userPreferenceKey;
                upreferenceItem.UserPreferenceCategory = userPreferenceCategory;
                upreferenceItem.value                  = defValue;

                userPreferences.Add(upreferenceItem);
			}

			return userPreferenceValue;
		}

		public static bool GetApplicationUserPreferenceByKeyAsBoolean(string userPreferenceKey, string userPreferenceCategory = "General")
		{
			var value = GetApplicationUserPreferenceByKey(userPreferenceKey, userPreferenceCategory);
			return Convert.ToBoolean(value);
		}

        public static string GetApplicationInstancePreferenceByKey(string userPreferenceKey, string userPreferenceCategory = "General", string upValue = "")
        {
            var userPreferenceValue = String.Empty;

            if (ApplicationVariables.ApplicationInstancePreferences == null)
            {
                RefreshApplicationInstancePreferences();
            }

            var userPreferences = ApplicationVariables.ApplicationInstancePreferences;
            var preference = userPreferences.Find(item => item.UserPreferenceKey.Equals(userPreferenceKey, StringComparison.CurrentCultureIgnoreCase) && item.UserPreferenceCategory.Equals(userPreferenceCategory, StringComparison.CurrentCultureIgnoreCase));

            //check if current user has preference of for the key
            if (preference != null)
            {
                //if yes then assign it to a variable
                userPreferenceValue = preference.value;
            }
            else
            {
                var userPreferenceCategoryId = 0;
                if (!string.IsNullOrEmpty(userPreferenceCategory))
                {
                    userPreferenceCategoryId = CreateUserPreferenceCategoryIfNotExists(userPreferenceCategory, userPreferenceCategory);
                }
                //if not then find the default values for Prefrence
                var defValue            = String.Empty;
                var defDataTypeId       = 0;
                var userPreferenceKeyId = 0;

				var dataUPKey = new UserPreferenceKeyDataModel();
				dataUPKey.Name = userPreferenceKey;

                var dt1 = UserPreferenceKeyDataManager.Search(dataUPKey, SessionVariables.SystemRequestProfile);

				if (dt1.Rows.Count > 0)
                {
					userPreferenceKeyId = Convert.ToInt32(dt1.Rows[0][UserPreferenceCategory.UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId]);

                    if (string.IsNullOrEmpty(upValue))
                    {
						defValue = Convert.ToString(dt1.Rows[0][UserPreferenceCategory.UserPreferenceKeyDataModel.DataColumns.Value]);
                    }
                    else
                    {
                        defValue = upValue;
                    }

					defDataTypeId = Convert.ToInt32(dt1.Rows[0][UserPreferenceCategory.UserPreferenceKeyDataModel.DataColumns.DataTypeId]);
                }

                //insert new preference for current user using default values
                var dt                                 = new UserPreferenceDataModel();

                dt.ApplicationUserId                   = ApplicationCommon.ApplicationInstanceId;
                dt.UserPreferenceKeyId                 = userPreferenceKeyId;
                dt.Value                               = defValue;
                dt.DataTypeId                          = defDataTypeId;
                dt.ApplicationId                       = SessionVariables.RequestProfile.ApplicationId;
                dt.UserPreferenceCategoryId            = userPreferenceCategoryId;

                var upId                               = UserPreferenceDataManager.Create(dt, SessionVariables.SystemRequestProfile);

                userPreferenceValue                    = defValue;

                // Add the item back to cache as to avoid reloading whole list of UPs again.
                var upreferenceItem                    = new UPreference();
                upreferenceItem.ApplicationUserId      = SessionVariables.RequestProfile.AuditId;
                upreferenceItem.DataType               = string.Empty;
                upreferenceItem.Id                     = upId;
                upreferenceItem.UserPreferenceKey      = userPreferenceKey;
                upreferenceItem.UserPreferenceCategory = userPreferenceCategory;
                upreferenceItem.value                  = defValue;

                userPreferences.Add(upreferenceItem);
            }

            return userPreferenceValue;
        }

        public static bool GetApplicationInstancePreferenceByKeyAsBoolean(string userPreferenceKey, string userPreferenceCategory = "General")
        {
            var value = GetApplicationInstancePreferenceByKey(userPreferenceKey, userPreferenceCategory);

            return Convert.ToBoolean(value);
        }
	
	}
}