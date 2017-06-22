using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.RunTimeFeature
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "RunTimeFeature";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("RunTimeFeature", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.RunTimeFeature;

		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new RunTimeFeatureDataModel();
					data.RunTimeFeatureId = int.Parse(index);
                    TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.RunTimeFeature, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("RunTimeFeatureEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion

	}
}