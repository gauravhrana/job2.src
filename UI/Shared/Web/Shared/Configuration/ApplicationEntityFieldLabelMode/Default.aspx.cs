using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode
{
	public partial class Default : Shared.UI.WebFramework.BasePage
	{

		private void SetImagePaths()
		{
			string strUserTheme = ApplicationCommon.GetUserTheme();
		}

		private System.Data.DataTable GetData()
		{
			var dt = Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Search(oSearchFilter.SearchParameters, AuditId);

			return dt;
		}

		private string[] GetColumns()
		{
			if (!oDefaultWindowsControl.ApplicationEntityFieldLabelMode.Equals(string.Empty))
				return Framework.Components.ApplicationSecurity.GetApplicationEntityFieldLabelModeColumns(oDefaultWindowsControl.ApplicationEntityFieldLabelMode, AuditId);
			else
				return Framework.Components.ApplicationSecurity.GetApplicationEntityFieldLabelModeColumns("DBColumns", AuditId);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			oDefaultWindowsControl.Setup("ApplicationEntityFieldLabelMode", "Shared/Configuration", "ApplicationEntityFieldLabelModeId", true, new List.GetDataDelegate(GetData),
				new List.GetColumnDelegate(GetColumns), "ApplicationEntityFieldLabelMode");
			oDefaultWindowsControl.ExportMenu.Setup("ApplicationEntityFieldLabelMode", "Configuration", GetData, GetColumns, oSearchFilter.SearchParameters.ToURLQuery());
                       
			oSearchFilter.OnSearch += new EventHandler(oSearchFilter_OnSearch);
			
		}

		void oSearchFilter_OnSearch(object sender, EventArgs e)
		{
			oDefaultWindowsControl.ShowData(false, true);
		}

		protected void btnInsert_Click(object sender, EventArgs e)
		{
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Insert" }));
		}

		protected void btnHome_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Default.aspx");
		}

		protected void lnkExportToCSV_Click(object sender, EventArgs e)
		{
			ExportToCSV(GetData(), GetColumns(), "Application Roles");
		}

		private void ExportToCSV(DataTable dt, string[] strColumns, string strTableName)
		{
			Response.Clear();
			Response.Buffer = true;
			Response.AddHeader("content-disposition",
			 "attachment;filename=" + strTableName + ".csv");
			Response.Charset = "";
			Response.ContentType = "application/text";

			StringBuilder sb = new StringBuilder();
			foreach (string strColumn in strColumns)
			{
				//add separator
				sb.Append(strColumn + ',');
			}

			//append new line
			sb.Append("\r\n");
			foreach (DataRow dr in dt.Rows)
			{
				foreach (string strColumn in strColumns)
					sb.Append(Convert.ToString(dr[strColumn]) + ',');
				sb.Append("\r\n");
			}

			Response.Output.Write(sb.ToString());
			Response.Flush();
			Response.End();
		}

		protected void lnkExportToXML_Click(object sender, EventArgs e)
		{
			ExportToXML(GetData(), GetColumns(), "ApplicationEntityFieldLabelMode");
		}

		private void ExportToXML(DataTable dt, string[] strColumns, string strTableName)
		{
			Response.Clear();
			Response.Buffer = true;
			Response.AddHeader("content-disposition",
			 "attachment;filename=" + strTableName + ".xml");
			Response.Charset = "";
			Response.ContentType = "application/download";

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
			sb.AppendLine("<" + strTableName.Replace(" ", "") + "s>");

			//append new line
			sb.Append("\r\n");
			foreach (DataRow dr in dt.Rows)
			{
				sb.AppendLine("<" + strTableName.Replace(" ", "") + ">");
				foreach (string strColumn in strColumns)
				{
					sb.AppendLine("<" + strColumn + ">" + Convert.ToString(dr[strColumn]) + "</" + strColumn + ">");
				}
				sb.AppendLine("</" + strTableName.Replace(" ", "") + ">");
				sb.Append("\r\n");
			}
			sb.AppendLine("</" + strTableName.Replace(" ", "") + "s>");
			Response.Output.Write(sb.ToString());
			Response.Flush();
			Response.End();
		}

	}
}