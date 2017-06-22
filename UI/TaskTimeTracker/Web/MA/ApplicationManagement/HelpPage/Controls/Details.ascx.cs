using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.HelpPage.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables


        #endregion

        #region private methods

        

        protected override void ShowData(int HelpPageid)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new HelpPageDataModel();
            data.HelpPageId = HelpPageid;
			var oDetail = Framework.Components.Core.HelpPageDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oDetail != null)
            {

                lblHelpPageId.Text         = oDetail.HelpPageId.ToString();
                lblName.Text               = oDetail.Name;
                lblContent.Text            = oDetail.Content;
                lblSortOrder.Text          = oDetail.SortOrder.ToString();
                lblSystemEntityTypeId.Text = oDetail.SystemEntityType;
                lblHelpPageContextId.Text  = oDetail.HelpPageContext;

                oUpdateInfo.LoadText(oDetail);

                oHistoryList.Setup("AuditHistory", "", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.HelpPage, HelpPageid, "HelpPage");
                dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "HelpPage");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblHelpPageId.Text         = String.Empty;
            lblName.Text               = String.Empty;
            lblContent.Text            = String.Empty;
            lblSortOrder.Text          = String.Empty;
            lblSystemEntityTypeId.Text = String.Empty;
            lblHelpPageContextId.Text  = String.Empty;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblHelpPageId.Visible = false;
                lblTextHelpPageId.Visible = false;
            }
        }

        

        #endregion

    }
}
