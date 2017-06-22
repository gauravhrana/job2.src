using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.Task;

namespace ApplicationContainer.UI.Web.TaskPackageXOwnerXTask
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
		#region Events		

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "TaskPackageXOwnerXTask";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("TaskPackageXOwnerXTask", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPackageXOwnerXTask;

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
						var data = new TaskPackageXOwnerXTaskDataModel();
						data.TaskPackageXOwnerXTaskId = int.Parse(index);
                        TaskTimeTracker.Components.BusinessLayer.Task.TaskPackageXOwnerXTaskDataManager.Delete(data, SessionVariables.RequestProfile);
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
						msg += "TaskPackageXOwnerXTaskId: " + id + " has detail records";
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.TaskPackageXOwnerXTask, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("TaskPackageXOwnerXTaskEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion       

    }
}