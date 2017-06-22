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

namespace ApplicationContainer.UI.Web.Milestone
{
	public partial class Delete : PageDelete
    {
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "Milestone";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("Milestone", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = SystemEntity.Milestone;

		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var notDeletableIds = new List<int>();
				string[] deleteIndexList = DeleteIds.Split(',');
				//foreach (string index in deleteIndexList)
				//{
				//    var data = new Framework.Components.EventMonitoring.Milestone.Data();
				//    data.MilestoneId = int.Parse(index);
				//    if (!Framework.Components.EventMonitoring.Milestone.IsDeletable(data, SessionVariables.RequestProfile.AuditId))
				//    {
				//        notDeletableIds.Add(Convert.ToInt32(data.MilestoneId));
				//    }
				//}
				if (notDeletableIds.Count == 0)
				{
					foreach (string index in deleteIndexList)
					{
						var data = new MilestoneDataModel();
						data.MilestoneId = int.Parse(index);
                        MilestoneDataManager.Delete(data, SessionVariables.RequestProfile);
					}

					DeleteAndRedirect();
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
						msg += "MilestoneId: " + id + " has detail records";
					}
					Response.Write(msg);
				}
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		private void DeleteAndRedirect()
		{
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.Milestone, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("MilestoneEntityRoute", new { Action = "Default", SetId = true }), false);
		}
	
		#endregion
    }
}