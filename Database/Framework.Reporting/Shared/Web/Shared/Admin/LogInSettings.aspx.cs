using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;


namespace Shared.UI.Web.Admin
{
	public partial class LogInSettings : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region Properties

        private int? UserCountryPreferenceId
        {
            get {
                if (ViewState["UserCountryPreferenceId"] != null)
                    return Convert.ToInt32(ViewState["UserCountryPreferenceId"]);
                else
                    return null;
            }
            set
            {
                ViewState["UserCountryPreferenceId"] = value;
            }
        }

        private int? UserCountryKeyId
        {
            get
            {
                if (ViewState["UserCountryKeyId"] != null)
                    return Convert.ToInt32(ViewState["UserCountryKeyId"]);
                else
                    return null;
            }
            set
            {
                ViewState["UserCountryKeyId"] = value;
            }
        }

        private int? UserCountryDataTypeId
        {
            get
            {
                if (ViewState["UserCountryDataTypeId"] != null)
                    return Convert.ToInt32(ViewState["UserCountryDataTypeId"]);
                else
                    return null;
            }
            set
            {
                ViewState["UserCountryDataTypeId"] = value;
            }
        }

		private int? UserMenuCategoryPreferenceId
		{
			get
			{
				if (ViewState["UserMenuCategoryPreferenceId"] != null)

					return Convert.ToInt32(ViewState["UserMenuCategoryPreferenceId"]);
				else
					return null;
			}
			set
			{
				ViewState["UserMenuCategoryPreferenceId"] = value;
			}
		}

		private int? UserMenuCategoryKeyId
		{
			get
			{
				if (ViewState["UserMenuCategoryKeyId"] != null)

					return Convert.ToInt32(ViewState["UserMenuCategoryKeyId"]);
				else
					return null;
			}
			set
			{
				ViewState["UserMenuCategoryKeyId"] = value;
			}
		}

		private int? UserMenuCategoryDataTypeId
		{
			get
			{
				if (ViewState["UserMenuCategoryDataTypeId"] != null)

					return Convert.ToInt32(ViewState["UserMenuCategoryDataTypeId"]);
				else
					return null;
			}
			set
			{
				ViewState["UserMenuCategoryDataTypeId"] = value;
			}
		}

        #endregion

        #region private methods

        private void SetUpCountryDropDown()
        {
			var countryData = Framework.Components.Core.CountryDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(countryData, drpCountry, StandardDataModel.StandardDataColumns.Name,
                CountryDataModel.DataColumns.CountryId);

            var dataKey = new UserPreferenceKeyDataModel();
            dataKey.Name = "UserCountry";
            var dtKeys = Framework.Components.UserPreference.UserPreferenceKeyDataManager.Search(dataKey, SessionVariables.RequestProfile);
            if (dtKeys != null && dtKeys.Rows.Count > 0)
            {
                UserCountryKeyId = Convert.ToInt32(dtKeys.Rows[0][UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId]);
                UserCountryDataTypeId = Convert.ToInt32(dtKeys.Rows[0][UserPreferenceKeyDataModel.DataColumns.DataTypeId]);
                var data = new UserPreferenceDataModel();
                data.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
                data.UserPreferenceKeyId = UserCountryKeyId;

				var dt = UserPreferenceDataManager.Search(data, SessionVariables.RequestProfile);
                if (dt.Rows.Count > 0)
                {
                    UserCountryPreferenceId = Convert.ToInt32(dt.Rows[0][UserPreferenceDataModel.DataColumns.UserPreferenceId]);
                    drpCountry.SelectedValue = Convert.ToString(dt.Rows[0][UserPreferenceDataModel.DataColumns.Value]);
                }
            }

        }

		private void SetUpMenuCategoryDropDown()
		{
			var menuCategoryData = Framework.Components.Core.MenuCategoryDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(menuCategoryData, drpMenuCategory, StandardDataModel.StandardDataColumns.Name,
                StandardDataModel.StandardDataColumns.Name);

			drpMenuCategory.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.UserMenuCategory);
		}

		private void SetUpLanguageDropDown()
		{
			var languageData = Framework.Components.Core.LanguageDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(languageData, drpLanguage, StandardDataModel.StandardDataColumns.Name,
				LanguageDataModel.DataColumns.LanguageId);

			drpLanguage.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.Language);
		}	
		

        private void SetUpIsTestingDropdown()
        {
            drpIsTesting.Items.Clear();
            drpIsTesting.Items.Add("true");
            drpIsTesting.Items.Add("false");
        }

        private void SetupThemeDropdown()
        {
            drpTheme.Items.Clear();
			drpTheme.Items.Add("Default");
            drpTheme.Items.Add("Development");
            drpTheme.Items.Add("Professional");
            drpTheme.Items.Add("Casual");
        }

        protected void vldCode_ServerValidate(Object source, ServerValidateEventArgs e)
        {
            try
            {
                // 
                var val = Int32.Parse(txtAuditId.Text);

                // you should use getDetails, if no results based off id, then 
                // invalid Id ... iteating over list here does not make sense.
				var dataTable = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);

                // create linq query
                var query = from DataRow row in dataTable.Rows
                            select (int)row["ApplicationUserId"];

                // create linq query
                var flag = query.Any(item => item == val);

                e.IsValid = flag;
            }
            catch
            {
                // An error occurred in the conversion.
                // The value is not valid.
                e.IsValid = false;
            }
        }

        private void SetAuditId()
        {
			SessionVariables.RequestProfile.AuditId = Convert.ToInt32(txtAuditId.Text);
        }

        private void SetAuditId(int personid)
        {
			SessionVariables.RequestProfile.AuditId = Convert.ToInt32(personid);
            PerferenceUtility.RefreshUserPreferencesCache();
            ApplicationCommon.SetApplicationUserFullName();
            ApplicationCommon.SetApplicationUserRoles();
        }

        private void SetIsTesting()
        {
            var isTesting = drpIsTesting.SelectedItem.Value;

            switch (isTesting)
            {
                case "true":
                    SessionVariables.IsTesting = true;
                    break;
                case "false":
                    SessionVariables.IsTesting = false;
                    break;
            }
        }

        private void SetUserTheme()
        {
            var userTheme = drpTheme.SelectedItem.Value;
            SessionVariables.UserTheme = userTheme;
        }

        private void SetUserCountry()
        {
            if (drpCountry.SelectedValue != "-1")
            {
                //UserCountryId = Convert.ToInt32(drpCountry.SelectedValue);

                var data = new UserPreferenceDataModel();
                if (UserCountryPreferenceId != null)
                {                   
                    data.UserPreferenceId = UserCountryPreferenceId;

					var dt = UserPreferenceDataManager.Search(data, SessionVariables.RequestProfile);
                    if (dt.Rows.Count > 0)
                    {
                        data.UserPreferenceCategoryId = Convert.ToInt32(dt.Rows[0][UserPreferenceDataModel.DataColumns.UserPreferenceCategoryId]);
                        data.UserPreferenceKeyId      = Convert.ToInt32(dt.Rows[0][UserPreferenceDataModel.DataColumns.UserPreferenceKeyId]);
                        data.DataTypeId               = Convert.ToInt32(dt.Rows[0][UserPreferenceDataModel.DataColumns.DataTypeId]);
                        data.Value                    = Convert.ToString(drpCountry.SelectedValue);
                        data.ApplicationUserId        = SessionVariables.RequestProfile.AuditId;
                        data.ApplicationId            = SessionVariables.RequestProfile.ApplicationId;

						UserPreferenceDataManager.Update(data, SessionVariables.RequestProfile);
                    }
                }
                else
                {
                    data.UserPreferenceCategoryId = 1;
                    data.UserPreferenceKeyId      = UserCountryKeyId;
                    data.Value                    = Convert.ToString(drpCountry.SelectedValue);
                    data.ApplicationUserId        = SessionVariables.RequestProfile.AuditId;
                    data.DataTypeId               = UserCountryDataTypeId;
                    data.ApplicationId            = SessionVariables.RequestProfile.ApplicationId;

					UserPreferenceDataManager.Create(data, SessionVariables.RequestProfile);
                }

                var dataCountry = new CountryDataModel();
                dataCountry.CountryId = Convert.ToInt32(drpCountry.SelectedValue);
				var dtCountry = Framework.Components.Core.CountryDataManager.Search(dataCountry, SessionVariables.RequestProfile);
                if (dtCountry.Rows.Count > 0)
                {
                    var timeZoneId = Convert.ToInt32(dtCountry.Rows[0][CountryDataModel.DataColumns.TimeZoneId]);
                                        
                    var dataKey = new UserPreferenceKeyDataModel();
                    dataKey.Name = "UserTimeZone";
					var dtKeys = Framework.Components.UserPreference.UserPreferenceKeyDataManager.Search(dataKey, SessionVariables.RequestProfile);
                    if (dtKeys != null && dtKeys.Rows.Count > 0)
                    {
                        var keyId = Convert.ToInt32(dtKeys.Rows[0][UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId]);
                        var dataTypeId = Convert.ToInt32(dtKeys.Rows[0][UserPreferenceKeyDataModel.DataColumns.DataTypeId]);

                        data = new UserPreferenceDataModel();
                        data.UserPreferenceKeyId = keyId;

						var dt = UserPreferenceDataManager.Search(data, SessionVariables.RequestProfile);
                        if (dt.Rows.Count > 0)
                        {
                            data.Value                    = Convert.ToString(timeZoneId);
                            data.UserPreferenceCategoryId = Convert.ToInt32(dt.Rows[0][UserPreferenceDataModel.DataColumns.UserPreferenceCategoryId]);
                            data.UserPreferenceId         = Convert.ToInt32(dt.Rows[0][UserPreferenceDataModel.DataColumns.UserPreferenceId]);
                            data.DataTypeId               = Convert.ToInt32(dt.Rows[0][UserPreferenceDataModel.DataColumns.DataTypeId]);
                            data.ApplicationUserId        = SessionVariables.RequestProfile.AuditId;
							data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
							UserPreferenceDataManager.Update(data, SessionVariables.RequestProfile);
                        }
                        else
                        {
                            data.Value = Convert.ToString(timeZoneId);
                            data.DataTypeId = dataTypeId;
                            data.UserPreferenceKeyId = keyId;
                            data.UserPreferenceCategoryId = 1;                            
                            data.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
							data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
							UserPreferenceDataManager.Create(data, SessionVariables.RequestProfile);
                        }

                    }

                }

                
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            // 1st time only
            if (!IsPostBack)
            {
                SetUpIsTestingDropdown();
                SetupThemeDropdown();
                SetUpCountryDropDown();
				SetUpMenuCategoryDropDown();
				SetUpLanguageDropDown();
                drpDefaultClickAction.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.GridDefaultClickActionKey);
                txtAuditId.Text = SessionVariables.RequestProfile.AuditId.ToString();
                drpIsTesting.SelectedValue = SessionVariables.IsTesting.ToString().ToLower();
                if (!string.IsNullOrEmpty(SessionVariables.UserTheme))
                    drpTheme.SelectedValue = SessionVariables.UserTheme;
                else
                {
                    //drpTheme.SelectedValue = ApplicationVariables.Branding;

                    string strTmp = ApplicationVariables.Branding;
                    string[] strTmpArry = strTmp.Split(new char[] { '/' });
                    if (strTmpArry.Length > 0)
                    {
                        drpTheme.SelectedValue = strTmpArry[strTmpArry.Length - 1];
                    }
                }
                
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {

            try
            {
                var pId = Int32.Parse(txtAuditId.Text.Trim());

                var data = new DataModel.Framework.AuthenticationAndAuthorization.ApplicationUserDataModel();
                data.ApplicationUserId = pId;

				var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(data, SessionVariables.RequestProfile);
                if (dt != null & dt.Rows.Count == 1)
                {
                    var row = dt.Rows[0];

                    SetAuditId(Convert.ToInt32(row[ApplicationUserDataModel.DataColumns.ApplicationUserId]));

                    SetIsTesting();

                    SetUserTheme();

					PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.GridDefaultClickActionKey, drpDefaultClickAction.SelectedValue);
					PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.UserMenuCategory, drpMenuCategory.SelectedValue);
					PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.Language, drpLanguage.SelectedValue);

                    SetUserCountry();

					//SetUserMenuCategory();

                    
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    // Perform Action if Audit Id is invalid
                }
            }
            catch
            {
            }


        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        #endregion

    }
}