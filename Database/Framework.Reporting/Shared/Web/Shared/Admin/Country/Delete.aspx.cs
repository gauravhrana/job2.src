using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.Country
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= Framework.Components.DataAccess.SystemEntity.Country;			
			PrimaryEntityKey	= "Country";
			BreadCrumbObject	= Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("Country", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;			
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var notDeletableIds = new List<int>();
				string[] deleteIndexList = DeleteIds.Split(',');
				
				if (notDeletableIds.Count == 0)
				{
					foreach (string index in deleteIndexList)
					{
						var data = new CountryDataModel();
						data.CountryId = int.Parse(index);
						Framework.Components.Core.CountryDataManager.Delete(data, SessionVariables.RequestProfile);
					}
					DeleteAndRedirect();
				}
				else
				{
					var msg = String.Empty;
					foreach (var id in notDeletableIds)
					{
						if (!string.IsNullOrEmpty(msg))
						{
							msg += ", <br/>";
						}
						msg += "CountryId: " + id + " has detail records";
					}
					Response.Write(msg);
				}
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.Country, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("CountryEntityRoute", new { Action = "Default", SetId = true }), false);
		}
	
		#endregion           

    }
}