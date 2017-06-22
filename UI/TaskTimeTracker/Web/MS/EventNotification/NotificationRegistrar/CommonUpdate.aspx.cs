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
using Dapper;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationRegistrar
{
    public partial class CommonUpdate : PageCommonUpdate
   {
		#region Methods

		protected override DataTable UpdateData()
        {
            var UpdatedData = new List<NotificationRegistrarDataModel>();
            var data = new NotificationRegistrarDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.NotificationRegistrarId =
                    Convert.ToInt32(SelectedData.Rows[i][NotificationRegistrarDataModel.DataColumns.NotificationRegistrarId].ToString());

                data.NotificationEventTypeId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(NotificationRegistrarDataModel.DataColumns.NotificationEventTypeId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(NotificationRegistrarDataModel.DataColumns.NotificationEventTypeId).ToString())
                    : int.Parse(SelectedData.Rows[i][NotificationRegistrarDataModel.DataColumns.NotificationEventTypeId].ToString());

				data.NotificationPublisherId =
				   !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(NotificationRegistrarDataModel.DataColumns.NotificationPublisherId))
				   ? int.Parse(CheckAndGetRepeaterTextBoxValue(NotificationRegistrarDataModel.DataColumns.NotificationPublisherId).ToString())
				   : int.Parse(SelectedData.Rows[i][NotificationRegistrarDataModel.DataColumns.NotificationPublisherId].ToString());
                data.Message =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(NotificationRegistrarDataModel.DataColumns.Message))
                    ? CheckAndGetRepeaterTextBoxValue(NotificationRegistrarDataModel.DataColumns.Message).ToString()
                    : SelectedData.Rows[i][NotificationRegistrarDataModel.DataColumns.Message].ToString();
                data.PublishDateId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(NotificationRegistrarDataModel.DataColumns.PublishDateId))
                    ?int.Parse(CheckAndGetRepeaterTextBoxValue(NotificationRegistrarDataModel.DataColumns.PublishDateId).ToString())
                    :int.Parse(SelectedData.Rows[i][NotificationRegistrarDataModel.DataColumns.PublishDateId].ToString());

				Framework.Components.EventMonitoring.NotificationRegistrarDataManager.Update(data, SessionVariables.RequestProfile);
                data = new NotificationRegistrarDataModel();
                data.NotificationRegistrarId = Convert.ToInt32(SelectedData.Rows[i][NotificationRegistrarDataModel.DataColumns.NotificationRegistrarId].ToString());
				var dt = Framework.Components.EventMonitoring.NotificationRegistrarDataManager.GetEntityList(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var notificationRegistrardata = new NotificationRegistrarDataModel();
            notificationRegistrardata.NotificationRegistrarId = entityKey;
			var results = Framework.Components.EventMonitoring.NotificationRegistrarDataManager.GetEntityList(notificationRegistrardata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationRegistrar;
            PrimaryEntityKey = "NotificationRegistrar";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
   }
}