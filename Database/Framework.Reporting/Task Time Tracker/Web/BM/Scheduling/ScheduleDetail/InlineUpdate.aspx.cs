using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleDetail
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
				var scheduleDetaildata = new ScheduleDetailDataModel();

                selectedrows = ScheduleDetailDataManager.GetDetails(scheduleDetaildata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						scheduleDetaildata.ScheduleDetailId = entityKey;
                        var result = ScheduleDetailDataManager.GetDetails(scheduleDetaildata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					scheduleDetaildata.ScheduleDetailId = SetId;
                    var result = ScheduleDetailDataManager.GetDetails(scheduleDetaildata, SessionVariables.RequestProfile);
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
			var data = new ScheduleDetailDataModel();

            if (values.ContainsKey(ScheduleDetailDataModel.DataColumns.ScheduleDetailId))
            {
                data.ScheduleDetailId = int.Parse(values[ScheduleDetailDataModel.DataColumns.ScheduleDetailId].ToString());
            }

            if (values.ContainsKey(ScheduleDetailDataModel.DataColumns.ScheduleId))
            {
                data.ScheduleId = int.Parse(values[ScheduleDetailDataModel.DataColumns.ScheduleId].ToString());
            }           

            if (values.ContainsKey(ScheduleDetailDataModel.DataColumns.InTime))
            {
                data.InTime = DateTime.Parse(values[ScheduleDetailDataModel.DataColumns.InTime].ToString());
            }

            if (values.ContainsKey(ScheduleDetailDataModel.DataColumns.OutTime))
            {
                data.OutTime = DateTime.Parse(values[ScheduleDetailDataModel.DataColumns.OutTime].ToString());
            }

            if (values.ContainsKey(ScheduleDetailDataModel.DataColumns.CreatedDate))
            {
                data.CreatedDate = DateTime.Parse(values[ScheduleDetailDataModel.DataColumns.CreatedDate].ToString());
            }

            if (values.ContainsKey(ScheduleDetailDataModel.DataColumns.Message))
            {
                data.Message = values[ScheduleDetailDataModel.DataColumns.Message].ToString();
            }

            ScheduleDetailDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = SystemEntity.ScheduleDetail;
            PrimaryEntityKey = "ScheduleDetail";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}