using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleItem
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{		
			var UpdatedData = new DataTable();
			var data = new ScheduleItemDataModel();
            UpdatedData = ScheduleItemDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ScheduleItemId =
					Convert.ToInt32(SelectedData.Rows[i][ScheduleItemDataModel.DataColumns.ScheduleItemId].ToString());
				data.ScheduleId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleItemDataModel.DataColumns.ScheduleId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleItemDataModel.DataColumns.ScheduleId).ToString())
					: int.Parse(SelectedData.Rows[i][ScheduleItemDataModel.DataColumns.ScheduleId].ToString());

				data.TaskFormulationId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleItemDataModel.DataColumns.TaskFormulationId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleItemDataModel.DataColumns.TaskFormulationId).ToString())
					: int.Parse(SelectedData.Rows[i][ScheduleItemDataModel.DataColumns.TaskFormulationId].ToString());

				data.TotalTimeSpent =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleItemDataModel.DataColumns.TotalTimeSpent))
					? Decimal.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleItemDataModel.DataColumns.TotalTimeSpent).ToString())
					: Decimal.Parse(SelectedData.Rows[i][ScheduleItemDataModel.DataColumns.TotalTimeSpent].ToString());

                ScheduleItemDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ScheduleItemDataModel();
				data.ScheduleItemId = Convert.ToInt32(SelectedData.Rows[i][ScheduleItemDataModel.DataColumns.ScheduleItemId].ToString());
                var dt = TaskTimeTracker.Components.Module.TimeTracking.ScheduleItemDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
                }

            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var scheduleItemdata = new ScheduleItemDataModel();
            scheduleItemdata.ScheduleItemId = entityKey;
            var results = ScheduleItemDataManager.Search(scheduleItemdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleItem;
            PrimaryEntityKey = "ScheduleItem";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}