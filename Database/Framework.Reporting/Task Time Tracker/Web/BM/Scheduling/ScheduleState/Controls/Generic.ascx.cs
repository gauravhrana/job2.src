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
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{
		#region methods

		public override int? Save(string action)
		{
			var data = new ScheduleStateDataModel();

			data.ScheduleStateId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
                var dtScheduleState = TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtScheduleState.Rows.Count == 0)
				{
                    TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.ScheduleStateId;
		}

		public override void SetId(int setId, bool chkScheduleStateId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkScheduleStateId);
			CoreSystemKey.Enabled = chkScheduleStateId;
			//txtDescription.Enabled = !chkDeliverableArtifactId;
			//txtName.Enabled = !chkDeliverableArtifactId;
			//txtSortOrder.Enabled = !chkDeliverableArtifactId;
		}

		public void LoadData(int scheduleStateId, bool showId)
		{
			Clear();

			var data = new ScheduleStateDataModel();
			data.ScheduleStateId = scheduleStateId;

            var items = TaskTimeTracker.Components.Module.TimeTracking.ScheduleStateDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.ScheduleStateId;
				oHistoryList.Setup(PrimaryEntity, scheduleStateId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ScheduleStateDataModel();

			SetData(data);
		}

		public void SetData(ScheduleStateDataModel data)
		{
			SystemKeyId = data.ScheduleStateId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblScheduleStateId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleState;
			PrimaryEntityKey = "ScheduleState";
			FolderLocationFromRoot = "ScheduleState";

			PlaceHolderCore = dynScheduleStateId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtScheduleStateId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		#endregion
	}
}