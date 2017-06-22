using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationPublisher
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()	
		{
			var UpdatedData = new List<NotificationPublisherDataModel>();
			var data = new NotificationPublisherDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.NotificationPublisherId =
					Convert.ToInt32(SelectedData.Rows[i][NotificationPublisherDataModel.DataColumns.NotificationPublisherId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.EventMonitoring.NotificationPublisherDataManager.Update(data, SessionVariables.RequestProfile);
				data = new NotificationPublisherDataModel();
				data.NotificationPublisherId = Convert.ToInt32(SelectedData.Rows[i][NotificationPublisherDataModel.DataColumns.NotificationPublisherId].ToString());
				var dt = Framework.Components.EventMonitoring.NotificationPublisherDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var notificationPublisherdata = new NotificationPublisherDataModel();
            notificationPublisherdata.NotificationPublisherId = entityKey;
			var results = Framework.Components.EventMonitoring.NotificationPublisherDataManager.GetEntityDetails(notificationPublisherdata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationPublisher;
            PrimaryEntityKey = "NotificationPublisher";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}