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

namespace ApplicationContainer.UI.Web.TaskXDeliverableArtifact
{
    public partial class Delete : PageDelete
    {     

        #region Events

		protected override void OnInit(EventArgs e)
		{
			try
			{
				base.OnInit(e);
				PrimaryEntityKey = "TaskXDeliverableArtifact";
				BreadCrumbObject = Master.BreadCrumbObject;

				var detailscontrolpath = ApplicationCommon.GetControlPath("TaskXDeliverableArtifact", ControlType.DetailsControl);
				DetailsControlPath = detailscontrolpath;
				PrimaryPlaceHolder = plcDetailsList;
				PrimaryEntity = SystemEntity.TaskXDeliverableArtifact;
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
                    var data = new TaskXDeliverableArtifactDataModel();
                    data.TaskXDeliverableArtifactId = int.Parse(index);
                    TaskXDeliverableArtifactDataManager.Delete(data, SessionVariables.RequestProfile);
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
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.TaskXDeliverableArtifact, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("TaskXDeliverableArtifactEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion
      
    }
}