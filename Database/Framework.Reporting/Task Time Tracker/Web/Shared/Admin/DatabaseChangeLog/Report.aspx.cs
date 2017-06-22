using Framework.Components.LogAndTrace;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using Shared.UI.WebFramework;
using Shared.UI.Web.Controls;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.DatabaseChangeLog
{
	public partial class Report : BasePage
    {

        #region Properties

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

        StringBuilder DatabaseChangeLogHtmlTable
        {
            get;
            set;
        }
        string LogTimePeriod { get; set; }
		//string GroupByField;

		protected ControlVisibilityManager VisibilityManagerCore { get; set; }

        #endregion        
        
        #region Methods

		protected  DataTable GetData()
		{
			var dt = Framework.Components.LogAndTrace.DatabaseChangeLogDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.DatabaseChangeLog, "DBColumns", SessionVariables.RequestProfile);
		}

        private void AddHeaderRow(HtmlGenericControl tableElement)
        {
            var headerControl = new HtmlGenericControl("tr");
            headerControl.Attributes["class"] = "row text-center";

            DatabaseChangeLogHtmlTable.AppendLine("<tr class='row text-center'>");

            var cell = new HtmlGenericControl("th");
            cell.Attributes["class"] = "col-sm-1";
            cell.InnerText = "Id";
            headerControl.Controls.Add(cell);

            DatabaseChangeLogHtmlTable.AppendLine("<th class='col-sm-1'>Id</th>");

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.DataBaseName)
			{
				cell = new HtmlGenericControl("th");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = "Database";
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<th class='col-sm-1'>Database</th>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.SchemaName)
			{
				cell = new HtmlGenericControl("th");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = "Schema";
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<th class='col-sm-1'>Schema</th>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.ObjectName)
			{
				cell = new HtmlGenericControl("th");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = "Object";
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<th class='col-sm-1'>Object</th>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.ObjectType)
			{
				cell = new HtmlGenericControl("th");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = "Type";
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<th class='col-sm-1'>Type</th>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.EventType)
			{
				cell = new HtmlGenericControl("th");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = "Event";
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<th class='col-sm-1'>Event</th>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.RecordDate)
			{
				cell = new HtmlGenericControl("th");
				cell.Attributes["class"] = "col-sm-3";
				cell.InnerText = "Date";
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<th class='col-sm-2'>Date</th>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.CurrentUser)
			{
				cell = new HtmlGenericControl("th");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = "User";
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<th class='col-sm-1'>User</th>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.HostName)
			{
				cell = new HtmlGenericControl("th");
				cell.Attributes["class"] = "col-sm-2";
				cell.InnerText = "Host";
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<th class='col-sm-2'>Host</th>");
			}

            //cell = new HtmlGenericControl("th");
            //cell.Attributes["class"] = "col-sm-3";
            //cell.InnerText = "Text";
            //headerControl.Controls.Add(cell);

            //DatabaseChangeLogHtmlTable.AppendLine("<th class='col-sm-3'>Text</th>");
            
            DatabaseChangeLogHtmlTable.AppendLine("<tr/>");

            tableElement.Controls.Add(headerControl);
        }

        private void AddDetailRow(DataRow objModel, HtmlGenericControl tableElement)
        {
            var headerControl = new HtmlGenericControl("tr");
            headerControl.Attributes["class"] = "row text-center";

            DatabaseChangeLogHtmlTable.AppendLine("<tr class='row text-center'>");

            var cell = new HtmlGenericControl("td");
            cell.Attributes["class"] = "col-sm-1";
            cell.InnerText = objModel["Id"].ToString();
            headerControl.Controls.Add(cell);

            DatabaseChangeLogHtmlTable.AppendLine("<td class='col-sm-1'>" + objModel["Id"].ToString() + "</td>");

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.DataBaseName)
			{
				cell = new HtmlGenericControl("td");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = objModel["DataBaseName"].ToString();
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<td class='col-sm-1'>" + objModel["DataBaseName"].ToString() + "</td>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.SchemaName)
			{
				cell = new HtmlGenericControl("td");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = objModel["SchemaName"].ToString();
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<td class='col-sm-1'>" + objModel["SchemaName"].ToString() + "</td>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.ObjectName)
			{
				cell = new HtmlGenericControl("td");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = objModel["ObjectName"].ToString();
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<td class='col-sm-1'>" + objModel["ObjectName"].ToString() + "</td>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.ObjectType)
			{
				cell = new HtmlGenericControl("td");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = objModel["ObjectType"].ToString();
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<td class='col-sm-1'>" + objModel["ObjectType"].ToString() + "</td>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.EventType)
			{
				cell = new HtmlGenericControl("td");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = objModel["EventType"].ToString();
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<td class='col-sm-1'>" + objModel["EventType"] + "</td>");
			}
            //SessionVariables.UserDateFormat
			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.RecordDate)
			{
				cell = new HtmlGenericControl("td");
				cell.Attributes["class"] = "col-sm-3";
				cell.InnerText = Convert.ToDateTime(objModel["RecordDate"]).ToString(SessionVariables.UserDateFormat + " " + SessionVariables.UserTimeFormat);
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<td class='col-sm-3'>" + Convert.ToDateTime(objModel["RecordDate"]).ToString(SessionVariables.UserDateFormat + " " + SessionVariables.UserTimeFormat) + "</td>");
			}

			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.CurrentUser)
			{
				cell = new HtmlGenericControl("td");
				cell.Attributes["class"] = "col-sm-1";
				cell.InnerText = objModel["CurrentUser"].ToString();
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<td class='col-sm-1'>" + objModel["CurrentUser"].ToString() + "</td>");
			}
			if (oSearchFilter.GroupBy != DatabaseChangeLogDataModel.DataColumns.HostName)
			{
				cell = new HtmlGenericControl("td");
				cell.Attributes["class"] = "col-sm-2";
				cell.InnerText = objModel["HostName"].ToString();
				headerControl.Controls.Add(cell);

				DatabaseChangeLogHtmlTable.AppendLine("<td class='col-sm-2'>" + objModel["HostName"].ToString() + "</td>");
			}
            //cell = new HtmlGenericControl("td");
            //cell.Attributes["class"] = "col-sm-3";
            //cell.InnerText = objModel.CommandText;
            //headerControl.Controls.Add(cell);

            //DatabaseChangeLogHtmlTable.AppendLine("<td class='col-sm-3'>" + objModel.CommandText + "</td>");

            DatabaseChangeLogHtmlTable.AppendLine("<tr/>");

            tableElement.Controls.Add(headerControl);
        }

		void GetDatabaseChangeLogData()
		{
			//contentHolder.Controls.Clear();
			var data = new DatabaseChangeLogDataModel();

			//data.DataBaseName = txtDatabase.Text.Trim();
			//data.HostName = txtHostName.Text.Trim();
			//data.ObjectName = txtObjectName.Text.Trim();
			//if (drpObjectType.SelectedValue != "-1")
			//{
			//	data.ObjectType = drpObjectType.SelectedValue;
			//}

			//data.FromSearchDate = oDateRange.FromDate;
			//data.ToSearchDate = oDateRange.ToDate;
			var databaseChangeLogData = DatabaseChangeLogDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			//var databaseChangeLogData = DatabaseChangeLogDataManager.Search(data, SessionVariables.RequestProfile);

			if (databaseChangeLogData.Rows.Count > 0)
			{
				LogTimePeriod = string.Empty;
				if (data.FromSearchDate != null)
				{
					LogTimePeriod = " From " + data.FromSearchDate.Value.ToString(SessionVariables.UserDateFormat);
				}
				else
				{
					LogTimePeriod = " From " + databaseChangeLogData.AsEnumerable().OrderBy(x => Convert.ToDateTime(x["RecordDate"]).ToString(SessionVariables.UserDateFormat)).First();
				}

				if (data.ToSearchDate != null)
				{
					LogTimePeriod += " Upto " + data.ToSearchDate.Value.ToString(SessionVariables.UserDateFormat);
				}
				else
				{
					LogTimePeriod += " Upto " + databaseChangeLogData.AsEnumerable().OrderBy(x => Convert.ToDateTime(x["RecordDate"]).ToString(SessionVariables.UserDateFormat)).Last();
				}
			}

			//GroupByField = ddlGroupBy.SelectedValue;
			//pnlGroupListContainer.Controls.Clear();

			IEnumerable<string> distinctTabNames = null;

			if (oSearchFilter.GroupBy != "-1" && !string.IsNullOrEmpty(oSearchFilter.GroupBy))
			{
				distinctTabNames = (from row in databaseChangeLogData.AsEnumerable()
									orderby row[oSearchFilter.GroupBy].ToString().Trim()
									select row[oSearchFilter.GroupBy].ToString().Trim())
												.Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
			}


			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.Setup(SettingCategory, true);

			DatabaseChangeLogHtmlTable = new StringBuilder();

			if (oSearchFilter.GroupBy != "-1" && !string.IsNullOrEmpty(oSearchFilter.GroupBy))
			{
				foreach (var tabName in distinctTabNames)
				{

					var detailContainer = new HtmlGenericControl("div");
					DataTable dtGroupingResult = null;
					string strHeader = string.Empty;


					//dtGroupingResult = databaseChangeLogData.AsEnumerable().Where(t => t[oSearchFilter.GroupBy].ToString() == tabName).CopyToDataTable();
					//dtGroupingResult = databaseChangeLogData.AsEnumerable().Where(t => t.Field<string>(oSearchFilter.GroupBy) == tabName).CopyToDataTable();
					IEnumerable<DataRow> drLogData = databaseChangeLogData.AsEnumerable().Where(t => t.Field<string>(oSearchFilter.GroupBy) == tabName);
					if (drLogData.Count() != 0)
					{

						dtGroupingResult = drLogData.CopyToDataTable();

						var totalRecordsInTab = dtGroupingResult.Rows.Count;

						var groupHeader = tabName;

						strHeader = groupHeader + " (" + totalRecordsInTab.ToString(CultureInfo.InvariantCulture) + ")";

						var tableElement = new HtmlGenericControl("table");
						tableElement.Attributes["class"] = "table table-bordered";

						// prepare html string
						DatabaseChangeLogHtmlTable.AppendLine("<table class='table table-bordered'>");

						DatabaseChangeLogHtmlTable.AppendFormat("<tr style='vertical-align:left;'>");

						DatabaseChangeLogHtmlTable.AppendLine(strHeader);
						DatabaseChangeLogHtmlTable.AppendFormat("</tr>");

						AddHeaderRow(tableElement);

						foreach (var objModel in dtGroupingResult.AsEnumerable())
						{
							AddDetailRow(objModel, tableElement);
						}

						DatabaseChangeLogHtmlTable.AppendLine("</table>");
						//contentHolder.Controls.Add(tableElement);

						detailContainer.Controls.Add(tableElement);
						tabControl.AddTab(tabName, detailContainer, strHeader);
					}
				}
				//pnlGroupListContainer.Controls.Add(tabControl);
			}
			else
			{
				var tableElement = new HtmlGenericControl("table");
				tableElement.Attributes["class"] = "table table-bordered";

				// prepare html string
				DatabaseChangeLogHtmlTable.AppendLine("<table class='table table-bordered'>");

				DatabaseChangeLogHtmlTable.AppendFormat("<tr style='vertical-align:left;'>");


				DatabaseChangeLogHtmlTable.AppendFormat("</tr>");

				AddHeaderRow(tableElement);

				foreach (var objModel in databaseChangeLogData.AsEnumerable())
				{
					AddDetailRow(objModel, tableElement);
				}

				DatabaseChangeLogHtmlTable.AppendLine("</table>");
				//contentHolder.Controls.Add(tableElement);
				//pnlGroupListContainer.Controls.Add(contentHolder);
				
			}
			
		}

        private string GetEmailTemplate()
        {
            var emailTemplate = string.Empty;

            var stream = System.Web.HttpContext.Current.Server.MapPath("~/Templates/" + "DatabaseChangeLogReport.html");

            using (var reader = new StreamReader(stream))
            {
                emailTemplate = reader.ReadToEnd();
            }

            GetDatabaseChangeLogData();

			if (DatabaseChangeLogHtmlTable != null && LogTimePeriod != null)
			{
				emailTemplate = emailTemplate.Replace("##DatabaseChangeLogSummaryTable##", DatabaseChangeLogHtmlTable.ToString());
				emailTemplate = emailTemplate.Replace("##TimePeriod##", LogTimePeriod);
			}

            return emailTemplate;
        }

        #endregion

        #region Events

		//protected void Page_Load(object sender, EventArgs e)
		//{
		//	if (!IsPostBack)
		//	{
		//		// set date range for a single day initially to avoid loading all data
		//		oDateRange.SetDateValues(DateTime.Now.AddDays(-1), null);
		//	}
		//}

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "DatabaseChangeLogReportDefaultView";
			DetailUserPreferenceCategoryId = PerferenceUtility.CreateUserPreferenceCategoryIfNotExists("DatabaseChangeLog", "DatabaseChangeLog");
			VisibilityManagerCore = oVC;
			oSearchFilter.SearchControl.SettingCategory = SettingCategory + "SearchControl";
			oSearchFilter.GetFilter(SystemEntity.DatabaseChangeLog, "Id");           
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			var sbm						= Master.SubMenuObject;

			sbm.SettingCategory			= SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

			var bcControl				= Master.BreadCrumbObject;
			bcControl.SettingCategory	= SettingCategory + "BreadCrumbControl";
			bcControl.Setup("DataBase Change Log Report");
			bcControl.GenerateMenu();

			VisibilityManagerCore		= oVC;

			var isSubMenuVisible		= PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, sbm.SettingCategory);
			var isSearchControlVisible	= PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, oSearchFilter.SearchControl.SettingCategory);

			// set visibility
			oSearchFilter.Visible		= isSearchControlVisible;
			sbm.Visible					= isSubMenuVisible;

			VisibilityManagerCore.ClearChildMenuItems();

			VisibilityManagerCore.AddChildControl(oSearchFilter.SearchControl.Title, isSearchControlVisible);
			VisibilityManagerCore.AddChildControl(sbm.Title, isSubMenuVisible);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			var sbm = Master.SubMenuObject;

			if (!IsPostBack)
			{
				oSearchFilter.SearchControl.Title = "Search Box";
				sbm.Title = "Sub Menu";
				oGroupList.SettingCategory = SettingCategory + "ListControl";
			}

			oSearchFilter.SearchControl.SetupSearch();

			VisibilityManagerCore.Setup(ManageControlVisibility, SettingCategory);

			oGroupList.Setup("", "", "", "", "DatabaseChangeLog", String.Empty, "Id",
				true, GetData, GetColumns, oGroupList.SettingCategory, String.Empty, oSearchFilter.SearchControl, true);

			if (!IsPostBack)
			{
				oGroupList.ShowData(false, true);
			}

			oSearchFilter.SearchControl.OnSearch += oSearchFilter_OnSearch;
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

		//protected void btnSearch_Click(object sender, EventArgs e)
		//{
		//	GetDatabaseChangeLogData();
		//}

		void oSearchFilter_OnSearch(object sender, EventArgs e)
		{
			oGroupList.SettingCategory = SettingCategory + "ListControl";
			oGroupList.Setup("", "", "", "", "DatabaseChangeLog", String.Empty, "Id",
				true, GetData, GetColumns, oGroupList.SettingCategory, String.Empty, oSearchFilter.SearchControl, true);

			oGroupList.ShowData(false, true);

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
                toEmailAddress = "development-common@indusvalleyresearch.com";
            }

            var nMail = new MailMessage(fromEmailAddress, toEmailAddress);
            if (!string.IsNullOrEmpty(txtCCAddress.Text.Trim()))
            {
                var copyEmail = new MailAddress(txtCCAddress.Text.Trim());
                nMail.CC.Add(copyEmail);
            }

            var mailSubject = "Database Change Log Summary" + LogTimePeriod;

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