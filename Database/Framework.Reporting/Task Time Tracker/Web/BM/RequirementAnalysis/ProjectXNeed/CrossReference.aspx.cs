using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectXNeed
{
	public partial class CrossReference : Framework.UI.Web.BaseClasses.PageBasePage
	{

		#region Methods

		private DataTable GetProjectList()
		{
			var dt = ProjectDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedProjects(int needId)
		{
			var id = Convert.ToInt32(drpNeed.SelectedValue);
            var dt = ProjectXNeedDataManager.GetByNeed(id, SessionVariables.RequestProfile);
			return dt;
		}

		private void SaveByNeed(int needId, List<int> projectIds)
		{
			var id = Convert.ToInt32(drpNeed.SelectedValue);
            ProjectXNeedDataManager.DeleteByNeed(id, SessionVariables.RequestProfile);
            ProjectXNeedDataManager.CreateByNeed(id, projectIds.ToArray(), SessionVariables.RequestProfile);
		}

		private DataTable GetNeedList()
		{
            var dt = NeedDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedNeeds(int projectId)
		{
			var id = Convert.ToInt32(drpProject.SelectedValue);
			var dt = ProjectXNeedDataManager.GetByProject(id, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveByProject(int projectId, List<int> needIds)
		{
			var id = Convert.ToInt32(drpProject.SelectedValue);
            ProjectXNeedDataManager.DeleteByProject(id, SessionVariables.RequestProfile);
            ProjectXNeedDataManager.CreateByProject(id, needIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void BindLists()
		{
			drpNeed.DataSource = GetNeedList();
			drpNeed.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpNeed.DataValueField = NeedDataModel.DataColumns.NeedId;
			drpNeed.DataBind();

			drpProject.DataSource = GetProjectList();
			drpProject.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpProject.DataValueField = ProjectDataModel.DataColumns.ProjectId;
			drpProject.DataBind();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			BindLists();

			BucketOfNeed.ConfigureBucket("Need", 1, GetNeedList, GetAssociatedNeeds, SaveByProject);
			BucketOfProject.ConfigureBucket("Project", 1, GetProjectList, GetAssociatedProjects, SaveByNeed);
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByProject")
			{
				dynProject.Visible = true;
				dynNeed.Visible = false;
				BucketOfNeed.ReloadBucketList();
			}
			else if (drpSelection.SelectedValue == "ByNeed")
			{
				dynProject.Visible = false;
				dynNeed.Visible = true;
				BucketOfProject.ReloadBucketList();
			}
		}

		protected void drpNeed_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfProject.ReloadBucketList();
		}

		protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfNeed.ReloadBucketList();
		}

		#endregion

	}
}