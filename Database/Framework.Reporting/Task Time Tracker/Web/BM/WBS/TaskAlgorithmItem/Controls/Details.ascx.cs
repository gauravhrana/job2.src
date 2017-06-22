using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithmItem.Controls
{
	public partial class Details : ControlDetails
	{

		#region private methods

		protected override void ShowData(int taskAlgorithmItemId)
		{
			base.ShowData(taskAlgorithmItemId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new TaskAlgorithmItemDataModel();
			data.TaskAlgorithmItemId = taskAlgorithmItemId;

            var items = TaskAlgorithmItemDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				lblTaskAlgorithmItemId.Text = item.TaskAlgorithmItemId.ToString();
				lblTaskAlgorithmId.Text = item.TaskAlgorithmId.ToString();
				lblActivityId.Text = item.ActivityId.ToString();
				lblDescription.Text = item.Description;
				lblSortOrder.Text = item.SortOrder.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, taskAlgorithmItemId, "TaskAlgorithmItem");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTaskAlgorithmItemIdText,lblTaskAlgorithmIdText, lblActivityIdText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			DictionaryLabel = CacheConstants.TaskAlgorithmItemLabelDictionary;
			PrimaryEntity = SystemEntity.TaskAlgorithmItem;

			base.OnInit(e);

			PlaceHolderCore = dynTaskAlgorithmItemId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblTaskAlgorithmItemIdText.Visible = isTesting;
				lblTaskAlgorithmItemId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

	}

}