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

namespace Shared.UI.Web.EventMonitoring.ApplicationMonitoredEventProcessingState
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new List<ApplicationMonitoredEventProcessingStateDataModel>();
            var data = new ApplicationMonitoredEventProcessingStateDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.ApplicationMonitoredEventProcessingStateId =
                    Convert.ToInt32(SelectedData.Rows[i][ApplicationMonitoredEventProcessingStateDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId].ToString());

                data.Code =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventProcessingStateDataModel.DataColumns.Code))
                    ? CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventProcessingStateDataModel.DataColumns.Code)
                    : SelectedData.Rows[i][ApplicationMonitoredEventProcessingStateDataModel.DataColumns.Code].ToString();

                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventProcessingStateDataModel.DataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventProcessingStateDataModel.DataColumns.Description)
                    : SelectedData.Rows[i][ApplicationMonitoredEventProcessingStateDataModel.DataColumns.Description].ToString();

				Framework.Components.EventMonitoring.ApplicationMonitoredEventProcessingStateDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ApplicationMonitoredEventProcessingStateDataModel();
                data.ApplicationMonitoredEventProcessingStateId = Convert.ToInt32(SelectedData.Rows[i][ApplicationMonitoredEventProcessingStateDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId].ToString());
				var dt = Framework.Components.EventMonitoring.ApplicationMonitoredEventProcessingStateDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var applicationMonitoredEventProcessingStatedata = new ApplicationMonitoredEventProcessingStateDataModel();
            applicationMonitoredEventProcessingStatedata.ApplicationMonitoredEventProcessingStateId = entityKey;
			var results = Framework.Components.EventMonitoring.ApplicationMonitoredEventProcessingStateDataManager.GetEntityDetails(applicationMonitoredEventProcessingStatedata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEventProcessingState;
            PrimaryEntityKey = "ApplicationMonitoredEventProcessingState";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}