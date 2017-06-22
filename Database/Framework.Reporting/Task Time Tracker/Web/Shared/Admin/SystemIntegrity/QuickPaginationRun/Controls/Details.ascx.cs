using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.QuickPaginationRun.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables


        #endregion

        #region private methods

        

        protected override void ShowData(int QuickPaginationRunid)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new QuickPaginationRunDataModel();
            data.QuickPaginationRunId = QuickPaginationRunid;
			var dt = Framework.Components.Core.QuickPaginationRunDatatManager.GetDetails(data, SessionVariables.RequestProfile);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

                lblQuickPaginationRunId.Text         = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.QuickPaginationRunId]);
                lblSortClause.Text                   = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.SortClause]);
                lblWhereClause.Text                  = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.WhereClause]);
                lblApplicationUserId.Text            = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.ApplicationUserId]);
                lblSystemEntityTypeId.Text           = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.SystemEntityTypeId]);
                lblExpirationTime.Text               = Convert.ToString(row[QuickPaginationRunDataModel.DataColumns.ExpirationTime]);

				oUpdateInfo.LoadText(dt.Rows[0]);

                oHistoryList.Setup("AuditHistory", "", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.QuickPaginationRun, QuickPaginationRunid, "QuickPaginationRun");
                dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "QuickPaginationRun");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblQuickPaginationRunId.Text = String.Empty;
            lblSortClause.Text           = String.Empty;
            lblWhereClause.Text          = String.Empty;
            lblApplicationUserId.Text    = String.Empty;
            lblSystemEntityTypeId.Text   = String.Empty;
            lblExpirationTime.Text       = String.Empty;
        }

        #endregion

        #region Events


		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.QuickPaginationRun;

			PlaceHolderCore = dynQuickPaginationRunId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv = borderdiv;

			
		}

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblQuickPaginationRunId.Visible = false;
                lblTextQuickPaginationRunId.Visible = false;
            }
        }

        

        #endregion

    }
}