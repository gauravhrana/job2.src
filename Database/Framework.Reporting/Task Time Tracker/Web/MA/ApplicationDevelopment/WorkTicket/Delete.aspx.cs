using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.WorkTicket
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

		#region variables



		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.WorkTicket;
			PrimaryEntityKey = "WorkTicket";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("WorkTicket", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.WorkTicket, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("WorkTicketEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var notDeletableIds = new List<int>();
				string[] deleteIndexList = DeleteIds.Split(',');

				if (notDeletableIds.Count == 0)
				{
					foreach (string index in deleteIndexList)
					{
						var data = new WorkTicketDataModel();
						data.WorkTicketId = int.Parse(index);
						TaskTimeTracker.Components.Module.ApplicationDevelopment.WorkTicketDataManager.Delete(data, SessionVariables.RequestProfile);
					}
					Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.WorkTicket, SessionVariables.RequestProfile);
					Response.Redirect(Page.GetRouteUrl("WorkTicketEntityRoute", new { Action = "Default", SetId = true }), false);
				}
				else
				{
					var msg = String.Empty;
					foreach (var id in notDeletableIds)
					{
						if (!string.IsNullOrEmpty(msg))
						{
							msg += ", <br/>";
						}
						msg += "WorkTicketId: " + id + " has detail records";
					}
					Response.Write(msg);
				}
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		override protected void btnBack_Click(object sender, EventArgs e)
		{
			Response.Redirect(Page.GetRouteUrl("WorkTicketEntityRoute", new { Action = "Default" }), false);
		}

		#endregion

	}
}