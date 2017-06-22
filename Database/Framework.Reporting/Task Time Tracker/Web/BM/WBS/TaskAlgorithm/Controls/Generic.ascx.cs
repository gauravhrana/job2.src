using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithm.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{

		#region methods

		public override int? Save(string action)
		{
			var data = new TaskAlgorithmDataModel();

			data.TaskAlgorithmId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
                var dtTaskAlgorithm = TaskAlgorithmDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtTaskAlgorithm.Rows.Count == 0)
				{
                    TaskAlgorithmDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                TaskAlgorithmDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.TaskAlgorithmId;
		}

		public override void SetId(int setId, bool chkTaskAlgorithmId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTaskAlgorithmId);
			CoreSystemKey.Enabled = chkTaskAlgorithmId;
			//txtDescription.Enabled = !chkTaskAlgorithmId;
			//txtName.Enabled = !chkTaskAlgorithmId;
			//txtSortOrder.Enabled = !chkTaskAlgorithmId;
		}

		public void LoadData(int taskAlgorithmId, bool showId)
		{
			Clear();

			var data = new TaskAlgorithmDataModel();
			data.TaskAlgorithmId = taskAlgorithmId;

            var items = TaskAlgorithmDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.TaskAlgorithmId;
				oHistoryList.Setup(PrimaryEntity, taskAlgorithmId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TaskAlgorithmDataModel();

			SetData(data);
		}

		public void SetData(TaskAlgorithmDataModel data)
		{
			SystemKeyId = data.TaskAlgorithmId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblTaskAlgorithmId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskAlgorithm;
			PrimaryEntityKey = "TaskAlgorithm";
			FolderLocationFromRoot = "TaskAlgorithm";

			PlaceHolderCore = dynTaskAlgorithmId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtTaskAlgorithmId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		#endregion

	}
}