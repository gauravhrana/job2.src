using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleDetail
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new ScheduleDetailDataModel();
            UpdatedData = ScheduleDetailDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ScheduleDetailId =
					Convert.ToInt32(SelectedData.Rows[i][ScheduleDetailDataModel.DataColumns.ScheduleDetailId].ToString());

				data.ScheduleId =
					Convert.ToInt32(SelectedData.Rows[i][ScheduleDetailDataModel.DataColumns.ScheduleId].ToString());
				
				data.InTime =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleDetailDataModel.DataColumns.InTime))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleDetailDataModel.DataColumns.InTime).ToString())
					: DateTime.Parse(SelectedData.Rows[i][ScheduleDetailDataModel.DataColumns.InTime].ToString());

				data.OutTime =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleDetailDataModel.DataColumns.OutTime))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleDetailDataModel.DataColumns.OutTime).ToString())
					: DateTime.Parse(SelectedData.Rows[i][ScheduleDetailDataModel.DataColumns.OutTime].ToString());

				data.Message  =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleDetailDataModel.DataColumns.Message))
					? CheckAndGetRepeaterTextBoxValue(ScheduleDetailDataModel.DataColumns.Message).ToString()
					: SelectedData.Rows[i][ScheduleDetailDataModel.DataColumns.Message].ToString();

				data.CreatedDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.CreatedDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.CreatedDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.CreatedDate].ToString());

                ScheduleDetailDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ScheduleDetailDataModel();
				data.ScheduleDetailId = Convert.ToInt32(SelectedData.Rows[i][ScheduleDetailDataModel.DataColumns.ScheduleDetailId].ToString());
                var dt = ScheduleDetailDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}

			}
			return UpdatedData;
		}

        protected override DataTable GetEntityData(int? entityKey)
		{
			var scheduleDetaildata = new ScheduleDetailDataModel();
			scheduleDetaildata.ScheduleDetailId = entityKey;
            var results = ScheduleDetailDataManager.Search(scheduleDetaildata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = SystemEntity.ScheduleDetail;
			PrimaryEntityKey = "ScheduleDetail";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}