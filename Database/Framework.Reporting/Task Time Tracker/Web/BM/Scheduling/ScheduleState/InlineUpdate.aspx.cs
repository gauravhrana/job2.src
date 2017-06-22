using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleState
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
				var scheduleStatedata = new ScheduleStateDataModel();

                selectedrows = ScheduleStateDataManager.GetDetails(scheduleStatedata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						scheduleStatedata.ScheduleStateId = entityKey;
                        var result = ScheduleStateDataManager.GetDetails(scheduleStatedata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}

				}
				else
				{
					scheduleStatedata.ScheduleStateId = SetId;
                    var result = ScheduleStateDataManager.GetDetails(scheduleStatedata, SessionVariables.RequestProfile);
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
			var data = new ScheduleStateDataModel();

            if (values.ContainsKey(ScheduleStateDataModel.DataColumns.ScheduleStateId))
            {
                data.ScheduleStateId = int.Parse(values[ScheduleStateDataModel.DataColumns.ScheduleStateId].ToString());
            }

            if (values.ContainsKey(StandardDataModel.StandardDataColumns.Name))
            {
                data.Name = values[StandardDataModel.StandardDataColumns.Name].ToString();
            }

            if (values.ContainsKey(StandardDataModel.StandardDataColumns.Description))
            {
                data.Description = values[StandardDataModel.StandardDataColumns.Description].ToString();
            }

            if (values.ContainsKey(StandardDataModel.StandardDataColumns.SortOrder))
            {
                data.SortOrder = int.Parse(values[StandardDataModel.StandardDataColumns.SortOrder].ToString());
            }

            TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.Update(data,SessionVariables.RequestProfile);
			InlineEditingList.Data = GetData();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleState;
            PrimaryEntityKey = "ScheduleState";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}
