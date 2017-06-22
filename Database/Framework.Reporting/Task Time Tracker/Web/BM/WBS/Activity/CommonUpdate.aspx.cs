using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.Activity
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new ActivityDataModel();
			UpdatedData = ActivityDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.ActivityId =
                    Convert.ToInt32(SelectedData.Rows[i][ActivityDataModel.DataColumns.ActivityId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                data.LayerId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ActivityDataModel.DataColumns.LayerId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(ActivityDataModel.DataColumns.LayerId))
                    : int.Parse(SelectedData.Rows[i][ActivityDataModel.DataColumns.LayerId].ToString());

				ActivityDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ActivityDataModel();
                data.ActivityId = Convert.ToInt32(SelectedData.Rows[i][ActivityDataModel.DataColumns.ActivityId].ToString());
				var dt = ActivityDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var activitydata = new ActivityDataModel();
            activitydata.ActivityId = entityKey;
			var results = ActivityDataManager.Search(activitydata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Activity;
            PrimaryEntityKey = "Activity";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}