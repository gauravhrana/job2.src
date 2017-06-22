using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using DataModel.TaskTimeTracker.TimeTracking;
using DataModel.TaskTimeTracker.Task;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;


namespace ApplicationContainer.UI.Web.Scheduling.ScheduleItem.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric

    {

		#region properties

		public int? ScheduleItemId
		{
			get
			{
				if (txtScheduleItemId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtScheduleItemId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtScheduleItemId.Text);
				}
			}
			set
			{
				txtScheduleItemId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ScheduleId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtScheduleId.Text.Trim());
				else
					return int.Parse(drpScheduleList.SelectedItem.Value);
			}
			set
			{
				txtScheduleId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? TaskFormulationId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtTaskFormulationId.Text.Trim());
				else
					return int.Parse(drpTaskFormulationList.SelectedItem.Value);
			}
			set
			{
				txtTaskFormulationId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public decimal? TotalTimeSpent
		{
			get
			{
				return decimal.Parse(txtTotalTimeSpent.Text);
			}
			set
			{
				txtTotalTimeSpent.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		#endregion properties		

		#region private methods

		public override int? Save(string action)
		{
			var data = new ScheduleItemDataModel();

			data.ScheduleItemId		= ScheduleItemId;
			data.ScheduleId			= ScheduleId;
			data.TaskFormulationId	= TaskFormulationId;
			data.TotalTimeSpent		= TotalTimeSpent;
			
			if (action == "Insert")
			{
                var dtScheduleItem = TaskTimeTracker.Components.Module.TimeTracking.ScheduleItemDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtScheduleItem.Rows.Count == 0)
				{
                    TaskTimeTracker.Components.Module.TimeTracking.ScheduleItemDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                TaskTimeTracker.Components.Module.TimeTracking.ScheduleItemDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ScheduleID ?
			return ScheduleItemId;
		}

		public override void SetId(int setId, bool chkScheduleItemId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkScheduleItemId);
			txtScheduleId.Enabled = chkScheduleItemId;

		}

		public void LoadData(int scheduleItemId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new ScheduleItemDataModel();
			dataQuery.ScheduleItemId = scheduleItemId;

            var items = TaskTimeTracker.Components.Module.TimeTracking.ScheduleItemDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			ScheduleItemId		= item.ScheduleItemId;
			ScheduleId			= item.ScheduleId;
			TaskFormulationId	= item.TaskFormulationId;
			TotalTimeSpent		= item.TotalTimeSpent;
			
			if (!showId)
			{
				txtScheduleItemId.Text = item.ScheduleId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ScheduleItem, scheduleItemId, "ScheduleItem");

			}
			else
			{
				txtScheduleItemId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}


		protected override void Clear()
		{
			base.Clear();

			var data = new ScheduleItemDataModel();

			ScheduleItemId		= data.ScheduleItemId;
			ScheduleId			= data.ScheduleId;
			TaskFormulationId	= data.TaskFormulationId;
			TotalTimeSpent		= data.TotalTimeSpent;
			
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ScheduleItem";
			FolderLocationFromRoot = "Scheduling/ScheduleItem";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleItem;

			// set object variable reference            
			PlaceHolderCore = dynScheduleItemId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var isTesting = SessionVariables.IsTesting;
                txtScheduleItemId.Visible = isTesting;
                lblScheduleItemId.Visible = isTesting;

                SetupDropdown();
            }
        }
        

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
            var scheduleData = TaskTimeTracker.Components.Module.TimeTracking.ScheduleDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(scheduleData, drpScheduleList, ScheduleDataModel.DataColumns.ScheduleId,
				ScheduleDataModel.DataColumns.ScheduleId);

            var taskFormulationData = TaskTimeTracker.Components.BusinessLayer.Task.TaskFormulationDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(taskFormulationData, drpTaskFormulationList, TaskFormulationDataModel.DataColumns.TaskFormulationId,
				TaskFormulationDataModel.DataColumns.TaskFormulationId);

            if (isTesting)
            {
                drpScheduleList.AutoPostBack = true;
                drpTaskFormulationList.AutoPostBack = true;
                if (drpScheduleList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtScheduleId.Text.Trim()))
                    {
                        drpScheduleList.SelectedValue = txtScheduleId.Text;
                    }
                    else
                    {
                        txtScheduleId.Text = drpScheduleList.SelectedItem.Value;
                    }
                }
                if (drpTaskFormulationList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTaskFormulationId.Text.Trim()))
                    {
                        drpTaskFormulationList.SelectedValue = txtTaskFormulationId.Text;
                    }
                    else
                    {
                        txtTaskFormulationId.Text = drpTaskFormulationList.SelectedItem.Value;
                    }
                }
                txtScheduleId.Visible = true;
                txtTaskFormulationId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTaskFormulationId.Text.Trim()))
                {
                    drpTaskFormulationList.SelectedValue = txtTaskFormulationId.Text;
                }
                if (!string.IsNullOrEmpty(txtScheduleId.Text.Trim()))
                {
                    drpScheduleList.SelectedValue = txtScheduleId.Text;
                }
            }
        }

        protected void drpScheduleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtScheduleId.Text = drpScheduleList.SelectedItem.Value;
        }

        protected void drpTaskFormulationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTaskFormulationId.Text = drpTaskFormulationList.SelectedItem.Value;
		}

		#endregion 
	}
}