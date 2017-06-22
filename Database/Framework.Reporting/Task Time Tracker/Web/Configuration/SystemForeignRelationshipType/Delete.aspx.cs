using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipType
{
	public partial class Delete : PageDelete
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationshipType;
			PrimaryEntityKey = "SystemForeignRelationshipType";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("SystemForeignRelationshipType", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			string[] deleteIndexList = DeleteIds.Split(',');
			foreach (string index in deleteIndexList)
			{
				var data = new SystemForeignRelationshipTypeDataModel();
				data.SystemForeignRelationshipTypeId = int.Parse(index);
				Framework.Components.Core.SystemForeignRelationshipTypeDataManager.Delete(data, SessionVariables.RequestProfile);
			}

			DeleteAndRedirect();
		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.SystemForeignRelationshipType, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("SystemForeignRelationshipTypeEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion
	}
}