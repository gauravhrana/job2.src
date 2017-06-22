using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationMonitoredEventEmail
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
		#region Events

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new ApplicationMonitoredEventEmailDataModel();
					data.ApplicationMonitoredEventEmailId = int.Parse(index);
					Framework.Components.EventMonitoring.ApplicationMonitoredEventEmailDataManager.Delete(data, SessionVariables.RequestProfile);
				}

				DeleteAndRedirect();
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

	
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEventEmail;
			PrimaryEntityKey = "ApplicationMonitoredEventEmail";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("ApplicationMonitoredEventEmail", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}


		#endregion

		#region methods

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEventEmail, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ApplicationMonitoredEventEmailEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion


	}
}
