using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.FeatureXTask
{
	public partial class CrossReference : Framework.UI.Web.BaseClasses.PageBasePage
	{
		#region Methods

		private DataTable GetFeatureList()
		{
			var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedFeatures(int TaskId)
		{
			var id = Convert.ToInt32(drpTask.SelectedValue);
			var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXTaskDataManager.GetByTask(id, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveByTask(int TaskId, List<int> featureIds)
		{
			var id = Convert.ToInt32(drpTask.SelectedValue);
			TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXTaskDataManager.DeleteByTask(id, SessionVariables.RequestProfile.AuditId);
            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXTaskDataManager.Create(id, featureIds.ToArray(), SessionVariables.RequestProfile);
		}

		private DataTable GetTaskList()
		{
			var dt = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedTasks(int featureId)
		{
			var id = Convert.ToInt32(drpFeature.SelectedValue);
			var dt = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXTaskDataManager.GetByFeature(id, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveByFeature(int FeatureId, List<int> TaskIds)
		{
			var id = Convert.ToInt32(drpFeature.SelectedValue);
			TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXTaskDataManager.DeleteByFeature(id, SessionVariables.RequestProfile.AuditId);
            TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXTaskDataManager.CreateByFeature(id, TaskIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void BindLists()
		{
			drpTask.DataSource = GetTaskList();
			drpTask.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpTask.DataValueField = TaskDataModel.DataColumns.TaskId;
			drpTask.DataBind();

			drpFeature.DataSource = GetFeatureList();
			drpFeature.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpFeature.DataValueField = FeatureDataModel.DataColumns.FeatureId;
			drpFeature.DataBind();
		}
         
		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			BindLists();

			BucketOfTask.ConfigureBucket("Task", 1, GetTaskList, GetAssociatedTasks, SaveByFeature);
			BucketOfFeature.ConfigureBucket("Feature", 1, GetFeatureList, GetAssociatedFeatures, SaveByTask);
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByFeature")
			{
				dynFeature.Visible = true;
				dynTask.Visible = false;
				BucketOfTask.ReloadBucketList();
			}
			else if (drpSelection.SelectedValue == "ByTask")
			{
				dynFeature.Visible = false;
				dynTask.Visible = true;
				BucketOfFeature.ReloadBucketList();
			}
		}

		protected void drpTask_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfFeature.ReloadBucketList();
		}

		protected void drpFeature_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfTask.ReloadBucketList();
		}

		#endregion

	}
}