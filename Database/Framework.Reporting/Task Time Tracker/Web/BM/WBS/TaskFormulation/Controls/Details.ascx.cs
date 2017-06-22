using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.Task;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.WBS.TaskFormulation.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region private methods

		protected override void ShowData(int taskFormulationId)
		{
			base.ShowData(taskFormulationId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new TaskFormulationDataModel();
			data.TaskFormulationId = taskFormulationId;

            var items = TaskTimeTracker.Components.BusinessLayer.Task.TaskFormulationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				lblTaskFormulationId.Text = item.TaskFormulationId.ToString();
				lblFeature.Text = item.FeatureName.ToString();
				lblProject.Text = item.ProjectName.ToString();
				lblTask.Text = item.TaskName.ToString();
				lblSortOrder.Text = item.SortOrder.ToString();

				
				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, taskFormulationId, "TaskFormulation");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTaskFormulationIdText, lblFeatureText, lblProjectText, lblTaskText, lblSortOrderText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			DictionaryLabel = CacheConstants.TaskFormulationLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskFormulation;

			base.OnInit(e);

			PlaceHolderCore = dynTaskFormulationId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblTaskFormulationIdText.Visible = isTesting;
				lblTaskFormulationId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

        
	}

}