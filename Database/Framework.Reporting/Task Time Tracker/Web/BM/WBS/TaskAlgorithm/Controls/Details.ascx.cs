using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithm.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
	{

		#region private methods

		protected override void ShowData(int taskAlgorithmId)
		{
			base.ShowData(taskAlgorithmId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new TaskAlgorithmDataModel();
			data.TaskAlgorithmId = taskAlgorithmId;

			var items = TaskAlgorithmDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, taskAlgorithmId, "TaskAlgorithm");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTaskAlgorithmIdText, lblNameText, lblDescriptionText, lblSortOrderText, });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TaskAlgorithmDataModel();

			SetData(data);
		}

		public void SetData(TaskAlgorithmDataModel item)
		{
			SystemKeyId = item.TaskAlgorithmId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.TaskAlgorithmLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskAlgorithm;

			PlaceHolderCore = dynTaskAlgorithmId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblTaskAlgorithmId;
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
				lblTaskAlgorithmIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

	}
}