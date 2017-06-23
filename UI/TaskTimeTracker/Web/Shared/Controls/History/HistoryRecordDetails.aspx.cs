using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Audit;
using Framework.Components;
using Shared.WebCommon.UI.Web;using Shared.WebCommon.UI.Web;
using System.Data;
using System.Globalization;

namespace Shared.UI.Web.History
{
    public partial class HistoryRecordDetails : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region private methods

        private DataTable GetData()
        {
            var dt = new DataTable();

            var data = new AuditHistory();

            if (!string.IsNullOrEmpty(Request.QueryString["SystemEntity"]))
            {
                data.SystemEntityId = Convert.ToInt32(Request.QueryString["SystemEntity"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["EntityKey"]))
            {
                data.EntityKey = Convert.ToInt32(Request.QueryString["EntityKey"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["AuditAction"]))
            {
                data.AuditActionId = Convert.ToInt32(Request.QueryString["AuditAction"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["ActionBy"]))
            {
                data.PersonId = Convert.ToInt32(Request.QueryString["ActionBy"]);
            }

            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Start"]))
                {
                    data.FromSearchDate = DateTime.ParseExact((Request.QueryString["Start"]), "yyyy-MM-dd hh-mm-ss tt", DateTimeFormatInfo.InvariantInfo);
                }
            }
            catch { }

            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["End"]))
                {
                    data.ToSearchDate = DateTime.ParseExact((Request.QueryString["End"]), "yyyy-MM-dd hh-mm-ss tt", DateTimeFormatInfo.InvariantInfo);
                }
            }
            catch { }

			dt = Framework.Components.Audit.AuditHistoryDataManager.Find(data, SessionVariables.RequestProfile);

            return dt;
        }

        private string[] GetColumns()
        {
			var validColumns = FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.AuditHistory, "Find", SessionVariables.RequestProfile);
            return validColumns;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            oList.Setup("AuditHistory", "Audit", "AuditHistoryId", true, GetData, GetColumns, true, false, false, "AuditHistory");
            myExportMenu.Setup("AuditHistory", String.Empty, GetData, GetColumns, String.Empty);
            if (Request.QueryString["Added"] != null)
                oList.SetSession(Request.QueryString["Added"]);
            else if (Request.QueryString["Deleted"] != null)
                oList.SetSession(Request.QueryString["Deleted"]);
            else
                oList.SetSession("true");

            var lblStatus = ((Label)Master.FindControl("lblStatus"));
            var isTesting = SessionVariables.IsTesting;
            if (lblStatus != null && !(isTesting))
            {
                lblStatus.Visible = false;
            }
        }

        #endregion

    }
}