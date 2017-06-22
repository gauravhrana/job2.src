using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Task;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer;
using System.Data;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.TaskXActivityInstance.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric

	{

		#region properties		

		public int? TaskXActivityInstanceId
		{
			get
			{
				return int.Parse(txtTaskXActivityInstanceId.Text);
				
			}
			set
			{
				txtTaskXActivityInstanceId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}


		public int? TaskId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtTaskId.Text.Trim());
				else
					return int.Parse(drpTaskList.SelectedItem.Value);
				
			}
			set
			{
				txtTaskId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ActivityId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtActivityId.Text.Trim());
				else
					return int.Parse(drpActivityList.SelectedItem.Value);
			}
			set
			{
				txtActivityId.Text = (value == null) ? String.Empty : value.ToString();
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

		#endregion properties

		public override int? Save(string action)
		{
			var data = new TaskXActivityInstanceDataModel();

			data.TaskXActivityInstanceId	= TaskXActivityInstanceId;
			data.TaskId						= TaskId;
			data.ActivityId					= ActivityId;
			data.Name						= Name;
			data.Description				= Description;
			data.SortOrder					= SortOrder;

			if (action == "Insert")
			{
                TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
                TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of TaskXActivityInstanceID ?
			return TaskXActivityInstanceId;
		}
	
		public override void SetId(int setId, bool chkTaskId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTaskId);
			txtTaskId.Enabled = chkTaskId;
			//txtName.Enabled = !chkTaskId;
			//txtDescription.Enabled = !chkTaskId;
			//txtSortOrder.Enabled = !chkTaskId;
			//txtActivityId.Enabled = !chkTaskId;
			//drpActivityList.Enabled = !chkTaskId;
		}

		public void LoadData(int taskXActivityInstanceId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new TaskXActivityInstanceDataModel();
			data.TaskXActivityInstanceId = taskXActivityInstanceId;

			// get data
            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskXActivityInstanceDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			TaskXActivityInstanceId = item.TaskXActivityInstanceId;
			TaskId					= item.TaskId;
			ActivityId				= item.ActivityId;
			Name					= item.Name;
			Description				= item.Description;
			SortOrder				= item.SortOrder;

			if (!showId)
			{
				txtTaskXActivityInstanceId.Text = item.TaskXActivityInstanceId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, taskXActivityInstanceId, PrimaryEntityKey);
			}
			else
			{
				txtTaskXActivityInstanceId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TaskXActivityInstanceDataModel();

			TaskXActivityInstanceId = data.TaskXActivityInstanceId;
			TaskId					= data.TaskId;
			ActivityId				= data.ActivityId;			
			Description				= data.Description;
			Name					= data.Name;
			SortOrder				= data.SortOrder;
		}

		
		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			var activityData = ActivityDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(activityData, drpActivityList, StandardDataModel.StandardDataColumns.Name, ActivityDataModel.DataColumns.ActivityId);

            var taskData = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(taskData, drpTaskList, StandardDataModel.StandardDataColumns.Name, TaskDataModel.DataColumns.TaskId);

			if (isTesting)
			{
				drpActivityList.AutoPostBack = true;
				drpTaskList.AutoPostBack = true;

				if (drpActivityList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtActivityId.Text.Trim()))
					{
						drpActivityList.SelectedValue = txtActivityId.Text;
					}
					else
					{
						txtActivityId.Text = drpActivityList.SelectedItem.Value;
					}
				}
				if (drpTaskList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtTaskId.Text.Trim()))
					{
						drpTaskList.SelectedValue = txtTaskId.Text;
					}
					else
					{
						txtTaskId.Text = drpTaskList.SelectedItem.Value;
					}
				}
				txtActivityId.Visible = true;
				txtTaskId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtActivityId.Text.Trim()))
				{
					drpActivityList.SelectedValue = txtActivityId.Text;
				}
				if (!string.IsNullOrEmpty(txtTaskId.Text.Trim()))
				{
					drpTaskList.SelectedValue = txtTaskId.Text;
				}
			}
		}


		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				txtTaskXActivityInstanceId.Visible = isTesting;
				lblTaskXActivityInstanceId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskXActivityInstance;
			PrimaryEntityKey = "TaskXActivityInstance";
			FolderLocationFromRoot = "TaskXActivityInstance";

			// set object variable reference            
			PlaceHolderCore = dynTaskXActivityInstance;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpActivityList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtActivityId.Text = drpActivityList.SelectedItem.Value;
		}

		protected void drpTaskList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtTaskId.Text = drpTaskList.SelectedItem.Value;
		}

	}
}