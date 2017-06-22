using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.ApplicationUser;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace ApplicationContainer.UI.Web.BM.Scheduling
{
    public partial class ResolvedJiraSummaryEmail : System.Web.UI.Page
    {

        #region Properties

        StringBuilder ResolvedJiraHtmlTable
        {
            get;
            set;
        }
        string ResultTimePeriod { get; set; }

        #endregion

        #region Methods

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

        private string GetUserFullName(string applicationUserName)
        {
            var userFullName = applicationUserName;
            try
            {
                var x = drpPersons.Items.Cast<ListItem>()
                        .Where(o => o.Value.ToLower() == applicationUserName.ToLower()).First();

                userFullName = x.Text;
            }
            catch { }
            var listItem = drpPersons.Items.FindByValue(applicationUserName);
            if (listItem != null)
            {
                userFullName = listItem.Text;
            }

            return userFullName;
        }

        private void AddHeaderRow(List<string> distinctPersons, HtmlGenericControl tableElement)
        {            
            var headerControl = new HtmlGenericControl("tr");
            headerControl.Attributes["class"] = "row text-center";

            ResolvedJiraHtmlTable.AppendLine("<tr class='row text-center'>");

            var cell = new HtmlGenericControl("th");
            cell.Attributes["class"] = "col-sm-2";
            cell.InnerText = "";
            headerControl.Controls.Add(cell);

            ResolvedJiraHtmlTable.AppendLine("<th class='col-sm-2'></th>");

            cell = new HtmlGenericControl("th");
            cell.Attributes["class"] = "col-sm-11 text-center";
            cell.Attributes["colspan"] = (distinctPersons.Count + 1).ToString();
            cell.InnerText = "Asignees";
            headerControl.Controls.Add(cell);

            ResolvedJiraHtmlTable.AppendLine("<th class='col-sm-11 text-center' colspan='" + (distinctPersons.Count + 1).ToString() + "'>Asignees</th>");
            ResolvedJiraHtmlTable.AppendLine("</tr>");

            tableElement.Controls.Add(headerControl);

            var headerControl1 = new HtmlGenericControl("tr");
            headerControl1.Attributes["class"] = "row text-center";

            ResolvedJiraHtmlTable.AppendLine("<tr class='row text-center'>");

            cell = new HtmlGenericControl("th");
            cell.Attributes["class"] = "col-sm-2";
            cell.InnerText = "Projects";
            headerControl1.Controls.Add(cell);

            ResolvedJiraHtmlTable.AppendLine("<th class='col-sm-2'>Projects</th>");

            foreach (var str in distinctPersons)
            {
                var userFullName = GetUserFullName(str);
                cell = new HtmlGenericControl("th");
                cell.Attributes["class"] = "col-sm-1";
                cell.InnerText = userFullName;
                headerControl1.Controls.Add(cell);

                ResolvedJiraHtmlTable.AppendLine("<th class='col-sm-1'>" + userFullName + "</th>");
            }

            cell = new HtmlGenericControl("th");
            cell.Attributes["class"] = "col-sm-1";
            cell.InnerText = "Total";
            headerControl1.Controls.Add(cell);

            ResolvedJiraHtmlTable.AppendLine("<th class='col-sm-1'>Total</th>");
            ResolvedJiraHtmlTable.AppendLine("<tr/>");

            tableElement.Controls.Add(headerControl1);
        }

        private void AddDetailRow(string project, List<string> distinctPersons, List<JiraDataModel> jiraList, HtmlGenericControl tableElement)
        {
            var rowAggeregates = new List<decimal>();
            var headerControl1 = new HtmlGenericControl("tr");
            headerControl1.Attributes["class"] = "row text-right";

            ResolvedJiraHtmlTable.AppendLine("<tr class='row text-right'>");

            var cell = new HtmlGenericControl("td");
            cell.Attributes["class"] = "col-sm-2";
            cell.InnerText = project;
            headerControl1.Controls.Add(cell);

            ResolvedJiraHtmlTable.AppendLine("<td class='col-sm-2'>" + project + "</td>");

            foreach (var person in distinctPersons)
            {
                decimal aggValue = 0;
                try
                {
                    var list = (from row in jiraList
                                where row.Assignee == person && row.Project == project
                                select row).ToList();
                    aggValue = list.Count;
                }
                catch { }

                cell = new HtmlGenericControl("td");
                cell.Attributes["class"] = "col-sm-1";
                cell.InnerText = aggValue.ToString("0.00");
                headerControl1.Controls.Add(cell);

                ResolvedJiraHtmlTable.AppendLine("<td class='col-sm-1'>" + aggValue.ToString("0.00") + "</td>");

                rowAggeregates.Add(aggValue);
            }

            cell = new HtmlGenericControl("td");
            cell.Attributes["class"] = "col-sm-1";
            cell.InnerText = rowAggeregates.Sum().ToString("0.00");
            headerControl1.Controls.Add(cell);

            ResolvedJiraHtmlTable.AppendLine("<td class='col-sm-1'>" + rowAggeregates.Sum().ToString("0.00") + "</td>");
            ResolvedJiraHtmlTable.AppendLine("</tr>");

            tableElement.Controls.Add(headerControl1);
        }

        private void AddAggeregateRow(List<string> distinctPersons, List<JiraDataModel> jiraList, HtmlGenericControl tableElement)
        {
            var rowAggeregates = new List<decimal>();

            var headerControl1 = new HtmlGenericControl("tr");
            headerControl1.Attributes["class"] = "row text-right";

            ResolvedJiraHtmlTable.AppendLine("<tr class='row text-right'>");

            var cell = new HtmlGenericControl("td");
            cell.Attributes["class"] = "col-sm-2";
            cell.InnerText = "Total";
            headerControl1.Controls.Add(cell);

            ResolvedJiraHtmlTable.AppendLine("<td class='col-sm-2'>Total</td>");

            foreach (var person in distinctPersons)
            {
                decimal aggValue = 0;
                try
                {
                    var list = (from row in jiraList
                                where row.Assignee == person
                                select row).ToList();
                    aggValue = list.Count;
                }
                catch { }

                cell = new HtmlGenericControl("td");
                cell.Attributes["class"] = "col-sm-1";
                cell.InnerText = aggValue.ToString("0.00");
                headerControl1.Controls.Add(cell);

                ResolvedJiraHtmlTable.AppendLine("<td class='col-sm-1'>" + aggValue.ToString("0.00") + "</td>");

                rowAggeregates.Add(aggValue);
            }

            cell = new HtmlGenericControl("td");
            cell.Attributes["class"] = "col-sm-1";
            cell.InnerText = rowAggeregates.Sum().ToString("0.00");
            headerControl1.Controls.Add(cell);

            ResolvedJiraHtmlTable.AppendLine("<td class='col-sm-1'>" + rowAggeregates.Sum().ToString("0.00") + "</td>");
            ResolvedJiraHtmlTable.AppendLine("</tr");

            tableElement.Controls.Add(headerControl1);
        }

        private void GetJiraData()
        {
            contentHolder.Controls.Clear();
            var jiraData = new JiraDataModel();

            if (drpPersons.SelectedValue != "-1")
            {
                jiraData.Assignee = drpPersons.SelectedValue;
            }

            jiraData.FromUpdatedDate = oDateRange.FromDate;
            jiraData.ToUpdatedDate = oDateRange.ToDate;

            var jiraList = JiraDataManager.GetResolvedJiras(jiraData);

            var distinctPersons = (from row in jiraList
                                    select row.Assignee)
                                    .Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();

            var distinctProjects = (from row in jiraList
                                    select row.Project.Trim())
                                    .Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();

            if (jiraList.Count > 0)
            {
                ResultTimePeriod = string.Empty;
                if (oDateRange.FromDate != null)
                {
                    ResultTimePeriod = " From " + oDateRange.FromDate.Value.ToString(SessionVariables.UserDateFormat);
                }
                else
                {
                    ResultTimePeriod = " From " + jiraList.OrderBy(x => x.UpdatedDate).First().UpdatedDate.Value.ToString(SessionVariables.UserDateFormat);
                }

                if (oDateRange.ToDate != null)
                {
                    ResultTimePeriod += " Upto " + oDateRange.ToDate.Value.ToString(SessionVariables.UserDateFormat);
                }
                else
                {
                    ResultTimePeriod += " Upto " + jiraList.OrderBy(x => x.UpdatedDate).Last().UpdatedDate.Value.ToString(SessionVariables.UserDateFormat);
                }

            }

            var tableElement = new HtmlGenericControl("table");
            tableElement.Attributes["class"] = "table table-bordered";

            ResolvedJiraHtmlTable = new StringBuilder();

            // prepare html string
            ResolvedJiraHtmlTable.AppendLine("<table class='table table-bordered'>");

            AddHeaderRow(distinctPersons, tableElement);

            foreach (var project in distinctProjects)
            {
                AddDetailRow(project, distinctPersons, jiraList, tableElement);
            }

            AddAggeregateRow(distinctPersons, jiraList, tableElement);

            ResolvedJiraHtmlTable.AppendLine("</table>");

            contentHolder.Controls.Add(tableElement);       

        }

        private string GetCurrentUserEmailAddress()
        {
            var currentUserEmail = string.Empty;
            var data = new ApplicationUserDataModel();

            data.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
            data.ApplicationUserName = SessionVariables.ApplicationUserName;

            DataTable dt = ApplicationUserDataManager.Search(data, SessionVariables.RequestProfile);
            if (dt.Rows.Count == 1)
            {
                txtEmailAddress.Text = dt.Rows[0][ApplicationUserDataModel.DataColumns.EmailAddress].ToString();
            }

            currentUserEmail = txtEmailAddress.Text;

            return currentUserEmail;
        }

        private string GetEmailTemplate()
        {
            var emailTemplate = string.Empty;

            var stream = System.Web.HttpContext.Current.Server.MapPath("~/Templates/" + "ResolvedJiraSummaryReport.html");

            using (var reader = new StreamReader(stream))
            {
                emailTemplate = reader.ReadToEnd();
            }

            GetJiraData();

            emailTemplate = emailTemplate.Replace("##ResolvedIssuesTable##", ResolvedJiraHtmlTable.ToString());
            emailTemplate = emailTemplate.Replace("##TimePeriod##", ResultTimePeriod);

            return emailTemplate;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsers();
                GetCurrentUserEmailAddress();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetJiraData();
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            var emailTemplate = GetEmailTemplate();

            var fromEmailAddress = ConfigurationManager.AppSettings["fromEmail"];
            var toEmailAddress = string.Empty;

            if (!string.IsNullOrEmpty(txtEmailAddress.Text.Trim()))
            {
                toEmailAddress = txtEmailAddress.Text.Trim();
            }
            else
            {
                toEmailAddress = GetCurrentUserEmailAddress();
            }

            var nMail = new MailMessage(fromEmailAddress, toEmailAddress);
            if (!string.IsNullOrEmpty(txtCCAddress.Text.Trim()))
            {
                var copyEmail = new MailAddress(txtCCAddress.Text.Trim());
                nMail.CC.Add(copyEmail);
            }

            var mailSubject = "Resolved Jira Summary" + ResultTimePeriod;

            nMail.Subject = mailSubject;
            nMail.Body = emailTemplate;
            nMail.IsBodyHtml = true;

            var smtpClient = new SmtpClient();
            smtpClient.Send(nMail);

            Response.Write("Email Sent");

        }

        protected void btnPreviewEmail_Click(object sender, EventArgs e)
        {
            var emailTemplate = GetEmailTemplate();

            Editor1.Content = emailTemplate;
            Editor1.NoUnicode = true;
            Editor1.ActiveMode = AjaxControlToolkit.HtmlEditor.ActiveModeType.Preview;
        }

        #endregion

    }
}