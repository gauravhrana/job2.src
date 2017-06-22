using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;


namespace Shared.UI.Web.Configuration.UserPreferenceKey.Controls
{
    public partial class Generic : ControlGeneric
    {

        #region properties

        public int? UserPreferenceKeyId
        {
            get
            {
                if (txtUserPreferenceKeyId.Enabled)
                {
                    return DefaultDataRules.CheckAndGetEntityId(txtUserPreferenceKeyId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtUserPreferenceKeyId.Text);
                }
            }
            set
            {
                txtUserPreferenceKeyId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string Name
        {
            get
            {
                return txtName.Text;
            }
            set
            {
                txtName.Text = value ?? String.Empty;
            }
        }

        public string Description
        {
            get
            {
                return DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.InnerText);
            }
            set
            {
                txtDescription.InnerText = value ?? String.Empty;
            }
        }

        public int? SortOrder
        {
            get
            {
                return DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
            }
            set
            {
                txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
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
                txtName.Text = value ?? String.Empty;
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
                return Server.MapPath("~/Configuration/UserPreferenceKey/Controls/Validation.xml"); //ConfigurationSettings.AppSettings["DynamicValidatorsConfigurationFile"];
            }
        }

        #endregion

        #region private methods

        public override void SetId(int setId, bool chkUserPreferenceKeyId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"]);
            txtUserPreferenceKeyId.Enabled = chkUserPreferenceKeyId;
            //txtName.Enabled = !chkUserPreferenceKeyId;
            //txtValue.Enabled = !chkUserPreferenceKeyId;
            //txtDataTypeId.Enabled = !chkUserPreferenceKeyId;
            //txtDescription.Enabled = !chkUserPreferenceKeyId;
            //txtSortOrder.Enabled = !chkUserPreferenceKeyId;
            //drpDataTypeList.Enabled = !chkUserPreferenceKeyId;
        }

        public void SetId(int setId, bool chkUserPreferenceKeyId, bool chkNameEnabled)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"]);
            txtUserPreferenceKeyId.Enabled = chkUserPreferenceKeyId;
            //txtName.Enabled = chkNameEnabled;
            //if (chkNameEnabled)
            //    txtName.Text = String.Empty;
            //txtValue.Enabled = !chkUserPreferenceKeyId;
            //txtDataTypeId.Enabled = !chkUserPreferenceKeyId;
            //txtDescription.Enabled = !chkUserPreferenceKeyId;
            //txtSortOrder.Enabled = !chkUserPreferenceKeyId;
            //drpDataTypeList.Enabled = !chkUserPreferenceKeyId;
        }

        public void LoadData(int userPreferenceKeyId)
        {
            var oData = new UserPreferenceKeyDataModel();
            oData.UserPreferenceKeyId = userPreferenceKeyId;

            var items = UserPreferenceKeyDataManager.GetEntityDetails(oData, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];
                txtUserPreferenceKeyId.Text = Convert.ToString(item.UserPreferenceKeyId);
                txtName.Text					 = Convert.ToString(item.Name);
                txtValue.Text					 = Convert.ToString(item.Value);
                txtDataTypeId.Text				 = Convert.ToString(item.DataTypeId);
                txtDescription.InnerText		 = Convert.ToString(item.Description);
                txtSortOrder.Text				 = Convert.ToString(item.SortOrder);
                drpDataTypeList.SelectedValue	 = Convert.ToString(item.DataTypeId);

                oHistoryList.Setup(PrimaryEntity, userPreferenceKeyId, PrimaryEntityKey);        
                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
			}
            else
            {
                txtUserPreferenceKeyId.Text = String.Empty;
                txtName.Text = String.Empty;
                txtValue.Text = String.Empty;
                txtDataTypeId.Text = String.Empty;
                txtDescription.InnerText = String.Empty;
                txtSortOrder.Text = String.Empty;
            }
        }

        public void LoadData(int userPreferenceKeyId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new UserPreferenceKeyDataModel();
            data.UserPreferenceKeyId = userPreferenceKeyId;

            // get data
			var items = UserPreferenceKeyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtValue.Text = Convert.ToString(item.Value);
            txtDataTypeId.Text = Convert.ToString(item.DataTypeId);
            txtDescription.InnerText = item.Description;
            txtName.Text = item.Name;
            txtSortOrder.Text = item.SortOrder.ToString();


            if (!showId)
            {
                txtUserPreferenceKeyId.Text = item.UserPreferenceKeyId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, userPreferenceKeyId, PrimaryEntityKey);
            }
            else
            {
                txtUserPreferenceKeyId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new UserPreferenceKeyDataModel();

            UserPreferenceKeyId = data.UserPreferenceKeyId;
            Name = data.Name;
            Description = data.Description;
            SortOrder = data.SortOrder;
            DataTypeId = data.DataTypeId;
            Value = data.Value;
        }


        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
            var dataTypeData = UserPreferenceDataTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(dataTypeData, drpDataTypeList, StandardDataModel.StandardDataColumns.Name, UserPreferenceDataTypeDataModel.DataColumns.UserPreferenceDataTypeId);

            if (isTesting)
            {
                drpDataTypeList.AutoPostBack = true;
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
                txtDataTypeId.Visible = true;
            }
            else
            {
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
                txtUserPreferenceKeyId.Visible = isTesting;
                lblUserPreferenceKeyId.Visible = isTesting;

                SetupDropdown();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "UserPreferenceKey";
            FolderLocationFromRoot = "/Shared/Configuration";
            PrimaryEntity = SystemEntity.UserPreferenceKey;

            // set object variable reference            
            PlaceHolderCore = dynUserPreferenceKeyId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        protected void drpDataTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDataTypeId.Text = drpDataTypeList.SelectedItem.Value;
        }

        #endregion

    }
}