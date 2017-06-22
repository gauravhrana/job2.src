using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker;
using Framework.Components.Audit;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ClientXProject
{
	public partial class Delete : PageDelete
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
            
			try
			{
		
				var detailsControlPath = "~/ClientXProject/Controls/Details.ascx";
				var superKey = ApplicationCommon.GetSuperKey();
				var setId = ApplicationCommon.GetSetId();

				if (!string.IsNullOrEmpty(superKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(superKey);

					// Change System Entity Type
					data.SystemEntityTypeId = (int)SystemEntity.ClientXProject;
					var dt = SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);
					
					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (DataRow dr in dt.Rows)
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

							//chkVisible.Checked = detailsControl.IsHistoryVisible;
						}
					}
				}
				else
				{
					var key = setId;
					DeleteIds = setId.ToString();

					var detailsControl = (Controls.Details)Page.LoadControl(detailsControlPath);
					detailsControl.SetId = key;
					plcDetailsList.Controls.Add(detailsControl);
				
				}

				ShowAuditHistory(true);
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			
			SettingCategory = "ClientXProjectDefaultView";			
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var deleteIndexList = DeleteIds.Split(',');

				foreach (string index in deleteIndexList)
				{
					var data = new ClientXProjectDataModel();
					data.ClientXProjectId = int.Parse(index);
                    ClientXProjectDataManager.Delete(data, SessionVariables.RequestProfile);
				}

				AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.ClientXProject, SessionVariables.RequestProfile);
				Response.Redirect(Page.GetRouteUrl("ClientXProjectEntityRoute", new { Action = "Default", SetId = true }), false);
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}
	
		#endregion	
	}
}