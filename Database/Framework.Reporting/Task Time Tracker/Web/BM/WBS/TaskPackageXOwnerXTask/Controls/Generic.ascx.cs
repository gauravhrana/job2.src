using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Priority;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using DataModel.TaskTimeTracker.Task;
using TaskTimeTracker.Components.Module.Priority;

namespace ApplicationContainer.UI.Web.TaskPackageXOwnerXTask.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

		#region properties

		
		public int? TaskPackageXOwnerXTaskId
		{
			get
			{
				if (txtTaskPackageXOwnerXTaskId.Enabled)
				{
					// review
					//return -1;//Framework.Components.DefaultDataRules.CheckAndGetApplicationId(txtTaskPackageXOwnerXTaskId.Text);
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTaskPackageXOwnerXTaskId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtTaskPackageXOwnerXTaskId.Text);
				}
			}
			set
			{
				txtTaskPackageXOwnerXTaskId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ApplicationUserId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtApplicationUserId.Text.Trim());
				else
					return int.Parse(drpApplicationUserList.SelectedItem.Value);
			}
			set
			{
				txtApplicationUserId.Text = (value == null) ? String.Empty : value.ToString();
			}

		}

		public int? TaskPackageId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtTaskPackageId.Text.Trim());
				else
					return int.Parse(drpTaskPackageList.SelectedItem.Value);
			}

			set
			{
				txtTaskPackageId.Text = (value == null) ? String.Empty : value.ToString();
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
		

		#endregion properties

		#region private methods

		public override void SetId(int setId, bool chkTaskPackageXOwnerXTaskId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTaskPackageXOwnerXTaskId);
			txtTaskPackageXOwnerXTaskId.Enabled = chkTaskPackageXOwnerXTaskId;
			//txtTaskPackageId.Enabled = !chkTaskPackageXOwnerXTaskId;
			//txtPersonId.Enabled = !chkTaskPackageXOwnerXTaskId;
			//txtApplicationUserId.Enabled = !chkTaskPackageXOwnerXTaskId;

			//drpPersonList.Enabled = !chkTaskPackageXOwnerXTaskId;
			//drpTaskPackageList.Enabled = !chkTaskPackageXOwnerXTaskId;
			//drpApplicationUserList.Enabled = !chkTaskPackageXOwnerXTaskId;
		}

		public override int? Save(string action)
		{
			var data = new TaskPackageXOwnerXTaskDataModel();

			data.TaskPackageXOwnerXTaskId	= TaskPackageXOwnerXTaskId;
			data.TaskId						= TaskId;
			data.TaskPackageId				= TaskPackageId;
			data.ApplicationUserId			= ApplicationUserId;
			

			if (action == "Insert")
			{
                TaskTimeTracker.Components.BusinessLayer.Task.TaskPackageXOwnerXTaskDataManager.Create(data, SessionVariables.RequestProfile);				
			}
			else
			{
                TaskTimeTracker.Components.BusinessLayer.Task.TaskPackageXOwnerXTaskDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of TaskPackageXOwnerXTaskID ?
			return TaskPackageXOwnerXTaskId;
		}

		public void LoadData(int taskPackageXOwnerXTaskId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new TaskPackageXOwnerXTaskDataModel();
			data.TaskPackageXOwnerXTaskId = taskPackageXOwnerXTaskId;

			// get data
            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskPackageXOwnerXTaskDataManager.GetEntityList(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			TaskPackageXOwnerXTaskId	= item.TaskPackageXOwnerXTaskId;
			TaskId						= item.TaskId;
			TaskPackageId				= item.TaskPackageId;
			ApplicationUserId			= item.ApplicationUserId;			

			if (!showId)
			{
				txtTaskPackageXOwnerXTaskId.Text = item.TaskPackageXOwnerXTaskId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, taskPackageXOwnerXTaskId, PrimaryEntityKey);
			}
			else
			{
				txtTaskPackageXOwnerXTaskId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TaskPackageXOwnerXTaskDataModel();

			TaskPackageXOwnerXTaskId = data.TaskPackageXOwnerXTaskId;
			TaskId = data.TaskId;
			TaskPackageId = data.TaskPackageId;
			ApplicationUserId = data.ApplicationUserId;			
		}

		
		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

			var TaskPackagePriorityTypeData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(TaskPackagePriorityTypeData, drpApplicationUserList, ApplicationUserDataModel.DataColumns.ApplicationUserName, ApplicationUserDataModel.DataColumns.ApplicationUserId);


            var TaskPackageData = TaskPackageDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(TaskPackageData, drpTaskPackageList, StandardDataModel.StandardDataColumns.Name, TaskPackageDataModel.DataColumns.TaskPackageId);

            var TaskData = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(TaskData, drpTaskList,
				StandardDataModel.StandardDataColumns.Name,
				TaskDataModel.DataColumns.TaskId);

			
			if (isTesting)
			{
				drpTaskPackageList.AutoPostBack = true;
				drpApplicationUserList.AutoPostBack = true;
				drpTaskList.AutoPostBack = true;
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
				if (drpTaskPackageList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtTaskPackageId.Text.Trim()))
					{
						drpTaskPackageList.SelectedValue = txtTaskPackageId.Text;
					}
					else
					{
						txtTaskPackageId.Text = drpTaskPackageList.SelectedItem.Value;
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
				
				txtApplicationUserId.Visible = true;
				txtTaskPackageId.Visible = true;
				
				txtTaskId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtApplicationUserId.Text.Trim()))
				{
					drpApplicationUserList.SelectedValue = txtApplicationUserId.Text;
				}
				if (!string.IsNullOrEmpty(txtTaskPackageId.Text.Trim()))
				{
					drpTaskPackageList.SelectedValue = txtTaskPackageId.Text;
				}
				if (!string.IsNullOrEmpty(txtTaskId.Text.Trim()))
				{
					drpTaskList.SelectedValue = txtTaskId.Text;
				}
			}
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				txtTaskPackageXOwnerXTaskId.Visible = isTesting;
				lblTaskPackageXOwnerXTaskId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "TaskPackageXOwnerXTask";
			FolderLocationFromRoot = "TaskPackageXOwnerXTask";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPackageXOwnerXTask;

			// set object variable reference            
			PlaceHolderCore = dynTaskPackageXOwnerXTaskId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpApplicationUserList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtApplicationUserId.Text = drpApplicationUserList.SelectedItem.Value;
		}

		protected void drpTaskPackageList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtTaskPackageId.Text = drpTaskPackageList.SelectedItem.Value;
		}
		
		protected void drpTaskList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtTaskId.Text = drpTaskList.SelectedItem.Value;
		}

		#endregion

	}
}