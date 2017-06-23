using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin
{
	public partial class DatabaseCleanup : Framework.UI.Web.BaseClasses.PageBasePage
	{

		#region Methods

		[WebMethod]
		public static string GetProcedureText(string dbName, string procName)
		{
			var procText = string.Empty;

			if (!string.IsNullOrEmpty(dbName))
			{

				var sql = "	EXEC sp_helptext N'" + procName + "'";
				var oDT = new Framework.Components.DataAccess.DBDataTable("ProcedureText", sql, dbName);
				if (oDT.DBTable != null && oDT.DBTable.Rows.Count > 0)
				{
					foreach (DataRow dr in oDT.DBTable.Rows)
					{
						procText += dr[0].ToString();
					}
				}				
			}

			return procText;
		}		

		private void BindDBName()
		{
			var dtConnectionStrings = ConnectionStringXApplicationDataManager.GetByApplication(SessionVariables.RequestProfile.ApplicationId, SessionVariables.RequestProfile);

			drpDBName.DataSource    = dtConnectionStrings;
			drpDBName.DataTextField = ConnectionStringXApplicationDataModel.DataColumns.ConnectionString;
			drpDBName.DataTextField = ConnectionStringXApplicationDataModel.DataColumns.ConnectionString;
			drpDBName.DataBind();
		}

		private void bindGrid()
		{
			var dbName                                   = drpDBName.SelectedValue; 
			var name                                     = txtName.Text.Trim();
			var objType                                  = drpObjectType.SelectedValue;

			var sql                                      = "	SELECT * FROM sysobjects"
										+ "	WHERE	type =		'" + objType + "' "
										+ " AND		name LIKE	'%" + name + "%'";


			var oDT                                      = new Framework.Components.DataAccess.DBDataTable("Search", sql, dbName);
			grdResult.DataSource                         = oDT.DBTable;
			grdResult.DataBind();
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindDBName();
				bindGrid();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "DatabaseCleanupDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			bindGrid();
		}

		protected void grdResult_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				var chkBoxItem = e.Row.FindControl("chkItem");
				if (chkBoxItem != null)
				{
					var procName = grdResult.DataKeys[e.Row.RowIndex].Value.ToString();
					var chkBox = (CheckBox)chkBoxItem;
					chkBox.Attributes.Add("onchange", "showProcedurePreview('" + chkBox.ClientID + "', '" + procName + "')");
				}
			}
		}

		protected void btnDropProcedures_Click(object sender, EventArgs e)
		{
			if (grdResult.Rows.Count > 0)
			{
				foreach (GridViewRow row in grdResult.Rows)
				{
					var chkBoxItem = row.FindControl("chkItem");
					if (chkBoxItem != null)
					{
						var procName = grdResult.DataKeys[row.RowIndex].Value.ToString();
						var chkBox = (CheckBox)chkBoxItem;
						if (chkBox.Checked)
						{
							// drop procedure code
							var dbName = drpDBName.SelectedValue;
							var sql = "DROP Procedure " + procName;
							Framework.Components.DataAccess.DBDML.RunSQL("Drop Procedure", sql, dbName);
						}
					}
				}

				bindGrid();
			}
		}

		#endregion

	}
}