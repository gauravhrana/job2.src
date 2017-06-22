using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.DeveloperRole.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {
        #region properties

        public int? ApplicationId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplication.Text.Trim());
                else
                    return int.Parse(drpApplication.SelectedItem.Value);
            }
            set
            {
                txtApplication.Text = (value == null) ? String.Empty : value.ToString();
            }

        }

        #endregion

        #region methods

        public override int? Save(string action)
        {
            var data = new DeveloperRoleDataModel();

            data.DeveloperRoleId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;
            data.ApplicationId = ApplicationId;


            if (action == "Insert")
            {
				if(!TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
					TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.DeveloperRoleId;
        }

        public override void SetId(int setId, bool chkDeveloperRoleId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkDeveloperRoleId);
            CoreSystemKey.Enabled = chkDeveloperRoleId;
            //txtDescription.Enabled = !chkDeveloperRoleId;
            //txtName.Enabled = !chkDeveloperRoleId;
            //txtSortOrder.Enabled = !chkDeveloperRoleId;
        }

        public void LoadData(int DeveloperRoleId, bool showId)
        {
            Clear();

            var data = new DeveloperRoleDataModel();
            data.DeveloperRoleId = DeveloperRoleId;

            var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.GetEntityDetails(data,  SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            ApplicationId = item.ApplicationId;

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.DeveloperRoleId;
                oHistoryList.Setup(PrimaryEntity, DeveloperRoleId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new DeveloperRoleDataModel();

            SetData(data);

        }

        public void SetData(DeveloperRoleDataModel data)
        {
            SystemKeyId = data.DeveloperRoleId;

            base.SetData(data);
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var Applicationdata = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(Applicationdata, drpApplication,
               StandardDataModel.StandardDataColumns.Name,
               BaseDataModel.BaseDataColumns.ApplicationId);

            if (isTesting)
            {
                drpApplication.AutoPostBack = true;
                if (drpApplication.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplication.Text.Trim()))
                    {
                        drpApplication.SelectedValue = txtApplication.Text;
                    }
                    else
                    {
                        txtApplication.Text = drpApplication.SelectedItem.Value;
                    }
                }
                txtApplication.Visible = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplication.Text.Trim()))
                {
                    drpApplication.SelectedValue = txtApplication.Text;
                }
            }
        }

        protected void drpApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplication.Text = drpApplication.SelectedItem.Value;
        }


        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblDeveloperRoleId.Visible = isTesting;

            if (!IsPostBack)
            {
                SetupDropdown();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.DeveloperRole;
            PrimaryEntityKey = "DeveloperRole";
            FolderLocationFromRoot = "DeveloperRole";

            PlaceHolderCore = dynDeveloperRoleId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtDeveloperRoleId;
            CoreControlName = txtName;
            CoreControlDescriptionKendoEditor = txtDescription2;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}