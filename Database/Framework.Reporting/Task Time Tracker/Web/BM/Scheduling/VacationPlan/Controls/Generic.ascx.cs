using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using Framework.Components;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using System.Globalization;
using DataModel.TaskTimeTracker.TimeTracking;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.VacationPlan.Controls
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

        public int? VacationPlanId
        {
            get
            {
                if (txtVacationPlanId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtVacationPlanId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtVacationPlanId.Text);
                }
            }
            set
            {
                txtVacationPlanId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

		//public int? ApplicationUserId
		//{
		//	get
		//	{
		//		var isTesting = SessionVariables.IsTesting;
		//		if (isTesting)
		//			return int.Parse(txtApplicationUserList.Text.Trim());
		//		else
		//			return int.Parse(txtApplicationUserId.Text);
		//	}
		//	set
		//	{
		//		txtApplicationUserList.Text = txtApplicationUserId.Text = (value == null) ? String.Empty : value.ToString();
		//	}
		//}

		public int? ApplicationUserId
		{
			get
			{

				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
				{
					if (!string.IsNullOrEmpty(txtApplicationUserId.Text.Trim()))
					{
						return int.Parse(txtApplicationUserId.Text.Trim());
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(txtApplicationUserList.Text.Trim()))
					{
						return int.Parse(txtApplicationUserList.Text);
					}

				}
				return null;
			}
			set
			{
				txtApplicationUserList.Text = txtApplicationUserId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

        public DateTime? StartDate
        {
            get
            {
				return DateTime.ParseExact(txtStartDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);			
            }
            set
            {
				txtStartDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
            }
        }

        public DateTime? EndDate
        {
            get
            {
				return DateTime.ParseExact(txtEndDate.Text, SessionVariables.UserDateFormat, DateTimeFormatInfo.InvariantInfo);				
            }
            set
            {
				txtEndDate.Text = (value == null) ? String.Empty : value.Value.ToString(SessionVariables.UserDateFormat);
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
                return Framework.Components.DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.InnerText);
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
                return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
            }
            set
            {
                txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

    #endregion

        #region private methods

        public override void SetId(int setId, bool chkVacationPlanId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkVacationPlanId);
            txtVacationPlanId.Enabled = chkVacationPlanId;           
            //txtDescription.Enabled = !chkApplicationRoleId;
            //txtName.Enabled = !chkApplicationRoleId;
            //txtSortOrder.Enabled = !chkApplicationRoleId;
        }

        public override int? Save(string action)
        {
            var data = new VacationPlanDataModel();

            data.VacationPlanId      = VacationPlanId;
            data.ApplicationUserId   = ApplicationUserId;
            data.StartDate           = StartDate;
            data.EndDate             = EndDate;
            data.Name                = Name;
            data.Description         = Description;
            data.SortOrder           = SortOrder;                       

            if (action == "Insert")
            {
                var dtVacationPlan = VacationPlanDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtVacationPlan.Rows.Count == 0)
                {
                    VacationPlanDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                VacationPlanDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.VacationPlanId;
        }

        public void LoadData(int vacationPlanId, bool showId)
        {
            // clear UI				

            Clear();

            var dataQuery = new VacationPlanDataModel();
            dataQuery.VacationPlanId = vacationPlanId;

            var items = TaskTimeTracker.Components.BusinessLayer.VacationPlanDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];
            //var dt = new ApplicationUserDataModel();
            //var appUserId = item.ApplicationUserId;
            //dt.ApplicationUserId = appUserId;

            //var appUserName = ApplicationUserDataManager.Search(dt, SessionVariables.RequestProfile);

            VacationPlanId           = item.VacationPlanId;
            ApplicationUserId       = item.ApplicationUserId;
            StartDate                = item.StartDate;
            EndDate                  = item.EndDate;
            Name                     = item.Name;
            Description              = item.Description;
            SortOrder                = item.SortOrder;

            if (!showId)
            {
                txtVacationPlanId.Text = item.VacationPlanId.ToString();


                oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.VacationPlan, vacationPlanId, "VacationPlan");

            }
            else
            {
                txtVacationPlanId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
        }    
          
        protected override void Clear()
        {
            base.Clear();

            var data = new VacationPlanDataModel();

            SetData(data);
        }

        public void SetData(VacationPlanDataModel data)
        {
            SystemKeyId = data.VacationPlanId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            SetupDropdown();
            CoreSystemKey.Visible = isTesting;
            lblVacationPlanId.Visible = isTesting;
			
        }

        protected void drpApplicationUserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationUserId.Text = drpApplicationUserList.SelectedItem.Value;
        }

        private string GetKendoComboBoxConfigScript(string methodName, string dataTextField, string dataValueField, TextBox txtBoxList, bool addAllItem = true)
        {
            const string stringA = @"
			$(document).ready(function ()
			{{
				var local_url = '{0}';
				var local_element = '#{1}';
				var local_dataTextField = '{2}';
				var local_dataValueField = '{3}';
				var local_addElement = {4};
				libary_kendo_getData(local_url, local_element, local_dataTextField, local_dataValueField, local_addElement);

			}});
			";

            var a = string.Format(stringA
                , ("http://localhost:53331/API/AutoComplete.asmx/" + methodName)
                , txtBoxList.ClientID
                , dataTextField
                , dataValueField
                , addAllItem.ToString().ToLower());

            return a;
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

            var configScript = GetKendoComboBoxConfigScript("GetApplicationUserList", "FullName", "ApplicationUserId", txtApplicationUserList);

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ApplicationUserId", configScript, true);

            if (isTesting)
            {
                drpApplicationUserList.AutoPostBack = true;
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
                txtApplicationUserId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplicationUserId.Text.Trim()))
                {
                    drpApplicationUserList.SelectedValue = txtApplicationUserId.Text;
                }
                txtApplicationUserId.Visible = false;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.VacationPlan;
            PrimaryEntityKey = "VacationPlan";
            FolderLocationFromRoot = "VacationPlan";

            PlaceHolderCore = dynVacationPlanId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtVacationPlanId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;            

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
         
}
