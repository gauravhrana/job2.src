using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.StoredProcedureLogDetail.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region private methods

		protected override void ShowData(int StoredProcedureLogDetailId)
		{
			var data = new Framework.Components.LogAndTrace.StoredProcedureLogDetailDataModel();
			data.StoredProcedureLogDetailId = StoredProcedureLogDetailId;

			var dt = Framework.Components.LogAndTrace.StoredProcedureLogDetailDataManager.GetStoredProcedureLogDetails(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count == 1)
			{
				var row = dt.Rows[0];

                lblStoredProcedureLogDetailId.Text = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogDetailId]);
                lblStoredProcedureLogId.Text       = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogDetailDataModel.DataColumns.StoredProcedureLogId]);
                lblParameterName.Text              = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogDetailDataModel.DataColumns.ParameterName]);
                lblParameterValue.Text             = Convert.ToString(row[Framework.Components.LogAndTrace.StoredProcedureLogDetailDataModel.DataColumns.ParameterValue]);

				//oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.StoredProcedureLogDetail, StoredProcedureLogDetailId, "StoredProcedureLogDetail");
				dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "StoredProcedureLogDetail");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblStoredProcedureLogDetailId.Text = String.Empty;
			lblStoredProcedureLogId.Text = String.Empty;
			lblParameterName.Text = String.Empty;
			lblParameterValue.Text = String.Empty;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                var isTesting = SessionVariables.IsTesting;
				lblStoredProcedureLogDetailIdText.Visible = isTesting;
				lblStoredProcedureLogDetailId.Visible = isTesting;
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
            var isTesting = SessionVariables.IsTesting;

			if (isTesting == true)
			{
				EnableControl(true, dynStoredProcedureLogDetailId.Controls);
			}
			else
			{
				EnableControl(false, dynStoredProcedureLogDetailId.Controls);
			}
		}

		#endregion
	}
}