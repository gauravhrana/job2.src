using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.UseCaseXUseCaseStep.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region private methods

		protected override void ShowData(int useCaseXUseCaseStepId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new UseCaseXUseCaseStepDataModel();
			data.UseCaseXUseCaseStepId = useCaseXUseCaseStepId;

            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count == 1)
			{
				var row = dt.Rows[0];

				lblUseCaseXUseCaseStepId.Text	 = Convert.ToString(row[UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId]);
				lblUseCaseStep.Text				 = Convert.ToString(row[UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStep]);
				lblUseCase.Text					 = Convert.ToString(row[UseCaseXUseCaseStepDataModel.DataColumns.UseCase]);
				lblUseCaseWorkFlowCategory.Text	 = Convert.ToString(row[UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategory]);

				oUpdateInfo.LoadText(dt.Rows[0]);

				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.UseCaseXUseCaseStep, useCaseXUseCaseStepId, "UseCaseXUseCaseStep");
				dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "UseCaseXUseCaseStep");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblUseCaseXUseCaseStepId.Text = String.Empty;
			lblUseCase.Text = String.Empty;
			lblUseCaseStep.Text = String.Empty;
			lblUseCaseWorkFlowCategory.Text = String.Empty;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblUseCaseXUseCaseStepIdText.Visible = isTesting;
				lblUseCaseXUseCaseStepId.Visible = isTesting;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.UseCaseXUseCaseStepLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseXUseCaseStep;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynUseCaseXUseCaseStepId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

		}

		#endregion
	}
}