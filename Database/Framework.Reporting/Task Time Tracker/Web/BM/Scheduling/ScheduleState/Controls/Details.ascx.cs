using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Collections;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.TaskTimeTracker.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleState.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
	{

		#region private methods

		protected override void ShowData(int scheduleStateId)
		{
			base.ShowData(scheduleStateId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ScheduleStateDataModel();
			data.ScheduleStateId = scheduleStateId;

            var items = TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, scheduleStateId, "ScheduleState");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblScheduleStateIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ScheduleStateDataModel();

			SetData(data);
		}

		public void SetData(ScheduleStateDataModel item)
		{
			SystemKeyId = item.ScheduleStateId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.ScheduleStateLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleState;

			PlaceHolderCore = dynScheduleStateId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblScheduleStateId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblScheduleStateIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion
	}
}