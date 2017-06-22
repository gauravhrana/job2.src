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
using Framework.Components.Core;
using Dapper;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType
{
	public partial class InlineUpdate : Shared.UI.WebFramework.BasePage
	{
		public delegate void UpdateDelegate(Dictionary<string, string> values);

		private DataTable GetData()
		{
			try
			{
                var superKey = ApplicationCommon.GetSuperKey();
                var setId = ApplicationCommon.GetSetId();

				var selectedrows = new List<NotificationPublisherXEventTypeDataModel>();
				var NotificationPublisherXEventTypedata = new NotificationPublisherXEventTypeDataModel();

                if (!string.IsNullOrEmpty(superKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(superKey);
					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType;
					var listSuperKeyDetails = SuperKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                    if (listSuperKeyDetails != null && listSuperKeyDetails.Count > 0)
                    {
                        var keys = new int[listSuperKeyDetails.Count];
                        for (var i = 0; i < listSuperKeyDetails.Count; i++)
                        {

                            keys[i] = listSuperKeyDetails[i].EntityKey.Value;
							NotificationPublisherXEventTypedata.NotificationPublisherXEventTypeId = keys[i];
							var result = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.GetDetails(NotificationPublisherXEventTypedata, SessionVariables.RequestProfile);
                            selectedrows.Add(result);
						}
					}
				}
                else if (setId != 0)
				{
                    var key = setId;
					NotificationPublisherXEventTypedata.NotificationPublisherXEventTypeId = key;
					var result = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.GetDetails(NotificationPublisherXEventTypedata, SessionVariables.RequestProfile);
                    selectedrows.Add(result);
				}
				return selectedrows.ToDataTable();
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