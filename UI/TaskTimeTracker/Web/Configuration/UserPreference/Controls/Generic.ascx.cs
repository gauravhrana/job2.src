using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.Configuration.UserPreference.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? UserPreferenceId
        {
            get
            {
                if (txtUserPreferenceId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtUserPreferenceId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtUserPreferenceId.Text);
                }
            }
            set
            {
                txtUserPreferenceId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? ApplicationUserId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplicationUserId.Text.Trim());
                else
                    return int.Parse(drpApplicationUserList.SelectedItem.Value);
            }
            set
            {
                txtApplicationUserId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? UserPreferenceCategoryId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtUserPreferenceCategoryId.Text.Trim());
                else
                    return int.Parse(drpUserPreferenceCategoryList.SelectedItem.Value);
            }
            set
            {
                txtUserPreferenceCategoryId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? UserPreferenceKeyId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtUserPreferenceKeyId.Text.Trim());
                else
                    return int.Parse(drpUserPreferenceKeyList.SelectedItem.Value);
            }
            set
            {
                txtUserPreferenceKeyId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string Value
        {
            get
            {
                return txtValue.Text;
            }
            set
            {
                txtValue.Text = value ?? String.Empty;
            }
        }

        public int? DataTypeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtDataTypeId.Text.Trim());
                else
                    return int.Parse(drpDataTypeList.SelectedItem.Value);
            }
            set
            {
                txtDataTypeId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Configuration/UserPreference/Controls/Validation.xml"); //"R:\Projects\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        private System.Data.DataTable GetData()
        {
            var data = new UserPreferenceDataModel();

            data.UserPreferenceKeyId = Convert.ToInt32(txtUserPreferenceKeyId.Text);
            data.Value = txtValue.Text.Trim();
            var topN = Convert.ToInt32(Session["TopNCount"]);
            var dt = UserPreferenceDataManager.GetTopNUserPreferences(data, topN, SessionVariables.RequestProfile);
            return dt;
        }

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UserPreference, "DBColumns", SessionVariables.RequestProfile);
		}

        public override void SetId(int setId, bool chkUserPreferenceId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkUserPreferenceId);
            txtUserPreferenceId.Enabled = chkUserPreferenceId;
            txtUserPreferenceKeyId.Enabled = chkUserPreferenceId;
            txtApplicationUserId.Enabled = chkUserPreferenceId;

            drpApplicationUserList.Enabled = chkUserPreferenceId;
            drpUserPreferenceKeyList.Enabled = chkUserPreferenceId;

            //txtValue.Enabled = !chkUserPreferenceId;
            //txtDataTypeId.Enabled = !chkUserPreferenceId;
            //drpDataTypeList.Enabled = !chkUserPreferenceId;
        }

		public override int? Save(string action)
		{
			var data = new UserPreferenceDataModel();

			data.UserPreferenceId			= UserPreferenceId;
			data.UserPreferenceKeyId		= UserPreferenceKeyId;
			data.DataTypeId					= DataTypeId;
			data.UserPreferenceCategoryId	= UserPreferenceCategoryId;
			data.ApplicationUserId			= ApplicationUserId;
			data.Value						= Value;

			if (action == "Insert")
			{
				if (!UserPreferenceDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					UserPreferenceDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				UserPreferenceDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of UserPreferenceID ?
			return data.UserPreferenceId;
		}
        public void LoadData(int userPreferenceId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new UserPreferenceDataModel();
            data.UserPreferenceId = userPreferenceId;

            // get data
            var items = UserPreferenceDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtUserPreferenceKeyId.Text = item.UserPreferenceKeyId.ToString();
            txtApplicationUserId.Text = item.ApplicationUserId.ToString();
            txtValue.Text = item.Value.ToString();
            txtDataTypeId.Text = item.DataTypeId.ToString();
            txtUserPreferenceCategoryId.Text = Convert.ToString(item.UserPreferenceCategoryId);

            drpUserPreferenceKeyList.SelectedValue = item.UserPreferenceKeyId.ToString();
            drpDataTypeList.SelectedValue = item.DataTypeId.ToString();
            drpApplicationUserList.SelectedValue = Convert.ToString(item.ApplicationUserId);
            drpUserPreferenceCategoryList.SelectedValue = Convert.ToString(item.UserPreferenceCategoryId);

            if (!showId)
            {
                txtUserPreferenceId.Text = item.UserPreferenceId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, userPreferenceId, PrimaryEntityKey);
            }
            else
            {
                txtUserPreferenceId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new UserPreferenceDataModel();

            UserPreferenceId = data.UserPreferenceId;
            UserPreferenceCategoryId = data.UserPreferenceCategoryId;
            UserPreferenceKeyId = data.UserPreferenceKeyId;
            ApplicationUserId = data.ApplicationUserId;
            Value = data.Value;
            DataTypeId = data.DataTypeId;
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var personData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(personData, drpApplicationUserList, ApplicationUserDataModel.DataColumns.FullName, ApplicationUserDataModel.DataColumns.ApplicationUserId);

            var dataTypeData = Framework.Components.UserPreference.UserPreferenceDataTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(dataTypeData, drpDataTypeList, StandardDataModel.StandardDataColumns.Name, DataModel.Framework.Configuration.UserPreferenceDataTypeDataModel.DataColumns.UserPreferenceDataTypeId);

            var userPreferenceKeyData = Framework.Components.UserPreference.UserPreferenceKeyDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(userPreferenceKeyData, drpUserPreferenceKeyList, StandardDataModel.StandardDataColumns.Name, UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId);

            var userPreferenceCategory = Framework.Components.UserPreference.UserPreferenceCategoryDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(userPreferenceCategory, drpUserPreferenceCategoryList, StandardDataModel.StandardDataColumns.Name, UserPreferenceCategoryDataModel.DataColumns.UserPreferenceCategoryId);
	

            if (isTesting)
            {
                drpApplicationUserList.AutoPostBack = true;
                drpUserPreferenceKeyList.AutoPostBack = true;
                drpDataTypeList.AutoPostBack = true;
                drpUserPreferenceCategoryList.AutoPostBack = true;
				
                if (drpUserPreferenceCategoryList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtUserPreferenceCategoryId.Text.Trim()))
                    {
                        drpUserPreferenceCategoryList.SelectedValue = txtUserPreferenceCategoryId.Text;
                    }
                    else
                    {
                        txtUserPreferenceCategoryId.Text = drpUserPreferenceCategoryList.SelectedItem.Value;
                    }
                }
                if (drpApplicationUserList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplicationUserId.Text.Trim()))
                    {
                        drpApplicationUserList.SelectedValue = txtApplicationUserId.Text;
                    }
                    else
                    {
                        txtApplicationUserId.Text = drpApplicationUserList.SelectedItem.Value;
                    }
                }

                if (drpUserPreferenceKeyList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtUserPreferenceKeyId.Text.Trim()))
                    {
                        drpUserPreferenceKeyList.SelectedValue = txtUserPreferenceKeyId.Text;
                    }
                    else
                    {
                        txtUserPreferenceKeyId.Text = drpUserPreferenceKeyList.SelectedItem.Value;
                    }
                }
                if (drpDataTypeList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtDataTypeId.Text.Trim()))
                    {
                        drpDataTypeList.SelectedValue = txtDataTypeId.Text;
                    }
                    else
                    {
                        txtDataTypeId.Text = drpDataTypeList.SelectedItem.Value;
                    }
                }
                txtApplicationUserId.Visible = true;
                txtUserPreferenceKeyId.Visible = true;
                txtDataTypeId.Visible = true;
                txtUserPreferenceCategoryId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtUserPreferenceCategoryId.Text.Trim()))
                {
                    drpUserPreferenceCategoryList.SelectedValue = txtUserPreferenceCategoryId.Text;
                }

                if (!string.IsNullOrEmpty(txtApplicationUserId.Text.Trim()))
                {
                    drpApplicationUserList.SelectedValue = txtApplicationUserId.Text;
                }

                if (!string.IsNullOrEmpty(txtUserPreferenceKeyId.Text.Trim()))
                {
                    drpUserPreferenceKeyList.SelectedValue = txtUserPreferenceKeyId.Text;
                }

                if (!string.IsNullOrEmpty(txtDataTypeId.Text.Trim()))
                {
                    drpDataTypeList.SelectedValue = txtDataTypeId.Text;
                }
            }
        }
  
        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                txtUserPreferenceId.Visible = isTesting;
                lblUserPreferenceId.Visible = isTesting;

                SetupDropdown();
            }
        }

        protected void drpApplicationUserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationUserId.Text = drpApplicationUserList.SelectedItem.Value;
        }

        protected void drpUserPreferenceKeyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUserPreferenceKeyId.Text = drpUserPreferenceKeyList.SelectedItem.Value;
        }

        protected void drpDataTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDataTypeId.Text = drpDataTypeList.SelectedItem.Value;
        }

        protected void drpUserPreferenceCategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUserPreferenceCategoryId.Text = drpUserPreferenceCategoryList.SelectedItem.Value;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "UserPreference";
            FolderLocationFromRoot = "/Shared/Configuration";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserPreference;

            // set object variable reference            
            PlaceHolderCore = dynUserPreferenceId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        #endregion

    }
}