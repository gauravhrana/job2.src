using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			try
			{
				base.OnInit(e);

                var superKey = ApplicationCommon.GetSuperKey();
                var setId = ApplicationCommon.GetSetId();
               
				var detailsControlPath = "~/EventNotification/NotificationPublisherXEventType/Controls/Details.ascx";

				if (!string.IsNullOrEmpty(superKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(superKey);

					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType;
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

							//chkVisible.Checked = detailsControl.IsHistoryVisible;
						}
					}
				}
				else
				{					
					var detailsControl = (Controls.Details)Page.LoadControl(detailsControlPath);
					detailsControl.SetId = setId;
					plcDetailsList.Controls.Add(detailsControl);
					//chkVisible.Checked = detailsControl.IsHistoryVisible;
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
            			
			SettingCategory = "NotificationPublisherXEventTypeDefaultView";
			
		}       

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');
				foreach (string index in deleteIndexList)
				{
					var data = new NotificationPublisherXEventTypeDataModel();
					data.NotificationPublisherXEventTypeId = int.Parse(index);
					Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.Delete(data, SessionVariables.RequestProfile);
				}

				Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType, SessionVariables.RequestProfile);
				Response.Redirect("Default.aspx?Deleted=" + true, false);
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}
		
		#endregion

    }
}