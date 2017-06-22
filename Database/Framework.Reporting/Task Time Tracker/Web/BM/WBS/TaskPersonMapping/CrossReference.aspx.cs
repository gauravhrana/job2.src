using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.WBS.TaskPersonMapping
{
	public partial class CrossReference : Shared.UI.WebFramework.BasePage
	{

		#region properties

		public int TaskId
		{
			get
			{
				return int.Parse(drpTask.SelectedItem.Value);
			}
		}

		public int PersonId
		{
			get
			{
				return int.Parse(drpPerson.SelectedItem.Value);
			}
		}

		#endregion

		#region private methods

		private void SetupCalendars()
		{
			var isTesting = SessionVariables.IsTesting;

			if (isTesting)
			{
				if (!string.IsNullOrEmpty(txtStartDate.Text.Trim()))
				{
					clnStartDate.SelectedDate = DateTime.ParseExact(txtStartDate.Text.Trim(), SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
				}
				else
				{
					txtStartDate.Text = clnStartDate.SelectedDate.ToString(SessionVariables.UserDateFormat);
				}

				if (!string.IsNullOrEmpty(txtDueDate.Text.Trim()))
				{
					clnDueDate.SelectedDate = DateTime.ParseExact(txtDueDate.Text.Trim(), SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
				}
				else
				{
					txtDueDate.Text = clnDueDate.SelectedDate.ToString(SessionVariables.UserDateFormat);
				}

				if (!string.IsNullOrEmpty(txtCompletedDate.Text.Trim()))
				{
					clnCompletedDate.SelectedDate = DateTime.ParseExact(txtCompletedDate.Text.Trim(), SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
				}
				else
				{
					txtCompletedDate.Text = clnCompletedDate.SelectedDate.ToString(SessionVariables.UserDateFormat);
				}

				txtStartDate.Visible = true;
				txtDueDate.Visible = true;
				txtCompletedDate.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtStartDate.Text.Trim()))
				{
					clnStartDate.SelectedDate = DateTime.ParseExact(txtStartDate.Text.Trim(), SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
				}
				if (!string.IsNullOrEmpty(txtDueDate.Text.Trim()))
				{
					clnDueDate.SelectedDate = DateTime.ParseExact(txtDueDate.Text.Trim(), SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
				}
				if (!string.IsNullOrEmpty(txtCompletedDate.Text.Trim()))
				{
					clnCompletedDate.SelectedDate = DateTime.ParseExact(txtCompletedDate.Text.Trim(), SessionVariables.UserDateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
				}
			}
		}

		private void PopulateDropDownList()
		{
			//parent

            var TaskTypeEntry = TaskTimeTracker.Components.BusinessLayer.Task.TaskTypeDataManager.GetList(SessionVariables.RequestProfile);
			drpTaskType.DataSource = TaskTypeEntry.DefaultView;
			drpTaskType.DataTextField = "Name";
			drpTaskType.DataValueField = "TaskTypeId";
			drpTaskType.DataBind();

			TaskDataModel data = new TaskDataModel();
			data.TaskTypeId = Convert.ToInt32(drpTaskType.SelectedItem.Value);
            var TaskEntry = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.Search(data, SessionVariables.RequestProfile);

			drpTask.DataSource = TaskEntry.DefaultView;
			drpTask.DataTextField = "Name";
			drpTask.DataValueField = "TaskId";
			drpTask.DataBind();


			if (drpTask.Items.Count == 0)
			{
				drpTask.Items.Add(new ListItem("None", "-1"));
			}


			//Source List
			var PersonEntry = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			lstSourcePerson.DataSource = PersonEntry.DefaultView;
			lstSourcePerson.DataTextField = "FirstName";
			lstSourcePerson.DataValueField = "ApplicationUserId";
			lstSourcePerson.DataBind();

			PopulateTargetPerson();
			CleanUpPerson();

			drpPerson.DataSource = PersonEntry.DefaultView;
			drpPerson.DataTextField = "FirstName";
			drpPerson.DataValueField = "ApplicationUserId";
			drpPerson.DataBind();

            TaskEntry = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetList(SessionVariables.RequestProfile);
			lstSourceTask.DataSource = TaskEntry.DefaultView;
			lstSourceTask.DataTextField = "Name";
			lstSourceTask.DataValueField = "TaskId";
			lstSourceTask.DataBind();

            var TaskStatusType = TaskTimeTracker.Components.BusinessLayer.Task.TaskStatusTypeDataManager.GetList(SessionVariables.RequestProfile);
			drpTaskStatusType.DataSource = TaskStatusType.DefaultView;
			drpTaskStatusType.DataTextField = "Name";
			drpTaskStatusType.DataValueField = "TaskStatusTypeId";
			drpTaskStatusType.DataBind();

			PopulateTargetTask();
			CleanUpTask();
		}

		private void PopulateTargetTask()
		{
			// Current Target List
			var PersonId = int.Parse(drpPerson.SelectedItem.Value);
            var CurrentAssignment = TaskTimeTracker.Components.BusinessLayer.Task.TaskXApplicationUserDataManager.GetByApplicationUser(PersonId, SessionVariables.RequestProfile);
			lstTargetTask.DataSource = CurrentAssignment.DefaultView;
			lstTargetTask.DataTextField = "Task";
			lstTargetTask.DataValueField = "TaskId";
			lstTargetTask.DataBind();
		}

		private void PopulateTargetPerson()
		{
			//Current Target List
			var TaskId = int.Parse(drpTask.SelectedItem.Value);
            var CurrentAssignment = TaskTimeTracker.Components.BusinessLayer.Task.TaskXApplicationUserDataManager.GetByTask(TaskId, SessionVariables.RequestProfile);
			lstTargetPerson.DataSource = CurrentAssignment.DefaultView;
			lstTargetPerson.DataTextField = "ApplicationUser";
			lstTargetPerson.DataValueField = "ApplicationUserId";
			lstTargetPerson.DataBind();
		}

		private void SwitchValues(ListBox source, ListBox target)
		{
			var listRemoval = new System.Collections.ArrayList();

			// Find the number of items selected in the List and items selected
			// Call the move function equal to the number of items selected
			// Remove from Source list, The items moved

			// iterate through source list
			foreach (ListItem itemCurrent in source.Items)
			{
				if (itemCurrent.Selected == true)
				{
					// 1. DETERIMNE - find out which item(s) was selected of SOURCE LIST
					//Response.Write(itemCurrent.ToString());

					// 2. MOVE / COPY - Add it to TARGET LIST
					var copy = new ListItem(itemCurrent.Text, itemCurrent.Value);
					target.Items.Add(copy);

					// 3. REMOVE - Add to external list so we can remove afterwards from the source
					listRemoval.Add(itemCurrent);

					// 4. Set the moved selection as selected, so quickly can move back
					// avoiding the user from reselecting items, disable any preveiously selected items     
					if (target.SelectedItem != null)
					{
						target.SelectedItem.Selected = false;
					}
					target.Items.FindByValue(copy.Value).Selected = true;
				}
			}

			foreach (ListItem itemToRemove in listRemoval)
			{
				source.Items.Remove(itemToRemove);
			}
		}

		// Popluate the complete list for the source side
		private void ResetSourcePerson()
		{
			var RoleEntry = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			lstSourcePerson.DataSource = RoleEntry.DefaultView;
			lstSourcePerson.DataTextField = "FirstName";
			lstSourcePerson.DataValueField = "ApplicationUserId";
			lstSourcePerson.DataBind();
		}

		// Popluate the complete list for the source side
		private void ResetSourceTask()
		{
            var TaskEntry = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetList(SessionVariables.RequestProfile);
			lstSourceTask.DataSource = TaskEntry.DefaultView;
			lstSourceTask.DataTextField = "Name";
			lstSourceTask.DataValueField = "TaskId";
			lstSourceTask.DataBind();
		}

		/// <summary>
		/// 1. the left side should not have any of the values that are on the right side
		// and simlar right should not have any that is on left
		/// </summary>
		private void CleanUpPerson()
		{
			foreach (ListItem item in lstTargetPerson.Items)
			{
				ListItem newItem = new ListItem();
				newItem.Text = item.Text;
				newItem.Value = item.Value;
				lstSourcePerson.Items.Remove(newItem);
			}
		}

		private void CleanUpTask()
		{

			var PersonId = int.Parse(drpPerson.SelectedItem.Value);
            var CurrentAssignment = TaskTimeTracker.Components.BusinessLayer.Task.TaskXApplicationUserDataManager.GetByApplicationUser(PersonId, SessionVariables.RequestProfile);

			foreach (DataRow row in CurrentAssignment.Rows)
			{
				var item = new ListItem();

				var data = new TaskDataModel();

				data.TaskId = int.Parse(row["TaskId"].ToString());

                var PersonTable = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetDetails(data, SessionVariables.RequestProfile);
				var row1 = PersonTable.Rows[0];
				item.Value = row["TaskId"].ToString();
				item.Text = row1["Name"].ToString();
				lstSourceTask.Items.Remove(item);
			}
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				PopulateDropDownList();
				SetupCalendars();
			}

		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			SettingCategory = "TaskPersonMappingDefaultView";
			var sbm = this.Master.SubMenuObject;
			

			sbm.SettingCategory = SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

			//bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			//bcControl.Setup("Cross Reference");
			//bcControl.GenerateMenu();

		}

		protected void btnLeftPerson_Click(object sender, EventArgs e)
		{
			SwitchValues(lstSourcePerson, lstTargetPerson);
		}

		protected void btnRightPerson_Click(object sender, EventArgs e)
		{
			SwitchValues(lstTargetPerson, lstSourcePerson);
		}

		protected void btnSavePerson_Click(object sender, EventArgs e)
		{
			//Gather items
			var i = 0;
			var finalList = new int[lstTargetPerson.Items.Count];
			foreach (ListItem itemCurrent in lstTargetPerson.Items)
			{
				finalList[i++] = int.Parse(itemCurrent.Value);
			}

			if (TaskId != -1)
			{
				//  Delete all that are previously stored in database
                TaskTimeTracker.Components.BusinessLayer.Task.TaskXApplicationUserDataManager.DeleteByTask(TaskId, SessionVariables.RequestProfile);

				int applicationId = SessionVariables.RequestProfile.ApplicationId;
				var TaskStatusTypeId = int.Parse(drpTaskStatusType.SelectedValue);
				var StartDate = int.Parse(txtStartDate.Text.Replace("-", ""));
				var DueDate = int.Parse(txtDueDate.Text.Replace("-", ""));
				var CompletedDate = int.Parse(txtCompletedDate.Text.Replace("-", ""));
				//Save final list
                TaskTimeTracker.Components.BusinessLayer.Task.TaskXApplicationUserDataManager.Create(TaskId, finalList, TaskStatusTypeId, StartDate, DueDate, CompletedDate, SessionVariables.RequestProfile);
			}
		}

		protected void drpTask_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			ResetSourcePerson();
			PopulateTargetPerson();
			CleanUpPerson();
		}

		protected void btnLeftTask_Click(object sender, EventArgs e)
		{
			SwitchValues(lstSourceTask, lstTargetTask);
		}

		protected void btnRightTask_Click(object sender, EventArgs e)
		{
			SwitchValues(lstTargetTask, lstSourceTask);
		}

		protected void clnStartDate_SelectionChanged(object sender, EventArgs e)
		{
			txtStartDate.Text = clnStartDate.SelectedDate.ToString(SessionVariables.UserDateFormat);
		}

		protected void clnDueDate_SelectionChanged(object sender, EventArgs e)
		{
			txtDueDate.Text = clnDueDate.SelectedDate.ToString(SessionVariables.UserDateFormat);
		}

		protected void clnCompletedDate_SelectionChanged(object sender, EventArgs e)
		{
			txtCompletedDate.Text = clnCompletedDate.SelectedDate.ToString(SessionVariables.UserDateFormat);
		}

		protected void btnSaveTask_Click(object sender, EventArgs e)
		{
			// Gather items
			var i = 0;
			var finalList = new int[lstTargetTask.Items.Count];
			foreach (ListItem itemCurrent in lstTargetTask.Items)
			{
				finalList[i++] = int.Parse(itemCurrent.Value);
			}

			if (PersonId != -1)
			{
				//  Delete all that are previously stored in database
                TaskTimeTracker.Components.BusinessLayer.Task.TaskXApplicationUserDataManager.DeleteByApplicationUser(PersonId, SessionVariables.RequestProfile);
				int applicationId = SessionVariables.RequestProfile.ApplicationId;
				var TaskStatusTypeId = int.Parse(drpTaskStatusType.SelectedValue);
				var StartDate = int.Parse(txtStartDate.Text.Replace("-", ""));
				var DueDate = int.Parse(txtDueDate.Text.Replace("-", ""));
				var CompletedDate = int.Parse(txtCompletedDate.Text.Replace("-", ""));
				// Save final list
                TaskTimeTracker.Components.BusinessLayer.Task.TaskXApplicationUserDataManager.CreateByApplicationUser(PersonId, finalList, TaskStatusTypeId, StartDate, DueDate, CompletedDate, SessionVariables.RequestProfile);
			}
		}

		protected void drpPerson_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			ResetSourceTask();
			PopulateTargetTask();
			CleanUpTask();
		}

		protected void drpTaskStatusType_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			ResetSourceTask();
			PopulateTargetTask();
			CleanUpTask();
		}

		protected void drpTaskType_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			TaskDataModel data = new TaskDataModel();
			data.TaskTypeId = Convert.ToInt32(drpTaskType.SelectedItem.Value);
            var TaskEntry = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.Search(data, SessionVariables.RequestProfile);

			drpTask.DataSource = TaskEntry.DefaultView;
			drpTask.DataTextField = "Name";
			drpTask.DataValueField = "TaskId";
			drpTask.DataBind();

			if (drpTask.Items.Count == 0)
			{
				drpTask.Items.Add(new ListItem("None", "-1"));
			}

			ResetSourcePerson();
			PopulateTargetPerson();
			CleanUpPerson();
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByTask")
			{
				dynTask.Visible = true;
				dynPerson.Visible = false;

				ResetSourcePerson();
				PopulateTargetPerson();
				CleanUpPerson();
			}
			else if (drpSelection.SelectedValue == "ByPersons")
			{
				dynTask.Visible = false;
				dynPerson.Visible = true;

				ResetSourceTask();
				PopulateTargetTask();
				CleanUpTask();
			}

		}

		#endregion

	}
}