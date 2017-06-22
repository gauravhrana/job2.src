using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.ReleasePublishCategory
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleasePublishCategory;
			PrimaryEntityKey = "ReleasePublishCategory";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("ReleasePublishCategory", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
			ShowAuditHistory(chkVisible.Checked);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string[] deleteIndexList = DeleteIds.Split(',');
            foreach (string index in deleteIndexList)
            {
                var data = new ReleasePublishCategoryDataModel();
                data.ReleasePublishCategoryId = int.Parse(index);
				Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.Delete(data, SessionVariables.AuditId);
            }

			DeleteAndRedirect();
        }

		private void DeleteAndRedirect()
		{
            Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ReleasePublishCategory, AuditId);
			Response.Redirect(Page.GetRouteUrl("ReleasePublishCategoryEntityRoute", new { Action = "Default", SetId = true }), false);
		}
        
        #endregion
    }
}