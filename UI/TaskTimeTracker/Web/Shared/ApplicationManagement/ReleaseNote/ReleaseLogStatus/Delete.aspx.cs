using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLogStatus
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
		
		#region Events	

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLogStatus;
			PrimaryEntityKey = "ReleaseLogStatus";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("ReleaseLogStatus", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		} 
		
		protected void btnDelete_Click(object sender, EventArgs e)
		{
			string[] deleteIndexList = DeleteIds.Split(',');
			foreach (string index in deleteIndexList)
			{
				var data = new ReleaseLogStatusDataModel();
				data.ReleaseLogStatusId = int.Parse(index);
				Framework.Components.ReleaseLog.ReleaseLogStatusDataManager.Delete(data, SessionVariables.RequestProfile);
			}

			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ReleaseLogStatus, SessionVariables.RequestProfile);
			var userMode = String.Empty;
			if (!string.IsNullOrEmpty(Request.QueryString["user"]))
			{
				userMode = "&user=" + Request.QueryString["user"];
			}
			//Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ReleaseLogStatus", Action = "Default", SetId = true }), false);
			DeleteAndRedirect();
			
		}

		

		#endregion

		#region methods

		private void DeleteAndRedirect()
		{
			Response.Redirect(Page.GetRouteUrl("ReleaseLogStatusEntityRoute", new { Action = "Default", SetId = true }), false);
		}



		#endregion
	}
}