using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.StoredProcedureLog.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region variables

		//public string Border
		//{
		//	set
		//	{
		//		borderdiv.Style.Add("border", "2px");
		//		borderdiv.Style.Add("border-color", "Blue");
		//		borderdiv.Style.Add("border-width", "2px");
		//		borderdiv.Style.Add("border-style", "groove");
		//	}

		//}
		#endregion

		#region private methods

		protected override void ShowData(int StoredProcedureLogId)
		{
			var data = new Framework.Components.LogAndTrace.StoredProcedureLogDataModel();
			data.StoredProcedureLogId = StoredProcedureLogId;

			var dt = Framework.Components.LogAndTrace.StoredProcedureLogDataManager.GetStoredProcedureLogDetails(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count == 1)
			{
				var row = dt.Rows[0];

				lblStoredProcedureLogId.Text = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogDataModel.DataColumns.StoredProcedureLogId]);
                lblName.Text                 = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogDataModel.DataColumns.Name]);
                lblTimeOfExecution.Text      = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogDataModel.DataColumns.TimeOfExecution]);
                lblExecutedBy.Text           = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogDataModel.DataColumns.ExecutedBy]);
				
				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.StoredProcedureLog, StoredProcedureLogId, "StoredProcedureLog");
				dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "StoredProcedureLog");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblStoredProcedureLogId.Text = String.Empty;
			lblName.Text = String.Empty;
			lblTimeOfExecution.Text = String.Empty;
			lblExecutedBy.Text = String.Empty;
			
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblStoredProcedureLogIdText.Visible = isTesting;
				lblStoredProcedureLogId.Visible = isTesting;
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
            var isTesting = SessionVariables.IsTesting;

			if (isTesting == true)
			{
				EnableControl(true, dynStoredProcedureLogId.Controls);
			}
			else
			{
				EnableControl(false, dynStoredProcedureLogId.Controls);
			}
		}

		#endregion
	}
}