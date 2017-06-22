using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.Web.UI.HtmlControls;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Functionality.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        public int? FunctionalityActiveStatusId
        {
            get
            {
                return int.Parse(drpFunctionalityActiveStatusList.SelectedItem.Value);
            }
            set
            {
                txtFunctionalityActiveStatusId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? FunctionalityPriorityId
        {
            get
            {
               return int.Parse(drpFunctionalityPriorityList.SelectedItem.Value);
            }
            set
            {
                txtFunctionalityPriorityId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;


			var functionalityActiveStatusData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetEntityDetails(FunctionalityActiveStatusDataModel.Empty, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            UIHelper.LoadDropDown(functionalityActiveStatusData, drpFunctionalityActiveStatusList,
                StandardDataModel.StandardDataColumns.Name,
                FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId);

			var functionalityPriorityData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityPriorityDataManager.GetEntityDetails(FunctionalityPriorityDataModel.Empty, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            UIHelper.LoadDropDown(functionalityPriorityData, drpFunctionalityPriorityList,
                StandardDataModel.StandardDataColumns.Name,
                FunctionalityPriorityDataModel.DataColumns.FunctionalityPriorityId);

            
            if (isTesting)
            {
                drpFunctionalityActiveStatusList.AutoPostBack = true;
                
                if (drpFunctionalityActiveStatusList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFunctionalityId.Text.Trim()))
                    {
                        drpFunctionalityActiveStatusList.SelectedValue = txtFunctionalityActiveStatusId.Text;
                    }
                    else
                    {
                        txtFunctionalityActiveStatusId.Text = drpFunctionalityActiveStatusList.SelectedItem.Value;
                    }
                }
               
                txtFunctionalityActiveStatusId.Visible = true;

                drpFunctionalityPriorityList.AutoPostBack = true;

                if (drpFunctionalityPriorityList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFunctionalityPriorityId.Text.Trim()))
                    {
                        drpFunctionalityPriorityList.SelectedValue = txtFunctionalityPriorityId.Text;
                    }
                    else
                    {
                        txtFunctionalityPriorityId.Text = drpFunctionalityPriorityList.SelectedItem.Value;
                    }
                }

                txtFunctionalityPriorityId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFunctionalityActiveStatusId.Text.Trim()))
                {
                    drpFunctionalityActiveStatusList.SelectedValue = txtFunctionalityActiveStatusId.Text;
                }
               
                txtFunctionalityActiveStatusId.Visible = false;

                if (!string.IsNullOrEmpty(txtFunctionalityPriorityId.Text.Trim()))
                {
                    drpFunctionalityPriorityList.SelectedValue = txtFunctionalityPriorityId.Text;
                }

                txtFunctionalityPriorityId.Visible = false;
            }
        }



        #region methods

        public override int? Save(string action)
        {
            var data = new FunctionalityDataModel();

            data.FunctionalityId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;
            data.FunctionalityActiveStatusId = FunctionalityActiveStatusId;
            data.FunctionalityPriorityId = FunctionalityPriorityId;

            if (action == "Insert")
            {
                if(!TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
					TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.FunctionalityId;
        }

        public override void SetId(int setId, bool chkFunctionalityId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFunctionalityId);
            CoreSystemKey.Enabled = chkFunctionalityId;
            //txtDescription.Enabled = !chkFunctionalityId;
            //txtName.Enabled = !chkFunctionalityId;
            //txtSortOrder.Enabled = !chkFunctionalityId;
        }

        public void LoadData(int functionalityId, bool showId)
        {
            Clear();

            var data = new FunctionalityDataModel();
            data.FunctionalityId = functionalityId;
			
			
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count > 1)
			{
				var item1 = items[0];
				txtApplicationId.Text = item1.Application.ToString();
				txtName.Text = item1.Name;
				txtSortOrder.Text = item1.SortOrder.ToString();
				editor.Text = item1.Description;
				txtFunctionalityActiveStatusId.Text = item1.FunctionalityActiveStatusId.ToString(); ;
				txtFunctionalityPriorityId.Text = item1.FunctionalityPriorityId.ToString();
				//return;
			}

			
            var item = items[0];

			var applicationInfo = ApplicationCommon.ApplicationCache[SessionVariables.RequestProfile.ApplicationId];
			txtApplicationId.Text = applicationInfo.Description;
			
            txtApplicationId.Enabled = false;

            drpFunctionalityPriorityList.SelectedValue = item.FunctionalityPriorityId.ToString();
            txtFunctionalityPriorityId.Text = item.FunctionalityPriorityId.ToString();

            drpFunctionalityActiveStatusList.SelectedValue = item.FunctionalityActiveStatusId.ToString();
            txtFunctionalityActiveStatusId.Text = item.FunctionalityActiveStatusId.ToString();

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.FunctionalityId;
                oHistoryList.Setup(PrimaryEntity, functionalityId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FunctionalityDataModel();

            SetData(data);
        }

        public void SetData(FunctionalityDataModel data)
        {
            SystemKeyId = data.FunctionalityId;
            FunctionalityPriorityId = data.FunctionalityPriorityId;
            
            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SetupDropdown();
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblFunctionalityId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Functionality;
            PrimaryEntityKey = "Functionality";
            FolderLocationFromRoot = "Functionality";

            PlaceHolderCore = dynFunctionalityId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtFunctionalityId;
            CoreControlName = txtName;
            CoreControlDescriptionKendoEditor = editor;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        protected void drpFunctionalityActiveStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFunctionalityActiveStatusId.Text = drpFunctionalityActiveStatusList.SelectedItem.Value;
        }

        protected void drpFunctionalityPriorityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFunctionalityPriorityId.Text = drpFunctionalityPriorityList.SelectedItem.Value;
        }

        #endregion

    }
}