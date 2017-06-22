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

namespace Shared.UI.Web.EventMonitoring.ApplicationMonitoredEventProcessingState
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();
            var data = new ApplicationMonitoredEventProcessingStateDataModel();
			UpdatedData = Framework.Components.EventMonitoring.ApplicationMonitoredEventProcessingStateDataManager.Search(data, SessionVariables.RequestProfile).Clone();
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
				var dt = Framework.Components.EventMonitoring.ApplicationMonitoredEventProcessingStateDataManager.Search(data, SessionVariables.RequestProfile);
                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var applicationMonitoredEventProcessingStatedata = new ApplicationMonitoredEventProcessingStateDataModel();
            applicationMonitoredEventProcessingStatedata.ApplicationMonitoredEventProcessingStateId = entityKey;
			var results = Framework.Components.EventMonitoring.ApplicationMonitoredEventProcessingStateDataManager.Search(applicationMonitoredEventProcessingStatedata, SessionVariables.RequestProfile);
            return results;
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