using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.UseCaseActorXUseCase.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region private methods

		protected override void ShowData(int useCaseActorXUseCaseId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new UseCaseActorXUseCaseDataModel();
			data.UseCaseActorXUseCaseId = useCaseActorXUseCaseId;

            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count == 1)
			{
				var row = dt.Rows[0];

                lblUseCaseActorXUseCaseId.Text = Convert.ToString(row[UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId]);
				lblUseCaseActor.Text			 = Convert.ToString(row[UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActor]);
				lblUseCase.Text					 = Convert.ToString(row[UseCaseActorXUseCaseDataModel.DataColumns.UseCase]);
				lblUseCaseRelationship.Text		 = Convert.ToString(row[UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationship]);

				oUpdateInfo.LoadText(dt.Rows[0]);

				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.UseCaseActorXUseCase, useCaseActorXUseCaseId, "UseCaseActorXUseCase");
				dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "UseCaseActorXUseCase");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblUseCaseActorXUseCaseId.Text = String.Empty;
			lblUseCase.Text = String.Empty;
			lblUseCaseActor.Text = String.Empty;
			lblUseCaseRelationship.Text = String.Empty;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblUseCaseActorXUseCaseIdText.Visible = isTesting;
				lblUseCaseActorXUseCaseId.Visible = isTesting;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.UseCaseActorXUseCaseLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseActorXUseCase;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynUseCaseActorXUseCaseId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

		}

		#endregion
	}
}