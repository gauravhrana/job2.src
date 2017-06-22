using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SuperKey.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables


        #endregion

        #region private methods

        

        protected override void ShowData(int SuperKeyid)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new SuperKeyDataModel();
            data.SuperKeyId = SuperKeyid;
			var dt = Framework.Components.Core.SuperKeyDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

                lblSuperKeyId.Text         = Convert.ToString(row[SuperKeyDataModel.DataColumns.SuperKeyId]);
                lblName.Text               = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
                lblDescription.Text        = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
                lblSortOrder.Text          = Convert.ToString(row[StandardDataModel.StandardDataColumns.SortOrder]);
                lblSystemEntityTypeId.Text = Convert.ToString(row[SuperKeyDataModel.DataColumns.SystemEntityTypeId]);
                lblExpirationDate.Text     = Convert.ToString(row[SuperKeyDataModel.DataColumns.ExpirationDate]);

				oUpdateInfo.LoadText(dt.Rows[0]);

                oHistoryList.Setup("AuditHistory", "", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.SuperKey, SuperKeyid, "SuperKey");
                dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "SuperKey");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblSuperKeyId.Text = String.Empty;
            lblName.Text = String.Empty;
            lblDescription.Text = String.Empty;
            lblSortOrder.Text= String.Empty;
            lblSystemEntityTypeId.Text = String.Empty;
            lblExpirationDate.Text = String.Empty;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblSuperKeyId.Visible = false;
                lblTextSuperKeyId.Visible = false;
            }
        }

        

        #endregion

    }
}