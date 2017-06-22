using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.FeatureRule
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRule;
			PrimaryEntityKey = "FeatureRule";
			BreadCrumbObject = Master.BreadCrumbObject;
			DetailsControlPath = ApplicationCommon.GetControlPath("FeatureRule", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;			
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new FeatureRuleDataModel();
					data.FeatureRuleId = int.Parse(index);
                    TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FeatureRule, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("FeatureRuleEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion

	}
}