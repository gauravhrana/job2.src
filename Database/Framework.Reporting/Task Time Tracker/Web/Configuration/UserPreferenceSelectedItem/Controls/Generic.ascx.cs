using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.UserPreferenceSelectedItem.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? UserPreferenceSelectedItemId
        {
            get
            {
                if (txtUserPreferenceSelectedItemId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtUserPreferenceSelectedItemId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtUserPreferenceSelectedItemId.Text);
                }
            }
            set
            {
                txtUserPreferenceSelectedItemId.Text = (value == null) ? String.Empty : value.ToString();
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

        public string ParentKey
        {
            get
            {
                return txtParentKey.Text;
            }
            set
            {
                txtParentKey.Text = value ?? String.Empty;
            }
        }

        public int? SortOrder
        {
            get
            {
                return int.Parse(txtSortOrder.Text);
            }
            set
            {
                txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Configuration/UserPreferenceSelectedItem/Controls/Validation.xml"); //"R:\Projects\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        private string[] GetColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UserPreferenceSelectedItem, "DBColumns", SessionVariables.RequestProfile);
        }

        public override void SetId(int setId, bool chkUserPreferenceSelectedItemId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkUserPreferenceSelectedItemId);
            txtUserPreferenceSelectedItemId.Enabled = chkUserPreferenceSelectedItemId;

            //txtValue.Enabled = !chkUserPreferenceSelectedItemId;
            //txtParentKey.Enabled = !chkUserPreferenceSelectedItemId;
            //drpDataTypeList.Enabled = !chkUserPreferenceSelectedItemId;
        }

        public void LoadData(int userPreferenceSelectedItemId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel();
            data.UserPreferenceKeyId = userPreferenceSelectedItemId;

            // get data
			var items = Framework.Components.UserPreference.UserPreferenceSelectedItemDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtUserPreferenceKeyId.Text = item.UserPreferenceKeyId.ToString();
            txtApplicationUserId.Text = item.ApplicationUserId.ToString();
            txtValue.Text = item.Value.ToString();
            txtParentKey.Text = item.ParentKey.ToString();
            txtSortOrder.Text = item.SortOrder.ToString();

            if (!showId)
            {
                txtUserPreferenceSelectedItemId.Text = item.UserPreferenceSelectedItemId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, userPreferenceSelectedItemId, PrimaryEntityKey);
            }
            else
            {
                txtUserPreferenceSelectedItemId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel();

            UserPreferenceSelectedItemId = data.UserPreferenceSelectedItemId;
            UserPreferenceKeyId = data.UserPreferenceKeyId;
            ApplicationUserId = data.ApplicationUserId;
            Value = data.Value;
            ParentKey = data.ParentKey;
            SortOrder = data.SortOrder;
            Value = data.Value;
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var personData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(personData, drpApplicationUserList, ApplicationUserDataModel.DataColumns.FullName, ApplicationUserDataModel.DataColumns.ApplicationUserId);

            var UserPreferenceKeyData = Framework.Components.UserPreference.UserPreferenceKeyDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(UserPreferenceKeyData, drpUserPreferenceKeyList, StandardDataModel.StandardDataColumns.Name, UserPreferenceKeyDataModel.DataColumns.UserPreferenceKeyId);

            if (isTesting)
            {
                drpApplicationUserList.AutoPostBack = true;
                drpUserPreferenceKeyList.AutoPostBack = true;                
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
                txtApplicationUserId.Visible = true;
                txtUserPreferenceKeyId.Visible = true;
            }
            else
            {

                if (!string.IsNullOrEmpty(txtApplicationUserId.Text.Trim()))
                {
                    drpApplicationUserList.SelectedValue = txtApplicationUserId.Text;
                }

                if (!string.IsNullOrEmpty(txtUserPreferenceKeyId.Text.Trim()))
                {
                    drpUserPreferenceKeyList.SelectedValue = txtUserPreferenceKeyId.Text;
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
                txtUserPreferenceSelectedItemId.Visible = isTesting;
                lblUserPreferenceSelectedItemId.Visible = isTesting;

                SetupDropdown();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "UserPreferenceSelectedItem";
            FolderLocationFromRoot = "/Shared/Configuration";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserPreferenceSelectedItem;

            // set object variable reference            
            PlaceHolderCore = dynUserPreferenceSelectedItemId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        protected void drpApplicationUserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationUserId.Text = drpApplicationUserList.SelectedItem.Value;
        }

        protected void drpUserPreferenceKeyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUserPreferenceKeyId.Text = drpUserPreferenceKeyList.SelectedItem.Value;
        }

        #endregion

    }
}