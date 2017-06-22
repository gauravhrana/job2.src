using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.DeliverableArtifactStatus
{
    public partial class Delete : PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PrimaryEntityKey = "DeliverableArtifactStatus";
            BreadCrumbObject = Master.BreadCrumbObject;

            var detailscontrolpath = ApplicationCommon.GetControlPath("DeliverableArtifactStatus", ControlType.DetailsControl);
            DetailsControlPath = detailscontrolpath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity = SystemEntity.DeliverableArtifactStatus;
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new DeliverableArtifactStatusDataModel();
                    data.DeliverableArtifactStatusId = int.Parse(index);
                    DeliverableArtifactStatusDataManager.Delete(data, SessionVariables.RequestProfile);
                    DeleteAndRedirect();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void DeleteAndRedirect()
        {
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.DeliverableArtifactStatus, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("DeliverableArtifactStatusEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion

    }
}