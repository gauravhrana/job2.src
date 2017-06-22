using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.ProjectXUseCase.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region private methods

		protected override void ShowData(int projectXUseCaseId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new ProjectXUseCaseDataModel();
			data.ProjectXUseCaseId = projectXUseCaseId;

            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count == 1)
			{
				var row = dt.Rows[0];

				lblProjectXUseCaseId.Text = Convert.ToString(row[ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId]);
				lblProject.Text = Convert.ToString(row[ProjectXUseCaseDataModel.DataColumns.Project]);
				lblUseCase.Text = Convert.ToString(row[ProjectXUseCaseDataModel.DataColumns.UseCase]);
				lblProjectUseCaseStatus.Text = Convert.ToString(row[ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatus]);

				oUpdateInfo.LoadText(dt.Rows[0]);

				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ProjectXUseCase, projectXUseCaseId, "ProjectXUseCase");
				dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ProjectXUseCase");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblProjectXUseCaseId.Text = String.Empty;
			lblUseCase.Text = String.Empty;
			lblProject.Text = String.Empty;
			lblProjectUseCaseStatus.Text = String.Empty;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblProjectXUseCaseIdText.Visible = isTesting;
				lblProjectXUseCaseId.Visible = isTesting;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ProjectXUseCaseLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectXUseCase;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynProjectXUseCaseId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

		}

		#endregion
	}
}