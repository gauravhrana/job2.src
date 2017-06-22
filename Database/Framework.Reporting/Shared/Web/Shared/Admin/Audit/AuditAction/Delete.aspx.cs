using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.Audit.AuditAction
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "AuditAction";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("AuditAction", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.AuditAction;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var data = new DataModel.Framework.Audit.AuditActionDataModel();
				data.AuditActionId = SetId;
				if (Framework.Components.Audit.AuditActionDataManager.IsDeletable(data, SessionVariables.RequestProfile))
				{
					Framework.Components.Audit.AuditActionDataManager.Delete(data, SessionVariables.RequestProfile);
				}
				else
				{
					var msg = String.Empty;
					msg += "AuditId: " + SetId + " has detail records";
					Response.Write(msg);
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
            Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.AuditAction, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("AuditActionEntityRoute", new { Action = "Default" }), false);
		}	

		#endregion 		
				
    }
}