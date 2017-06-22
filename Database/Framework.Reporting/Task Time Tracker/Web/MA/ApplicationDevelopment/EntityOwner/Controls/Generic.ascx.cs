using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityOwner.Controls
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

		public int? EntityOwnerId
		{
			get
			{
				if (txtEntityOwnerId.Enabled)
				{
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtEntityOwnerId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtEntityOwnerId.Text);
				}
			}
            set
            {
                txtEntityOwnerId.Text = (value == null) ? String.Empty : value.ToString();
            }
		}

		public int? EntityId
		{
			get
			{
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtEntity.Text.Trim());
                else
                    return int.Parse(drpEntityList.SelectedItem.Value);
			}
            set
            {
                txtEntity.Text = (value == null) ? String.Empty : value.ToString();
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
            var data = new EntityOwnerDataModel();

            data.EntityOwnerId = EntityOwnerId;
            data.DeveloperRoleId = DeveloperRoleId;
            data.EntityId = EntityId;
            data.FeatureOwnerStatusId = FeatureOwnerStatusId;
            data.Developer = Developer;
            data.ApplicationId = ApplicationId;

            if (action == "Insert")
            {
				var dtEntityOwner = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtEntityOwner.Rows.Count == 0)
                {
					TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.EntityOwnerId;
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var EntityData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(EntityData, drpEntityList,
                SystemEntityTypeDataModel.DataColumns.EntityName,
                SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

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
                drpEntityList.AutoPostBack = true;
                drpDeveloperRoleList.AutoPostBack = true;
                drpFeatureOwnerStatusList.AutoPostBack = true;
                if (drpEntityList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtEntity.Text.Trim()))
                    {
                        drpEntityList.SelectedValue = txtEntity.Text;
                    }
                    else
                    {
                        txtEntity.Text = drpEntityList.SelectedItem.Value;
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
                txtEntity.Visible = true;
                txtDeveloperRole.Visible = true;
                txtFeatureOwnerStatus.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtEntity.Text.Trim()))
                {
                    drpEntityList.SelectedValue = txtEntity.Text;
                }
                if (!string.IsNullOrEmpty(txtDeveloperRole.Text.Trim()))
                {
                    drpDeveloperRoleList.SelectedValue = txtDeveloperRole.Text;
                }
                if (!string.IsNullOrEmpty(txtFeatureOwnerStatus.Text.Trim()))
                {
                    drpFeatureOwnerStatusList.SelectedValue = txtFeatureOwnerStatus.Text;
                }
                if (!string.IsNullOrEmpty(txtApplication.Text.Trim()))
                {
                    drpApplication.SelectedValue = txtApplication.Text;
                }
            }
        }

		public override void SetId(int setId, bool chkEntityOwnerId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkEntityOwnerId);
			txtEntityOwnerId.Enabled = chkEntityOwnerId;
			//txtDeveloper.Enabled = !chkEntityOwnerId;
			//txtName.Enabled = !chkEntityOwnerId;
			//txtFeatureOwnerStatusId.Enabled = !chkEntityOwnerId;
		}

        public void LoadData(int entityOwnerId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new EntityOwnerDataModel();
            data.EntityOwnerId = entityOwnerId;

            // get data
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtDeveloper.Text           =  item.Developer.ToString();
            txtEntity.Text              =  item.Entity.ToString();
            txtDeveloperRole.Text       =  item.DeveloperRoleId.ToString();
            txtFeatureOwnerStatus.Text  =  item.FeatureOwnerStatusId.ToString();

            drpEntityList.SelectedValue             =  item.EntityId.ToString();
            drpDeveloperRoleList.SelectedValue      =  item.DeveloperRoleId.ToString();
            drpFeatureOwnerStatusList.SelectedValue =  item.FeatureOwnerStatusId.ToString();


            if (!showId)
            {
                txtEntityOwnerId.Text = item.EntityOwnerId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, entityOwnerId, PrimaryEntityKey);
            }
            else
            {
                txtEntityOwnerId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new EntityOwnerDataModel();

            DeveloperRoleId = data.DeveloperRoleId;
            EntityOwnerId = data.EntityOwnerId;
            EntityId = data.EntityId;
            FeatureOwnerStatusId = data.FeatureOwnerStatusId;
            Developer = data.Developer;
            ApplicationId = data.ApplicationId;
            
        }

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupDropdown();
            }
			var isTesting = SessionVariables.IsTesting;
			txtEntityOwnerId.Visible = isTesting;
			lblEntityOwnerId.Visible = isTesting;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			var isTesting = SessionVariables.IsTesting;

			if (isTesting == true)
			{
				EnableControl(true, dynEntityOwnerId.Controls);
			}
			else
			{
				EnableControl(false, dynEntityOwnerId.Controls);
			}
        }

        protected void drpEntityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEntity.Text = drpEntityList.SelectedItem.Value;
        }

        protected void drpDeveloperRoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDeveloperRole.Text = drpDeveloperRoleList.SelectedItem.Value;
        }

        protected void drpFeatureOwnerStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFeatureOwnerStatus.Text = drpFeatureOwnerStatusList.SelectedItem.Value;
        }

        protected void drpApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplication.Text = drpApplication.SelectedItem.Value;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.EntityOwner;
            PrimaryEntityKey = "EntityOwner";
            FolderLocationFromRoot = "/Shared/QualityAssurance";

            // set object variable reference            
            PlaceHolderCore = dynDeveloperRoleId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

		#endregion

	}
}