using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SearchKey
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			try
			{
				base.OnInit(e);

				PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SearchKey;
				PrimaryEntityKey = "SearchKey";
				BreadCrumbObject = Master.BreadCrumbObject;

				DetailsControlPath = ApplicationCommon.GetControlPath("SearchKey", ControlType.DetailsControl);
				PrimaryPlaceHolder = plcDetailsList;

			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var notDeletableIds = new List<int>();
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new SearchKeyDataModel();
					data.SearchKeyId = int.Parse(index);
					if (!Framework.Components.Core.SearchKeyDataManager.IsDeletable(data, SessionVariables.RequestProfile))
					{
						notDeletableIds.Add(Convert.ToInt32(data.SearchKeyId));
					}
				}
				if (notDeletableIds.Count == 0)
				{
					foreach (string index in deleteIndexList)
					{
						var data = new SearchKeyDataModel();
						data.SearchKeyId = int.Parse(index);
						Framework.Components.Core.SearchKeyDataManager.Delete(data, SessionVariables.RequestProfile);
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
						msg += "SearchKeyId: " + id + " has detail records";
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.SearchKey, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("SearchKeyEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion
	}
}