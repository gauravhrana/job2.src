using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.Task;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.WBS.TaskFormulation
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "TaskFormulation";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("TaskFormulation", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskFormulation;
		}


		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new TaskFormulationDataModel();
					data.TaskFormulationId = int.Parse(index);
                    TaskTimeTracker.Components.BusinessLayer.Task.TaskFormulationDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.TaskFormulation, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("TaskFormulationEntityRoute", new { Action = "Default", SetId = true }), false);
		}
        
		#endregion
        
    }
}