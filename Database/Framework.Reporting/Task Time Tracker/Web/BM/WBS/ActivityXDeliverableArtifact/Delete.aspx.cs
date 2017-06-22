using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ActivityXDeliverableArtifact
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
				base.OnInit(e);
				PrimaryEntityKey = "ActivityXDeliverableArtifact";
				BreadCrumbObject = Master.BreadCrumbObject;

				var detailscontrolpath = ApplicationCommon.GetControlPath("ActivityXDeliverableArtifact", ControlType.DetailsControl);
				DetailsControlPath = detailscontrolpath;
				PrimaryPlaceHolder = plcDetailsList;
				PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ActivityXDeliverableArtifact;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
		
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new ActivityXDeliverableArtifactDataModel();
                    data.ActivityXDeliverableArtifactId = int.Parse(index);
                    ActivityXDeliverableArtifactDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ActivityXDeliverableArtifact, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ActivityXDeliverableArtifactEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion

        #region Methods
        
        #endregion

    }
}