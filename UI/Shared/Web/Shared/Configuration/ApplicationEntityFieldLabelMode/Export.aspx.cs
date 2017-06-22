using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode
{
	public partial class Export : Shared.UI.WebFramework.BasePage
	{
		string SearchCondition = string.Empty;

		private System.Data.DataTable GetData()
		{
			var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Data();
			var dt = Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Search(data, AuditId);

			return dt;
		}

		private string[] GetColumns()
		{
			return Framework.Components.ApplicationSecurity.GetApplicationEntityFieldLabelColumns("DBColumns", AuditId);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				// see what parameter was passed 
				SearchCondition = Request.QueryString["SearchCondition"];
				oList.Setup("ApplicationEntityFieldLabelMode", " ", "ApplicationEntityFieldLabelModeId", false, new List.GetDataDelegate(GetData), new List.GetColumnDelegate(GetColumns), false);
				oList.ExportMenu.Visible = false;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// After all the control have loaded, we can set values
		/// try to follow stratedy in general avoid doing alot of work in OnLoad
		/// OnLoad is quick operation ...
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);

			oList.ShowData(true, true);
		}

	}
}