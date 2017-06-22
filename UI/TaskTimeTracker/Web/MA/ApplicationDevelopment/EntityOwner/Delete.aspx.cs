using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityOwner
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

        #region variables

        

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.EntityOwner;
            PrimaryEntityKey = "EntityOwner";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("EntityOwner", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        private void DeleteAndRedirect()
        {
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.EntityOwner, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("EntityOwnerEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {                
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new EntityOwnerDataModel();
                    data.EntityOwnerId = int.Parse(index);
					TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityOwnerDataManager.Delete(data, SessionVariables.RequestProfile);
                }
				Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.EntityOwner, SessionVariables.RequestProfile);
                Response.Redirect(Page.GetRouteUrl("EntityOwnerEntityRoute", new { Action = "Default", SetId = true }), false);                
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        override protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl("EntityOwnerEntityRoute", new { Action = "Default" }), false);
        }

        #endregion

	}
}