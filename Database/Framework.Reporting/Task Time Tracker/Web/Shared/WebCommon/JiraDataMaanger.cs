using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Atlassian.Jira;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class JiraDataMaanger : BaseDataManager
    {
        private static string jiraUserName = "general.dev";
        private static string jiraPassword = "demodemo";
        private static string jiraURL = "http://ivr-app-jra-01:8080";

        private static JiraDataModel MapFromJiraIssueToModel(Issue issue)
        {
            JiraDataModel data = null;

            if (issue != null)
            {
                data             = new JiraDataModel();

                data.Title       = issue.Summary;
                data.Description = issue.Description;
                data.WorkTicket  = issue.Key.Value;

                data.CreatedDate = issue.Created;
                data.UpdatedDate = issue.Updated;
                data.DueDate     = issue.DueDate;

                data.Assignee    = issue.Assignee;
                data.Status      = issue.Status.Name;
                data.Priority    = issue.Priority.Name;
                //data.Type        = issue.Type.Name;
                
            }

            return data;
        }

        public static JiraDataModel GetDetails(string jiraKey)
        {
            JiraDataModel result = null;

            if (jiraKey != "N/A")
            {

                // create a connection to JIRA
                var jira = new Jira(jiraURL, jiraUserName, jiraPassword);

                // try catch becuase if jiraKey is invalid, it throws exception
                try
                {
                   var issue = (from i in jira.Issues
                             where i.Key == jiraKey
                             select i).First();

                   result = MapFromJiraIssueToModel(issue);

                }
                catch { }
            }

            return result;
        }

        public static List<JiraDataModel> GetByUser(string userName)
        {
            var list = new List<JiraDataModel>();

            // create a connection to JIRA
            var jira = new Jira(jiraURL, jiraUserName, jiraPassword);
            jira.MaxIssuesPerRequest = 200;

            // try catch becuase if jiraKey is invalid, it throws exception
            try
            {
                var issues = from i in jira.Issues
                                where i.Assignee == userName 
                                 && (i.Status == "Open" || i.Status == "Reopened" || i.Status == "QA" || i.Status == "In Progress")
                                select i;

                foreach (var issue in issues)
                {
                    list.Add(MapFromJiraIssueToModel(issue));
                }

            }
            catch { 
            }

            return list;
        }

    }
}
