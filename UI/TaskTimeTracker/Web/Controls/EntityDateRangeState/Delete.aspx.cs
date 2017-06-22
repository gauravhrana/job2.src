using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.EntityDateRangeState
{
	public partial class Delete : PageDelete
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			try
			{
				base.OnInit(e);
				PrimaryEntityKey = "EntityDateRangeState";
				BreadCrumbObject = Master.BreadCrumbObject;

				var detailscontrolpath = ApplicationCommon.GetControlPath("EntityDateRangeState", ControlType.DetailsControl);
				DetailsControlPath = detailscontrolpath;
				PrimaryPlaceHolder = plcDetailsList;
				PrimaryEntity = SystemEntity.EntityDateRangeState;
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
					var data = new EntityDateRangeStateDataModel();
					data.EntityDateRangeStateId = int.Parse(index);
                    EntityDateRangeStateDataManager.Delete(data, SessionVariables.RequestProfile);
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
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.EntityDateRangeState, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("EntityDateRangeStateEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion
	}
}