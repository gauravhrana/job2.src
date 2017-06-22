using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithmItem.Controls
{
	public partial class Generic : ControlGeneric
	{

		#region properties

		public int? TaskAlgorithmItemId
		{
			get
			{
				if (txtTaskAlgorithmItemId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtTaskAlgorithmItemId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtTaskAlgorithmItemId.Text);
				}
			}
			set
			{
				txtTaskAlgorithmItemId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? ActivityId
		{
			get
			{
				if (txtActivityId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtActivityId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtActivityId.Text);
				}
			}
			set
			{
				txtActivityId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public int? TaskAlgorithmId
		{
			get
			{
				if (txtTaskAlgorithmId.Enabled)
				{
					return DefaultDataRules.CheckAndGetEntityId(txtTaskAlgorithmId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtTaskAlgorithmId.Text);
				}
			}
			set
			{
				txtTaskAlgorithmId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		public string Description
		{
			get
			{
				return DefaultDataRules.CheckAndGetDescription(txtActivityId.Text, txtDescription.InnerText);
			}
			set
			{
				txtDescription.InnerText = value ?? String.Empty;
			}
		}

		public int? SortOrder
		{
			get
			{
				return DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
			}
			set
			{
				txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		#endregion properties

		#region methods

		public override int? Save(string action)
		{
			var data = new TaskAlgorithmItemDataModel();

			data.TaskAlgorithmItemId	= TaskAlgorithmItemId;
			data.TaskAlgorithmId		= TaskAlgorithmId;
			data.ActivityId				= ActivityId;
			data.Description			= Description;
			data.SortOrder				= SortOrder;

			if (action == "Insert")
			{
                var dtTaskAlgorithmItem = TaskAlgorithmItemDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtTaskAlgorithmItem.Rows.Count == 0)
				{
					TaskAlgorithmItemDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				TaskAlgorithmItemDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return TaskAlgorithmItemId;
		}

		public override void SetId(int setId, bool chkTaskAlgorithmItemId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTaskAlgorithmItemId);
			txtTaskAlgorithmItemId.Enabled = chkTaskAlgorithmItemId;
			//txtDescription.Enabled = !chkTaskAlgorithmItemId;
			//txtActivityId.Enabled = !chkTaskAlgorithmItemId;
			//txtSortOrder.Enabled = !chkTaskAlgorithmItemId;
		}

		public void LoadData(int taskAlgorithmItemId, bool showId)
		{

			Clear();

			var data = new TaskAlgorithmItemDataModel();
			data.TaskAlgorithmItemId = taskAlgorithmItemId;
			var items = TaskAlgorithmItemDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);


			if (items.Count != 1) return;

			var item = items[0];

			TaskAlgorithmItemId = item.TaskAlgorithmItemId;
			TaskAlgorithmId = item.TaskAlgorithmId;
			ActivityId = item.ActivityId;
			Description = item.Description;
			SortOrder = item.SortOrder;

			if (!showId)
			{
				txtTaskAlgorithmItemId.Text = item.TaskAlgorithmItemId.ToString();
				oHistoryList.Setup(PrimaryEntity, taskAlgorithmItemId, PrimaryEntityKey);
			}
			else
			{
				txtTaskAlgorithmItemId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TaskAlgorithmItemDataModel();

			TaskAlgorithmItemId = data.TaskAlgorithmItemId;
			TaskAlgorithmId = data.TaskAlgorithmId;
			ActivityId = data.ActivityId;
			Description = data.Description;
			SortOrder = data.SortOrder;
		}

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			var activityData = ActivityDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(activityData, drpActivityList, StandardDataModel.StandardDataColumns.Name, ActivityDataModel.DataColumns.ActivityId);

            var taskAlgorithmItemIdData = TaskAlgorithmDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(taskAlgorithmItemIdData, drpTaskAlgorithmList, StandardDataModel.StandardDataColumns.Name, TaskAlgorithmDataModel.DataColumns.TaskAlgorithmId);

			if (isTesting)
			{
				drpActivityList.AutoPostBack = true;
				drpTaskAlgorithmList.AutoPostBack = true;

				if (drpActivityList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtActivityId.Text.Trim()))
					{
						drpActivityList.SelectedValue = txtActivityId.Text;
					}
					else
					{
						txtActivityId.Text = drpActivityList.SelectedItem.Value;
					}
				}

				if (drpTaskAlgorithmList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtTaskAlgorithmId.Text.Trim()))
					{
						drpTaskAlgorithmList.SelectedValue = txtTaskAlgorithmId.Text;
					}
					else
					{
						txtTaskAlgorithmId.Text = drpTaskAlgorithmList.SelectedItem.Value;
					}
				}

				txtActivityId.Visible = true;
				txtTaskAlgorithmId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtActivityId.Text.Trim()))
				{
					drpActivityList.SelectedValue = txtActivityId.Text;
				}

				if (!string.IsNullOrEmpty(txtTaskAlgorithmId.Text.Trim()))
				{
					drpTaskAlgorithmList.SelectedValue = txtTaskAlgorithmId.Text;
				}
			}
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				txtTaskAlgorithmItemId.Visible = isTesting;
				lblTaskAlgorithmItemId.Visible = isTesting;
				SetupDropdown();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.TaskAlgorithmItem;
			PrimaryEntityKey = "TaskAlgorithmItem";
			FolderLocationFromRoot = "TaskAlgorithmItem";

			PlaceHolderCore = dynTaskAlgorithmItemId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpActivityList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtActivityId.Text = drpActivityList.SelectedItem.Value;
		}

		protected void drpTaskAlgorithmList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtTaskAlgorithmId.Text = drpTaskAlgorithmList.SelectedItem.Value;
		}

		#endregion

	}
}