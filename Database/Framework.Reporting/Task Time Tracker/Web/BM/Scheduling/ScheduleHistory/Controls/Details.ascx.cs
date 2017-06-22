using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.TaskTimeTracker.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleHistory.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region private methods

		protected override void ShowData(int scheduleHistoryid)
		{
			base.ShowData(scheduleHistoryid);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ScheduleHistoryDataModel();
			data.ScheduleHistoryId = scheduleHistoryid;

            var items = TaskTimeTracker.Components.Module.TimeTracking.ScheduleHistoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
				
			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblScheduleHistoryId.Text = item.ScheduleHistoryId.ToString();
				lblPerson.Text = item.Person;				
				lblWorkDate.Text = item.WorkDate.ToString();
				lblNextWorkDate.Text = item.NextWorkDate.ToString();
				lblStartTime.Text = item.StartTime.ToString();
				lblEndTime.Text = item.EndTime.ToString();
				lblTotalHoursWorked.Text = item.TotalHoursWorked.ToString();
				lblScheduleStateName.Text = item.ScheduleStateName.ToString();
				lblAcknowledgedBy.Text = item.AcknowledgedBy;
				lblAcknowledgedById.Text = item.AcknowledgedById.ToString();
				
				lblScheduleId.Text = item.ScheduleId.ToString();


				oHistoryList.Setup(PrimaryEntity, scheduleHistoryid, "ScheduleHistory");
			}

		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblScheduleIdText, lblScheduleHistoryIdText, lblPersonText, 
					lblWorkDateText, lblNextWorkDateText,lblStartTimeText,lblEndTimeText,lblTotalHoursWorkedText,lblScheduleStateNameText });
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
				lblScheduleHistoryIdText.Visible = isTesting;
				lblScheduleHistoryId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ScheduleHistoryLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleHistory;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynScheduleHistoryId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		#endregion
	}
}