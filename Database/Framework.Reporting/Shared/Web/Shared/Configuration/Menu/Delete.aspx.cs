using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.Menu
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.Menu;
            PrimaryEntityKey   = "Menu";
            BreadCrumbObject   = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("Menu", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new MenuDataModel();
                    data.MenuId = int.Parse(index);
                    Framework.Components.Core.MenuDataManager.Delete(data, SessionVariables.RequestProfile);
                }

				Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.Menu, SessionVariables.RequestProfile);
                
				// refresh
				SessionVariables.SiteMenuData = Framework.Components.Core.MenuDataManager.GetList(SessionVariables.RequestProfile);
                
                DeleteAndRedirect();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void DeleteAndRedirect()
        {
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.Menu, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("MenuEntityRoute", new { Action = "Default", SetId = true }), false);
        }

       #endregion

    }
}