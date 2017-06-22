using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ApplicationManagement.HelpPage
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {       

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.HelpPage;
			PrimaryEntityKey = "HelpPage";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("HelpPage", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {                
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new HelpPageDataModel();
                    data.HelpPageId = int.Parse(index);

                	Framework.Components.Core.HelpPageDataManager.Delete(data, SessionVariables.RequestProfile);
                }
					Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.HelpPage, SessionVariables.RequestProfile);
                    Response.Redirect(Page.GetRouteUrl("HelpPageEntityRoute", new { Action = "Default", SetId = true }), false);
            }            

            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
   
        #endregion

    }
}