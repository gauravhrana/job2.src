using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisherXEventType
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()	
		{
			var UpdatedData = new DataTable();
			var data = new NotificationPublisherXEventTypeDataModel();
			UpdatedData = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.NotificationPublisherXEventTypeId =
					Convert.ToInt32(SelectedData.Rows[i][NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherXEventTypeId].ToString());
				data.NotificationPublisherXEventTypeId =
				   !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherXEventTypeId))
				   ? int.Parse(CheckAndGetRepeaterTextBoxValue(NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherXEventTypeId).ToString())
				   : int.Parse(SelectedData.Rows[i][NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherXEventTypeId].ToString());

				data.NotificationEventTypeId =
				    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventTypeId))
				    ? int.Parse(CheckAndGetRepeaterTextBoxValue(NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventTypeId).ToString())
				    : int.Parse(SelectedData.Rows[i][NotificationPublisherXEventTypeDataModel.DataColumns.NotificationEventTypeId].ToString());

				//data.CreatedDateId  =
				//	!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(NotificationPublisherXEventTypeDataModel.DataColumns.CreatedDateId))
				//	? int.Parse(CheckAndGetRepeaterTextBoxValue(NotificationPublisherXEventTypeDataModel.DataColumns.CreatedDateId).ToString())
				//	: int.Parse(SelectedData.Rows[i][NotificationPublisherXEventTypeDataModel.DataColumns.CreatedDateId].ToString());

				Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.Update(data, SessionVariables.RequestProfile);
				data = new NotificationPublisherXEventTypeDataModel();
				data.NotificationPublisherXEventTypeId = Convert.ToInt32(SelectedData.Rows[i][NotificationPublisherXEventTypeDataModel.DataColumns.NotificationPublisherXEventTypeId].ToString());
				var dt = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var notificationPublisherXEventTypedata = new NotificationPublisherXEventTypeDataModel();
            notificationPublisherXEventTypedata.NotificationPublisherXEventTypeId = entityKey;
			var results = Framework.Components.EventMonitoring.NotificationPublisherXEventTypeDataManager.Search(notificationPublisherXEventTypedata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationPublisherXEventType;
            PrimaryEntityKey = "NotificationPublisherXEventType";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}