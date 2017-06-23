using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Admin
{
	public partial class JiraList : Framework.UI.Web.BaseClasses.PageBasePage
    {

        string jiraUserName = "general.dev";
        string jiraPassword = "demodemo";
        string jiraURL = "http://ivr-app-jra-01:8080";

        private void BindGrid()
        {
            // create a connection to JIRA
            var jira = new Jira(jiraURL, jiraUserName, jiraPassword);

            jira.MaxIssuesPerRequest = 250;

            var issues = (from i in jira.Issues
                          where i.Assignee == "gaurav.rana"
                          orderby i.Priority
                          select i).ToList();

            gridViewJira.DataSource = issues;
            gridViewJira.DataBind();

            gridViewJira.AutoGenerateColumns = true;



        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }


    }
}