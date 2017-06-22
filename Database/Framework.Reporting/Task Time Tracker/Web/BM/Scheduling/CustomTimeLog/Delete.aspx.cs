using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.CustomTimeLog
{
	public partial class Delete : PageDelete
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "CustomTimeLog";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("CustomTimeLog", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = SystemEntity.CustomTimeLog;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var notDeletableIds = new List<int>();
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new CustomTimeLogDataModel();
					data.CustomTimeLogId = int.Parse(index);

					foreach (string index1 in deleteIndexList)
					{
						var data1 = new CustomTimeLogDataModel();
						data1.CustomTimeLogId = int.Parse(index1);
						CustomTimeLogDataManager.Delete(data1, SessionVariables.RequestProfile);
					}
					DeleteAndRedirect();
				}
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
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.CustomTimeLog, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("CustomTimeLogEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion

	}
}