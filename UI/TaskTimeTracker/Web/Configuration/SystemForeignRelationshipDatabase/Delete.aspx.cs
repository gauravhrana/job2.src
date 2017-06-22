using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipDatabase
{
	public partial class Delete : PageDelete
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationshipDatabase;
			PrimaryEntityKey = "SystemForeignRelationshipDatabase";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("SystemForeignRelationshipDatabase", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			string[] deleteIndexList = DeleteIds.Split(',');
			foreach (string index in deleteIndexList)
			{
				var data = new SystemForeignRelationshipDatabaseDataModel();
				data.SystemForeignRelationshipDatabaseId = int.Parse(index);
				Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.Delete(data, SessionVariables.RequestProfile);
			}

			DeleteAndRedirect();
		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.SystemForeignRelationshipDatabase, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("SystemForeignRelationshipDatabaseEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion
	}
}