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

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleQuestion
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()	
		{
			var UpdatedData = new DataTable();
			var data = new ScheduleQuestionDataModel();
            UpdatedData = ScheduleQuestionDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ScheduleQuestionId =
					Convert.ToInt32(SelectedData.Rows[i][ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId].ToString());
				data.ScheduleId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleQuestionDataModel.DataColumns.ScheduleId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleQuestionDataModel.DataColumns.ScheduleId).ToString())
					: int.Parse(SelectedData.Rows[i][ScheduleQuestionDataModel.DataColumns.ScheduleId].ToString());

				data.QuestionId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleQuestionDataModel.DataColumns.QuestionId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ScheduleQuestionDataModel.DataColumns.QuestionId).ToString())
					: int.Parse(SelectedData.Rows[i][ScheduleQuestionDataModel.DataColumns.QuestionId].ToString());

				data.Answer =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ScheduleQuestionDataModel.DataColumns.Answer))
					?CheckAndGetRepeaterTextBoxValue(ScheduleQuestionDataModel.DataColumns.Answer).ToString()
					: SelectedData.Rows[i][ScheduleQuestionDataModel.DataColumns.Answer].ToString();

                ScheduleQuestionDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ScheduleQuestionDataModel();
				data.ScheduleQuestionId = Convert.ToInt32(SelectedData.Rows[i][ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId].ToString());
                var dt = ScheduleQuestionDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var scheduleQuestiondata = new ScheduleQuestionDataModel();
            scheduleQuestiondata.ScheduleQuestionId = entityKey;
            var results = ScheduleQuestionDataManager.Search(scheduleQuestiondata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleQuestion;
            PrimaryEntityKey = "ScheduleQuestion";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}