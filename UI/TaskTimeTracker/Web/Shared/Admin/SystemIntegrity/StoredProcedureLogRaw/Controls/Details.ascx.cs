using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.StoredProcedureLogRaw.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int StoredProcedureLogRawId)
		{
			var data = new Framework.Components.LogAndTrace.StoredProcedureLogRawDataModel();
			data.StoredProcedureLogRawId = StoredProcedureLogRawId;

			var dt = Framework.Components.LogAndTrace.StoredProcedureLogRawDataManager.GetStoredProcedureLogRaws(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count == 1)
			{
				var row = dt.Rows[0];

                lblStoredProcedureLogRawId.Text = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogRawDataModel.DataColumns.StoredProcedureLogRawId]);
                lblStoredProcedureLogId.Text    = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogRawDataModel.DataColumns.StoredProcedureLogId]);
                lblInputParameters.Text         = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogRawDataModel.DataColumns.InputParameters]);
                lblInputValues.Text             = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogRawDataModel.DataColumns.InputValues]);

				//oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.StoredProcedureLogRaw, StoredProcedureLogRawId, "StoredProcedureLogRaw");
				dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "StoredProcedureLogRaw");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblStoredProcedureLogRawId.Text = String.Empty;
			lblStoredProcedureLogId.Text = String.Empty;
			lblInputParameters.Text = String.Empty;
			lblInputValues.Text = String.Empty;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                var isTesting = SessionVariables.IsTesting;
				lblStoredProcedureLogRawIdText.Visible = isTesting;
				lblStoredProcedureLogRawId.Visible = isTesting;
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
            var isTesting = SessionVariables.IsTesting;

			if (isTesting == true)
			{
				EnableControl(true, dynStoredProcedureLogRawId.Controls);
			}
			else
			{
				EnableControl(false, dynStoredProcedureLogRawId.Controls);
			}
		}

		#endregion
	}
}