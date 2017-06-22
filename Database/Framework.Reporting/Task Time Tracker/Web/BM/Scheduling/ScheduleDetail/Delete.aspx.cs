using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleDetail
{
	public partial class Delete : PageDelete
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "ScheduleDetail";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("ScheduleDetail", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = SystemEntity.ScheduleDetail;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new ScheduleDetailDataModel();
					data.ScheduleDetailId = int.Parse(index);
                    ScheduleDetailDataManager.Delete(data, SessionVariables.RequestProfile);
				}

				DeleteAndRedirect();
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}        

		#endregion

		#region Methods

		private void DeleteAndRedirect()
		{
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.ScheduleDetail, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ScheduleDetailEntityRoute", new { Action = "Default", SetId = true }), false);
		}	

		#endregion
	}
}