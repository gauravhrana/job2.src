using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.LogAndTrace;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Prototype.Mongodb
{
	public partial class UserLoginHistorySearch : Framework.UI.Web.BaseClasses.PageBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			SetupDropdown();

			if (string.IsNullOrEmpty(txtUserName.Text) && string.IsNullOrEmpty(txtServerName.Text))
			{
				var userdata = UserLoginHistoryMongoDbDataManager.GetList();

				gridUserLogin.DataSource = userdata;
				gridUserLogin.DataBind();
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			var data = new Framework.Components.LogAndTrace.UserLoginHistoryDataModel();
			data.ServerName = txtServerName.Text;
			data.UserName = txtUserName.Text;

			var userdata = UserLoginHistoryMongoDbDataManager.GetList(data, SessionVariables.RequestProfile);

			gridUserLogin.DataSource = userdata;
			gridUserLogin.DataBind();
		}

		private void SetupDropdown()
		{

			var configScript = AjaxHelper.GetKendoComboBoxConfigScript("GetAllApplicationUserList", "ApplicationUserFullName", "ApplicationUserFullName",
				txtUserName.ClientID);

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
			str.AppendLine("$(\"#" + txtServerName.ClientID + "\").kendoAutoComplete({");
			str.AppendLine("    dataSource: msg.d,filter: \"startswith\"");
			str.AppendLine("        });");
			str.AppendLine("        }");
			str.AppendLine("        });");
			str.AppendLine("        });");

			configScript = str.ToString();

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "Computer", configScript, true);
			
		}
	}
}