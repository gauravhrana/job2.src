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
using ReferenceData.Components.BusinessLayer;
using DataModel.ReferenceData;

namespace Shared.UI.Web.Admin.Controls
{
    public partial class LoginSettings : Shared.UI.WebFramework.BaseControl
    {

        #region Properties

        private int? UserCountryPreferenceId
        {
            get
            {
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
                CountryDataModel.DataColumns.CountryId);

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
                MenuCategoryDataModel.DataColumns.MenuCategoryId);

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
			var dt = Framework.Components.UserPreference.ApplicationModeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(dt, drpIsTesting, StandardDataModel.StandardDataColumns.Name,
                ApplicationModeDataModel.DataColumns.ApplicationModeId);
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
            ApplicationCommon.SetApplicationUserFullName();
            ApplicationCommon.SetApplicationUserRoles();
        }

        private void SetIsTesting()
        {
            var appMode = drpIsTesting.SelectedItem.Text;

            switch (appMode.ToLower())
            {
                case "testing":
                    SessionVariables.IsTesting = true;
                    break;
                case "live":
                    SessionVariables.IsTesting = false;
                    break;
                default:
                    SessionVariables.IsTesting = false;
                    break;
            }

            PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.UserApplicationModeId, drpIsTesting.SelectedValue);
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
                    data.UserPreferenceKeyId = UserCountryKeyId;
                    data.Value = Convert.ToString(drpCountry.SelectedValue);
                    data.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
                    data.DataTypeId = UserCountryDataTypeId;
                    data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

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
                    var objKey = Framework.Components.UserPreference.UserPreferenceKeyDataManager.GetDetails(dataKey, SessionVariables.RequestProfile);
                    if (objKey != null)
                    {
                        var keyId = objKey.UserPreferenceKeyId.Value;
                        var dataTypeId = objKey.DataTypeId.Value;

                        data = new UserPreferenceDataModel();
                        data.UserPreferenceKeyId = keyId;

                        var objUP = UserPreferenceDataManager.GetDetails(data, SessionVariables.RequestProfile);
                        if (objUP != null)
                        {
                            //data.Value = Convert.ToString(timeZoneId);
                            data.UserPreferenceCategoryId = objUP.UserPreferenceCategoryId.Value;
                            data.UserPreferenceId         = objUP.UserPreferenceId.Value;
                            data.DataTypeId               = objUP.DataTypeId.Value;
                            data.ApplicationUserId        = SessionVariables.RequestProfile.AuditId;
                            data.ApplicationId            = SessionVariables.RequestProfile.ApplicationId;
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

        protected override void OnInit(EventArgs e)
        {
            try
            {
                SetUpIsTestingDropdown();
                SetupThemeDropdown();
                SetUpCountryDropDown();
                SetUpMenuCategoryDropDown();
                SetUpLanguageDropDown();

                drpDefaultClickAction.SelectedValue = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.GridDefaultClickActionKey);
                drpDateFormat.SelectedValue         = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.UserDateFormat);
                drpTimeFormat.SelectedValue         = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.UserTimeFormat);
                txtAuditId.Text                     = SessionVariables.RequestProfile.AuditId.ToString();

                txtActiveClient.Text                = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.UserActiveClient);
                txtActiveProject.Text               = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.UserActiveProject);
                txtActiveNeed.Text                  = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.UserActiveNeed);
                txtActiveTask.Text                  = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.UserActiveTask);
				txtDefaultRowCount.Text             = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.DefaultRowCountKey);

                drpIsTesting.SelectedValue = SessionVariables.IsTesting == true ? "Testing" : "Live";

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
            catch { }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {

            try
            {
                var pId = Int32.Parse(txtAuditId.Text.Trim());

                var data = new ApplicationUserDataModel();
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

                    PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.UserDateFormat, drpDateFormat.SelectedValue);
                    PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.UserTimeFormat, drpTimeFormat.SelectedValue);
					SessionVariables.UserDateFormat = drpDateFormat.SelectedValue;
                    SessionVariables.UserTimeFormat = drpTimeFormat.SelectedValue;

                    PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.UserActiveClient, string.IsNullOrEmpty(txtActiveClient.Text.Trim()) ? " " : txtActiveClient.Text.Trim());
                    PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.UserActiveProject, string.IsNullOrEmpty(txtActiveProject.Text.Trim()) ? " " : txtActiveProject.Text.Trim());
                    PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.UserActiveNeed, string.IsNullOrEmpty(txtActiveNeed.Text.Trim()) ? " " : txtActiveNeed.Text.Trim());
                    PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.UserActiveTask, string.IsNullOrEmpty(txtActiveTask.Text.Trim()) ? " " : txtActiveTask.Text.Trim());
					PreferenceUtility.UpdateUserPreference("General", ApplicationCommon.DefaultRowCountKey, string.IsNullOrEmpty(txtDefaultRowCount.Text.Trim()) ? "" : txtDefaultRowCount.Text.Trim());

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