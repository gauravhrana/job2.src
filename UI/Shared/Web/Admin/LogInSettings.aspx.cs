using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;


namespace Shared.UI.Web.Admin
{
    public partial class LogInSettings : Shared.UI.WebFramework.BasePage
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

        #endregion

        #region private methods

        private void LoadDropDown(System.Data.DataTable dt, DropDownList drpSource, string textField, string valueField)
        {
            drpSource.DataSource = dt;
            drpSource.DataTextField = textField;
            drpSource.DataValueField = valueField;

            drpSource.DataBind();
            drpSource.SelectedIndex = -1;
        }

        private void SetUpCountryDropDown()
        {
            var countryData = Framework.Components.Core.Country.GetList(SessionVariables.AuditId);
            LoadDropDown(countryData, drpCountry, Framework.Components.Core.Country.DataColumns.Name,
                Framework.Components.Core.Country.DataColumns.CountryId);

            var dataKey = new Framework.Components.UserPreference.UserPreferenceKey.Data();
            dataKey.Name = "UserCountry";
            var dtKeys = Framework.Components.UserPreference.UserPreferenceKey.Search(dataKey, SessionVariables.AuditId);
            if (dtKeys != null && dtKeys.Rows.Count > 0)
            {
                UserCountryKeyId = Convert.ToInt32(dtKeys.Rows[0][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.UserPreferenceKeyId]);
                UserCountryDataTypeId = Convert.ToInt32(dtKeys.Rows[0][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.DataTypeId]);
                var data = new Framework.Components.UserPreference.UserPreference.Data();
                data.ApplicationUserId = SessionVariables.AuditId;
                data.UserPreferenceKeyId = UserCountryKeyId;

                var dt = Framework.Components.UserPreference.UserPreference.Search(data, SessionVariables.AuditId);
                if (dt.Rows.Count > 0)
                {
                    UserCountryPreferenceId = Convert.ToInt32(dt.Rows[0][Framework.Components.UserPreference.UserPreference.DataColumns.UserPreferenceId]);
                    drpCountry.SelectedValue = Convert.ToString(dt.Rows[0][Framework.Components.UserPreference.UserPreference.DataColumns.Value]);
                }
            }

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
                var dataTable = Framework.Components.ApplicationUser.ApplicationUser.GetList(SessionVariables.AuditId);

                // create linq query
                var query = from DataRow row in dataTable.Rows
                            select (int)row["PersonId"];

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
            SessionVariables.AuditId = Convert.ToInt32(txtAuditId.Text);
        }

        private void SetAuditId(int personid)
        {
            SessionVariables.AuditId = Convert.ToInt32(personid);
            ApplicationCommon.SetUserPreferences();
            ApplicationCommon.SetApplicationUserName();
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

                var data = new Framework.Components.UserPreference.UserPreference.Data();
                if (UserCountryPreferenceId != null)
                {                   
                    data.UserPreferenceId = UserCountryPreferenceId;

                    var dt = Framework.Components.UserPreference.UserPreference.Search(data, SessionVariables.AuditId);
                    if (dt.Rows.Count > 0)
                    {
                        data.UserPreferenceCategoryId = Convert.ToInt32(dt.Rows[0][Framework.Components.UserPreference.UserPreference.DataColumns.UserPreferenceCategoryId]);
                        data.UserPreferenceKeyId      = Convert.ToInt32(dt.Rows[0][Framework.Components.UserPreference.UserPreference.DataColumns.UserPreferenceKeyId]);
                        data.DataTypeId               = Convert.ToInt32(dt.Rows[0][Framework.Components.UserPreference.UserPreference.DataColumns.DataTypeId]);
                        data.Value                    = Convert.ToString(drpCountry.SelectedValue);
                        data.ApplicationUserId        = SessionVariables.AuditId;
                        data.ApplicationId            = Framework.Components.DataAccess.SetupConfiguration.ApplicationId;

                        Framework.Components.UserPreference.UserPreference.Update(data, SessionVariables.AuditId);
                    }
                }
                else
                {
                    data.UserPreferenceCategoryId = 1;
                    data.UserPreferenceKeyId      = UserCountryKeyId;
                    data.Value                    = Convert.ToString(drpCountry.SelectedValue);
                    data.ApplicationUserId        = SessionVariables.AuditId;
                    data.DataTypeId               = UserCountryDataTypeId;
                    data.ApplicationId            = Framework.Components.DataAccess.SetupConfiguration.ApplicationId;

                    Framework.Components.UserPreference.UserPreference.Create(data, SessionVariables.AuditId);
                }

                var dataCountry = new Framework.Components.Core.Country.Data();
                dataCountry.CountryId = Convert.ToInt32(drpCountry.SelectedValue);
                var dtCountry = Framework.Components.Core.Country.Search(dataCountry, SessionVariables.AuditId);
                if (dtCountry.Rows.Count > 0)
                {
                    var timeZoneId = Convert.ToInt32(dtCountry.Rows[0][Framework.Components.Core.Country.DataColumns.TimeZoneId]);
                                        
                    var dataKey = new Framework.Components.UserPreference.UserPreferenceKey.Data();
                    dataKey.Name = "UserTimeZone";
                    var dtKeys = Framework.Components.UserPreference.UserPreferenceKey.Search(dataKey, SessionVariables.AuditId);
                    if (dtKeys != null && dtKeys.Rows.Count > 0)
                    {
                        var keyId = Convert.ToInt32(dtKeys.Rows[0][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.UserPreferenceKeyId]);
                        var dataTypeId = Convert.ToInt32(dtKeys.Rows[0][Framework.Components.UserPreference.UserPreferenceKey.DataColumns.DataTypeId]);

                        data = new Framework.Components.UserPreference.UserPreference.Data();
                        data.UserPreferenceKeyId = keyId;

                        var dt = Framework.Components.UserPreference.UserPreference.Search(data, SessionVariables.AuditId);
                        if (dt.Rows.Count > 0)
                        {
                            data.Value                    = Convert.ToString(timeZoneId);
                            data.UserPreferenceCategoryId = Convert.ToInt32(dt.Rows[0][Framework.Components.UserPreference.UserPreference.DataColumns.UserPreferenceCategoryId]);
                            data.UserPreferenceId         = Convert.ToInt32(dt.Rows[0][Framework.Components.UserPreference.UserPreference.DataColumns.UserPreferenceId]);
                            data.DataTypeId               = Convert.ToInt32(dt.Rows[0][Framework.Components.UserPreference.UserPreference.DataColumns.DataTypeId]);
                            data.ApplicationUserId        = SessionVariables.AuditId;
							data.ApplicationId = Framework.Components.DataAccess.SetupConfiguration.ApplicationId; 
							Framework.Components.UserPreference.UserPreference.Update(data, SessionVariables.AuditId);
                        }
                        else
                        {
                            data.Value = Convert.ToString(timeZoneId);
                            data.DataTypeId = dataTypeId;
                            data.UserPreferenceKeyId = keyId;
                            data.UserPreferenceCategoryId = 1;                            
                            data.ApplicationUserId = SessionVariables.AuditId;
							data.ApplicationId = Framework.Components.DataAccess.SetupConfiguration.ApplicationId; 
                            Framework.Components.UserPreference.UserPreference.Create(data, SessionVariables.AuditId);
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
                drpDefaultClickAction.SelectedValue = ApplicationCommon.GetUserPreferenceByKey(ApplicationCommon.GridDefaultClickActionKey);
                txtAuditId.Text = SessionVariables.AuditId.ToString();
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
                drpUpdateInfoStyle.SelectedValue = ApplicationCommon.GetUserPreferenceByKey(ApplicationCommon.UpdateInfoStyle);
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {

            try
            {
                var pId = Int32.Parse(txtAuditId.Text.Trim());

                var data = new Framework.Components.ApplicationUser.ApplicationUser.Data();
                data.ApplicationUserId = pId;

                var dt = Framework.Components.ApplicationUser.ApplicationUser.GetDetails(data, AuditId);
                if (dt != null & dt.Rows.Count == 1)
                {
                    var row = dt.Rows[0];

                    SetAuditId(Convert.ToInt32(row[Framework.Components.ApplicationUser.ApplicationUser.DataColumns.ApplicationUserId]));

                    SetIsTesting();

                    SetUserTheme();

                    SetUserCountry();

                    ApplicationCommon.UpdateUserPreference("General", ApplicationCommon.GridDefaultClickActionKey, drpDefaultClickAction.SelectedValue);
                    ApplicationCommon.UpdateUserPreference("General", ApplicationCommon.UpdateInfoStyle, drpUpdateInfoStyle.SelectedValue);

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