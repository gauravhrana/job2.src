using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.App_Workflow
{
	public class ScheduleWorkflow
	{
		public static void AddScheduleQuestions(int scheduleId, RequestProfile requestProfile)
		{
			// get all questions
			var questionData = QuestionDataManager.GetList(requestProfile);

			// filter question records using Question Category 'EOD Questions'
			questionData.DefaultView.RowFilter = "QuestionCategory = 'EOD Questions'";
			questionData = questionData.DefaultView.ToTable();

			var scheduleQuestionData = new ScheduleQuestionDataModel();

			// for loop for entering default answers for all questions
			for (var i = 0; i < questionData.Rows.Count; i++)
			{

				var questionid = int.Parse(questionData.Rows[i][QuestionDataModel.DataColumns.QuestionId].ToString());

				scheduleQuestionData = new ScheduleQuestionDataModel();

				scheduleQuestionData.ScheduleId = scheduleId;
				scheduleQuestionData.QuestionId = int.Parse(questionid.ToString());
				scheduleQuestionData.Answer = "No";

				ScheduleQuestionDataManager.Create(scheduleQuestionData, requestProfile);

			}
		}

		public static void SyncQuestions(int scheduleId, RequestProfile requestProfile)
		{
			// Get all ScheduleId related records from ScheduleQuestion
            var dtScheduleQuestion = ScheduleQuestionDataManager.GetBySchedule(scheduleId, requestProfile);

			// Get all Question Records
			var questionData = QuestionDataManager.GetList(requestProfile);

			// filter question records using Question Category 'EOD Questions'
			questionData.DefaultView.RowFilter = "QuestionCategory = 'EOD Questions'";
			questionData = questionData.DefaultView.ToTable();

			var scheduleQuestionData = new ScheduleQuestionDataModel();

			// for loop for entering default answers for all questions
			for (var i = 0; i < questionData.Rows.Count; i++)
			{
				var questionid = int.Parse(questionData.Rows[i][QuestionDataModel.DataColumns.QuestionId].ToString());

				var filterExpression = QuestionDataModel.DataColumns.QuestionId + " = " + questionid;

				// find if record for this question id exists in ScheduleQuestion for the particular ScheduleId
				var rows = dtScheduleQuestion.Select(filterExpression);

				// insert if no record found
				if (rows.Length == 0)
				{
					scheduleQuestionData = new ScheduleQuestionDataModel();

					scheduleQuestionData.ScheduleId = scheduleId;
					scheduleQuestionData.QuestionId = int.Parse(questionid.ToString());
					scheduleQuestionData.Answer = "No";

                    ScheduleQuestionDataManager.Create(scheduleQuestionData, requestProfile);
				}
			}
			// refresh data table with newly added records
			dtScheduleQuestion = ScheduleQuestionDataManager.GetBySchedule(scheduleId, requestProfile);

			// for removing invalid category schedule questions
			for (var i = 0; i < dtScheduleQuestion.Rows.Count; i++)
			{
				var questionid = int.Parse(dtScheduleQuestion.Rows[i][ScheduleQuestionDataModel.DataColumns.QuestionId].ToString());

				var filterExpression = ScheduleQuestionDataModel.DataColumns.QuestionId + " = " + questionid;

				// find if record for this question id exists in ScheduleQuestion for the particular ScheduleId
				var rows = questionData.Select(filterExpression);

				// insert if no record found, so it is invalid
				if (rows.Length == 0)
				{
					scheduleQuestionData = new ScheduleQuestionDataModel();
					scheduleQuestionData.ScheduleQuestionId = int.Parse(dtScheduleQuestion.Rows[i][ScheduleQuestionDataModel.DataColumns.ScheduleQuestionId].ToString());
					ScheduleQuestionDataManager.Delete(scheduleQuestionData, requestProfile);
				}

			}

		}

	}
}