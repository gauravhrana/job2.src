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

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityOwner.Controls
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

		public int? FunctionalityOwnerId
		{
			get
			{
				if (txtFunctionalityOwnerId.Enabled)
				{
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtFunctionalityOwnerId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtFunctionalityOwnerId.Text);
				}
			}
            set
            {
                txtFunctionalityOwnerId.Text = (value == null) ? String.Empty : value.ToString();
            }
		}

		public int? FunctionalityId
		{
			get
			{
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFunctionality.Text.Trim());
                else
                    return int.Parse(drpFunctionalityList.SelectedItem.Value);
			}
            set
            {
                txtFunctionality.Text = (value == null) ? String.Empty : value.ToString();
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
            var data = new FunctionalityOwnerDataModel();

            data.FunctionalityOwnerId = FunctionalityOwnerId;
            data.DeveloperRoleId = DeveloperRoleId;
            data.FunctionalityId = FunctionalityId;
            data.FeatureOwnerStatusId = FeatureOwnerStatusId;
            data.Developer = Developer;
            data.ApplicationId = ApplicationId;

            if (action == "Insert")
            {
                if(!TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
                    TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.FunctionalityOwnerId;
        }       
        
        private void SetupDropdown()

        {
            var isTesting = SessionVariables.IsTesting;

			var FunctionalityData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(FunctionalityData, drpFunctionalityList,
                StandardDataModel.StandardDataColumns.Name,
                FunctionalityDataModel.DataColumns.FunctionalityId);

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
                drpFunctionalityList.AutoPostBack = true;
                drpDeveloperRoleList.AutoPostBack = true;
                drpFeatureOwnerStatusList.AutoPostBack = true;
                if (drpFunctionalityList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFunctionality.Text.Trim()))
                    {
                        drpFunctionalityList.SelectedValue = txtFunctionality.Text;
                    }
                    else
                    {
                        txtFunctionality.Text = drpFunctionalityList.SelectedItem.Value;
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
                txtFunctionality.Visible = true;
                txtDeveloperRole.Visible = true;
                txtFeatureOwnerStatus.Visible = true;
            }
            else                           
            {
                if (!string.IsNullOrEmpty(txtFunctionality.Text.Trim()))
                {
                    drpFunctionalityList.SelectedValue = txtFunctionality.Text;
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

				txtFunctionality.Visible = false;
				txtDeveloperRole.Visible = false;
				txtFeatureOwnerStatus.Visible = false;
				txtApplication.Visible = false;
            }           
        }      
      

		public override void SetId(int setId, bool chkFunctionalityOwnerId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkFunctionalityOwnerId);
			txtFunctionalityOwnerId.Enabled = chkFunctionalityOwnerId;
			//txtDeveloper.Enabled = !chkFunctionalityOwnerId;
			//txtName.Enabled = !chkFunctionalityOwnerId;
			//txtFeatureOwnerStatusId.Enabled = !chkFunctionalityOwnerId;
		}

        public void LoadData(int functionalityOwnerId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new FunctionalityOwnerDataModel();
            data.FunctionalityOwnerId = functionalityOwnerId;

            // get data
            var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityOwnerDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

			txtDeveloper.Text = item.Developer.ToString();
			txtFunctionality.Text = item.Functionality.ToString();
			txtDeveloperRole.Text = item.DeveloperRole.ToString();
			txtFeatureOwnerStatus.Text = item.FeatureOwnerStatus.ToString();

			drpFunctionalityList.SelectedValue = item.FunctionalityId.ToString();
			drpDeveloperRoleList.SelectedValue = item.DeveloperRoleId.ToString();
			drpFeatureOwnerStatusList.SelectedValue = item.FeatureOwnerStatusId.ToString();

            if (!showId)
            {
                txtFunctionalityOwnerId.Text = item.FunctionalityOwnerId.ToString();

                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, functionalityOwnerId, PrimaryEntityKey);
            }
            else
            {
                txtFunctionalityOwnerId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FunctionalityOwnerDataModel();

            FunctionalityOwnerId = data.FunctionalityOwnerId;
            FunctionalityId = data.FunctionalityId;
            DeveloperRoleId = data.DeveloperRoleId;
            Developer = data.Developer;
            FeatureOwnerStatusId = data.FeatureOwnerStatusId;
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
			txtFunctionalityOwnerId.Visible = isTesting;
			lblFunctionalityOwnerId.Visible = isTesting;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			var isTesting = SessionVariables.IsTesting;

			if (isTesting == true)
			{
				EnableControl(true, dynFunctionalityOwnerId.Controls);
			}
			else
			{
				EnableControl(false, dynFunctionalityOwnerId.Controls);
			}
        }

        protected void drpFunctionalityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFunctionality.Text = drpFunctionalityList.SelectedItem.Value;
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

            PrimaryEntityKey             = "FunctionalityOwner";
            FolderLocationFromRoot       = "/Shared/QualityAssurance";
            PrimaryEntity                = Framework.Components.DataAccess.SystemEntity.FunctionalityOwner;

            // set object variable reference            
            PlaceHolderCore              = dynFunctionalityOwnerId;
            PlaceHolderAuditHistory      = dynAuditHistory;
            BorderDiv                    = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

		#endregion

	}
}