using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.Competency;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.BusinessLayer.Feature;
using TaskTimeTracker.Components.BusinessLayer.Task;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.WBS.Task
{
    public partial class Update : PageUpdate
    {

		#region Methods

		protected override Control GetTabControl(int setId, Control updateControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();
			var bucketControl = ApplicationCommon.GetNewBucketControl();
			var bucketControl2 = ApplicationCommon.GetNewBucketControl();
			var bucketControl3 = ApplicationCommon.GetNewBucketControl();
			var bucketControl4 = (BucketFor3Control)Page.LoadControl(ApplicationCommon.BucketFor3ControlPath);

			tabControl.Setup("TaskUpdateView");

			var selected = false;

			if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
			{
				selected = true;
			}

			tabControl.AddTab("Task", updateControl, String.Empty, selected);

			selected = false;
			if (Request.QueryString["tab"] == "2")
			{
				selected = true;
			}

			bucketControl.ConfigureBucket("Competency", setId, GetCompetencyList, GetAssociatedCompetencies, SaveTaskXCompetency);
			tabControl.AddTab("Competency", bucketControl, String.Empty, selected);
			plcUpdateList.Controls.Add(tabControl);

			bucketControl2.ConfigureBucket("ApplicationUser", setId, GetApplicationUserList, GetAssociatedApplicationUsers, SaveTaskXApplicationUser);
			tabControl.AddTab("ApplicationUser", bucketControl2, "Application User", selected);

			bucketControl3.ConfigureBucket("TaskNote", setId, GetTaskNoteList, GetAssociatedTaskNote, SaveTaskXTaskNote);
			tabControl.AddTab("TaskNote", bucketControl3, "Task Note", selected);
			
			bucketControl4.ConfigureBucket("DeliverableArtifact", "DeliverableArtifactStatus", "Name", "Name", setId,
				GetDeliverableArtifactList, GetDeliverableArtifactStatusList, GetAssociatedDeliverableArtifact, SaveTaskXDeliverableArtifact, RemoveByStatus);

			tabControl.AddTab("Deliverable Artifact", bucketControl4, "Deliverable Artifact", selected);
			return tabControl;
		}	
		
		private DataTable GetCompetencyList()
		{
            var dt = CompetencyDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetFeatureList()
		{
            var dt = FeatureDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetTaskNoteList()
		{
            var dt = TaskNoteDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetDeliverableArtifactList()
		{
            var dt = DeliverableArtifactDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetDeliverableArtifactStatusList()
		{
            var dt = DeliverableArtifactStatusDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetApplicationUserList()
		{
			var dt = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedCompetencies(int taskId)
		{
			var data = new TaskXCompetencyDataModel();
			data.TaskId = taskId;
            var dt = TaskXCompetencyDataManager.Search(data, SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedApplicationUsers(int taskId)
		{
            var dt = TaskXApplicationUserDataManager.GetByTask(taskId, SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedFeatures(int taskId)
		{
			var dt = FeatureXTaskDataManager.GetByTask(taskId, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private DataTable GetAssociatedTaskNote(int taskId)
		{
            var dt = TaskXTaskNote.GetByTask(taskId, SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedDeliverableArtifact(int taskId)
		{
			var dt = TaskXDeliverableArtifactDataManager.GetByTask(taskId, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveTaskXCompetency(int taskId, List<int> competencyIds)
		{
			TaskXCompetencyDataManager.DeleteByTask(taskId, SessionVariables.RequestProfile.AuditId);
            TaskXCompetencyDataManager.CreateByTask(taskId, competencyIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void SaveTaskXApplicationUser(int taskId, List<int> ApplicationUserIds)
		{
			var data = new TaskDataModel();
			data.TaskId = taskId;
            var taskdata = TaskDataManager.Search(data, SessionVariables.RequestProfile);
			var row = taskdata.Rows[0];
			var taskStatusTypeId = Convert.ToString(row[TaskXApplicationUserDataModel.DataColumns.TaskStatusTypeId]);
            TaskXApplicationUserDataManager.DeleteByTask(taskId, SessionVariables.RequestProfile);
            TaskXApplicationUserDataManager.CreateByTask(taskId, ApplicationUserIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void SaveFeatureXTask(int taskId, List<int> FeatureIds)
		{
			FeatureXTaskDataManager.DeleteByTask(taskId, SessionVariables.RequestProfile.AuditId);
            FeatureXTaskDataManager.Create(taskId, FeatureIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void SaveTaskXTaskNote(int taskId, List<int> TaskNoteIds)
		{
            TaskXTaskNote.DeleteByTask(taskId, SessionVariables.RequestProfile);
            TaskXTaskNote.Create(taskId, TaskNoteIds.ToArray(), SessionVariables.RequestProfile);
		}
		private void SaveTaskXDeliverableArtifact(int taskId, int deliverableArtifactId, int statuslId)
		{
			//Components.BusinessLayer.TaskXDeliverableArtifact.DeleteByTask(taskId, AuditId);
			var data = new TaskXDeliverableArtifactDataModel();
			data.TaskId = taskId;
			data.DeliverableArtifactId = deliverableArtifactId;
			data.DeliverableArtifactStatusId = statuslId;
            TaskXDeliverableArtifactDataManager.Create(data, SessionVariables.RequestProfile);
		}

		private void RemoveByStatus(int statusId)
		{
			var data = new TaskXDeliverableArtifactDataModel();
			data.DeliverableArtifactStatusId = statusId;
            TaskXDeliverableArtifactDataManager.Delete(data, SessionVariables.RequestProfile);
		}


		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.Task;

			GenericControlPath = ApplicationCommon.GetControlPath("Task", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey = "Task";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnClone = btnClone;
			BtnCancel = btnCancel;
		}

		#endregion

	}
}