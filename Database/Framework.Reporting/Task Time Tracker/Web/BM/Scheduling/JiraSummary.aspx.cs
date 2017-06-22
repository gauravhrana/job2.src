using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.ApplicationUser;
using Shared.WebCommon.UI.Web;
using Atlassian.Jira;
using System.Reflection;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.BM.Scheduling
{
	public static class ConvertToDataTable
	{
		public static DataTable ToDataTable<T>(this IEnumerable<T> source)
		{
			var table = new DataTable();

			int i = 0;
			var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var prop in props)
			{
				table.Columns.Add(new DataColumn(prop.Name, prop.PropertyType));
				++i;
			}

			foreach (var item in source)
			{
				var values = new object[i];
				i = 0;
				foreach (var prop in props)
					values[i++] = prop.GetValue(item);
				table.Rows.Add(values);
			}

			return table;
		}
	}

	public partial class JiraSummary : System.Web.UI.Page
	{
		#region Methods

		string jiraUserName = "general.dev";
		string jiraPassword = "demodemo";
		string jiraURL = "http://ivr-app-jra-01:8080";

		private void BindUsers()
		{
			var dtUsers = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			dtUsers = (from row in dtUsers.AsEnumerable()
					   where row["ApplicationId"].ToString() == SessionVariables.RequestProfile.ApplicationId.ToString()
					   select row).CopyToDataTable();

			UIHelper.LoadDropDown(dtUsers, drpPersons,
				ApplicationUserDataModel.DataColumns.FullName, ApplicationUserDataModel.DataColumns.ApplicationUserName);
			
			drpPersons.Items.Insert(0, new ListItem() { Text = "All", Value = "-1", Selected = true });
		}

		private void BindStatus()
		{
			// create a connection to JIRA
			var jira = new Jira(jiraURL, jiraUserName, jiraPassword);			
			var dtStatus = ConvertToDataTable.ToDataTable(jira.GetIssueStatuses());			
			UIHelper.LoadDropDown(dtStatus, drpStatus, "Name", "Name");
			drpStatus.Items.Insert(0, new ListItem() { Text = "All", Value = "-1", Selected = true });
		}

		private void BindPriority()
		{
			// create a connection to JIRA
			var jira = new Jira(jiraURL, jiraUserName, jiraPassword);
			var dtPriority = ConvertToDataTable.ToDataTable(jira.GetIssuePriorities());
			UIHelper.LoadDropDown(dtPriority, drpPriority, "Name", "Name");
			drpPriority.Items.Insert(0, new ListItem() { Text = "All", Value = "-1", Selected = true });
		}

		private void BindProject()
		{
			// create a connection to JIRA
			var jira = new Jira(jiraURL, jiraUserName, jiraPassword);
			var dtProject = ConvertToDataTable.ToDataTable(jira.GetProjects());
			UIHelper.LoadDropDown(dtProject, drpProject, "Name", "Name");
			drpProject.Items.Insert(0, new ListItem() { Text = "All", Value = "-1", Selected = true });
		}

		private void GetJiraData()
		{
			contentHolder.Controls.Clear();
			var jiraData = new JiraDataModel();

			if (drpPersons.SelectedValue != "-1")
			{
				jiraData.Assignee = drpPersons.SelectedValue;
			}

			if (drpPriority.SelectedValue != "-1")
			{
				jiraData.Priority = drpPriority.SelectedValue;
			}

			if (drpProject.SelectedValue != "-1")
			{
				jiraData.Project = drpProject.SelectedValue;
			}

			if (drpStatus.SelectedValue != "-1")
			{
				jiraData.Status = drpStatus.SelectedValue;
			}

			jiraData.FromUpdatedDate = oDateRange.FromDate;
			jiraData.ToUpdatedDate = oDateRange.ToDate;
			
			//var jiraList = new List<JiraDataModel>();

			
			var jiraList = JiraDataManager.GetSelectiveJiras(jiraData);
			
			var distinctPersons = (from row in jiraList
								   select row.Assignee)
									.Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();

			var distinctProjects = (from row in jiraList
									select row.Project.Trim())
									.Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
			gridViewJira.DataSource = jiraList;
			gridViewJira.DataBind();
			gridViewJira.AutoGenerateColumns = true;
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindUsers();
				BindStatus();
				BindProject();
				BindPriority();
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			GetJiraData();
		}

		#endregion
	}
}