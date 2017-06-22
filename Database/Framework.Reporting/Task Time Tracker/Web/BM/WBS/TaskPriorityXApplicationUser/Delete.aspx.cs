using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.Priority;

namespace ApplicationContainer.UI.Web.WBS.TaskPriorityXApplicationUser
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "TaskPriorityXApplicationUser";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("TaskPriorityXApplicationUser", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskPriorityXApplicationUser;
		}


		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new TaskPriorityXApplicationUserDataManager.Data();
					data.TaskPriorityXApplicationUserId = int.Parse(index);
                    TaskPriorityXApplicationUserDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.TaskPriorityXApplicationUser, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("TaskPriorityXApplicationUserEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion		

    }
}