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

namespace Shared.UI.Web.EventMonitoring.ApplicationMonitoredEventEmail
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
		{            
            var UpdatedData = new List<ApplicationMonitoredEventEmailDataModel>();
            var data = new ApplicationMonitoredEventEmailDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.ApplicationMonitoredEventEmailId =
                    Convert.ToInt32(SelectedData.Rows[i][ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventEmailId].ToString());

                data.ApplicationMonitoredEventSourceId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventSourceId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventSourceId).ToString())
                    : int.Parse(SelectedData.Rows[i][ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventSourceId].ToString());

                data.UserId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventEmailDataModel.DataColumns.UserId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventEmailDataModel.DataColumns.UserId).ToString())
                    : int.Parse(SelectedData.Rows[i][ApplicationMonitoredEventEmailDataModel.DataColumns.UserId].ToString());
               
                data.CorrespondenceLevel =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventEmailDataModel.DataColumns.CorrespondenceLevel))
                    ? CheckAndGetRepeaterTextBoxValue(ApplicationMonitoredEventEmailDataModel.DataColumns.CorrespondenceLevel)
                    : SelectedData.Rows[i][ApplicationMonitoredEventEmailDataModel.DataColumns.CorrespondenceLevel].ToString();

				Framework.Components.EventMonitoring.ApplicationMonitoredEventEmailDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ApplicationMonitoredEventEmailDataModel();
                data.ApplicationMonitoredEventEmailId = Convert.ToInt32(SelectedData.Rows[i][ApplicationMonitoredEventEmailDataModel.DataColumns.ApplicationMonitoredEventEmailId].ToString());
				var dt = Framework.Components.EventMonitoring.ApplicationMonitoredEventEmailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {                    
                    UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var applicationMonitoredEventEmaildata = new ApplicationMonitoredEventEmailDataModel();
            applicationMonitoredEventEmaildata.ApplicationMonitoredEventEmailId = entityKey;
			var results = Framework.Components.EventMonitoring.ApplicationMonitoredEventEmailDataManager.GetEntityDetails(applicationMonitoredEventEmaildata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEventEmail;
            PrimaryEntityKey = "ApplicationMonitoredEventEmail";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}