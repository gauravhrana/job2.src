using System;
using System.Collections.Generic;
using System.Web.UI;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.RiskReward;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.RiskAndReward.Demo
{

	public partial class Delete : PageDelete
	{ 

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= SystemEntity.Risk;		
			PrimaryEntityKey	= "Risk";
			BreadCrumbObject	= Master.BreadCrumbObject;

			DetailsControlPath	= ApplicationCommon.GetControlPath("Risk", ControlType.DetailsControl);
			PrimaryPlaceHolder	= plcDetailsList;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var notDeletableIds = new List<int>();
				var deleteIndexList = DeleteIds.Split(',');

				foreach (var index in deleteIndexList)
				{
					var data = new RiskDataModel();
					data.RiskId = int.Parse(index);

					if (!RiskDataManager.IsDeletable(data, SessionVariables.RequestProfile))
					{
						notDeletableIds.Add((int)(data.RiskId));
					}
				}

				if (notDeletableIds.Count == 0)
				{
					foreach (var index in deleteIndexList)
					{
						var data = new RiskDataModel();
						data.RiskId = int.Parse(index);

						RiskDataManager.Delete(data, SessionVariables.RequestProfile);
					}
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
						msg += "RiskId: " + id + " has detail records";
					}

					foreach (string index in deleteIndexList)
					{
						var data = new RiskDataModel();
						data.RiskId = int.Parse(index);

						//RiskDataManager.DeleteChildren(data, SessionVariables.RequestProfile);
						RiskDataManager.Delete(data, SessionVariables.RequestProfile);
					}
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
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.Risk, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("RiskEntityRoute", new {Action = "Default", SetId = true}), false);
		}  

	}
}
