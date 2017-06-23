using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Import;

namespace Shared.UI.Web.BatchFileSet
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "BatchFileSet";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("BatchFileSet", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFileSet;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var notDeletableIds = new List<int>();
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new BatchFileSetDataModel();
					data.BatchFileSetId = int.Parse(index);
					if (!Framework.Components.Import.BatchFileSetDataManager.IsDeletable(data, SessionVariables.RequestProfile))
					{
						notDeletableIds.Add(Convert.ToInt32(data.BatchFileSetId));
					}
				}
				if (notDeletableIds.Count == 0)
				{
					foreach (string index in deleteIndexList)
					{
						var data = new BatchFileSetDataModel();
						data.BatchFileSetId = int.Parse(index);
						Framework.Components.Import.BatchFileSetDataManager.Delete(data, SessionVariables.RequestProfile);
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
						msg += "BatchFileSetId: " + id + " has detail records";
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.BatchFileSet, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("BatchFileSetEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion 	
      

    }
}