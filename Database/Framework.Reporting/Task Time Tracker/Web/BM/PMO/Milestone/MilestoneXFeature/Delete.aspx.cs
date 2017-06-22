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

namespace ApplicationContainer.UI.Web.MilestoneXFeature
{
	public partial class Delete : PageDelete
    {
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.MilestoneXFeature;
			PrimaryEntityKey = "MilestoneXFeature";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("MilestoneXFeature", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new MilestoneXFeatureDataModel();
					data.MilestoneXFeatureId = int.Parse(index);
                    MilestoneXFeatureDataManager.Delete(data, SessionVariables.RequestProfile);
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
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.MilestoneXFeature, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("MilestoneXFeatureEntityRoute", new { Action = "Default", SetId = true }), false);
		}	
	
		#endregion

    }
}