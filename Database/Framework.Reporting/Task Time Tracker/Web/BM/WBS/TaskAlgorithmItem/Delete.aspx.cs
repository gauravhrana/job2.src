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

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithmItem
{
	public partial class Delete : PageDelete
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.TaskAlgorithmItem;
			PrimaryEntityKey = "TaskAlgorithmItem";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("TaskAlgorithmItem", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var notDeletableIds = new List<int>();
				var deleteIndexList = DeleteIds.Split(',');

				foreach (var index in deleteIndexList)
				{
					var data = new TaskAlgorithmItemDataModel();
					data.TaskAlgorithmItemId = int.Parse(index);

                    if (!TaskAlgorithmItemDataManager.IsDeletable(data, SessionVariables.RequestProfile))
					{
						notDeletableIds.Add((int)(data.TaskAlgorithmItemId));
					}
				}

				if (notDeletableIds.Count == 0)
				{
					foreach (var index in deleteIndexList)
					{
						var data = new TaskAlgorithmItemDataModel();
						data.TaskAlgorithmItemId = int.Parse(index);

						TaskAlgorithmItemDataManager.Delete(data, SessionVariables.RequestProfile);
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
						msg += "TaskAlgorithmItemId: " + id + " has detail records";
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
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.TaskAlgorithmItem, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("TaskAlgorithmItemEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion

	}
}