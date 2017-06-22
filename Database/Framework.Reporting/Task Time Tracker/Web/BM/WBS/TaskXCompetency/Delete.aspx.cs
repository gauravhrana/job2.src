using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Competency;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.WBS.TaskXCompetency
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {        

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "TaskXCompetency";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("TaskXCompetency", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskXCompetency;
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new TaskXCompetencyDataModel();
                    data.TaskXCompetencyId = int.Parse(index);
                    TaskTimeTracker.Components.Module.Competency.TaskXCompetencyDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.TaskXCompetency, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("TaskXCompetencyEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion       
    }
}