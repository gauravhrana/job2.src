using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Shared.WebCommon.UI.Web
{
    public class JiraHelper
    {
        private static string jiraUserName = "general.dev";
        private static string jiraPassword = "demodemo";
        private static string jiraURL = "http://ivr-app-jra-01:8080";        

        public static string GetJiraTitle(string jiraKey)
        {
            var jiraTitle = string.Empty;

            if (jiraKey == "N/A")
            {
                jiraTitle = "N/A";
            }
            else
            {

                // create a connection to JIRA
                var jira = new Jira(jiraURL, jiraUserName, jiraPassword);

                // try catch becuase if jiraKey is invalid, it throws exception
                try
                {
                    var issue = (from i in jira.Issues
                                 where i.Key == jiraKey
                                 select i).First();

                    jiraTitle = issue.Summary;
                }
                catch { }
            }

            return jiraTitle;
        }

        public static Issue GetJiraDetails(string jiraKey)
        {
            Issue issue = null;

            if (jiraKey != "N/A")
            {               

                // create a connection to JIRA
                var jira = new Jira(jiraURL, jiraUserName, jiraPassword);

                // try catch becuase if jiraKey is invalid, it throws exception
                try
                {
                    issue = (from i in jira.Issues
                                 where i.Key == jiraKey
                                 select i).First();

                }
                catch { }
            }

            return issue;
        }

    }
}