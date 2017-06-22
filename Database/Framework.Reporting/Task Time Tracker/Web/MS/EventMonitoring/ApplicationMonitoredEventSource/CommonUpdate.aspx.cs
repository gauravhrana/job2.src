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

namespace Shared.UI.Web.EventMonitoring.ApplicationMonitoredEventSource
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()	
        {
            var UpdatedData = new DataTable();
            var data = new ApplicationMonitoredEventSourceDataModel();
			UpdatedData = Framework.Components.EventMonitoring.ApplicationMonitoredEventSourceDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.ApplicationMonitoredEventSourceId =
                    Convert.ToInt32(SelectedData.Rows[i][ApplicationMonitoredEventSourceDataModel.DataColumns.ApplicationMonitoredEventSourceId].ToString());

                data.Code =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventSourceDataModel.DataColumns.Code))
                    ? CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventSourceDataModel.DataColumns.Code)
                    : SelectedData.Rows[i][ApplicationMonitoredEventSourceDataModel.DataColumns.Code].ToString();

                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventSourceDataModel.DataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventSourceDataModel.DataColumns.Description)
                    : SelectedData.Rows[i][ApplicationMonitoredEventSourceDataModel.DataColumns.Description].ToString();

				Framework.Components.EventMonitoring.ApplicationMonitoredEventSourceDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ApplicationMonitoredEventSourceDataModel();
                data.ApplicationMonitoredEventSourceId = Convert.ToInt32(SelectedData.Rows[i][ApplicationMonitoredEventSourceDataModel.DataColumns.ApplicationMonitoredEventSourceId].ToString());
				var dt = Framework.Components.EventMonitoring.ApplicationMonitoredEventSourceDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {                   
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var applicationMonitoredEventSourcedata = new ApplicationMonitoredEventSourceDataModel();
            applicationMonitoredEventSourcedata.ApplicationMonitoredEventSourceId = entityKey;
			var results = Framework.Components.EventMonitoring.ApplicationMonitoredEventSourceDataManager.Search(applicationMonitoredEventSourcedata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEventSource;
            PrimaryEntityKey = "ApplicationMonitoredEventSource";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}