using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Framework.Components.ReleaseLog;
using Framework.Components.ReleaseLog.DomainModel;


namespace Shared.UI.Web.ApplicationManagement.ReleaseLogDetail.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? ReleaseLogDetailId
        {
            get
            {
                if (txtReleaseLogDetailId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtReleaseLogDetailId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtReleaseLogDetailId.Text);
                }
            }
            set
            {
                txtReleaseLogDetailId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? ApplicationId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplicationId.Text.Trim());
                else
                    return int.Parse(drpApplicationIdList.SelectedItem.Value);
            }
            set
            {
                txtApplicationId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

		public int? NewApplicationId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtNewApplicationId.Text.Trim());
				else
					return int.Parse(drpNewApplicationIdList.SelectedItem.Value);
			}
			set
			{
				txtNewApplicationId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

        public int? ReleaseLogId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtReleaseLogId.Text.Trim());
                else
                    return int.Parse(drpReleaseLogList.SelectedItem.Value);
            }
            set
            {
                txtReleaseLogId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public DropDownList ReleaseLogIdDropDownList
        {
            get
            {
                return drpReleaseLogList;
            }

        }

        public int? ReleaseIssueTypeId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtReleaseIssueTypeId.Text.Trim());
                else
                    return int.Parse(drpReleaseIssueTypeList.SelectedItem.Value);
            }
            set
            {
                txtReleaseIssueTypeId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? ReleasePublishCategoryId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtReleasePublishCategoryId.Text.Trim());
                else
                    return int.Parse(drpReleasePublishCategoryList.SelectedItem.Value);
            }
            set
            {
                txtReleasePublishCategoryId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string Feature
        {
            get
            {
                return txtFeature.Text;
            }
            set
            {
                txtFeature.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

		public int? ModuleId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtModuleId.Text.Trim());
				else
					return int.Parse(drpModuleList.SelectedItem.Value);
			}
			set
			{
				txtModuleId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ReleaseFeatureId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtReleaseFeatureId.Text.Trim());
				else
					return int.Parse(drpReleaseFeatureList.SelectedItem.Value);
			}
			set
			{
				txtReleaseFeatureId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? SystemEntityTypeId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtSystemEntityTypeId.Text.Trim());
				else
					return int.Parse(drpSystemEntityTypeList.SelectedItem.Value);
			}
			set
			{
				txtSystemEntityTypeId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

        public string TimeSpent
        {
            get
            {
                return txtTimeSpent.Text;
            }
            set
            {
                txtTimeSpent.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string JIRA
        {
            get
            {
                return txtJIRA.Text;
            }
            set
            {
                txtJIRA.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string PrimaryEntity
        {
            get
            {
                return txtPrimaryEntity.Text;
            }
            set
            {
                txtPrimaryEntity.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? ItemNo
        {
            get
            {
                return int.Parse(txtItemnNo.Text.Trim());
            }
            set
            {
                txtItemnNo.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string Description
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtItemnNo.Text, txtDescription.InnerText);
            }
            set
            {
                txtDescription.InnerText = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string PrimaryDeveloper
        {
            get
            {
                return txtPrimaryDeveloper.Text;
            }
            set
            {
                txtPrimaryDeveloper.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? SortOrder
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
            }
            set
            {
                txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        #endregion properties

        #region methods

        public override int? Save(string action)
        {
            var data                      = new ReleaseLogDetailDataModel();

            data.ReleaseLogDetailId       = ReleaseLogDetailId;
            data.ApplicationId            = ApplicationId;
            data.ItemNo                   = ItemNo;
            data.Description              = Description;
            data.PrimaryDeveloper         = PrimaryDeveloper;
            data.SortOrder                = SortOrder;
            data.ReleaseIssueTypeId       = ReleaseIssueTypeId;
            data.ReleasePublishCategoryId = ReleasePublishCategoryId;
            data.Feature                  = Feature;
			data.ModuleId				  = ModuleId;
			data.ReleaseFeatureId         = ReleaseFeatureId;
            data.JIRA                     = JIRA;
            data.PrimaryEntity            = PrimaryEntity;
            data.ReleaseLogId             = ReleaseLogId;
            data.TimeSpent                = TimeSpent;	
			data.SystemEntityTypeId		  = SystemEntityTypeId;

			//if (drpNewApplicationIdList.Enabled == false)
			if (drpNewApplicationIdList.SelectedItem.Value == "-1" || drpNewApplicationIdList.SelectedItem.Value == "100")
				Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.Save(data, AuditId, action);
			else
				ReleaseNotesWorkFlow.MoveToApplication(ReleaseLogDetailId.Value, NewApplicationId.Value, AuditId);
            // not correct ... when doing insert, we didn't get/change the value of ReleaseLogID ?
            return ReleaseLogDetailId;
        }

        public override void SetId(int setId, bool chkReleaseLogDetailId)
        {
            ViewState["SetId"] = setId;

            //load data
            LoadData((int)ViewState["SetId"], chkReleaseLogDetailId);
            txtReleaseLogDetailId.Enabled = chkReleaseLogDetailId;
            //txtDescription.Enabled = !chkReleaseLogId;
            //txtName.Enabled = chkReleaseLogId;
            //txtSortOrder.Enabled = !chkReleaseLogId;
        }

        public void LoadData(int releaseLogDetailId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new ReleaseLogDetailDataModel();
            data.ReleaseLogDetailId = releaseLogDetailId;

            // get data
			var items = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetEntityDetails(data, AuditId);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item                 = items[0];

            ReleaseLogDetailId	     = item.ReleaseLogDetailId;
            ApplicationId		     = item.ApplicationId;
            ItemNo				     = item.ItemNo;
            Description			     = item.Description;
            PrimaryDeveloper	     = item.PrimaryDeveloper;
            SortOrder			     = item.SortOrder;
            ReleaseIssueTypeId	     = item.ReleaseIssueTypeId;
            ReleasePublishCategoryId = item.ReleasePublishCategoryId;
            Feature				     = item.Feature;
			ModuleId			     = item.ModuleId;
			ReleaseFeatureId	     = item.ReleaseFeatureId;
            JIRA				     = item.JIRA;
            PrimaryEntity		     = item.PrimaryEntity;
            ReleaseLogId		     = item.ReleaseLogId;
            TimeSpent			     = item.TimeSpent;
			SystemEntityTypeId		 = item.SystemEntityTypeId;

            if (!showId)
            {
                txtReleaseLogDetailId.Text      = item.ReleaseLogDetailId.ToString();
                drpApplicationIdList.Enabled    = false;
                txtApplicationId.Enabled        = false;

				drpNewApplicationIdList.Enabled = true;
				txtNewApplicationId.Enabled     = true;
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(base.PrimaryEntity, releaseLogDetailId, PrimaryEntityKey);

            }
            else
            {
                txtReleaseLogDetailId.Text = String.Empty;
				
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
        }

        protected override void Clear()
        {
            base.Clear();

            var data                 = new ReleaseLogDetailDataModel();

            ReleaseLogDetailId	     = data.ReleaseLogDetailId;
            ApplicationId		     = data.ApplicationId;
            ItemNo				     = data.ItemNo;
            Description			     = data.Description;
            PrimaryDeveloper	     = data.PrimaryDeveloper;
            SortOrder			     = data.SortOrder;
            ReleaseIssueTypeId	     = data.ReleaseIssueTypeId;
            ReleasePublishCategoryId = data.ReleasePublishCategoryId;
            Feature				     = data.Feature;
			ModuleId				 = data.ModuleId;
			ReleaseFeatureId         = data.ReleaseFeatureId;
            JIRA				     = data.JIRA;
            PrimaryEntity		     = data.PrimaryEntity;
            ReleaseLogId		     = data.ReleaseLogId;
            TimeSpent			     = data.TimeSpent;
			SystemEntityTypeId		 = data.SystemEntityTypeId;
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var releaselogData = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetList(SessionVariables.AuditId);
            UIHelper.LoadDropDown(releaselogData, drpReleaseLogList,
                StandardDataModel.StandardDataColumns.Name,
                ReleaseLogDataModel.DataColumns.ReleaseLogId);

			var releaseIssueTypeData = Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.GetList(SessionVariables.AuditId);
            UIHelper.LoadDropDown(releaseIssueTypeData, drpReleaseIssueTypeList,
                StandardDataModel.StandardDataColumns.Name,
                ReleaseIssueTypeDataModel.DataColumns.ReleaseIssueTypeId);

			var releasePublishCategoryData = Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.GetList(SessionVariables.AuditId);
            UIHelper.LoadDropDown(releasePublishCategoryData, drpReleasePublishCategoryList,
                StandardDataModel.StandardDataColumns.Name,
				ReleasePublishCategoryDataModel.DataColumns.ReleasePublishCategoryId); 
			
			var moduleData = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetList(SessionVariables.AuditId);
			UIHelper.LoadDropDown(moduleData, drpModuleList,
				StandardDataModel.StandardDataColumns.Name,
				DataModel.TaskTimeTracker.ApplicationDevelopment.ModuleDataModel.DataColumns.ModuleId);

			var releaseFeatureData = Framework.Components.ReleaseLog.ReleaseFeatureDataManager.GetList(SessionVariables.AuditId);
			UIHelper.LoadDropDown(releaseFeatureData, drpReleaseFeatureList,
				StandardDataModel.StandardDataColumns.Name,
				ReleaseFeatureDataModel.DataColumns.ReleaseFeatureId);

			var systemEntityTypeData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.AuditId);
			UIHelper.LoadDropDown(systemEntityTypeData, drpSystemEntityTypeList,
				DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.EntityName,
				DataModel.Framework.Core.SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

			var applicationdata = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.AuditId);
            UIHelper.LoadDropDown(applicationdata, drpApplicationIdList,
                                StandardDataModel.StandardDataColumns.Name,
                                BaseDataModel.BaseDataColumns.ApplicationId);
            drpApplicationIdList.SelectedValue = Framework.Components.DataAccess.SetupConfiguration.ApplicationId.ToString();

			var newApplicationdata = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.AuditId);
			UIHelper.LoadDropDown(newApplicationdata, drpNewApplicationIdList,
								StandardDataModel.StandardDataColumns.Name,
								BaseDataModel.BaseDataColumns.ApplicationId);
			drpNewApplicationIdList.SelectedValue = Framework.Components.DataAccess.SetupConfiguration.ApplicationId.ToString();

            if (isTesting)
            {
                drpReleaseLogList.AutoPostBack = true;
                if (drpReleaseLogList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtReleaseLogId.Text.Trim()))
                    {
                        drpReleaseLogList.SelectedValue = txtReleaseLogId.Text;
                    }
                    else
                    {
                        txtReleaseLogId.Text = drpReleaseLogList.SelectedItem.Value;
                    }
                }
                txtReleaseLogId.Visible = true;

                drpReleaseIssueTypeList.AutoPostBack = true;
                if (drpReleaseIssueTypeList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtReleaseIssueTypeId.Text.Trim()))
                    {
                        drpReleaseIssueTypeList.SelectedValue = txtReleaseIssueTypeId.Text;
                    }
                    else
                    {
                        txtReleaseIssueTypeId.Text = drpReleaseIssueTypeList.SelectedItem.Value;
                    }
                }
				txtReleaseIssueTypeId.Visible = true;

				drpModuleList.AutoPostBack = true;
				if (drpModuleList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtModuleId.Text.Trim()))
					{
						drpModuleList.SelectedValue = txtModuleId.Text;
					}
					else
					{
						txtModuleId.Text = drpModuleList.SelectedItem.Value;
					}
				}
				txtModuleId.Visible = true;

				drpReleaseFeatureList.AutoPostBack = true;
				if (drpReleaseFeatureList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtReleaseFeatureId.Text.Trim()))
					{
						drpReleaseFeatureList.SelectedValue = txtReleaseFeatureId.Text;
					}
					else
					{
						txtReleaseFeatureId.Text = drpReleaseFeatureList.SelectedItem.Value;
					}
				}
				txtReleaseFeatureId.Visible = true;

				drpSystemEntityTypeList.AutoPostBack = true;
				if (drpSystemEntityTypeList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
					{
						drpSystemEntityTypeList.SelectedValue = txtSystemEntityTypeId.Text;
					}
					else
					{
						txtSystemEntityTypeId.Text = drpSystemEntityTypeList.SelectedItem.Value;
					}
				}
				txtSystemEntityTypeId.Visible = true;

                drpReleasePublishCategoryList.AutoPostBack = true;
                if (drpReleasePublishCategoryList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtReleasePublishCategoryId.Text.Trim()))
                    {
                        drpReleasePublishCategoryList.SelectedValue = txtReleasePublishCategoryId.Text;
                    }
                    else
                    {
                        txtReleasePublishCategoryId.Text = drpReleasePublishCategoryList.SelectedItem.Value;
                    }
                }
                txtReleasePublishCategoryId.Visible = true;

                if (drpApplicationIdList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
                    {
                        drpApplicationIdList.SelectedValue = txtApplicationId.Text;
                    }
                    else
                    {
                        txtApplicationId.Text = drpApplicationIdList.SelectedItem.Value;
                    }
                }

                txtApplicationId.Visible = true;

				if (drpNewApplicationIdList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtNewApplicationId.Text.Trim()))
					{
						drpNewApplicationIdList.SelectedValue = txtNewApplicationId.Text;
					}
					else
					{
						txtNewApplicationId.Text = drpNewApplicationIdList.SelectedItem.Value;
					}
				}

				txtNewApplicationId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtReleasePublishCategoryId.Text.Trim()))
                {
                    drpReleasePublishCategoryList.SelectedValue = txtReleasePublishCategoryId.Text;
                }
                if (!string.IsNullOrEmpty(txtReleaseLogId.Text.Trim()))
                {
                    drpReleaseLogList.SelectedValue = txtReleaseLogId.Text;
                }
                if (!string.IsNullOrEmpty(txtReleaseIssueTypeId.Text.Trim()))
                {
                    drpReleaseIssueTypeList.SelectedValue = txtReleaseIssueTypeId.Text;
                }
                if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
                {
                    drpApplicationIdList.SelectedValue = txtApplicationId.Text;
                }
				if (!string.IsNullOrEmpty(txtNewApplicationId.Text.Trim()))
				{
					drpNewApplicationIdList.SelectedValue = txtNewApplicationId.Text;
				}
				if (!string.IsNullOrEmpty(txtModuleId.Text.Trim()))
				{
					drpModuleList.SelectedValue = txtModuleId.Text;
				}
				if (!string.IsNullOrEmpty(txtReleaseFeatureId.Text.Trim()))
				{
					drpReleaseFeatureList.SelectedValue = txtReleaseFeatureId.Text;
				}
				if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
				{
					drpSystemEntityTypeList.SelectedValue = txtSystemEntityTypeId.Text;
				}
            }
        }

        public void LoadData(int releaseLogId)
        {

            var setId = ApplicationCommon.GetSetId();
            if (!string.IsNullOrEmpty(releaseLogId.ToString()) && releaseLogId != 0)
            {
                drpReleaseLogList.SelectedValue = releaseLogId.ToString();
                drpReleaseLogList.Enabled = false;
                txtReleaseLogId.Text = releaseLogId.ToString();
                txtReleaseLogId.Enabled = false;
            }


        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupDropdown();
				if (ApplicationCommon.GetSetId() == 0)
				{
					drpNewApplicationIdList.Enabled = false;
					txtNewApplicationId.Enabled = false;
				}
				
				LoadData(ApplicationCommon.GetSetId());			
                
            }

            var isTesting = SessionVariables.IsTesting;
            txtReleaseLogDetailId.Visible = isTesting;
            lblReleaseLogDetailId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            base.PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLogDetail;
            PrimaryEntityKey = "ReleaseLogDetail";
            FolderLocationFromRoot = "ReleaseLogDetail";

            // set object variable reference            
            PlaceHolderCore = dynReleaseLogId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        protected void drpReleaseLogListList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReleaseLogId.Text = drpReleaseLogList.SelectedItem.Value;
        }

        protected void drpReleasePublishCategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReleasePublishCategoryId.Text = drpReleasePublishCategoryList.SelectedItem.Value;
        }

		protected void drpReleaseFeatureList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtReleaseFeatureId.Text = drpReleaseFeatureList.SelectedItem.Value;
		}

		protected void drpSystemEntityTypeList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSystemEntityTypeId.Text = drpSystemEntityTypeList.SelectedItem.Value;
		}

		protected void drpModuleList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtModuleId.Text = drpModuleList.SelectedItem.Value;
		}

        protected void drpReleaseIssueTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReleaseIssueTypeId.Text = drpReleaseIssueTypeList.SelectedItem.Value;
        }

        protected void drpApplicationIdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationId.Text = drpApplicationIdList.SelectedItem.Value;
        }

		protected void drpNewApplicationIdList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtNewApplicationId.Text = drpNewApplicationIdList.SelectedItem.Value;
		}

        #endregion

    }
}