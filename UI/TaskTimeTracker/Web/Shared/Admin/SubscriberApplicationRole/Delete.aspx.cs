using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.SubscriberApplicationRole
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
        #region Events

        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SubscriberApplicationRole;
			PrimaryEntityKey = "SubscriberApplicationRole";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("SubscriberApplicationRole", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
        }

        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
			ShowAuditHistory(chkVisible.Checked);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new SubscriberApplicationRoleDataModel();
                    data.SubscriberApplicationRoleId = int.Parse(index);
                    Framework.Components.Core.SubscriberApplicationRoleDataManager.Delete(data, SessionVariables.RequestProfile);
                }

				DeleteAndRedirect();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

		private void DeleteAndRedirect()
		{
            Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.SubscriberApplicationRole, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("SubscriberApplicationRoleEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion

	}
}