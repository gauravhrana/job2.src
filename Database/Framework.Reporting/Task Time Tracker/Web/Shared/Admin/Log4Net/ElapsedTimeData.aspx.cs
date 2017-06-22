using System;
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
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using DataModel.Framework.Configuration;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.BusinessLayer;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.Log4Net
{
	public partial class ElapsedTimeData : PageCommon
	{
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

		public string ComputerName
		{
			get
			{
				return txtComputerName.Text;
			}
		}

		public string ConnectionKey
		{
			get
			{
				return txtConnectionKey.Text;
			}
		}

		public string LogUser
		{
			get
			{
				return txtApplicationUserList.Text;
			}
		}

		#region private methods

		private void DataBind()
		{
			var data = new Framework.Components.LogAndTrace.Log4NetDataModel();

			if (!string.IsNullOrEmpty(ComputerName))
			{
				data.Computer = ComputerName;
			}
			if (!string.IsNullOrEmpty(ConnectionKey))
			{
				data.ConnectionKey = ConnectionKey;
			}
			if(LogUser!= "-1")
			{
			data.LogUser		= LogUser;
			}

			var dt = Framework.Components.LogAndTrace.Log4NetDataManager.GetElapsedTimeRecords(data, SessionVariables.RequestProfile);

			var GroupByField = drpGroupBy.SelectedValue;
			pnlGroupListContainer.Controls.Clear();			

			if (GroupByField != "None")
			{
				var distinctTabNames = (from row in dt.AsEnumerable()
										orderby row[GroupByField].ToString().Trim()
										select row[GroupByField].ToString().Trim())
												.Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();

				var tabControl = ApplicationCommon.GetNewDetailTabControl();
				
				tabControl.Setup(SettingCategory, true);
				
				var iTabIndex = 0;

				foreach (var tabName in distinctTabNames)
				{

					var detailContainer = new HtmlGenericControl("div");

					var dtGroupingResult = dt.AsEnumerable().Where(t => t[GroupByField].ToString() == tabName).CopyToDataTable();

					var totalRecordsInTab = dtGroupingResult.Rows.Count;

					var groupHeader = tabName;

					var strHeader = groupHeader + " (" + totalRecordsInTab.ToString(CultureInfo.InvariantCulture) + ")";

					var gv = new GridView();					
					
					gv.ID = "myGridID" + iTabIndex;					

					GridViewSetUp(gv, dtGroupingResult);

					detailContainer.Controls.Add(gv);
					tabControl.AddTab(tabName, detailContainer, strHeader);

					iTabIndex++;
				}

				pnlGroupListContainer.Controls.Add(tabControl);
				
			}
			else
			{
				var gv = new GridView();

				gv.ID		= "myGridID";				

				GridViewSetUp(gv, dt);
				
				pnlGroupListContainer1.Controls.Add(gv);
			}
			
			SetupDropdown();
		}

		public void GridViewSetUp(GridView gv, DataTable dt)
		{
			gv.AutoGenerateColumns = false;
			gv.CssClass = "table table-hover table-striped";

			BoundField nameColumn = new BoundField();
			nameColumn.DataField = Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.LogUser;
            nameColumn.HeaderText = "Log User";
			gv.Columns.Add(nameColumn);

			nameColumn = new BoundField();
			nameColumn.DataField = Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.Date;
			nameColumn.HeaderText = Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.Date;
			gv.Columns.Add(nameColumn);

			nameColumn = new BoundField();
			nameColumn.DataField = Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.ApplicationUser;
            nameColumn.HeaderText = "Application User";
			gv.Columns.Add(nameColumn);

			nameColumn = new BoundField();
			nameColumn.ItemStyle.CssClass = "text-right";
			nameColumn.DataFormatString = "{0:#0,0.00}";
			nameColumn.DataField = Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.ElapsedTime;
            nameColumn.HeaderText = "Elapsed Time" + "(ms)";
			gv.Columns.Add(nameColumn);

			nameColumn = new BoundField();
			nameColumn.ItemStyle.CssClass = "text-right";
			nameColumn.DataFormatString = "{0:#0,0.00}";
			nameColumn.DataField = Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.RecordCount;
            nameColumn.HeaderText = "Record Count";
			gv.Columns.Add(nameColumn);

			nameColumn = new BoundField();
			nameColumn.DataField = Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.Message;
			nameColumn.HeaderText = Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.Message;
			gv.Columns.Add(nameColumn);

			nameColumn = new BoundField();
			nameColumn.DataField = Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.Computer;
			nameColumn.HeaderText = Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.Computer;
			gv.Columns.Add(nameColumn);

			nameColumn = new BoundField();
			nameColumn.DataField = Framework.Components.LogAndTrace.Log4NetDataModel.DataColumns.ConnectionKey;
            nameColumn.HeaderText = "Connection Key";
            gv.Columns.Add(nameColumn);

			gv.DataSource = dt;
			gv.DataBind();
		}

		private void SetupDropdown()
		{

			var configScript = AjaxHelper.GetKendoComboBoxConfigScript("GetAllApplicationUserList", "ApplicationCode", "ApplicationUserId",
				txtApplicationUserList.ClientID);

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ApplicationUserId", configScript, true);

			var str = new StringBuilder();
			str.AppendLine("$(document).ready(function ()");
			str.AppendLine("        {");
			str.AppendLine("$.ajax(");
			str.AppendLine("        {");
			str.AppendLine("type: \"POST\",");
			str.AppendLine("url: \"http://localhost:53331/API/AutoComplete.asmx/GetComputerList\",");
			//str.AppendLine("data:\"{\'primaryEntity\':\'" + PrimaryEntity + "\',\'txtName\':\'" + name + "\',\'AuditId\':\'" + SessionVariables.RequestProfile.AuditId + "\'}\",");
			str.AppendLine("contentType: \"application/json; charset=utf-8\",");
			str.AppendLine("dataType: \"json\",");
			str.AppendLine("success: function (msg)");
			str.AppendLine("        {");
			str.AppendLine("$(\"#" + txtComputerName.ClientID + "\").kendoAutoComplete({");
			str.AppendLine("    dataSource: msg.d,filter: \"startswith\"");
			str.AppendLine("        });");
			str.AppendLine("        }");
			str.AppendLine("        });");
			str.AppendLine("        });");

			configScript = str.ToString();

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "Computer", configScript, true);

			str = new StringBuilder();
			str.AppendLine("$(document).ready(function ()");
			str.AppendLine("        {");
			str.AppendLine("$.ajax(");
			str.AppendLine("        {");
			str.AppendLine("type: \"POST\",");
			str.AppendLine("url: \"http://localhost:53331/API/AutoComplete.asmx/GetConnectionKeyList\",");
			//str.AppendLine("data:\"{\'primaryEntity\':\'" + PrimaryEntity + "\',\'txtName\':\'" + name + "\',\'AuditId\':\'" + SessionVariables.RequestProfile.AuditId + "\'}\",");
			str.AppendLine("contentType: \"application/json; charset=utf-8\",");
			str.AppendLine("dataType: \"json\",");
			str.AppendLine("success: function (msg)");
			str.AppendLine("        {");
			str.AppendLine("$(\"#" + txtConnectionKey.ClientID + "\").kendoAutoComplete({");
			str.AppendLine("    dataSource: msg.d,filter: \"startswith\"");
			str.AppendLine("        });");
			str.AppendLine("        }");
			str.AppendLine("        });");
			str.AppendLine("        });");

			configScript = str.ToString();

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ConnectionKey", configScript, true);
			
		}
		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "ElapsedTimeDataDefaultView";
			DetailUserPreferenceCategoryId = PerferenceUtility.CreateUserPreferenceCategoryIfNotExists("ElapsedTimeData", "ElapsedTimeData");

			BreadCrumbObject = Master.BreadCrumbObject;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				txtComputerName.Text = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.Computer);
				txtConnectionKey.Text = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.ConnectionKey);
				txtApplicationUserList.Text = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.LogUser);
				drpGroupBy.SelectedValue = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.GroupBy);

				SetupDropdown();

				DataBind();
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			DataBind();

			PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.Computer, ComputerName);
			PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.ConnectionKey, ConnectionKey);
			PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.LogUser, LogUser);
			PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.GroupBy, drpGroupBy.SelectedValue);
		}
		#endregion

	}
}