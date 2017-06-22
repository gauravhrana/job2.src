using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationRegistrar
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "NotificationRegistrar";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("NotificationRegistrar", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationRegistrar;
		}


		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new NotificationRegistrarDataModel();
                    data.NotificationRegistrarId = int.Parse(index);
					Framework.Components.EventMonitoring.NotificationRegistrarDataManager.Delete(data, SessionVariables.RequestProfile);
                
					DeleteAndRedirect();
				}			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.NotificationRegistrar, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("NotificationRegistrarEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion       

    }
}