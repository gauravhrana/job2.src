using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Framework.Components.UserPreference;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.IO;
using Shared.UI.Web.Controls;
using Framework.Components.DataAccess;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;
using DataModel.Framework.Configuration;
using Framework.Components.ApplicationUser;
using DataModel.Framework.AuthenticationAndAuthorization;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using AjaxControlToolkit.HtmlEditor;
using System.Collections;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule
{

    public partial class SendEmail : BasePage
    {
        
        public class WorkTicketSummary
        {
            public string workTicket;
            public string priority;
            public string description;
            public string workHoursByPerson;
            public string workHoursByTicket;
            public string status;
            public string dueDate;

            public WorkTicketSummary(string workTicket, string priority, string description, string workHoursByPerson, string workHoursByTicket, string status, string dueDate)
            {
                WorkTicket = workTicket;
                Priority = priority;
                Description = description;
                WorkHoursByPerson = workHoursByPerson;
                WorkHoursByTicket = workHoursByTicket;
                DueDate = dueDate;
                Status = status;
            }

            public string DueDate
            {
                get;
                set;
            }

            public string WorkHoursByPerson
            {
                get;
                set;
            }

            public string WorkHoursByTicket
            {
                get;
                set;
            }

            public string Status
            {
                get;
                set;
            }
            public string WorkTicket
            {
                get;
                set;
            }
            public string Priority
            {
                get;
                set;
            }
            public string Description
            {
                get;
                set;
            }
            public string WorkHours
            {
                get;
                set;
            }
        }

        #region variable

		int personId = 0;
		string plannedHrs;
		string plannedTime;
private  object Yes;
private object yes;

        private static string ScheduleEmailTemplate { get; set; }
        //private static int isPreview { get; set; }

        protected ControlVisibilityManager VisibilityManagerCore { get; set; }

        private int DetailUserPreferenceCategoryId
        {
            get
            {
                return Convert.ToInt32(ViewState["DetailUserPreferenceCategoryId"]);
            }
            set
            {
                ViewState["DetailUserPreferenceCategoryId"] = value;
            }
        }

        #endregion

        #region Email Methods

        public void SendMail(int? scheduleId, string mailFormat, string toEmailAddress, string ccEmailAddress, string fromEmailAddress,
            bool isPreview, RequestProfile requestProfile)
        {
            var data = new ScheduleDetailDataModel();
            data.ScheduleId = scheduleId;
            var dt = ScheduleDetailDataManager.Search(data, requestProfile);
			if(dt.Rows.Count != 0)
				FormatMail(dt, mailFormat, toEmailAddress, ccEmailAddress, fromEmailAddress, isPreview, requestProfile);
        }

        public void FormatMail(DataTable dt1, string mailFormat, string toEmailAddress, string ccEmailAddress, string fromEmailAddress,
            bool isPreview, RequestProfile requestProfile)
        {
            var mailTemplate = mailFormat;

            decimal totalhrs = 0;

            var stringBuilder = new StringBuilder();
            var strSubject = new StringBuilder();

            var scheduleDetailData = (from m in dt1.AsEnumerable()
                                      select new
                                      {
                                          InTime                           = m["InTime"],
                                          OutTime                          = m["OutTime"],
                                          Message                          = m["Message"].ToString(),
                                          ScheduleDetailActivityCategory   = m["ScheduleDetailActivityCategory"].ToString(),
                                          ScheduleDetailActivityCategoryId = m["ScheduleDetailActivityCategoryId"],
                                          WorkDate                         = m["WorkDate"],
                                          DateDiffHrs                      = m["DateDiffHrs"],
                                          Person                           = m["Person"],
                                          PersonId                         = m["PersonId"],
                                          EmailAddress                     = m["EmailAddress"],
                                          ScheduleId                       = m["ScheduleId"],
                                          WorkTicket                       = m["WorkTicket"]
                                      }).OrderBy(m => m.InTime).ToList();


            var workDate_distinct = (from c in dt1.AsEnumerable()
                                     select new
                                     {
                                         WorkDate   = c["WorkDate"].ToString(),
                                         Person     = c["Person"],
                                         ScheduleId = c["ScheduleId"]
                                     }).Distinct();
            if (!string.IsNullOrEmpty(mailTemplate))
            {
                foreach (var c in workDate_distinct)
                {
                    mailTemplate = mailTemplate.Replace("##PersonName##", c.Person.ToString());
                    mailTemplate = mailTemplate.Replace("##WorkDate##", Convert.ToDateTime(c.WorkDate).ToString("MMMM dd, yyyy"));
                }

                foreach (var item in scheduleDetailData)
                {
                    if (item.DateDiffHrs != null)
                    {
                        totalhrs = totalhrs + Convert.ToDecimal(item.DateDiffHrs);
                    }
                }
                mailTemplate = mailTemplate.Replace("##StartTime##", Convert.ToDateTime(scheduleDetailData.Select(grp => grp.InTime).First()).ToShortTimeString());
                mailTemplate = mailTemplate.Replace("##EndTime##", Convert.ToDateTime(scheduleDetailData.Select(grp => grp.OutTime).Last()).ToShortTimeString());
                mailTemplate = mailTemplate.Replace("##TotalHrs##", totalhrs.ToString("0.00"));

                var bColor = "#d9edf7";
                var i = 0;

                foreach (var item in scheduleDetailData)
                {

                    var jiraIssue       = JiraDataManager.GetDetails(item.WorkTicket.ToString());
                    var jiraDescription = "N/A";
                    var jiraPriority    = "N/A";
                    var jiraWorkHours   = GetJiraWorkHours(item.WorkTicket.ToString(), Convert.ToDateTime(item.WorkDate), Convert.ToInt32(item.PersonId));
					personId = Convert.ToInt32(item.PersonId);
					var minsDuration = ScheduleDataManager.GetMinutes(Convert.ToDouble(item.DateDiffHrs));
					var jiraURL = "http://ivr-app-jra-01:8080/browse/"+item.WorkTicket.ToString();

                    if (jiraIssue != null)
                    {
                        jiraDescription = jiraIssue.Title;
                        jiraPriority = jiraIssue.Priority;
                    }

                    if (i % 2 == 0)
                    {
                        stringBuilder.AppendFormat("<tr style='vertical-align:top'>");
                    }
                    else
                    {
                        stringBuilder.AppendFormat("<tr style='vertical-align:top; background-color: " + bColor + "'>");
                    }
                    stringBuilder.AppendFormat("<td align='left' rowspan='2'>");
                    stringBuilder.AppendFormat(item.ScheduleDetailActivityCategory);
                    stringBuilder.AppendFormat("</td>");

                    stringBuilder.AppendFormat("<td align='left' rowspan='2'>");
                    stringBuilder.AppendFormat(Convert.ToDateTime(item.InTime).ToShortTimeString() + "\t" + "-" + "\t" + Convert.ToDateTime(item.OutTime).ToShortTimeString() + "\t\t");
                    stringBuilder.AppendFormat("</td>");

					stringBuilder.AppendFormat("<td align='right' rowspan='2'>");
					stringBuilder.AppendFormat(minsDuration.ToString());
					stringBuilder.AppendFormat("</td>");

					if (item.WorkTicket.ToString() != "N/A")
					{
						stringBuilder.AppendFormat("<td align='right' rowspan='2'><a href='"+jiraURL+"'>");						
						stringBuilder.AppendFormat(item.WorkTicket.ToString());
						stringBuilder.AppendFormat("</a></td>");
					}
					else
					{
						stringBuilder.AppendFormat("<td align='right' rowspan='2'>");
						stringBuilder.AppendFormat(item.WorkTicket.ToString());
						stringBuilder.AppendFormat("</td>");
					}

					stringBuilder.AppendFormat("<td align='left' rowspan='2'>");
					stringBuilder.AppendFormat(item.Message);
					stringBuilder.AppendFormat("</td>");

					stringBuilder.AppendFormat("</tr>");

					if (i % 2 == 0)
					{
						stringBuilder.AppendFormat("<tr>");
					}
					else
					{
						stringBuilder.AppendFormat("<tr style='background-color: " + bColor + "'>");
					}
					
					stringBuilder.AppendFormat("</tr>");

					i++;
                }
                mailTemplate = mailTemplate.Replace("##Table1##", stringBuilder.ToString());

                scheduleDetailData = (from m in dt1.AsEnumerable()
                                      select new
                                      {
                                          InTime                           = m["InTime"],
                                          OutTime                          = m["OutTime"],
                                          Message                          = m["Message"].ToString(),
                                          ScheduleDetailActivityCategory   = m["ScheduleDetailActivityCategory"].ToString(),
                                          ScheduleDetailActivityCategoryId = m["ScheduleDetailActivityCategoryId"],
                                          WorkDate                         = m["WorkDate"],
                                          DateDiffHrs                      = m["DateDiffHrs"],
                                          Person                           = m["Person"],
                                          PersonId                         = m["PersonId"],
                                          EmailAddress                     = m["EmailAddress"],
                                          ScheduleId                       = m["ScheduleId"],
                                          WorkTicket                       = m["WorkTicket"]
                                      }).OrderBy(m => m.DateDiffHrs).ToList();

                stringBuilder = new StringBuilder();
                var groupedList = new List<WorkTicketSummary>();

                for (var j = 0; j < scheduleDetailData.Count; j++)
                {
                    var jiraIssue         = JiraHelper.GetJiraDetails(scheduleDetailData[j].WorkTicket.ToString());
                    var WorkHoursByTicket = GetJiraWorkHours(scheduleDetailData[j].WorkTicket.ToString(), Convert.ToDateTime(scheduleDetailData[j].WorkDate), 0);
                    var WorkHoursByPerson = GetJiraWorkHours(scheduleDetailData[j].WorkTicket.ToString(), Convert.ToDateTime(scheduleDetailData[j].WorkDate), Convert.ToInt32(scheduleDetailData[j].PersonId));
                    var priority          = string.Empty;
                    var desc              = string.Empty;
                    var workTicket        = string.Empty;
                    var dueDate           = string.Empty;
                    var Status            = string.Empty;
					
					if (jiraIssue != null)
					{
						if (WorkHoursByTicket.Equals("") || jiraIssue.Priority.Equals("") || jiraIssue.Summary.Equals("") || WorkHoursByPerson.Equals(("")))
						{
							continue;
						}
						else
						{
							workTicket = scheduleDetailData[j].WorkTicket.ToString();
							priority = jiraIssue.Priority.Name.ToString();
							desc = jiraIssue.Summary.ToString();
							dueDate = Convert.ToDateTime(jiraIssue.DueDate).ToString("M/d/yyyy");
							Status = jiraIssue.Status.Name.ToString();
						}
					}
                    groupedList.Add(new WorkTicketSummary(workTicket, priority.ToString(), desc.ToString(), WorkHoursByPerson, WorkHoursByTicket, Status, dueDate));

                }

                var selectedValue = groupedList.Select(r => r).OrderBy(r => r.WorkTicket).ToList();
                var selectedList = selectedValue.Select(r => r).GroupBy(r => r.WorkTicket)
									.Where(x => !string.IsNullOrWhiteSpace(x.Key)).ToList();
				
                foreach (var item in selectedList)
                {
					var jiraURL = "http://ivr-app-jra-01:8080/browse/" + item.First().WorkTicket.ToString();

                    if (i % 2 == 0)
                    {
                        stringBuilder.AppendFormat("<tr style='vertical-align:top'>");
                    }
                    else
                    {
                        stringBuilder.AppendFormat("<tr style='vertical-align:top; background-color: " + bColor + "'>");
                    }

					stringBuilder.AppendFormat("<td align='left'><a href='" + jiraURL + "'>");						
                    stringBuilder.AppendFormat(item.First().WorkTicket.ToString());
                    stringBuilder.AppendFormat("</td>");

                    stringBuilder.AppendFormat("<td align='left'>\t");
                    stringBuilder.AppendFormat(item.First().Priority.ToString());
                    stringBuilder.AppendFormat("</td>");

                    stringBuilder.AppendFormat("<td align='left'>\t");
                    stringBuilder.AppendFormat(item.First().Description.ToString());
                    stringBuilder.AppendFormat("</td>");

                    stringBuilder.AppendFormat("<td align='center'>\t");
                    stringBuilder.AppendFormat(item.First().WorkHoursByPerson.ToString() + "/\t" + item.First().WorkHoursByTicket.ToString());
                    stringBuilder.AppendFormat("</td>");

                    stringBuilder.AppendFormat("<td align='left'>\t");
                    stringBuilder.AppendFormat(item.First().Status.ToString());
                    stringBuilder.AppendFormat("</td>");

                    stringBuilder.AppendFormat("<td align='left'>\t");
                    stringBuilder.AppendFormat(item.First().DueDate.ToString());
                    stringBuilder.AppendFormat("</td>");

                    stringBuilder.AppendFormat("</tr>");

                    if (i % 2 == 0)
                    {
                        stringBuilder.AppendFormat("<tr>");
                    }
                    else
                    {
                        stringBuilder.AppendFormat("<tr style='background-color: " + bColor + "'>");
                    }
                   
                    stringBuilder.AppendFormat("</tr>");
                    i++;

                }
                mailTemplate = mailTemplate.Replace("##Table2##", stringBuilder.ToString());

                var applicationUserName = GetApplicationUserName(Convert.ToInt32(scheduleDetailData[0].PersonId));

                // formulate Pending Jiras for user
                var jiraIssues = JiraDataManager.GetByUser(applicationUserName);

				// QA Table starts

				var qaList = jiraIssues.Select(r => r).Where(r => r.Status == "QA").ToList();
				
				var stringBuilderQA = new StringBuilder();

				foreach (var item in qaList)
				{
					if (i % 2 == 0)
					{
						stringBuilderQA.AppendFormat("<tr style='vertical-align:top'>");
					}
					else
					{
						stringBuilderQA.AppendFormat("<tr style='vertical-align:top; background-color: " + bColor + "'>");
					}
					var jiraURL = "http://ivr-app-jra-01:8080/browse/" + item.WorkTicket.ToString();
					
					//stringBuilderQA.AppendFormat("<td align='left'>");
					stringBuilderQA.AppendFormat("<td align='left'><a href='" + jiraURL+ "'>");						
					stringBuilderQA.AppendFormat(item.WorkTicket.ToString());
					stringBuilderQA.AppendFormat("</td>");

					stringBuilderQA.AppendFormat("<td align='left'>\t");
					stringBuilderQA.AppendFormat(item.Priority.ToString());
					stringBuilderQA.AppendFormat("</td>");

					stringBuilderQA.AppendFormat("<td align='left'>\t");
					stringBuilderQA.AppendFormat(item.Title.ToString());
					stringBuilderQA.AppendFormat("</td>");

					stringBuilderQA.AppendFormat("</tr>");

					if (i % 2 == 0)
					{
						stringBuilderQA.AppendFormat("<tr>");
					}
					else
					{
						stringBuilderQA.AppendFormat("<tr style='background-color: " + bColor + "'>");
					}

					stringBuilderQA.AppendFormat("</tr>");
					i++;

				}
				mailTemplate = mailTemplate.Replace("##TableQA##", stringBuilderQA.ToString());

				// Reopened Table starts
				var reopenList = jiraIssues.Select(r => r).Where(r => r.Status == "Reopened").ToList();

				var stringBuilderRO = new StringBuilder();

				foreach (var item in reopenList)
				{
					if (i % 2 == 0)
					{
						stringBuilderRO.AppendFormat("<tr style='vertical-align:top'>");
					}
					else
					{
						stringBuilderRO.AppendFormat("<tr style='vertical-align:top; background-color: " + bColor + "'>");
					}
					var jiraURL = "http://ivr-app-jra-01:8080/browse/" + item.WorkTicket.ToString();

					//stringBuilderQA.AppendFormat("<td align='left'>");
					stringBuilderRO.AppendFormat("<td align='left'><a href='" + jiraURL + "'>");
					stringBuilderRO.AppendFormat(item.WorkTicket.ToString());
					stringBuilderRO.AppendFormat("</td>");

					stringBuilderRO.AppendFormat("<td align='left'>\t");
					stringBuilderRO.AppendFormat(item.Priority.ToString());
					stringBuilderRO.AppendFormat("</td>");

					stringBuilderRO.AppendFormat("<td align='left'>\t");
					stringBuilderRO.AppendFormat(item.Title.ToString());
					stringBuilderRO.AppendFormat("</td>");

					stringBuilderRO.AppendFormat("</tr>");

					if (i % 2 == 0)
					{
						stringBuilderRO.AppendFormat("<tr>");
					}
					else
					{
						stringBuilderRO.AppendFormat("<tr style='background-color: " + bColor + "'>");
					}

					stringBuilderRO.AppendFormat("</tr>");
					i++;

				}
				mailTemplate = mailTemplate.Replace("##TableReOpened##", stringBuilderRO.ToString());

				// QA table Generation ends				

                var strPendingJiras = new StringBuilder();

                var listJiraPriorities = new List<string>() { "Blocker", "Critical", "Major", "Standard", "Minor" };
                strPendingJiras.AppendFormat("<tr>");
                strPendingJiras.AppendLine("<th rowspan='2'>Project</th>");
                strPendingJiras.AppendLine("<th colspan='6'>Priority (Count)</th>");
                strPendingJiras.AppendFormat("</tr>");

                strPendingJiras.AppendFormat("<tr>");
                foreach (var jiraPriority in listJiraPriorities)
                {
                    strPendingJiras.AppendFormat("<th>" + jiraPriority + "</th>");
                }
				strPendingJiras.AppendFormat("<th>Total </th>");
                strPendingJiras.AppendFormat("</tr>");

                var distinctProjects = (from row in jiraIssues
                                    select row.Project)
                                    .Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();                

                foreach (var jiraProject in distinctProjects)
                {
                    var rowTotal = 0;
                    strPendingJiras.AppendFormat("<tr>");

                    strPendingJiras.AppendFormat("<td>" + jiraProject + "</td>");
                    foreach (var jiraPriority in listJiraPriorities)
                    {
                        strPendingJiras.AppendFormat("<td align='right' >");

                        var priorityCount = 0;

                        try
                        {
                            priorityCount = (from jiraIssue in jiraIssues
                                             where jiraIssue.Priority == jiraPriority && jiraIssue.Project == jiraProject
                                             select jiraIssue).Count();
                        }
                        catch { }

                        strPendingJiras.AppendFormat(priorityCount.ToString("G"));
                        strPendingJiras.AppendFormat("</td>");
                        rowTotal += priorityCount;
                    }

                    // add Total
					strPendingJiras.AppendFormat("<td align='right'  style='background-color: " + bColor + "' ><b>");
                    strPendingJiras.AppendFormat(rowTotal.ToString("G"));
                    strPendingJiras.AppendFormat("<b></td>");

                    strPendingJiras.AppendFormat("</tr>");
                }


                strPendingJiras.AppendFormat("<tr>");

                strPendingJiras.AppendFormat("<td>Total: </td>");
                foreach (var jiraPriority in listJiraPriorities)
                {
					strPendingJiras.AppendFormat("<td align='right' style='background-color: " + bColor + "'><b>");

                    var priorityCount = 0;

                    try
                    {
                        priorityCount = (from jiraIssue in jiraIssues
                                         where jiraIssue.Priority == jiraPriority
                                         select jiraIssue).Count();
                    }
                    catch { }

                    strPendingJiras.AppendFormat(priorityCount.ToString("G"));
                    strPendingJiras.AppendFormat("<b></td>");
                }

                // add Total
                strPendingJiras.AppendFormat("<td align='right' ><b>");
                strPendingJiras.AppendFormat(jiraIssues.Count.ToString("G"));
                strPendingJiras.AppendFormat("<b></td>");

                strPendingJiras.AppendFormat("</tr>");

                mailTemplate = mailTemplate.Replace("##TablePendingJIRAs##", strPendingJiras.ToString());

                mailTemplate = mailTemplate.Replace("##CurrentDate##", DateTime.Now.ToString(SessionVariables.UserDateFormat));

                foreach (var c in workDate_distinct)
                {
                    DateTime wd = Convert.ToDateTime(c.WorkDate).AddDays(1);
                    mailTemplate = mailTemplate.Replace("##NextWorkingDate##", wd.ToShortDateString());
					GetPlannedData(requestProfile);
					mailTemplate = mailTemplate.Replace("##NextWorkingTime##", plannedTime);
					mailTemplate = mailTemplate.Replace("##PlannedHours##", plannedHrs);
                }
                foreach (var item in workDate_distinct)
                {
                    var questionValues = (from m in ScheduleDetailDataManager.GetQuestionData(Convert.ToInt32(item.ScheduleId), requestProfile).AsEnumerable()
                                          select new
                                          {
                                              QuestionPhrase = m["QuestionPhrase"],
                                              Answer = m["Answer"]
                                          }).OrderBy(r => r.Answer).Reverse().Distinct();
				
					
                    stringBuilder.Clear();
                    foreach (var Qitem in questionValues)
					{
						stringBuilder.AppendFormat("<tr><td align='left'>");
						stringBuilder.AppendFormat(Qitem.QuestionPhrase.ToString());
					    stringBuilder.AppendFormat("</td><td align='left'>\t");
						stringBuilder.AppendFormat(Qitem.Answer.ToString());
						stringBuilder.AppendFormat("</td></tr>");
					}
                }

                mailTemplate = mailTemplate.Replace("##Table3##", stringBuilder.ToString());

            }
            if (isPreview)
            {
                Editor1.Content = mailTemplate;
                Editor1.NoUnicode = true;
                Editor1.ActiveMode = ActiveModeType.Preview;
            }
            else
            {

                var Msg = new MailMessage();

                var strToEmail = string.Empty;
                if (toEmailAddress != string.Empty)
                    strToEmail = toEmailAddress;
                else
                    strToEmail = "neeti.gupta@indusvalleyresearch.com";

                var nMail = new MailMessage(fromEmailAddress, strToEmail);
                //string strFromEmail = "mailer.ttt@ivr.com";
                if (ccEmailAddress != string.Empty)
                {
                    MailAddress copy = new MailAddress(ccEmailAddress);
                    nMail.CC.Add(copy);
                }

                foreach (var item in scheduleDetailData)
                {
                    string bccMail = item.EmailAddress.ToString();
                    if (bccMail != string.Empty)
                        nMail.Bcc.Add(new MailAddress(bccMail));
                }

                foreach (var c in workDate_distinct)
                {
                    strSubject.Append("EOD Daily Summary Email for " + Convert.ToDateTime(c.WorkDate).ToString("MMMM dd, yyyy") + "(" + totalhrs.ToString("0.00") + " hrs" + ")");
                    strSubject.Append(" - Sent on behalf of " + c.Person);
                }

                nMail.Subject = strSubject.ToString();
                nMail.Body = mailTemplate;
                nMail.IsBodyHtml = true;

                var a = new SmtpClient();
                a.Send(nMail);
            }
        }

        #endregion

        #region Methods

        private string GetApplicationUserName(int applicationUserId)
        {
            var applicationUserName = string.Empty;
            var data = new ApplicationUserDataModel();

            data.ApplicationUserId = applicationUserId;

            DataTable dt = ApplicationUserDataManager.Search(data, SessionVariables.RequestProfile);
            if (dt.Rows.Count == 1)
            {
                applicationUserName = dt.Rows[0][ApplicationUserDataModel.DataColumns.ApplicationUserName].ToString();
            }

            return applicationUserName;
        }

        private string GetJiraWorkHours(string jiraKey, DateTime workDate, int personId)
        {
            string workHours = string.Empty;

            if (!string.IsNullOrEmpty(jiraKey) && jiraKey != "N/A")
            {

                var data = new ScheduleDetailDataModel();
                if (personId == 0)
                {
                    data.WorkTicket = jiraKey;
                    data.ToSearchDate = workDate;   // toDate is WorkDate, so we can find the total hours developer worked on that issue upto that date.
                }
                else
                {
                    data.WorkTicket = jiraKey;
                    data.PersonId = personId;
                    data.ToSearchDate = workDate;   // toDate is WorkDate, so we can find the total hours developer worked on that issue upto that date.
                }

                var dt = ScheduleDetailDataManager.Search(data, SessionVariables.RequestProfile);
                if (dt != null && dt.Rows.Count > 0)
                {
                    // get count
                    workHours = (from row in dt.AsEnumerable()
                                 select Convert.ToDecimal(row["DateDiffHrs"])).Sum().ToString("0.00");
                }
            }

            return workHours;
        }

        protected DataTable GetData()
        {
            var dt = ScheduleDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.Schedule, "DBColumns", SessionVariables.RequestProfile);
        }

        private void ManageControlVisibility(string controlTitle)
        {
            var sbm = this.Master.SubMenuObject;

            switch (controlTitle)
            {
                case "Search Box":
                    oSearchFilter.Visible = true;
                    PerferenceUtility.UpdateUserPreference(oSearchFilter.SearchControl.SettingCategory, ApplicationCommon.ControlVisible, "true");
                    break;

                case "Sub Menu":
                    sbm.Visible = true;
                    PerferenceUtility.UpdateUserPreference(sbm.SettingCategory, ApplicationCommon.ControlVisible, "true");
                    break;
            }
        }

        private void GetCurrentUserEmailAddress()
        {
            var data = new ApplicationUserDataModel();

            data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            data.ApplicationUserName = SessionVariables.ApplicationUserName;

            DataTable dt = ApplicationUserDataManager.Search(data, SessionVariables.RequestProfile);
            if (dt.Rows.Count == 1)
            {
                txtEmailAddress.Text = dt.Rows[0]["EmailAddress"].ToString();
            }
        }

		public void GetPlannedData(RequestProfile requestProfile)
		{
			var dtNow = DateTime.Now;

			var fromDate = dtNow.AddDays(-30).Date;
			var toDate = dtNow.Date;

			var data = new ScheduleDataModel();
			data.FromSearchDate = fromDate;
			data.ToSearchDate = toDate;
			data.Person = personId.ToString();
			var dt = ScheduleDataManager.Search(data, requestProfile);

			var plannedHrsData = from row in dt.AsEnumerable()
								 select dt.AsEnumerable().Average(x => Convert.ToDecimal(x[ScheduleDataModel.DataColumns.TotalHoursWorked]));
			
			var rowItem = from rowPK in plannedHrsData.AsEnumerable() select rowPK;			

			var nextPlannedTimeData = dt.AsEnumerable().OrderBy(x => x[ScheduleDataModel.DataColumns.NextWorkTime]).Last();

			var nextPlannedTime = Convert.ToDateTime(nextPlannedTimeData[ScheduleDataModel.DataColumns.NextWorkTime]).ToShortTimeString();

			var nextPlannedHrs = Convert.ToDecimal(nextPlannedTimeData[ScheduleDataModel.DataColumns.PlannedHours]);

			plannedHrs = nextPlannedHrs.ToString() + '/' + rowItem.First().ToString("#0,0.00");
										
			double totalSec = 0;

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				TimeSpan ts =new TimeSpan(Convert.ToDateTime( Convert.ToDateTime(dt.Rows[i][ScheduleDataModel.DataColumns.StartTime]).ToShortTimeString()).Ticks);
				totalSec += ts.TotalSeconds;

			}

			double averageSec = totalSec / dt.Rows.Count;
			plannedTime = nextPlannedTime + '/' + DateTime.MinValue.AddSeconds(averageSec).ToShortTimeString();
		}


        public void SetScheduleEmailTemplate(bool isPreview)
        {
            var stream = System.Web.HttpContext.Current.Server.MapPath("~/Templates/" + "ScheduleEmailTemplate.html");

            using (var reader = new StreamReader(stream))
            {
                ScheduleEmailTemplate = reader.ReadToEnd();
            }

            if (!isPreview)
            {
                ScheduleEmailTemplate = ScheduleEmailTemplate.Replace("##CellTableBorder##", "none");
                ScheduleEmailTemplate = ScheduleEmailTemplate.Replace("##CellTableWidth##", "0");
            }
            else
            {
                ScheduleEmailTemplate = ScheduleEmailTemplate.Replace("##CellTableBorder##", "solid");
                ScheduleEmailTemplate = ScheduleEmailTemplate.Replace("##CellTableWidth##", "1");
            }

            var dt = ScheduleDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
            foreach (DataRow row in dt.Rows)
            {
                var scheduleId = Convert.ToInt32(row[0]);
                SendMail(scheduleId, ScheduleEmailTemplate, txtEmailAddress.Text, txtCCAddress.Text, ConfigurationManager.AppSettings["fromEmail"], isPreview, SessionVariables.RequestProfile);
                if (isPreview)
                {
                    break;
                }
            }

        }

        #endregion

        #region Events

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var sbm = Master.SubMenuObject;

            sbm.SettingCategory = SettingCategory + "SubMenuControl";
            sbm.Setup();
            sbm.GenerateMenu();

            var bcControl = Master.BreadCrumbObject;
            bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
            bcControl.Setup("Send Email");
            bcControl.GenerateMenu();

            VisibilityManagerCore = oVC;

            var isSubMenuVisible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, sbm.SettingCategory);
            var isSearchControlVisible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, oSearchFilter.SearchControl.SettingCategory);

            // set visibility
            oSearchFilter.Visible = isSearchControlVisible;
            sbm.Visible = isSubMenuVisible;

            VisibilityManagerCore.ClearChildMenuItems();

            VisibilityManagerCore.AddChildControl(oSearchFilter.SearchControl.Title, isSearchControlVisible);
            VisibilityManagerCore.AddChildControl(sbm.Title, isSubMenuVisible);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var sbm = Master.SubMenuObject;

            if (!IsPostBack)
            {
                GetCurrentUserEmailAddress();
                oSearchFilter.SearchControl.Title = "Search Box";
                sbm.Title = "Sub Menu";
                oGroupList.SettingCategory = SettingCategory + "ListControl";
            }

            oSearchFilter.SearchControl.SetupSearch();

            VisibilityManagerCore.Setup(ManageControlVisibility, SettingCategory);

            oGroupList.Setup("", "", "", "", "Schedule", String.Empty, "ScheduleId",
                true, GetData, GetColumns, oGroupList.SettingCategory, String.Empty, oSearchFilter.SearchControl, true);

            if (!IsPostBack)
            {
                oGroupList.ShowData(false, true);
            }

            oSearchFilter.SearchControl.OnSearch += oSearchFilter_OnSearch;
        }

        void oSearchFilter_OnSearch(object sender, EventArgs e)
        {
            oGroupList.SettingCategory = SettingCategory + "ListControl";
            oGroupList.Setup("", "", "", "", "Schedule", String.Empty, "ScheduleId",
                true, GetData, GetColumns, oGroupList.SettingCategory, String.Empty, oSearchFilter.SearchControl, true);

            oGroupList.ShowData(false, true);

        }

        protected override void OnInit(EventArgs e)
        {
            SettingCategory = "SendEmailDefaultView";
            DetailUserPreferenceCategoryId = PerferenceUtility.CreateUserPreferenceCategoryIfNotExists("Schedule", "Schedule");
            VisibilityManagerCore = oVC;
            oSearchFilter.SearchControl.SettingCategory = SettingCategory + "SearchControl";
            oSearchFilter.GetFilter(SystemEntity.Schedule, "ScheduleId");
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {         
            SetScheduleEmailTemplate(false);
            Response.Write("Email Sent!!");
        }

        protected void btnPreviewEmail_Click(object sender, EventArgs e)
        {           
            SetScheduleEmailTemplate(true);
        }

        #endregion

    }
}