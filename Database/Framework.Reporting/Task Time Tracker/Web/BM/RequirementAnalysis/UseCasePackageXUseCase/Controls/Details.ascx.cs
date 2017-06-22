using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.UseCasePackageXUseCase.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region private methods

		protected override void ShowData(int useCasePackageXUseCaseId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new UseCasePackageXUseCaseDataModel();
			data.UseCasePackageXUseCaseId = useCasePackageXUseCaseId;

            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.GetDetails(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count == 1)
			{
				var row = dt.Rows[0];

				lblUseCasePackageXUseCaseId.Text	 = Convert.ToString(row[UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageXUseCaseId]);
				lblUseCasePackage.Text				 = Convert.ToString(row[UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackage]);
				lblUseCase.Text						 = Convert.ToString(row[UseCasePackageXUseCaseDataModel.DataColumns.UseCase]);				

				oUpdateInfo.LoadText(dt.Rows[0]);

				oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.UseCasePackageXUseCase, useCasePackageXUseCaseId, "UseCasePackageXUseCase");
				dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "UseCasePackageXUseCase");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblUseCasePackageXUseCaseId.Text	 = String.Empty;
			lblUseCase.Text						 = String.Empty;
			lblUseCasePackage.Text				 = String.Empty;
		}			

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblUseCasePackageXUseCaseIdText.Visible = isTesting;
				lblUseCasePackageXUseCaseId.Visible = isTesting;
			}
		}

		protected override void OnInit(EventArgs e)
		{
			DictionaryLabel = CacheConstants.UseCasePackageXUseCaseLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCasePackageXUseCase;

			base.OnInit(e);       
			PlaceHolderCore = dynUseCasePackageXUseCaseId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

		}

		#endregion
	}
}