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
using Framework.Components;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer.Task;
using TaskTimeTracker.Components.Module.Priority;

namespace ApplicationContainer.UI.Web.WBS.TaskPriorityXApplicationUser.Controls
{
	public partial class Generic : ControlGeneric
	{

		#region properties

		public int? TaskPriorityXApplicationUserId
		{
			get
			{
				if (txtTaskPriorityXApplicationUserId.Enabled)
				{
                    return DefaultDataRules.CheckAndGetEntityId(txtTaskPriorityXApplicationUserId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtTaskPriorityXApplicationUserId.Text);
				}
			}
			set
			{
				txtTaskPriorityXApplicationUserId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? TaskPriorityTypeId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtTaskPriorityTypeId.Text.Trim());
				else
					return int.Parse(drpTaskPriorityTypeList.SelectedItem.Value);
			}
			set
			{
				txtTaskPriorityTypeId.Text = (value == null) ? String.Empty : value.ToString();
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

		#endregion properties

		#region methods

		public override int? Save(string action)
		{
			var data = new TaskPriorityXApplicationUserDataManager.Data();

			data.TaskPriorityXApplicationUserId = TaskPriorityXApplicationUserId;
			data.TaskId = TaskId;
			data.ApplicationUserId = ApplicationUserId;
			data.TaskPriorityTypeId = TaskPriorityTypeId;
			
			if (action == "Insert")
			{
                var dtTaskPriorityXApplicationUser = TaskPriorityXApplicationUserDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtTaskPriorityXApplicationUser.Rows.Count == 0)
				{
                    TaskPriorityXApplicationUserDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                TaskPriorityXApplicationUserDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return TaskPriorityXApplicationUserId;
		}

		public override void SetId(int setId, bool chkTaskPriorityXApplicationUserId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTaskPriorityXApplicationUserId);
			txtTaskPriorityXApplicationUserId.Enabled = chkTaskPriorityXApplicationUserId;
			//txtDescription.Enabled = !chkTaskPriorityXApplicationUserId;
			//txtActivityId.Enabled = !chkTaskPriorityXApplicationUserId;
			//txtSortOrder.Enabled = !chkTaskPriorityXApplicationUserId;
		}

		public void LoadData(int taskPriorityXApplicationUserId, bool showId)
		{

			Clear();

			var data = new TaskPriorityXApplicationUserDataManager.Data();
			data.TaskPriorityXApplicationUserId = taskPriorityXApplicationUserId;
            var items = TaskPriorityXApplicationUserDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);


			if (items.Count != 1) return;

			var item = items[0];

			TaskPriorityXApplicationUserId = item.TaskPriorityXApplicationUserId;
			TaskId = item.TaskId;
			ApplicationUserId = item.ApplicationUserId;
			TaskPriorityTypeId = item.TaskPriorityTypeId;
			
			if (!showId)
			{
				txtTaskPriorityXApplicationUserId.Text = item.TaskPriorityXApplicationUserId.ToString();
				oHistoryList.Setup(PrimaryEntity, taskPriorityXApplicationUserId, PrimaryEntityKey);
			}
			else
			{
				txtTaskPriorityXApplicationUserId.Text = String.Empty;
			}

		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TaskPriorityXApplicationUserDataManager.Data();

			TaskPriorityXApplicationUserId = data.TaskPriorityXApplicationUserId;
			TaskId = data.TaskId;
			ApplicationUserId = data.ApplicationUserId;
			TaskPriorityTypeId = data.TaskPriorityTypeId;			
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				txtTaskPriorityXApplicationUserId.Visible = isTesting;
				lblTaskPriorityXApplicationUserId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.TaskPriorityXApplicationUser;
			PrimaryEntityKey = "TaskPriorityXApplicationUser";
			FolderLocationFromRoot = "TaskPriorityXApplicationUser";

			PlaceHolderCore = dynTaskPriorityXApplicationUserId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}
		
		#endregion	


		#region private methods

		
		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;

            var taskPriorityTypeData = TaskPriorityTypeDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(taskPriorityTypeData, drpTaskPriorityTypeList, StandardDataModel.StandardDataColumns.Name,
				TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId);



            var taskData = TaskDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(taskData, drpTaskList, StandardDataModel.StandardDataColumns.Name,
				TaskDataModel.DataColumns.TaskId);



			var ApplicationUserData = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(ApplicationUserData, drpApplicationUserList, ApplicationUserDataModel.DataColumns.FirstName,
					ApplicationUserDataModel.DataColumns.ApplicationUserId);


			if (isTesting)
			{
				drpTaskList.AutoPostBack = true;
				drpTaskPriorityTypeList.AutoPostBack = true;
				drpApplicationUserList.AutoPostBack = true;
				if (drpTaskPriorityTypeList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtTaskPriorityTypeId.Text.Trim()))
					{
						drpTaskPriorityTypeList.SelectedValue = txtTaskPriorityTypeId.Text;
					}
					else
					{
						txtTaskPriorityTypeId.Text = drpTaskPriorityTypeList.SelectedItem.Value;
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
				txtTaskPriorityTypeId.Visible = true;
				txtTaskId.Visible = true;
				txtApplicationUserId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtTaskPriorityTypeId.Text.Trim()))
				{
					drpTaskPriorityTypeList.SelectedValue = txtTaskPriorityTypeId.Text;
				}
				if (!string.IsNullOrEmpty(txtTaskId.Text.Trim()))
				{
					drpTaskList.SelectedValue = txtTaskId.Text;
				}
				if (!string.IsNullOrEmpty(txtApplicationUserId.Text.Trim()))
				{
					drpApplicationUserList.SelectedValue = txtApplicationUserId.Text;
				}
			}
		}

		#endregion

		#region Events

		protected void drpTaskPriorityTypeList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtTaskPriorityTypeId.Text = drpTaskPriorityTypeList.SelectedItem.Value;
		}

		protected void drpTaskList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtTaskId.Text = drpTaskList.SelectedItem.Value;
		}

		protected void drpApplicationUserList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtApplicationUserId.Text = drpApplicationUserList.SelectedItem.Value;
		}

		#endregion

	}
}