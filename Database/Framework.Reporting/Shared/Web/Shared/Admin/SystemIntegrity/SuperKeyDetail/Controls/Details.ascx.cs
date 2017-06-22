using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SuperKeyDetail.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables


        #endregion

        #region private methods

        

        protected override void ShowData(int SuperKeyDetailid)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new SuperKeyDetailDataModel();
            data.SuperKeyDetailId = SuperKeyDetailid;
			var dt = Framework.Components.Core.SuperKeyDetailDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

                lblSuperKeyDetailId.Text         = Convert.ToString(row[SuperKeyDetailDataModel.DataColumns.SuperKeyDetailId]);
                lblSuperKeyId.Text               = Convert.ToString(row[SuperKeyDetailDataModel.DataColumns.SuperKey]);
                lblEntityKey.Text                = Convert.ToString(row[SuperKeyDetailDataModel.DataColumns.EntityKey]);

				oUpdateInfo.LoadText(dt.Rows[0]);

                oHistoryList.Setup("AuditHistory", "", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.SuperKeyDetail, SuperKeyDetailid, "SuperKeyDetail");
                dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "SuperKeyDetail");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblSuperKeyDetailId.Text   = String.Empty;
            lblSuperKeyId.Text         = String.Empty;
            lblEntityKey.Text          = String.Empty;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblSuperKeyDetailId.Visible = false;
                lblTextSuperKeyDetailId.Visible = false;
            }
        }

        

        #endregion

    }
}