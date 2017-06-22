using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Competency;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.WBS.TaskXCompetency.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int taskXCompetencyId)
		{
			base.ShowData(taskXCompetencyId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new TaskXCompetencyDataModel();
			data.TaskXCompetencyId = taskXCompetencyId;

            var items = TaskXCompetencyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);


			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblTaskXCompetencyId.Text    = item.TaskXCompetencyId.ToString();
				lblTask.Text                 = item.TaskId.ToString();
				lblCompetency.Text           = item.Competency.ToString();
				
				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, taskXCompetencyId, "TaskXCompetency");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTaskXCompetencyIdText});
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.TaskXCompetencyLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskXCompetency;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynTaskXCompetencyId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblTaskXCompetencyIdText.Visible = isTesting;
				lblTaskXCompetencyId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

	}

}