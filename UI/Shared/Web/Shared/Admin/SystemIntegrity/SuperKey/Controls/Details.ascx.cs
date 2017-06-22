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
			var oDetail = Framework.Components.Core.SuperKeyDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oDetail != null)
            {

                lblSuperKeyId.Text         = oDetail.SuperKeyId.ToString();
                lblName.Text               = oDetail.Name;
                lblDescription.Text        = oDetail.Description;
                lblSortOrder.Text          = oDetail.SortOrder.ToString();
                lblSystemEntityTypeId.Text = oDetail.SystemEntityTypeId.ToString();
                lblExpirationDate.Text     = oDetail.ExpirationDate.ToString();

				oUpdateInfo.LoadText(oDetail);

                oHistoryList.Setup("AuditHistory", "", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.SuperKey, SuperKeyid, "SuperKey");
                dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "SuperKey");
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