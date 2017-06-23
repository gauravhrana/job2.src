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
using System.Text;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLogDetail.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {
		
        public string TextBoxApplicationIdClientId;
        
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
                {
                    if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
                    {
                        return int.Parse(txtApplicationId.Text.Trim());
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtApplicationList.Text.Trim()))
                    {
                        return int.Parse(txtApplicationList.Text);
                    }

                }
                return null;	
            }
            set
            {
				txtApplicationList.Text = txtApplicationId.Text = (value == null) ? String.Empty : value.ToString();
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
                    return int.Parse(txtReleaseLogList.Text);
            }
            set
            {
                txtReleaseLogList.Text = txtReleaseLogId.Text = (value == null) ? String.Empty : value.ToString();
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
					return int.Parse(txtReleaseIssueTypeList.Text);
            }
            set
            {
				txtReleaseIssueTypeList.Text = txtReleaseIssueTypeId.Text = (value == null) ? String.Empty : value.ToString();
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
					return int.Parse(txtReleasePublishCategoryList.Text);
            }
            set
            {
				txtReleasePublishCategoryList.Text = txtReleasePublishCategoryId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string Feature
        {
            get
            {	
				return txtReleaseFeatureList.Text;
            }
        }

		public string Module
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return txtModule.Text.Trim();
				else
					return txtModuleList.Text.Trim();
			}
			set
			{
				txtModuleList.Text = txtModule.Text = (value == null) ? String.Empty : value.ToString();
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
					return int.Parse(txtReleaseFeatureList.Text);
			}
			set
			{
				txtReleaseFeatureList.Text = txtReleaseFeatureId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? SystemEntityTypeId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting && txtSystemEntityTypeId.Text.Trim() != "-1")
				{
					return int.Parse(txtSystemEntityTypeId.Text.Trim());
				}
				else if (txtEntityList.Text != "-1")
				{
					return int.Parse(txtEntityList.Text);
				}
				return null;
			}
			set
			{
				txtEntityList.Text = txtSystemEntityTypeId.Text = (value == null) ? String.Empty : value.ToString();
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
				return txtEntityList.Text;
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
			data.Module				      = Module;
			data.ReleaseFeatureId         = ReleaseFeatureId;
            data.JIRA                     = JIRA;
            data.PrimaryEntity            = PrimaryEntity;
            data.ReleaseLogId             = ReleaseLogId;
            data.TimeSpent                = TimeSpent;	
			data.SystemEntityTypeId		  = SystemEntityTypeId;
			data.RequestedDate			  =  DateTime.Now;  //RequestDate;      This will change, just a temp code

            //if (drpNewApplicationIdList.SelectedItem.Value == "-1" || drpNewApplicationIdList.SelectedItem.Value == "100")
			Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.Save(data, SessionVariables.RequestProfile, action);
            
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
			var items = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

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
			Module  			     = item.Module;
			ReleaseFeatureId	     = item.ReleaseFeatureId;
            JIRA				     = item.JIRA;
            ReleaseLogId		     = item.ReleaseLogId;
            TimeSpent			     = item.TimeSpent;
            if (item.SystemEntityTypeId != null)
            {
                SystemEntityTypeId = item.SystemEntityTypeId;
            }
            else
            {
                SystemEntityTypeId = -1;
            }

            if (!showId)
            {
                txtReleaseLogDetailId.Text      = item.ReleaseLogDetailId.ToString();
                //txtApplicationList.Enabled    = false;
                //txtApplicationId.Enabled        = false;

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
			Module  				 = data.Module;
			ReleaseFeatureId         = data.ReleaseFeatureId;
            JIRA				     = data.JIRA;
            ReleaseLogId		     = data.ReleaseLogId;
            TimeSpent			     = data.TimeSpent;
			SystemEntityTypeId		 = data.SystemEntityTypeId;
        }

		private void SetupDropdown()
		{

			var isTesting = SessionVariables.IsTesting;

            // Application Combo Configuration

            var configScript = AjaxHelper.GetKendoComboBoxConfigScript("GetApplicationList", "Name", "ApplicationId", 
                txtApplicationList.ClientID);
			
			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ApplicationId", configScript, true);

			txtApplicationList.Attributes.Add("onchange",
				"javascript:SetDevBoxValue('" + txtApplicationList.ClientID + "','" + txtApplicationId.ClientID + "')");

            string TextBoxApplicationIdClientId = txtApplicationList.ClientID;

            // set only in case of insert
            if (ApplicationId == null)
            {
                ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            }

            // System Entity Type Combo Configuration, Independent

            configScript = AjaxHelper.GetKendoComboBoxConfigScript("GetSystemEntityList", "EntityName", "SystemEntityTypeId",
                txtEntityList.ClientID);

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "SystemEntityTypeId", configScript, true);

            txtEntityList.Attributes.Add("onchange",
                "javascript:SetDevBoxValue('" + txtEntityList.ClientID + "','" + txtSystemEntityTypeId.ClientID + "')");

            // Release Log Combo Cascade Configuration, dependent on Application

            configScript = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetReleaseLogList", "Name", "ReleaseLogId",
                txtReleaseLogList.ClientID, TextBoxApplicationIdClientId);

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ReleaseLogId", configScript, true);

            txtReleaseLogList.Attributes.Add("onchange",
                "javascript:SetDevBoxValue('" + txtReleaseLogList.ClientID + "','" + txtReleaseLogId.ClientID + "')");

            // Module Combo  Cascade Configuration, dependent on Application

			configScript = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetModuleList", "Name", "Name", 
                txtModuleList.ClientID, TextBoxApplicationIdClientId);

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ModuleId", configScript, true);

			txtModuleList.Attributes.Add("onchange",
				"javascript:SetDevBoxValue('" + txtModuleList.ClientID + "','" + txtModule.ClientID + "')");

            // Release Feature Combo Cascade Configuration, dependent on Application

            configScript = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetReleaseFeatureList", "Name", "ReleaseFeatureId",
                txtReleaseFeatureList.ClientID, TextBoxApplicationIdClientId);

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ReleaseFeatureId", configScript, true);

            txtReleaseFeatureList.Attributes.Add("onchange",
                "javascript:SetDevBoxValue('" + txtReleaseFeatureList.ClientID + "','" + txtReleaseFeatureId.ClientID + "')");

            // Release Issue Type Combo Cascade Configuration, dependent on Application
            
            configScript = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetReleaseIssueTypeList", "Name", "ReleaseIssueTypeId",
                txtReleaseIssueTypeList.ClientID, TextBoxApplicationIdClientId);

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ReleaseIssueTypeId", configScript, true);

            txtReleaseIssueTypeList.Attributes.Add("onchange",
                "javascript:SetDevBoxValue('" + txtReleaseIssueTypeList.ClientID + "','" + txtReleaseIssueTypeId.ClientID + "')");

            // Release Publish Category Combo Cascade Configuration, dependent on Application

            configScript = AjaxHelper.GetCascadeKendoComboBoxConfigScript("GetReleasePublishCategoryList", "Name", "ReleasePublishCategoryId",
                txtReleasePublishCategoryList.ClientID, TextBoxApplicationIdClientId);

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ReleasePublishCategoryId", configScript, true);

            txtReleasePublishCategoryList.Attributes.Add("onchange",
                "javascript:SetDevBoxValue('" + txtReleasePublishCategoryList.ClientID + "','" + txtReleasePublishCategoryId.ClientID + "')");
		}
        		
        public void LoadData(int releaseLogId)
        {

            //var setId = ApplicationCommon.GetSetId();
            //if (!string.IsNullOrEmpty(releaseLogId.ToString()) && releaseLogId != 0)
            //{
            //    txtReleaseLogList.Text = releaseLogId.ToString();
            //    txtReleaseLogList.Enabled = false;
            //    txtReleaseLogId.Text = releaseLogId.ToString();
            //    txtReleaseLogId.Enabled = false;
            //}


        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupDropdown();
				
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

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        #endregion

    }
}