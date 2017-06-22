using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.AllEntityDetail
{
	public partial class Delete : PageDelete
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "AllEntityDetail";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("AllEntityDetail", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.AllEntityDetail;
		}


		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new AllEntityDetailDataModel();
					data.AllEntityDetailId = int.Parse(index);
					TaskTimeTracker.Components.Module.ApplicationDevelopment.AllEntityDetailDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.AllEntityDetail, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("AllEntityDetailEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion
	}
}