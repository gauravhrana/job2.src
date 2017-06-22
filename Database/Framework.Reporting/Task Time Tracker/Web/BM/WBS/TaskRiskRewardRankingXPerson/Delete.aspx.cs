using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.WBS.TaskRiskRewardRankingXPerson
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "TaskRiskRewardRankingXPerson";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("TaskRiskRewardRankingXPerson", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRiskRewardRankingXPerson;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new TaskRiskRewardRankingXPersonDataManager.Data();
					data.TaskRiskRewardRankingXPersonId = int.Parse(index);
                    TaskRiskRewardRankingXPersonDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.TaskRiskRewardRankingXPerson, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("TaskRiskRewardRankingXPersonEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion	

      }
}