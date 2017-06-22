using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule
{
	public partial class InlineUpdate : PageInlineUpdate
	{
		#region Methods

		protected override DataTable GetData()
		{
			try
			{
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();

				var selectedrows = new DataTable();
				var scheduledata = new ScheduleDataModel();

                selectedrows = ScheduleDataManager.GetDetails(scheduledata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
                    {
						scheduledata.ScheduleId = entityKey;
                        var result = ScheduleDataManager.GetDetails(scheduledata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
					
				}
				else 
				{					
					scheduledata.ScheduleId = SetId;
                    var result = ScheduleDataManager.GetDetails(scheduledata, SessionVariables.RequestProfile);
					selectedrows.ImportRow(result.Rows[0]);

				}
				return selectedrows;
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
			return null;
		}

		protected override void Update(Dictionary<string, string> values)
		{
			var data = new ScheduleDataModel();

            if (values.ContainsKey(ScheduleDataModel.DataColumns.ScheduleId))
            {
                data.ScheduleId = int.Parse(values[ScheduleDataModel.DataColumns.ScheduleId].ToString());
            }

            if (values.ContainsKey(ScheduleDataModel.DataColumns.PersonId))
            {
                data.PersonId = int.Parse(values[ScheduleDataModel.DataColumns.PersonId].ToString());
            }

            if (values.ContainsKey(ScheduleDataModel.DataColumns.WorkDate))
            {
                data.WorkDate = DateTime.Parse(values[ScheduleDataModel.DataColumns.WorkDate].ToString());
            }

            if (values.ContainsKey(ScheduleDataModel.DataColumns.StartTime))
            {
                data.StartTime = DateTime.Parse(values[ScheduleDataModel.DataColumns.StartTime].ToString());
            }

			if (values.ContainsKey(ScheduleDataModel.DataColumns.EndTime))
            {
                data.EndTime = DateTime.Parse(values[ScheduleDataModel.DataColumns.EndTime].ToString());
            }

            if (values.ContainsKey(ScheduleDataModel.DataColumns.TotalHoursWorked))
            {
                data.TotalHoursWorked = decimal.Parse(values[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString());
            }

            if (values.ContainsKey(ScheduleDataModel.DataColumns.NextWorkDate))
            {
                data.NextWorkDate = DateTime.Parse(values[ScheduleDataModel.DataColumns.NextWorkDate].ToString());
            }

			if (values.ContainsKey(ScheduleDataModel.DataColumns.NextWorkTime))
            {
                data.NextWorkTime = DateTime.Parse(values[ScheduleDataModel.DataColumns.NextWorkTime].ToString());
            }

            ScheduleDataManager.Update(data,SessionVariables.RequestProfile);
			InlineEditingList.Data = GetData();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = SystemEntity.Schedule;
            PrimaryEntityKey = "Schedule";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}