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
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleQuestion
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
				var scheduleQuestiondata = new ScheduleQuestionDataModel();

                selectedrows = ScheduleQuestionDataManager.GetDetails(scheduleQuestiondata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						scheduleQuestiondata.ScheduleQuestionId = entityKey;
                        var result = ScheduleQuestionDataManager.GetDetails(scheduleQuestiondata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					scheduleQuestiondata.ScheduleQuestionId = SetId;
                    var result = ScheduleQuestionDataManager.GetDetails(scheduleQuestiondata, SessionVariables.RequestProfile);
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
			var data = new ScheduleQuestionDataModel();

            if (values.ContainsKey(ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId))
            {
                data.ScheduleQuestionId = int.Parse(values[ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId].ToString());
            }

            if (values.ContainsKey(ScheduleQuestionDataModel.DataColumns.ScheduleId))
            {
                data.ScheduleId = int.Parse(values[ScheduleQuestionDataModel.DataColumns.ScheduleId].ToString());
            }

            if (values.ContainsKey(ScheduleQuestionDataModel.DataColumns.QuestionId))
            {
                data.QuestionId = int.Parse(values[ScheduleQuestionDataModel.DataColumns.QuestionId].ToString());
            }

            if (values.ContainsKey(ScheduleQuestionDataModel.DataColumns.Answer))
            {
                data.Answer = values[ScheduleQuestionDataModel.DataColumns.Answer].ToString();
            }

            ScheduleQuestionDataManager.Update(data,SessionVariables.RequestProfile);

			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleQuestion;
            PrimaryEntityKey = "ScheduleQuestion";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}
