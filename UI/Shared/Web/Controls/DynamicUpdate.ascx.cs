using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{
	public partial class DynamicUpdate : System.Web.UI.UserControl
	{
		public Repeater DynamicUpdateRepeater
		{
			get { return repUpdate; }
		}

		private DataTable columns;
		public DataTable Columns
		{
			get { return columns; }
			set { columns = value; }
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();
				var modedt = Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.GetList(SessionVariables.AuditId);
				DataRow[] rows = modedt.Select("Name = 'CommonEditable'");
				if(rows.Length == 1)
				{
					data.ApplicationEntityFieldLabelModeId =
						int.Parse(
							rows[0][
								Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.DataColumns.
									ApplicationEntityFieldLabelModeId].ToString());
				}
				data.SystemEntityTypeId = 3000;
				Columns = Framework.Components.UserPreference.ApplicationEntityFieldLabel.Search(data,
				                                                                                       SessionVariables.AuditId);
				repUpdate.DataSource = Columns;
				repUpdate.DataBind();
			}
		}
	}
}