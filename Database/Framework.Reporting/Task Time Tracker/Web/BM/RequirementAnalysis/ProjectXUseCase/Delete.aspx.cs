using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.ProjectXUseCase
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "ProjectXUseCase";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("ProjectXUseCase", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectXUseCase;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var deleteIndexList = DeleteIds.Split(',');

				foreach (string index in deleteIndexList)
				{
					var data = new ProjectXUseCaseDataModel();
					data.ProjectXUseCaseId = int.Parse(index);
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.Delete(data, SessionVariables.RequestProfile);
				}

				Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ProjectXUseCase, SessionVariables.RequestProfile);
				Response.Redirect(Page.GetRouteUrl("ProjectXUseCaseEntityRoute", new { Action = "Default", SetId = true }), false);
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		#endregion
	}
}