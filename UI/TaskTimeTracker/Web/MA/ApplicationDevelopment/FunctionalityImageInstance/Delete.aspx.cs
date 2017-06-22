using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImageInstance
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PrimaryEntityKey = "FunctionalityImageInstance";
            BreadCrumbObject = Master.BreadCrumbObject;

            var detailscontrolpath = ApplicationCommon.GetControlPath("FunctionalityImageInstance", ControlType.DetailsControl);
            DetailsControlPath = detailscontrolpath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImageInstance;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');

                foreach (string index in deleteIndexList)
                {
                    var data = new FunctionalityImageInstanceDataModel();
                    data.FunctionalityImageInstanceId = int.Parse(index);
					TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageInstanceDataManager.Delete(data, SessionVariables.RequestProfile);
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
            Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FunctionalityImageInstance, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("FunctionalityImageInstanceEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion
    }
}