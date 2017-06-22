using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using Dapper;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ClientXProject
{
	public partial class CrossReference : BasePage
	{
		#region Methods

		private DataTable GetClientList()
		{
			var list = ClientDataManager.GetList(SessionVariables.RequestProfile);
			return list;
		}

		private DataTable GetAssociatedClients(int ProjectId)
		{
			var id = Convert.ToInt32(drpProject.SelectedValue);
			var dt = ClientXProjectDataManager.GetByProject(id, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveByProject(int ProjectId, List<int> ClientIds)
		{
			var id = Convert.ToInt32(drpProject.SelectedValue);
			ClientXProjectDataManager.DeleteByProject(id, SessionVariables.RequestProfile.AuditId);
            ClientXProjectDataManager.CreateByProject(id, ClientIds.ToArray(), SessionVariables.RequestProfile);
		}

		private DataTable GetProjectList()
		{
            var dt = ProjectDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedProjects(int ClientId)
		{
			var id = Convert.ToInt32(drpClient.SelectedValue);
			var dt = ClientXProjectDataManager.GetByClient(id, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveByClient(int ClientId, List<int> ProjectIds)
		{
			var id = Convert.ToInt32(drpClient.SelectedValue);
			ClientXProjectDataManager.DeleteByClient(id, SessionVariables.RequestProfile.AuditId);
            ClientXProjectDataManager.CreateByClient(id, ProjectIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void BindLists()
		{
			drpProject.DataSource = GetProjectList();
			drpProject.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpProject.DataValueField = ProjectDataModel.DataColumns.ProjectId;
			drpProject.DataBind();

			drpClient.DataSource = GetClientList();
			drpClient.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpClient.DataValueField = ClientDataModel.DataColumns.ClientId;
			drpClient.DataBind();
		}

		#endregion

		#region Events

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "ClientXProjectDefaultView";
			
		}

		protected override void OnInit(EventArgs e)
		{

			BindLists();

			BucketOfProject.ConfigureBucket("Project", 1, GetProjectList, GetAssociatedProjects, SaveByClient);
			BucketOfClient.ConfigureBucket("Client", 1, GetClientList, GetAssociatedClients, SaveByProject);
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByClient")
			{
				dynClient.Visible = true;
				dynProject.Visible = false;
				BucketOfProject.ReloadBucketList();				
			}
			else if (drpSelection.SelectedValue == "ByProject")
			{
				dynClient.Visible = false;
				dynProject.Visible = true;
				BucketOfClient.ReloadBucketList();				
			}
		}

		protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfClient.ReloadBucketList();
		}

		protected void drpClient_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfProject.ReloadBucketList();			
		}

		#endregion
	}
}