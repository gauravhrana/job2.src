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
using DataModel.ReferenceData;

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
            var countryData = ReferenceData.Components.BusinessLayer.CountryDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(countryData, drpCountry, StandardDataModel.StandardDataColumns.Name,
                DataModel.ReferenceData.CountryDataModel.DataColumns.CountryId);

            var dataKey = new UserPreferenceKeyDataModel();
            dataKey.Name = "UserCountry";
            var objKey = Framework.Components.UserPreference.UserPreferenceKeyDataManager.GetDetails(dataKey, SessionVariables.RequestProfile);
            if (objKey != null)
            {
                UserCountryKeyId = objKey.UserPreferenceKeyId.Value;
                UserCountryDataTypeId = objKey.DataTypeId.Value;
                var data = new UserPreferenceDataModel();
                data.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
                data.UserPreferenceKeyId = UserCountryKeyId;

                var objUP = UserPreferenceDataManager.GetDetails(data, SessionVariables.RequestProfile);
                if (objUP != null)
                {
                    UserCountryPreferenceId = objUP.UserPreferenceId.Value;
                    drpCountry.SelectedValue = objUP.Value;
                }
            }

        }

		private void SetUpMenuCategoryDropDown()
		{
			var menuCategoryData = Framework.Components.Core.MenuCategoryDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(menuCategoryData, drpMenuCategory, StandardDataModel.StandardDataColumns.Name,
                StandardDataModel.StandardDataColumns.Name);

			drpMenuCategory.SelectedValue = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.UserMenuCategory);
		}

		private void SetUpLanguageDropDown()
		{
			var languageData = Framework.Components.Core.LanguageDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(languageData, drpLanguage, StandardDataModel.StandardDataColumns.Name,
				LanguageDataModel.DataColumns.LanguageId);

			drpLanguage.SelectedValue = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.Language);
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
                var flag = dataTable.Any(item => item.ApplicationUserId == val);

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
            PreferenceUtility.RefreshUserPreferencesCache();
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

                    var objUP = UserPreferenceDataManager.GetDetails(data, SessionVariables.RequestProfile);
                    if (objUP != null)
                    {
                        data.UserPreferenceCategoryId = objUP.UserPreferenceCategoryId.Value;
                        data.UserPreferenceKeyId      = objUP.UserPreferenceKeyId.Value;
                        data.DataTypeId               = objUP.DataTypeId.Value;
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
                var dtCountry = ReferenceData.Components.BusinessLayer.CountryDataManager.GetEntityDetails(dataCountry, SessionVariables.RequestProfile);
                if (dtCountry.Count > 0)
                {
                    //var timeZoneId = Convert.ToInt32(dtCountry.Rows[0][CountryDataModel.DataColumns.TimeZoneId]);
                                        
                    var dataKey = new UserPreferenceKeyDataModel();
                    dataKey.Name = "UserTimeZone";
                    var dtKeys = Framework.Components.UserPreference.UserPreferenceKeyDataManager.GetDetails(dataKey, SessionVariables.RequestProfile);
                    if (dtKeys != null)
                    {
                        var keyId = dtKeys.UserPreferenceKeyId.Value;
                        var dataTypeId = dtKeys.DataTypeId.Value;

                        data = new UserPreferenceDataModel();
                        data.UserPreferenceKeyId = keyId;

                        var objUP = UserPreferenceDataManager.GetDetails(data, SessionVariables.RequestProfile);
                        if (objUP != null)
                        {
                            //data.Value                    = Convert.ToString(timeZoneId);
                            data.UserPreferenceCategoryId = objUP.UserPreferenceCategoryId.Value;
                            data.UserPreferenceId         = objUP.UserPreferenceId.Value;
                            data.DataTypeId               = objUP.DataTypeId.Value;
                            data.ApplicationUserId        = SessionVariables.RequestProfile.AuditId;
							data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
							UserPreferenceDataManager.Update(data, SessionVariables.RequestProfile);
                        }
                        else
                        {
                            //data.Value = Convert.ToString(timeZoneId);
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
                drpDefaultClickAction.SelectedValue = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.GridDefaultClickActionKey);
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

                var item = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(data, SessionVariables.RequestProfile);
                if (item != null)
                {

                    SetAuditId(item.ApplicationUserId.Value);

                    SetIsTesting();

                    SetUserTheme();

					PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.GridDefaultClickActionKey, drpDefaultClickAction.SelectedValue);
					PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.UserMenuCategory, drpMenuCategory.SelectedValue);
					PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.Language, drpLanguage.SelectedValue);

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