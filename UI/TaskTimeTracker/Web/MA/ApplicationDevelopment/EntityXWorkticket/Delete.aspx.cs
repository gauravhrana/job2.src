using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityXWorkTicket
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			try
			{
				base.OnInit(e);

				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();
				var detailsControlPath = "~/ApplicationDevelopment/EntityXWorkTicket/Controls/Details.ascx";

				if (!string.IsNullOrEmpty(SuperKey))
				{

					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(SuperKey);

					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.EntityXWorkTicket;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);
					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							if (string.IsNullOrEmpty(DeleteIds))
							{
								DeleteIds = key.ToString();
							}
							else
							{
								DeleteIds += "," + key.ToString();
							}
							var detailsControl = (Controls.Details)Page.LoadControl(detailsControlPath);
							detailsControl.SetId = key;
							detailsControl.BorderClass = ApplicationCommon.DetailsBorderClassName;

							plcDetailsList.Controls.Add(detailsControl);
							plcDetailsList.Controls.Add(new LiteralControl("<br />"));

							chkVisible.Checked = detailsControl.IsHistoryVisible;
						}
					}
				}
				else if (SetId != 0)
				{
					DeleteIds = SetId.ToString();

					var detailsControl = (Controls.Details)Page.LoadControl(detailsControlPath);
					detailsControl.SetId = SetId;
					plcDetailsList.Controls.Add(detailsControl);
					chkVisible.Checked = detailsControl.IsHistoryVisible;
				}
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var notDeletableIds = new List<int>();
				string[] deleteIndexList = DeleteIds.Split(',');
				//foreach (string index in deleteIndexList)
				//{
				//    var data = new EntityXWorkTicketDataModel();
				//    data.EntityXWorkTicketId = int.Parse(index);
				//    if (!TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityXWorkTicket.IsDeletable(data, SessionVariables.RequestProfile.AuditId))
				//    {
				//        notDeletableIds.Add(Convert.ToInt32(data.EntityXWorkTicketId));
				//    }
				//}
				if (notDeletableIds.Count == 0)
				{
					foreach (string index in deleteIndexList)
					{
						var data = new EntityXWorkTicketDataModel();
						data.EntityXWorkTicketId = int.Parse(index);
						TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityXWorkTicketDataManager.Delete(data, SessionVariables.RequestProfile);
					}
					Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.EntityXWorkTicket, SessionVariables.RequestProfile);

					Response.Redirect(Page.GetRouteUrl("EntityXWorkTicketEntityRoute", new { Action = "Default", SetId = true }), false);
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
						msg += "EntityXWorkTicketId: " + id + " has detail records";
					}
					Response.Write(msg);
				}
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		#endregion

	}
}