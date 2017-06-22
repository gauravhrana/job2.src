using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.SystemForeignRelationship
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationship;
			PrimaryEntityKey = "SystemForeignRelationship";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("SystemForeignRelationship", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new SystemForeignRelationshipDataModel();
					data.SystemForeignRelationshipId = int.Parse(index);
					Framework.Components.Core.SystemForeignRelationshipDataManager.Delete(data, SessionVariables.RequestProfile);
				}

				Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.SystemForeignRelationship, SessionVariables.RequestProfile);
				Response.Redirect(Page.GetRouteUrl("SystemForeignRelationshipEntityRoute", new { Action = "Default", SetId = true }), false);
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		#endregion
	}
}