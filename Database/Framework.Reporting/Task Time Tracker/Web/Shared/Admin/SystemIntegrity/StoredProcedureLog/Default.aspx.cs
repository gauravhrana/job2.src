using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;

namespace Shared.UI.Web.SystemIntegrity.StoredProcedureLog
{
	public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
	{
		
        #region private methods

		private void SetImagePaths()
		{
			string strUserTheme = PerferenceUtility.GetUserTheme();
		}

		protected override DataTable GetData()
		{
			var dt = Framework.Components.LogAndTrace.StoredProcedureLogDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);

			return dt;
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

		#endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.StoredProcedureLog;
            PrimaryEntityKey		= "StoredProcedureLog";
            PrimaryEntityIdColumn	= "StoredProcedureLogId";

            MasterPageCore			= Master;
            SubMenuCore				= Master.SubMenuObject;
            BreadCrumbObject		= Master.BreadCrumbObject;

			SearchFilterCore		= oSearchFilter.GetFilter(PrimaryEntity, PrimaryEntityKey);
            GroupListCore		    = oGroupList;
			IsDynamicSearchControl	= true;
            VisibilityManagerCore	= oVC;
        }

		#endregion

	}
}