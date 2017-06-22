using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using System.Globalization;

namespace Shared.UI.Web.TasksAndWorkflow.TaskRun.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

		#region properties


		public int? TaskRunId
		{
			get
			{
				if (txtTaskRunId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTaskRunId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtTaskRunId.Text);
				}
			}
			set
			{
				txtTaskRunId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? TaskScheduleId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtTaskScheduleId.Text.Trim());
				else
					return int.Parse(drpTaskScheduleList.SelectedItem.Value);
			}
			set
			{
				txtTaskScheduleId.Text = (value == null) ? String.Empty : value.ToString();
			}

		}

		public int? TaskEntityId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtTaskEntityId.Text.Trim());
				else
					return int.Parse(drpTaskEntityList.SelectedItem.Value);
			}
			set
			{
				txtTaskEntityId.Text = (value == null) ? String.Empty : value.ToString();
			}

		}

		public DateTime? BusinessDate
		{
			get
			{
				return DateTime.Parse(txtBusinessDate.Text);
			}
			set
			{
				txtBusinessDate.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public DateTime? StartTime
		{
			get
			{

				if (txtStartTime.Text == "")
				{
					return null;
				}
				else
				{
					return DateTime.Parse(txtStartTime.Text);
				}
			}
			set
			{
				txtStartTime.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public DateTime? EndTime
		{
			get
			{
				if (txtEndTime.Text == "")
				{
					return null;
				}
				else
				{
					return DateTime.Parse(txtEndTime.Text);
				}
			}
			set
			{
				txtEndTime.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string RunBy
		{
			get
			{
				return txtRunBy.Text.Trim();
			}
			set
			{
				txtRunBy.Text = value ?? String.Empty;
			}
		}		

		#endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new TaskRunDataModel();

			data.TaskRunId		= TaskRunId;
			data.TaskEntityId	= TaskEntityId;
			data.TaskScheduleId = TaskScheduleId;
			data.StartTime		= StartTime;
			data.EndTime		= EndTime;
			data.RunBy			= RunBy;
			data.BusinessDate	= BusinessDate;

			if (action == "Insert")
			{
				if(!Framework.Components.TasksAndWorkflow.TaskRunDataManager.DoesExist(data, SessionVariables.RequestProfile))
				{
					Framework.Components.TasksAndWorkflow.TaskRunDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.TasksAndWorkflow.TaskRunDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of TaskRunID ?
			return TaskRunId;
		}

		public override void SetId(int setId, bool chkTaskRunId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTaskRunId);
			txtTaskRunId.Enabled = chkTaskRunId;
			//txtTaskEntityId.Enabled = !chkTaskRunId;
			//txtTaskScheduleId.Enabled = !chkTaskRunId;
			//txtBusinessDate.Enabled = !chkTaskRunId;
			//txtStartTime.Enabled = !chkTaskRunId;
			//txtEndTime.Enabled = !chkTaskRunId;
			//txtRunBy.Enabled = !chkTaskRunId;

			drpTaskEntityList.Enabled = !chkTaskRunId;
			drpTaskScheduleList.Enabled = !chkTaskRunId;
		}

		public void LoadData(int taskRunId, bool showId)
		{
			// clear UI				

			Clear();

			var dataQuery = new TaskRunDataModel();
			dataQuery.TaskRunId = taskRunId;

			var items = Framework.Components.TasksAndWorkflow.TaskRunDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			TaskRunId		= item.TaskRunId;
			TaskEntityId	= item.TaskEntityId;
			TaskScheduleId	= item.TaskScheduleId;
			//BusinessDate	= item.BusinessDate;
			StartTime		= item.StartTime;
			EndTime			= item.EndTime;
			RunBy			= item.RunBy;
		

			if (!showId)
			{
				txtTaskRunId.Text = item.TaskRunId.ToString();

				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.TaskRun, taskRunId, "TaskRun");

			}
			else
			{
				txtTaskRunId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}


		protected override void Clear()
		{
			base.Clear();

			var data = new TaskRunDataModel();

			TaskRunId		= data.TaskRunId;
			TaskEntityId	= data.TaskEntityId;
			TaskScheduleId	= data.TaskScheduleId;
			//BusinessDate	= data.BusinessDate;
			StartTime		= data.StartTime;
			EndTime			= data.EndTime;
			RunBy			= data.RunBy;
		}	
		

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var taskScheduleData = Framework.Components.TasksAndWorkflow.TaskScheduleDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskScheduleData, drpTaskScheduleList, TaskScheduleDataModel.DataColumns.TaskScheduleId, 
                TaskScheduleDataModel.DataColumns.TaskScheduleId);

			var taskEntityData = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskEntityData, drpTaskEntityList, StandardDataModel.StandardDataColumns.Name, 
                TaskEntityDataModel.DataColumns.TaskEntityId);

            if (isTesting)
            {
                drpTaskEntityList.AutoPostBack = true;
                drpTaskScheduleList.AutoPostBack = true;
                if (drpTaskScheduleList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTaskScheduleId.Text.Trim()))
                    {
                        drpTaskScheduleList.SelectedValue = txtTaskScheduleId.Text;
                    }
                    else
                    {
                        txtTaskScheduleId.Text = drpTaskScheduleList.SelectedItem.Value;
                    }
                }
                if (drpTaskEntityList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTaskEntityId.Text.Trim()))
                    {
                        drpTaskEntityList.SelectedValue = txtTaskEntityId.Text;
                    }
                    else
                    {
                        txtTaskEntityId.Text = drpTaskEntityList.SelectedItem.Value;
                    }
                }
                txtTaskScheduleId.Visible = true;
                txtTaskEntityId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTaskScheduleId.Text.Trim()))
                {
                    drpTaskScheduleList.SelectedValue = txtTaskScheduleId.Text;
                }
                if (!string.IsNullOrEmpty(txtTaskEntityId.Text.Trim()))
                {
                    drpTaskEntityList.SelectedValue = txtTaskEntityId.Text;
                }
            }
        }

        private void SetupCalendars()
        {
            var isTesting = SessionVariables.IsTesting;

            if (isTesting)
            {
                if (!string.IsNullOrEmpty(txtBusinessDate.Text.Trim()))
                {
                    clnBusinessDate.SelectedDate = DateTime.ParseExact(txtBusinessDate.Text.Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                }
                else
                {
                    txtBusinessDate.Text = clnBusinessDate.SelectedDate.ToString("yyyyMMdd"); 
                }

                txtBusinessDate.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtBusinessDate.Text.Trim()))
                {
                    clnBusinessDate.SelectedDate = DateTime.ParseExact(txtBusinessDate.Text.Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                }
            }
        }

        #endregion

        #region Events

        protected void clnBusinessDate_SelectionChanged(object sender, EventArgs e)
        {
			txtBusinessDate.Text = clnBusinessDate.SelectedDate.Date.ToShortDateString();
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                txtTaskRunId.Visible = isTesting;
                lblTaskRunId.Visible = isTesting;
                SetupDropdown();
                SetupCalendars();
            }
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "TaskRun";
			FolderLocationFromRoot = "TasksAndWorkFlow/TaskRun";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRun;

			// set object variable reference            
			PlaceHolderCore = dynTaskRunId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}


        protected void drpTaskScheduleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTaskScheduleId.Text = drpTaskScheduleList.SelectedItem.Value;
        }

        protected void drpTaskEntityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTaskEntityId.Text = drpTaskEntityList.SelectedItem.Value;
        }

        #endregion

    }
}