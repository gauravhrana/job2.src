using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ClientXProject.Controls
{
	public partial class Details : ControlDetails
	{

		#region properties

		#endregion

		#region private methods

		protected override void ShowData(int ClientXProjectId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new ClientXProjectDataModel();
			data.ClientXProjectId = ClientXProjectId;

            var dt = ClientXProjectDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count == 1)
            {
				var row = dt.Rows[0];

				lblClientXProjectId.Text = Convert.ToString(row[ClientXProjectDataModel.DataColumns.ClientXProjectId]);
				lblClient.Text = Convert.ToString(row[ClientXProjectDataModel.DataColumns.Client]);
				lblProject.Text = Convert.ToString(row[ClientXProjectDataModel.DataColumns.Project]);

				oUpdateInfo.LoadText(dt.Rows[0]);

				oHistoryList.Setup((int)SystemEntity.ClientXProject, ClientXProjectId, "ClientXProject");
				dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ClientXProject");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblClientXProjectId.Text = String.Empty;
			lblProject.Text = String.Empty;
			lblClient.Text = String.Empty;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblClientXProjectIdText.Visible = isTesting;
				lblClientXProjectId.Visible = isTesting;
			}
		}

		

		#endregion

	}
}