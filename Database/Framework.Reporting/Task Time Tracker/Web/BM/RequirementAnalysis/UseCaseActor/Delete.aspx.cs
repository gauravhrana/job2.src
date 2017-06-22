using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.UseCaseActor
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseActor;
			PrimaryEntityKey = "UseCaseActor";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("UseCaseActor", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new UseCaseActorDataModel();
					data.UseCaseActorId = int.Parse(index);
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.UseCaseActor, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("UseCaseActorEntityRoute", new { Action = "Default", SetId = true }), false);
		}
        
		#endregion

	}
}