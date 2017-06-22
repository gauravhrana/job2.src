using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using Atlassian.Jira;
using Atlassian;
using System.Globalization;
using Atlassian.Jira.Linq;

namespace TaskTimeTracker.Components.Module.TimeTracking
{

    public partial class JiraDataManager : BaseDataManager
    {
        // only 1000 allowed as per api information
        private static int jiraRequestLimit = 1000;
        private static string jiraUserName  = "general.dev";
        private static string jiraPassword  = "demodemo";
        private static string jiraURL       = "http://ivr-app-jra-01:8080";

		public static string[] JiraUserNames = { "Administrator", "Aparajitha.Neelakanta", "Gaurav.Rana", "Merene.Moses", "Neha.Garg", "Neeti.gupta",};

        private static JiraDataModel MapFromJiraIssueToModel(Issue issue)
        {
            JiraDataModel data = null;

            if (issue != null)
            {
                data = new JiraDataModel();

                data.Title = issue.Summary;
                data.Description = issue.Description;
                data.WorkTicket = issue.Key.Value;
                data.Project = issue.Project;

                data.CreatedDate = issue.Created;
                data.UpdatedDate = issue.Updated;
                data.DueDate = issue.DueDate;

                data.Assignee = issue.Assignee;
                data.Status = issue.Status.Name;
                data.Priority = issue.Priority.Name;
								
				//data.PersonId=issue.p
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
				var jira = Jira.CreateRestClient(jiraURL, jiraUserName, jiraPassword);

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

		public static List<JiraDataModel> GetByAdmin(string userName, DateTime currDate, int mthNumber, int year = 0)
		{
			var list = new List<JiraDataModel>();

			// create a connection to JIRA
			var jira = Jira.CreateRestClient(jiraURL, jiraUserName, jiraPassword);

			jira.MaxIssuesPerRequest = jiraRequestLimit;

			var status = "Review Ready";

			//var jqlQuery = "status changed by " + userName + "  AND status = '" + status + "'";
			var jqlQuery = "reporter in (" + userName + ") AND priority not in (Minor,Trivial) AND status = '" + status + "'";

			var issues = jira.GetIssuesFromJql(jqlQuery);
			

			issues = (from i in issues
					  where i.Assignee == "administrator"		
			  		  && i.Updated >= currDate.AddMonths(-6)			  
					  select i);

			if (issues.Count()==0)
			{
				issues = (from i in issues
						  where i.Assignee == "administrator"
						  select i).OrderByDescending(r => r.Updated).Take(10);
			}
			//issues = (from i in issues
			//		  where i.Assignee == "administrator"
			//		  && i.Updated.Value.Month == mthNumber
			//		  && i.Updated.Value.Year == year
			//		  select i);

			foreach (var issue in issues.OrderByDescending(r => r.Updated).ToList())
			{
				list.Add(MapFromJiraIssueToModel(issue));
			}

			return list;
		}

		public static List<JiraDataModel> GetByStatus(string userName, string jiraStatus, DateTime currDate)
		{
			var list = new List<JiraDataModel>();
			// create a connection to JIRA
			var jira = Jira.CreateRestClient(jiraURL, jiraUserName, jiraPassword);
			jira.MaxIssuesPerRequest = jiraRequestLimit;

			int mNumber = Convert.ToInt16(Convert.ToDateTime(currDate).Month);
			int year = Convert.ToInt16(Convert.ToDateTime(currDate).Year);
			int mthNumber = Convert.ToInt16(Convert.ToDateTime(currDate).AddMonths(-1).Month);

			if (mthNumber == 12)
			{
				year = year - 1;
			}

			// try catch becuase if jiraKey is invalid, it throws exception
			try
			{
				IEnumerable<Atlassian.Jira.Issue> issues;

				if (jiraStatus == "Review Ready")
				{
					var jiraAdminIssues = JiraDataManager.GetByAdmin(userName, currDate, mNumber, year);

					list = jiraAdminIssues.Select(r => r)
						.Where(r => (!r.WorkTicket.StartsWith("RNMDE"))).ToList();

				}
				else
				{
					if (jiraStatus == "ClosedLastMonth")
					{
						issues = (from i in jira.Issues
								  where i.Assignee == userName && i.Status == "Closed"
								  select i);
					}
					else
					{
						issues = (from i in jira.Issues
								  where i.Assignee == userName && i.Status == jiraStatus
								  select i);
					}

					foreach (var issue in issues.ToList())
					{
						list.Add(MapFromJiraIssueToModel(issue));
					}

					if ((jiraStatus == "Open") || (jiraStatus == "In Progress") || (jiraStatus == "Resolved"))
					{
						list = list.Select(r => r)
								   .Where(r => (!r.WorkTicket.StartsWith("RNMDE")
									&& (!r.WorkTicket.StartsWith("HR"))
									&& (r.Priority != "Trivial")
									&& (r.Priority != "Minor")))
								   .OrderBy(r => r.Priority)
								   .ThenBy(r => r.UpdatedDate).ToList();
					}
					else if ((jiraStatus == "QA") || ((jiraStatus == "Reopened")))
					{
						list = list.Select(r => r)
									.Where(r => (r.Priority != "Trivial")
									&& (r.Priority != "Minor"))
									.OrderBy(r => r.Priority)
									.ThenBy(r => r.DueDate).ToList();
					}
					else if (jiraStatus == "Closed")
					{
						list = list.Select(r => r).OrderBy(r => r.Priority).ThenBy(r => r.UpdatedDate).ToList();

						if (year == 0)
						{
							list = list.Select(r => r)
									.Where(r => (r.UpdatedDate.Value.Month == mNumber)
										&& (r.UpdatedDate.Value.Year == DateTime.Now.Year)).ToList();
						}
						else
						{
							list = list.Select(r => r)
									.Where(r => (r.UpdatedDate.Value.Month == mNumber)
										&& (r.UpdatedDate.Value.Year == year)).ToList();
						}

					}
					else if (jiraStatus == "ClosedLastMonth")
					{
						list = list.Select(r => r).OrderBy(r => r.Priority).ThenBy(r => r.UpdatedDate).ToList();

						if (year == 0)
						{
							list = list.Select(r => r)
									.Where(r => (r.UpdatedDate.Value.Month == mthNumber)
										&& (r.UpdatedDate.Value.Year == DateTime.Now.Year)).ToList();
						}
						else
						{
							list = list.Select(r => r)
									.Where(r => (r.UpdatedDate.Value.Month == mthNumber)
										&& (r.UpdatedDate.Value.Year == year)).ToList();
						}						
						list.ForEach(se => se.Status = "ClosedLastMonth"); 
					}
				}
				
			}           
            catch
            {
            }
			return list;

		}

        public static List<JiraDataModel> GetByUser(string userName, string jiraStatus = "")
        {
            var list = new List<JiraDataModel>();
            // create a connection to JIRA
            var jira = Jira.CreateRestClient(jiraURL, jiraUserName, jiraPassword);
            jira.MaxIssuesPerRequest = jiraRequestLimit;

            // try catch becuase if jiraKey is invalid, it throws exception
            try
            {

                IEnumerable<Atlassian.Jira.Issue> issues;

                if (!string.IsNullOrEmpty(jiraStatus))
                {
					if (!string.IsNullOrEmpty(userName))
					{
						issues = (from i in jira.Issues
								  where i.Assignee == userName && i.Priority != "Minor" && i.Priority != "Trivial"
								   && i.Status == jiraStatus
								  select i);
					}
					else
					{
						issues = (from i in jira.Issues
								  where i.Priority != "Minor" && i.Priority != "Trivial"
								   && i.Status == jiraStatus
								  select i);
					}
                }
                else
                {
					if (!string.IsNullOrEmpty(userName))
					{
						issues = (from i in jira.Issues
								  where i.Assignee == userName && i.Priority != "Minor" && i.Priority != "Trivial"
								   && (i.Status == "Open" || i.Status == "Reopened" || i.Status == "QA" || i.Status == "In Progress" || i.Status == "Continuous Monitoring")
								  select i);
					}
					else
					{
						issues = (from i in jira.Issues
								  where i.Priority != "Minor" && i.Priority != "Trivial"
								   && (i.Status == "Open" || i.Status == "Reopened" || i.Status == "QA" || i.Status == "In Progress" || i.Status == "Continuous Monitoring")
								  select i);
					}
                }
                

                foreach (var issue in issues.ToList())
                {
                    list.Add(MapFromJiraIssueToModel(issue));
                }

            }
            catch
            {
            }

            return list;
        }

		public static List<JiraDataModel> GetJiraClosedByUser(string userName, int mthNumber , int year = 0)
		{
			var list = new List<JiraDataModel>();
			// create a connection to JIRA
			var jira = Jira.CreateRestClient(jiraURL, jiraUserName, jiraPassword);
			jira.MaxIssuesPerRequest = jiraRequestLimit;			
			
			// try catch becuase if jiraKey is invalid, it throws exception
			try
			{
				IEnumerable<Atlassian.Jira.Issue> issues;

				issues = (from i in jira.Issues where i.Status == "Closed" && i.Assignee == userName.ToLower()
						  select i).OrderBy(r => r.Priority).ThenBy(r => r.Updated);

				
				if (year == 0)
				{
					issues = (from i in issues
							  where i.Updated.Value.Month == mthNumber && i.Updated.Value.Year == DateTime.Now.Year
							  select i);
				}
				else
				{					
						issues = (from i in issues
								  where i.Updated.Value.Month == mthNumber && i.Updated.Value.Year == year
								  select i);					
				}

				foreach (var issue in issues.ToList())
				{
					list.Add(MapFromJiraIssueToModel(issue));
				}
			}
			catch
			{
			}

			return list;
		}

		public static List<JiraDataModel> GetJiraLastNMonthClosedByUser(int monthsToConsider, string userName, DateTime CurrDate)
		{
			var list = new List<JiraDataModel>();
			// create a connection to JIRA
			var jira = Jira.CreateRestClient(jiraURL, jiraUserName, jiraPassword);
			jira.MaxIssuesPerRequest = jiraRequestLimit;

			// try catch becuase if jiraKey is invalid, it throws exception
			try
			{
				IEnumerable<Atlassian.Jira.Issue> issues;

				issues = (from i in jira.Issues
						  where i.Assignee == userName.ToLower() //&& i.Status == "Closed" && i.Priority != "Minor" && i.Priority != "Trivial" //&& !i.Key.Value.StartsWith("RNMDE") && !i.Key.Value.StartsWith("HR")
						  select i).OrderBy(r => r.Priority).ThenBy(r => r.Updated);

				issues = (from i in issues
						  where
                          DateTime.Compare(i.Updated.Value, CurrDate.AddMonths(-monthsToConsider)) >= 0 
                          && (i.Updated.Value.Year == DateTime.Now.Year)
						  select i);
				
				foreach (var issue in issues.ToList())
				{
					list.Add(MapFromJiraIssueToModel(issue));
				}
			}
			catch
			{
			}

			return list;
		}

        public static List<JiraDataModel> GetResolvedJiras(JiraDataModel searchData)
        {
            var list = new List<JiraDataModel>();

            // create a connection to JIRA
            var jira = Jira.CreateRestClient(jiraURL, jiraUserName, jiraPassword);
            jira.MaxIssuesPerRequest = jiraRequestLimit;

            // try catch becuase if jiraKey is invalid, it throws exception

            try
            {
				IEnumerable<Atlassian.Jira.Issue> issues;
				
                if (!string.IsNullOrEmpty(searchData.Assignee))
                {
                    issues = (from i in jira.Issues
                              where i.Status == "Closed" && i.Assignee == searchData.Assignee
                              select i);
                }
                else
                {
                    issues = (from i in jira.Issues
                             where i.Status == "Closed"
                             select i);
                }

                if (searchData.FromUpdatedDate != null)
                {
                    issues = (from i in issues
                             where i.Updated >= searchData.FromUpdatedDate
                             select i); 
                }

                if (searchData.ToUpdatedDate != null)
                {
                    issues = (from i in issues
                             where i.Updated <= searchData.ToUpdatedDate
                             select i); 
                }

                foreach (var issue in issues.ToList())
                {
                    list.Add(MapFromJiraIssueToModel(issue));
                }

            }
            catch
            {
            }

            return list;
        }

		public static List<JiraDataModel> GetSelectiveJiras(JiraDataModel searchData)
		{
			var list = new List<JiraDataModel>();

			// create a connection to JIRA
			var jira = Jira.CreateRestClient(jiraURL, jiraUserName, jiraPassword);
			jira.MaxIssuesPerRequest = jiraRequestLimit;

			// try catch becuase if jiraKey is invalid, it throws exception
			try
			{
				IEnumerable<Atlassian.Jira.Issue> issues;

				issues = (from i in jira.Issues select i);

				if (!string.IsNullOrEmpty(searchData.Assignee))
				{
					issues = (from i in issues where i.Assignee.ToString() == searchData.Assignee.ToLower() select i);
				}
				if (!string.IsNullOrEmpty(searchData.Priority))
				{
					issues = (from i in issues where i.Priority.Name == searchData.Priority select i);					
				}
				if (!string.IsNullOrEmpty(searchData.Project))
				{
					issues = issues.Where(i => i.Project == searchData.Project);
				}
				if (!string.IsNullOrEmpty(searchData.Status))
				{
					issues = (from i in issues where i.Status.Name == searchData.Status select i);

				}
				if (searchData.FromUpdatedDate != null)
				{
					issues = (from i in issues where i.Updated >= searchData.FromUpdatedDate select i);
				}

				if (searchData.ToUpdatedDate != null)
				{
					issues = (from i in issues where i.Updated <= searchData.ToUpdatedDate select i);

				}
				
				foreach (var issue in issues.ToList())
				{
					list.Add(MapFromJiraIssueToModel(issue));
				}

			}
			catch
			{
			}

			return list;
		}

		public static List<JiraDataModel> GetAllOpenJiras(JiraDataModel searchData)
		{
			var list = new List<JiraDataModel>();

			// create a connection to JIRA
			var jira = Jira.CreateRestClient(jiraURL, jiraUserName, jiraPassword);
			jira.MaxIssuesPerRequest = jiraRequestLimit;

			// try catch becuase if jiraKey is invalid, it throws exception
			try
			{
				IEnumerable<Atlassian.Jira.Issue> issues;

				issues = (from i in jira.Issues select i);

				issues = (from i in issues
						  where i.Status.Name == "Open"
						  select i);
				foreach (var issue in issues.ToList())
				{
					list.Add(MapFromJiraIssueToModel(issue));
				}

			}
			catch
			{
			}

			return list;
		}

		public static List<JiraDataModel> GetAllClosedJiras(JiraDataModel searchData) 
		{ 
			var list = new List<JiraDataModel>();

			// create a connection to JIRA
			var jira = Jira.CreateRestClient(jiraURL, jiraUserName, jiraPassword);
			jira.MaxIssuesPerRequest = jiraRequestLimit;

			// try catch becuase if jiraKey is invalid, it throws exception
			try
			{
				IEnumerable<Atlassian.Jira.Issue> issues;

				issues = (from i in jira.Issues select i);

				issues = (from i in issues
						  where i.Status.Name == "Closed"
						  select i);
				foreach (var issue in issues.ToList())
				{
					list.Add(MapFromJiraIssueToModel(issue));
				}

			}
			catch
			{
			}

			return list;
		}

		public static List<JiraDataModel> GetClosedJiras(JiraDataModel searchData,int month=0,int year=0)		
		{
			var list = new List<JiraDataModel>();

			// create a connection to JIRA
			var jira = Jira.CreateRestClient(jiraURL, jiraUserName, jiraPassword);
			jira.MaxIssuesPerRequest = jiraRequestLimit;

			// try catch becuase if jiraKey is invalid, it throws exception
			try
			{
				IEnumerable<Atlassian.Jira.Issue> issues;
				issues = (from i in jira.Issues select i);

				if (!string.IsNullOrEmpty(searchData.Assignee))
				{
					issues = (from i in issues where i.Assignee.ToString() == searchData.Assignee.ToLower() select i);
				}
				
				if (!string.IsNullOrEmpty(searchData.Project))
				{
					issues = issues.Where(i => i.Project == searchData.Project);
				}
				if (searchData.FromUpdatedDate != null)
				{
					issues = (from i in issues where i.Updated >= searchData.FromUpdatedDate select i);
				}

				if (searchData.ToUpdatedDate != null)
				{
					issues = (from i in issues where i.Updated <= searchData.ToUpdatedDate select i);

				}

				issues = (from i in issues
							where i.Status.Name == "Closed"
							select i);

				if (month == 0 && year == 0 && searchData.ToUpdatedDate == null && searchData.FromUpdatedDate == null)
				{
					issues = (from i in issues
							  where i.Updated.Value.Month == DateTime.Now.Month && i.Updated.Value.Year == DateTime.Now.Year
							  select i);
				}
				else
				{
					if (month != 0)
					{
						issues = (from i in issues
								  where i.Updated.Value.Month == month
								  select i);
					}
					if (year != 0)
					{
						issues = (from i in issues
								  where i.Updated.Value.Month == month && i.Updated.Value.Year == year
								  select i);
					}
				}

				foreach (var issue in issues.ToList())
				{
					list.Add(MapFromJiraIssueToModel(issue));
				}

			}
			catch
			{
			}

			return list;
		}
    }

}
