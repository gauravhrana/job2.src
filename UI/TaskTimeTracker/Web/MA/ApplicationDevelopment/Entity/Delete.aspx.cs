using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Entity
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

		#region variables



		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Entity;
			PrimaryEntityKey = "Entity";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("Entity", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.Entity, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("FeatureOwnerStatusEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var notDeletableIds = new List<int>();
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new EntityDataModel();
					data.EntityId = int.Parse(index);
					if (!TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityDataManager.IsDeletable(data, SessionVariables.RequestProfile))
					{
						notDeletableIds.Add(Convert.ToInt32(data.EntityId));
					}
				}
				if (notDeletableIds.Count == 0)
				{
					foreach (string index in deleteIndexList)
					{
						var data = new EntityDataModel();
						data.EntityId = int.Parse(index);
						TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityDataManager.Delete(data, SessionVariables.RequestProfile);
					}
					Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.Entity, SessionVariables.RequestProfile);
					Response.Redirect(Page.GetRouteUrl("EntityRoute", new { Action = "Default", SetId = true }), false);
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
						msg += "EntityId: " + id + " has detail records";
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
			Response.Redirect(Page.GetRouteUrl("EntityRoute", new { Action = "Default" }), false);
		}

		#endregion

	}
}