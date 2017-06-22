using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Framework.Components;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;

using System.Data;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus.Controls
{
	public partial class Generic : ControlGenericStandard
    {

        #region properties

		public string ConvertDateTimeFormat
		{
			get
			{
				return DateTimeHelper.CovertDateFormatToJavascript();
			}
		}

        public int? FunctionalityEntityStatusId
        {
            get
            {
                if (txtFunctionalityEntityStatusId.Enabled)
                {
                    return DefaultDataRules.CheckAndGetEntityId(txtFunctionalityEntityStatusId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtFunctionalityEntityStatusId.Text);
                }
            }
            set
            {
                txtFunctionalityEntityStatusId.Text = (value == null) ? String.Empty : value.ToString();
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

        public int? FunctionalityId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFunctionalityId.Text.Trim());
                else
                    return int.Parse(drpFunctionalityList.SelectedItem.Value);
            }
            set
            {
                txtFunctionalityId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

		public int? FunctionalityStatusId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFunctionalityStatusId.Text.Trim());
                else
                    return int.Parse(drpFunctionalityStatusList.SelectedItem.Value);
            }
            set
            {
                txtFunctionalityStatusId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? FunctionalityPriorityId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFunctionalityPriorityId.Text.Trim());
                else
                    return int.Parse(drpFunctionalityPriorityList.SelectedItem.Value);
            }
            set
            {
                txtFunctionalityPriorityId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string AssignedTo
        {
            get
            {
                return txtAssignedTo.Text;
            }
            set
            {
                txtAssignedTo.Text = value ?? String.Empty;
            }
        }

        public string Memo
        {
            get
            {
                return txtMemo.Text;
            }
            set
            {
                txtMemo.Text = value ?? String.Empty;
            }
        }

		public DateTime? TargetDate
		{
            get
            {
                return DateTimeHelper.FromUserDateFormatToDate(txtTargetDate.Text.Trim());
            }
            set
            {
                txtTargetDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
            }
		}

		public DateTime? StartDate
		{
			get
			{
                return DateTimeHelper.FromUserDateFormatToDate(txtStartDate.Text.Trim());
            }
            set
            {
                txtStartDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
            }
		}

		

        #endregion properties

        #region private methods

        public override int? Save(string action)
        {
            var data = new FunctionalityEntityStatusDataModel();

            data.FunctionalityEntityStatusId = FunctionalityEntityStatusId;
			data.FunctionalityId = FunctionalityId;
			data.FunctionalityPriorityId = FunctionalityPriorityId;
            data.FunctionalityStatusId = FunctionalityStatusId;
            data.Memo = Memo;
            data.StartDate = StartDate;
            data.TargetDate = TargetDate;
            data.SystemEntityTypeId = SystemEntityTypeId;
            data.AssignedTo = AssignedTo;

            if (action == "Insert")
            {
				var dtClient = FunctionalityEntityStatusDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtClient.Rows.Count == 0)
                {
                    FunctionalityEntityStatusDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				FunctionalityEntityStatusDataManager.Update(data, SessionVariables.RequestProfile);
            }

            // not correct ... when doing insert, we didn't get/change the value of ClientID ?
            return data.FunctionalityEntityStatusId;
        }


        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var systemEntityTypeData = SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(systemEntityTypeData, drpSystemEntityTypeList, 
                SystemEntityTypeDataModel.DataColumns.EntityName,
                SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

			var functionalityData = FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(functionalityData, drpFunctionalityList,
                StandardDataModel.StandardDataColumns.Name,
                FunctionalityDataModel.DataColumns.FunctionalityId);

            var functionalityStatusData = FunctionalityStatusDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(functionalityStatusData, drpFunctionalityStatusList,
                StandardDataModel.StandardDataColumns.Name,
                FunctionalityStatusDataModel.DataColumns.FunctionalityStatusId);

            var functionalityPriorityData = FunctionalityPriorityDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(functionalityPriorityData, drpFunctionalityPriorityList,
                StandardDataModel.StandardDataColumns.Name,
                FunctionalityPriorityDataModel.DataColumns.FunctionalityPriorityId);

            if (isTesting)
            {
                drpSystemEntityTypeList.AutoPostBack = true;
                drpFunctionalityList.AutoPostBack = true;
                drpFunctionalityStatusList.AutoPostBack = true;
                drpFunctionalityPriorityList.AutoPostBack = true;
                
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

                if (drpFunctionalityList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFunctionalityId.Text.Trim()))
                    {
                        drpFunctionalityList.SelectedValue = txtFunctionalityId.Text;
                    }
                    else
                    {
                        txtFunctionalityId.Text = drpFunctionalityList.SelectedItem.Value;
                    }
                }

                if (drpFunctionalityStatusList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFunctionalityStatusId.Text.Trim()))
                    {
                        drpFunctionalityStatusList.SelectedValue = txtFunctionalityStatusId.Text;
                    }
                    else
                    {
                        txtFunctionalityStatusId.Text = drpFunctionalityStatusList.SelectedItem.Value;
                    }
                }

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

                txtSystemEntityTypeId.Visible = true;
                txtFunctionalityId.Visible = true;
                txtFunctionalityStatusId.Visible = true;
                txtFunctionalityPriorityId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtSystemEntityTypeId.Text.Trim()))
                {
                    drpSystemEntityTypeList.SelectedValue = txtSystemEntityTypeId.Text;
                }
                
				if (!string.IsNullOrEmpty(txtFunctionalityId.Text.Trim()))
                {
                    drpFunctionalityList.SelectedValue = txtFunctionalityId.Text;
                }
                
				if (!string.IsNullOrEmpty(txtFunctionalityStatusId.Text.Trim()))
                {
                    drpFunctionalityStatusList.SelectedValue = txtFunctionalityStatusId.Text;
                }
                
				if (!string.IsNullOrEmpty(txtFunctionalityPriorityId.Text.Trim()))
                {
                    drpFunctionalityPriorityList.SelectedValue = txtFunctionalityPriorityId.Text;
                }

                txtSystemEntityTypeId.Visible = false;
                txtFunctionalityId.Visible = false;
                txtFunctionalityStatusId.Visible = false;
                txtFunctionalityPriorityId.Visible = false;
            }
        }

        public override void SetId(int setId, bool chkApplicationId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationId);
            txtFunctionalityEntityStatusId.Enabled = chkApplicationId;
            txtSystemEntityTypeId.Enabled = chkApplicationId;
            txtFunctionalityId.Enabled = chkApplicationId;
            drpFunctionalityList.Enabled = chkApplicationId;
            drpSystemEntityTypeList.Enabled = chkApplicationId;
        }

        private DataTable GetData(string key)
        {
            return GetFunctionalityEntityStatusArchiveData(int.Parse(key));
        }

        private DataTable GetFunctionalityEntityStatusArchiveData(int functionalityEntityStatusId)
        {
            var data = new FunctionalityEntityStatusArchiveDataModel();
            data.FunctionalityEntityStatusId = functionalityEntityStatusId;
			var dt = FunctionalityEntityStatusArchiveDataManager.SearchHistory(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetFunctionalityEntityStatusArchiveColumns()
        {
            //return Framework.Components.ApplicationSecurity.GetFunctionalityEntityStatusArchiveColumns("FunctionalityEntityStatusArchive_PC", AuditId);
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatusArchive, "Developer", SessionVariables.RequestProfile);
        }

        public void LoadData(int functionalityEntityStatusid, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new FunctionalityEntityStatusDataModel();
            data.FunctionalityEntityStatusId = functionalityEntityStatusid;

            // get data
			var items = FunctionalityEntityStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

			txtFunctionalityEntityStatusId.Text        = item.FunctionalityEntityStatusId.ToString();
            txtFunctionalityId.Text                    = item.FunctionalityId.ToString();
            txtFunctionalityPriorityId.Text            = item.FunctionalityPriorityId.ToString();
            txtFunctionalityStatusId.Text              = item.FunctionalityStatusId.ToString();
            txtSystemEntityTypeId.Text                 = item.SystemEntityTypeId.ToString();
            txtAssignedTo.Text                         = item.AssignedTo;
            txtMemo.Text                               = item.Memo;

            txtTargetDate.Text                         = item.TargetDate.Value.ToString(SessionVariables.UserDateFormat);
            txtStartDate.Text                          = item.StartDate.Value.ToString(SessionVariables.UserDateFormat);

            drpSystemEntityTypeList.SelectedValue      = item.SystemEntityTypeId.ToString();
            drpFunctionalityList.SelectedValue         = item.FunctionalityId.ToString();
            drpFunctionalityStatusList.SelectedValue   = item.FunctionalityStatusId.ToString();
            drpFunctionalityPriorityList.SelectedValue = item.FunctionalityPriorityId.ToString();


            if (!showId)
            {
                txtFunctionalityEntityStatusId.Text = item.FunctionalityEntityStatusId.ToString();

                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, functionalityEntityStatusid, PrimaryEntityKey);

                var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
                listControl.Setup("FunctionalityEntityStatusArchive", "", "FunctionalityEntityStatusArchiveId", functionalityEntityStatusid, true, GetData, GetFunctionalityEntityStatusArchiveColumns, "FunctionalityEntityStatusArchive");
                listControl.SetSession("true");
                listControl.HideControls();
                plcControlHolder.Controls.Add(listControl);
                plcControlHolder.Controls.Add(new Literal() { Text = "<br />" });
     
            }
            else
            {
                txtFunctionalityEntityStatusId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FunctionalityEntityStatusDataModel();

            FunctionalityEntityStatusId = data.FunctionalityEntityStatusId;
            FunctionalityId = data.FunctionalityId;
            FunctionalityPriorityId = data.FunctionalityPriorityId;
            FunctionalityStatusId = data.FunctionalityStatusId;
            SystemEntityTypeId = data.SystemEntityTypeId;
            AssignedTo = data.AssignedTo;
            Memo = data.Memo;
            TargetDate = data.TargetDate;
            StartDate = data.StartDate;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupDropdown();

                //CalendarExtenderTargetDate.Format = SessionVariables.UserDateFormat;
                lblUserDateTimeFormat.Text = "Date Format: " + SessionVariables.UserDateFormat;

                //CalendarExtenderStartDate.Format = SessionVariables.UserDateFormat;
                lblUserDateTimeFormat2.Text = "Date Format: " + SessionVariables.UserDateFormat;
            }

            var isTesting = SessionVariables.IsTesting;
            txtFunctionalityEntityStatusId.Visible = isTesting;
            lblFunctionalityEntityStatusId.Visible = isTesting;
        }

        protected void drpSystemEntityTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSystemEntityTypeId.Text = drpSystemEntityTypeList.SelectedItem.Value;
        }

        protected void drpFunctionalityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFunctionalityId.Text = drpFunctionalityList.SelectedItem.Value;
        }

        protected void drpFunctionalityPriorityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFunctionalityPriorityId.Text = drpFunctionalityPriorityList.SelectedItem.Value;
        }

        protected void drpFunctionalityStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFunctionalityStatusId.Text = drpFunctionalityStatusList.SelectedItem.Value;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "FunctionalityEntityStatus";
            FolderLocationFromRoot = "/Shared/QualityAssurance";
            PrimaryEntity = SystemEntity.FunctionalityEntityStatus;

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityEntityStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
            plcControlHolder.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        
        }


        #endregion

    }
}