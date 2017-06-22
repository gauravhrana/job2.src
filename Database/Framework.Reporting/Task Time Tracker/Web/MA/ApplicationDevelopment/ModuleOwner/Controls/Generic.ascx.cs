using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.ModuleOwner.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
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

		public int? ModuleOwnerId
		{
			get
			{
				if (txtModuleOwnerId.Enabled)
				{
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtModuleOwnerId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtModuleOwnerId.Text);
				}
			}
            set
            {
                txtModuleOwnerId.Text = (value == null) ? String.Empty : value.ToString();
            }
		}

		public int? ModuleId
		{
			get
			{
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtModule.Text.Trim());
                else
                    return int.Parse(drpModuleList.SelectedItem.Value);
			}
            set
            {
                txtModule.Text = (value == null) ? String.Empty : value.ToString();
            }
		}

		public string Developer
		{
			get
			{
				return txtDeveloper.Text.Trim();
			}
            set
            {
                txtDeveloper.Text = value ?? String.Empty;
            }
		}

		public int TotalHoursWorked
		{
			get
			{
				return int.Parse(txtTotalHoursWorked.Text.Trim());
			}
			set
			{
				txtTotalHoursWorked.Text = value.ToString() ;
			}
		}

        public int? DeveloperRoleId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtDeveloperRole.Text.Trim());
                else
                    return int.Parse(drpDeveloperRoleList.SelectedItem.Value);
            }
            set
            {
                txtDeveloperRole.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

		public int? FeatureOwnerStatusId
		{
			get
			{
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFeatureOwnerStatus.Text.Trim());
                else
                    return int.Parse(drpFeatureOwnerStatusList.SelectedItem.Value);
			}
            set
            {
                txtFeatureOwnerStatus.Text = (value == null) ? String.Empty : value.ToString();
            }
		}

		#endregion properties

        #region private methods

        public override int? Save(string action)
        {
            var data = new ModuleOwnerDataModel();

            data.ModuleOwnerId = ModuleOwnerId;
            data.DeveloperRoleId = DeveloperRoleId;
            data.ModuleId = ModuleId;
            data.FeatureOwnerStatusId = FeatureOwnerStatusId;
            data.Developer = Developer;
			data.TotalHoursWorked = TotalHoursWorked;
            data.ApplicationId = ApplicationId;


            if (action == "Insert")
            {
                var dtEntityOwner = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtEntityOwner.Rows.Count == 0)
                {
					TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ModuleOwnerId;
        }


        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var moduleData = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(moduleData, drpModuleList,
                StandardDataModel.StandardDataColumns.Name,
				ModuleDataModel.DataColumns.ModuleId);

			var developerRoleData = TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(developerRoleData, drpDeveloperRoleList,
                StandardDataModel.StandardDataColumns.Name,
                DeveloperRoleDataModel.DataColumns.DeveloperRoleId);

			var featureOwnerStatusData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(featureOwnerStatusData, drpFeatureOwnerStatusList,
                StandardDataModel.StandardDataColumns.Name,
                FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId);

			var Applicationdata = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(Applicationdata, drpApplication,
               StandardDataModel.StandardDataColumns.Name,
               BaseDataModel.BaseDataColumns.ApplicationId);

            if (isTesting)
            {
                drpModuleList.AutoPostBack = true;
                drpDeveloperRoleList.AutoPostBack = true;
                drpFeatureOwnerStatusList.AutoPostBack = true;
                if (drpModuleList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtModule.Text.Trim()))
                    {
                        drpModuleList.SelectedValue = txtModule.Text;
                    }
                    else
                    {
                        txtModule.Text = drpModuleList.SelectedItem.Value;
                    }
                }
                if (drpDeveloperRoleList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtDeveloperRole.Text.Trim()))
                    {
                        drpDeveloperRoleList.SelectedValue = txtDeveloperRole.Text;
                    }
                    else
                    {
                        txtDeveloperRole.Text = drpDeveloperRoleList.SelectedItem.Value;
                    }
                }
                if (drpFeatureOwnerStatusList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFeatureOwnerStatus.Text.Trim()))
                    {
                        drpFeatureOwnerStatusList.SelectedValue = txtFeatureOwnerStatus.Text;
                    }
                    else
                    {
                        txtFeatureOwnerStatus.Text = drpFeatureOwnerStatusList.SelectedItem.Value;
                    }
                }
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
                txtModule.Visible = true;
                txtApplication.Visible = true;
                txtDeveloperRole.Visible = true;
                txtFeatureOwnerStatus.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplication.Text.Trim()))
                {
                    drpApplication.SelectedValue = txtApplication.Text;
                }           
                if (!string.IsNullOrEmpty(txtModule.Text.Trim()))
                {
                    drpModuleList.SelectedValue = txtModule.Text;
                }
                if (!string.IsNullOrEmpty(txtDeveloperRole.Text.Trim()))
                {
                    drpDeveloperRoleList.SelectedValue = txtDeveloperRole.Text;
                }
                if (!string.IsNullOrEmpty(txtFeatureOwnerStatus.Text.Trim()))
                {
                    drpFeatureOwnerStatusList.SelectedValue = txtFeatureOwnerStatus.Text;
                }
            }
        }

        protected void drpApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplication.Text = drpApplication.SelectedItem.Value;
        }

		public override void SetId(int setId, bool chkModuleOwnerId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkModuleOwnerId);
			txtModuleOwnerId.Enabled = chkModuleOwnerId;
			//txtDeveloper.Enabled = !chkModuleOwnerId;
			//txtName.Enabled = !chkModuleOwnerId;
			//txtFeatureOwnerStatusId.Enabled = !chkModuleOwnerId;
		}

        public void LoadData(int moduleOwnerId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new ModuleOwnerDataModel();
            data.ModuleOwnerId = moduleOwnerId;

            // get data
            var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtDeveloper.Text = item.Developer.ToString();
            txtModule.Text = item.ModuleId.ToString();
            txtDeveloperRole.Text = item.DeveloperRoleId.ToString();
            txtFeatureOwnerStatus.Text = item.FeatureOwnerStatusId.ToString();
			txtTotalHoursWorked.Text = item.TotalHoursWorked.ToString();

            drpModuleList.SelectedValue = item.ModuleId.ToString();
            drpDeveloperRoleList.SelectedValue = item.DeveloperRoleId.ToString();
            drpFeatureOwnerStatusList.SelectedValue = item.FeatureOwnerStatusId.ToString();

            if (!showId)
            {
                txtModuleOwnerId.Text = item.ModuleOwnerId.ToString();

                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, moduleOwnerId, PrimaryEntityKey);
            }
            else
            {
                txtModuleOwnerId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ModuleOwnerDataModel();

            ModuleId = data.ModuleId;
            ModuleOwnerId = data.ModuleOwnerId;
            Developer = data.Developer;
            DeveloperRoleId = data.DeveloperRoleId;
            FeatureOwnerStatusId = data.FeatureOwnerStatusId;

        }

		#endregion

		#region Events


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "ModuleOwner";
            FolderLocationFromRoot = "/Shared/QualityAssurance";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ModuleOwner;

            // set object variable reference            
            PlaceHolderCore = dynModuleOwnerId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

		protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupDropdown();
            }
			var isTesting = SessionVariables.IsTesting;
			txtModuleOwnerId.Visible = isTesting;
			lblModuleOwnerId.Visible = isTesting;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			var isTesting = SessionVariables.IsTesting;

			if (isTesting == true)
			{
				EnableControl(true, dynModuleOwnerId.Controls);
			}
			else
			{
				EnableControl(false, dynModuleOwnerId.Controls);
			}
        }

        protected void drpModuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtModule.Text = drpModuleList.SelectedItem.Value;
        }

        protected void drpDeveloperRoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDeveloperRole.Text = drpDeveloperRoleList.SelectedItem.Value;
        }

        protected void drpFeatureOwnerStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFeatureOwnerStatus.Text = drpFeatureOwnerStatusList.SelectedItem.Value;
        }

		#endregion

	}
}