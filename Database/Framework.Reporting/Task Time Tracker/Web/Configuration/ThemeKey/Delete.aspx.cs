using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.ThemeKey
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.ThemeKey;
			PrimaryEntityKey = "ThemeKey";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("ThemeKey", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var notDeletableIds = new List<int>();
				var deleteIndexList = DeleteIds.Split(',');

				foreach (string index in deleteIndexList)
				{
					var data = new ThemeKeyDataModel();
					data.ThemeKeyId = int.Parse(index);
					Framework.Components.Core.ThemeKeyDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.ThemeKey, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ThemeKeyEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion		

    }
}