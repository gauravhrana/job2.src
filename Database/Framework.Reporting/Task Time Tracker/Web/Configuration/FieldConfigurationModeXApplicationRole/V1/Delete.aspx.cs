using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeXApplicationRole.V1
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);
			PrimaryEntityKey       = "FieldConfigurationModeXApplicationRole";
			BreadCrumbObject       = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("FieldConfigurationModeXApplicationRole", ControlType.DetailsControl);
			DetailsControlPath     = detailscontrolpath;
			PrimaryPlaceHolder     = plcDetailsList;
			PrimaryEntity          = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new FieldConfigurationModeXApplicationRoleDataModel();
                    data.FieldConfigurationModeXApplicationRoleId = int.Parse(index);
					FieldConfigurationModeXApplicationRoleDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("FieldConfigurationModeXApplicationRoleEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion        

    }
}