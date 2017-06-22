using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.EventMonitoring;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType
{
	public partial class InlineUpdate : Shared.UI.WebFramework.BasePage
	{
		public delegate void UpdateDelegate(Dictionary<string, string> values);

		private DataTable GetData()
		{
			try
			{
				var superKey = "";
				var selectedrows = new DataTable();
				var NotificationPublisherXEventTypedata = new NotificationPublisherXEventTypeDataModel();

				selectedrows = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.GetDetails(NotificationPublisherXEventTypedata, SessionVariables.RequestProfile).Clone();

				if (Request.QueryString["SuperKey"] != null)
				{
					superKey = Request.QueryString["SuperKey"].ToString();

					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(superKey);
					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						var keys = new int[dt.Rows.Count];
						for (var i = 0; i < dt.Rows.Count; i++)
						{

							keys[i] = Convert.ToInt32(dt.Rows[i][SuperKeyDetailDataModel.DataColumns.EntityKey]);
							NotificationPublisherXEventTypedata.NotificationPublisherXEventTypeId = keys[i];
							var result = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.GetDetails(NotificationPublisherXEventTypedata, SessionVariables.RequestProfile);
							selectedrows.ImportRow(result.Rows[0]);
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					NotificationPublisherXEventTypedata.NotificationPublisherXEventTypeId = key;
					var result = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.GetDetails(NotificationPublisherXEventTypedata, SessionVariables.RequestProfile);
					selectedrows.ImportRow(result.Rows[0]);
				}
				return selectedrows;
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
			return null;
		}

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType, "DBColumns", SessionVariables.RequestProfile);
		}

		protected override void OnInit(EventArgs e)
		{
			InlineEditingList.AddColumns(GetColumns());
		}


		protected void Page_Load(object sender, EventArgs e)
		{
			UpdateDelegate delupdate = new UpdateDelegate(Update);
			this.InlineEditingList.DelUpdateRef = delupdate;
			if (!IsPostBack)
			{
				InlineEditingList.SetUp(GetColumns(), "NotificationPublisherXEventType", GetData());
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "NotificationPublisherXEventTypeDefaultView";
			
		}

		private void Update(Dictionary<string, string> values)
		{
			var data = new NotificationPublisherXEventTypeDataModel();
			data.NotificationPublisherXEventTypeId = int.Parse(values[NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherXEventTypeId].ToString());
			data.NotificationPublisherId =  int.Parse(values[NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherId].ToString());
			data.NotificationEventTypeId = int.Parse(values[NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventTypeId].ToString());
			//data.CreatedDateId = int.Parse(values[NotificationPublisherXEventTypeDataModel.DataColumns.CreatedDateId].ToString());
			Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.Update(data, SessionVariables.RequestProfile);
			InlineEditingList.Data = GetData();
		}
	}
}