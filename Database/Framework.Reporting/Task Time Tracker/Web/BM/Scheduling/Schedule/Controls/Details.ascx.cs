using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Collections;
using TaskTimeTracker.Components.Module.TimeTracking;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.TaskTimeTracker.TimeTracking;


namespace ApplicationContainer.UI.Web.Scheduling.Schedule.Controls
{
    public partial class Details : ControlDetails
	{

		#region properties
		
		struct QuestionAnswerPair
		{
			private string question;
			private string answer;

			QuestionAnswerPair(string question, string answer)
			{
				this.question = question;
				this.answer = answer;
			}

			public string Question
			{
				get
				{ return question; }
				set { question = value; }
			}

			public string Answer
			{
				get
				{ return answer; }
				set
				{
					answer = value;
				}
			}

		}

		#endregion

		#region private methods

		protected override void ShowData(int scheduleId)
		{
			base.ShowData(scheduleId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new ScheduleDataModel();
			dataQuery.ScheduleId = scheduleId;

            var entityList = ScheduleDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{

					lblScheduleId.Text		= entityItem.ScheduleId.ToString();
					lblPersonId.Text		= entityItem.Person.ToString();
					lblScheduleState.Text	= entityItem.ScheduleStateName.ToString();
					lblWorkDate.Text		= String.Format("{0:d}",entityItem.WorkDate);
					lblStartTime.Text		= String.Format("{0:t}",entityItem.StartTime);
					lblEndTime.Text			= String.Format("{0:t}",entityItem.EndTime);
					lblNextWorkTime.Text	= String.Format("{0:t}",entityItem.NextWorkTime);
					lblNextWorkDate.Text	= String.Format("{0:d}",entityItem.NextWorkDate);
					lblTotalHoursWorked.Text = entityItem.TotalHoursWorked.ToString();
					lblPlannedHours.Text	= entityItem.PlannedHours.ToString();
					lblCreatedByAuditId.Text = entityItem.CreatedByAuditId.ToString();
					lblModifiedByAuditId.Text = entityItem.ModifiedByAuditId.ToString();
					lblCreatedDate.Text = entityItem.CreatedDate.Value.ToString(SessionVariables.UserDateFormat);
					lblModifiedDate.Text = entityItem.ModifiedDate.Value.ToString(SessionVariables.UserDateFormat);

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup(PrimaryEntity, scheduleId, "Schedule");
				}
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblScheduleIdText, lblPersonText,lblScheduleStateText, lblWorkDateText, lblStartTimeText, lblEndTimeText, lblNextWorkDateText, lblNextWorkTimeText, lblTotalHoursWorkedText,lblPlannedHoursText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblScheduleIdText.Visible = isTesting;
				lblScheduleId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ScheduleLabelDictionary;
			PrimaryEntity = SystemEntity.Schedule;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynScheduleId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		#endregion       
    }

}