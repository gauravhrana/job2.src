using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemDevNumbers.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables


        #endregion

        #region private methods

        

        protected override void ShowData(int systemDevNumbersId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new SystemDevNumbersDataModel();
            data.SystemDevNumbersId = systemDevNumbersId;

            var dt = Framework.Components.Core.SystemDevNumbersDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

                lblSystemDevNumbersId.Text      = Convert.ToString(row[SystemDevNumbersDataModel.DataColumns.SystemDevNumbersId]);
                lblPerson.Text                  = Convert.ToString(row[SystemDevNumbersDataModel.DataColumns.Person]);
                lblRangeFrom.Text               = Convert.ToString(row[SystemDevNumbersDataModel.DataColumns.RangeFrom]);
                lblRangeTo.Text                 = Convert.ToString(row[SystemDevNumbersDataModel.DataColumns.RangeTo]);

				oUpdateInfo.LoadText(dt.Rows[0]);

                oHistoryList.Setup("AuditHistory", "Audit", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.SystemDevNumbers, systemDevNumbersId, "SystemDevNumbers");
                dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "SystemDevNumbers");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblSystemDevNumbersId.Text = String.Empty;
            lblPerson.Text = String.Empty;
            lblRangeFrom.Text = String.Empty;
			lblRangeTo.Text = String.Empty;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblSystemDevNumbersIdText.Visible = isTesting;
                lblSystemDevNumbersId.Visible = isTesting;
            }
        }

        

        #endregion

    }
}