using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleItem
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
				var scheduleItemdata = new ScheduleItemDataModel();

                selectedrows = TaskTimeTracker.Components.Module.TimeTracking.ScheduleItemDataManager.GetDetails(scheduleItemdata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						scheduleItemdata.ScheduleItemId = entityKey;
                        var result = TaskTimeTracker.Components.Module.TimeTracking.ScheduleItemDataManager.GetDetails(scheduleItemdata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					scheduleItemdata.ScheduleItemId = SetId;
                    var result = TaskTimeTracker.Components.Module.TimeTracking.ScheduleItemDataManager.GetDetails(scheduleItemdata, SessionVariables.RequestProfile);
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
			var data = new ScheduleItemDataModel();

            if (values.ContainsKey(ScheduleItemDataModel.DataColumns.ScheduleItemId))
            {
                data.ScheduleItemId = int.Parse(values[ScheduleItemDataModel.DataColumns.ScheduleItemId].ToString());
            }

            if (values.ContainsKey(ScheduleItemDataModel.DataColumns.ScheduleId))
            {
                data.ScheduleId = int.Parse(values[ScheduleItemDataModel.DataColumns.ScheduleId].ToString());
            }

            if (values.ContainsKey(ScheduleItemDataModel.DataColumns.TaskFormulationId))
            {
                data.TaskFormulationId = int.Parse(values[ScheduleItemDataModel.DataColumns.TaskFormulationId].ToString());
            }

            if (values.ContainsKey(ScheduleItemDataModel.DataColumns.TotalTimeSpent))
            {
                data.TotalTimeSpent = decimal.Parse(values[ScheduleItemDataModel.DataColumns.TotalTimeSpent].ToString());
            }

            TaskTimeTracker.Components.Module.TimeTracking.ScheduleItemDataManager.Update(data, SessionVariables.RequestProfile);

			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleItem;
            PrimaryEntityKey = "ScheduleItem";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}
			
			
			