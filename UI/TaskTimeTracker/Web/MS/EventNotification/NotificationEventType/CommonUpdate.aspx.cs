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

namespace ApplicationContainer.UI.Web.EventNotification.NotificationEventType
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{        
            var UpdatedData = new List<NotificationEventTypeDataModel>();
            var data = new NotificationEventTypeDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.NotificationEventTypeId =
                    Convert.ToInt32(SelectedData.Rows[i][NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.EventMonitoring.NotificationEventTypeDataManager.Update(data, SessionVariables.RequestProfile);
                data = new NotificationEventTypeDataModel();
                data.NotificationEventTypeId = Convert.ToInt32(SelectedData.Rows[i][NotificationEventTypeDataModel.DataColumns.NotificationEventTypeId].ToString());
				var dt = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var notificationEventTypedata = new NotificationEventTypeDataModel();
            notificationEventTypedata.NotificationEventTypeId = entityKey;
			var results = Framework.Components.EventMonitoring.NotificationEventTypeDataManager.GetEntityDetails(notificationEventTypedata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationEventType;
            PrimaryEntityKey = "NotificationEventType";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}