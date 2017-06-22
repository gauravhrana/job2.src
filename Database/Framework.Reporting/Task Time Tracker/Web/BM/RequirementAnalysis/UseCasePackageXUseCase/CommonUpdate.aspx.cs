using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.RequirementAnalysis.UseCasePackageXUseCase
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new UseCasePackageXUseCaseDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.UseCasePackageXUseCaseId =
					Convert.ToInt32(SelectedData.Rows[i][UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageXUseCaseId].ToString());

				data.UseCasePackageId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId).ToString())
					: int.Parse(SelectedData.Rows[i][UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId].ToString());

				data.UseCaseId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId).ToString())
					: int.Parse(SelectedData.Rows[i][UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId].ToString());


                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.Update(data, SessionVariables.RequestProfile);
				data = new UseCasePackageXUseCaseDataModel();
				data.UseCasePackageXUseCaseId = Convert.ToInt32(SelectedData.Rows[i][UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageXUseCaseId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var useCasePackageXUseCasedata = new UseCasePackageXUseCaseDataModel();
			useCasePackageXUseCasedata.UseCasePackageXUseCaseId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.Search(useCasePackageXUseCasedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCasePackageXUseCase;
			PrimaryEntityKey = "UseCasePackageXUseCase";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}