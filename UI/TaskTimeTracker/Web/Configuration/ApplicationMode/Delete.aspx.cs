using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ApplicationMode
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMode;
			PrimaryEntityKey = "ApplicationMode";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("ApplicationMode", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string[] deleteIndexList = DeleteIds.Split(',');
            foreach (string index in deleteIndexList)
            {
                var data = new ApplicationModeDataModel();
                data.ApplicationModeId = int.Parse(index);
				Framework.Components.UserPreference.ApplicationModeDataManager.Delete(data, SessionVariables.RequestProfile);
            }

			DeleteAndRedirect();
        }

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ApplicationMode, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ApplicationModeEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion
    }
}