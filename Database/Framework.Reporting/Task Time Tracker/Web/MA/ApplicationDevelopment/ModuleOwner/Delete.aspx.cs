using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.ModuleOwner
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ModuleOwner;
            PrimaryEntityKey = "ModuleOwner";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("ModuleOwner", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        private void DeleteAndRedirect()
        {
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ModuleOwner, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("ModuleOwnerEntityRoute", new { Action = "Default", SetId = true }), false);
        }
         
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {                
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new ModuleOwnerDataModel();
                    data.ModuleOwnerId = int.Parse(index);
					TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleOwnerDataManager.Delete(data, SessionVariables.RequestProfile);
                }
				Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ModuleOwner, SessionVariables.RequestProfile);
                Response.Redirect(Page.GetRouteUrl("ModuleOwnerEntityRoute", new { Action = "Default", SetId = true }), false);                
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        override protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl("ModuleOwnerEntityRoute", new { Action = "Default" }), false);
        }

        #endregion

	}
}