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

namespace ApplicationContainer.UI.Web.RequirementAnalysis.UseCaseActorXUseCase
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new UseCaseActorXUseCaseDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.UseCaseActorXUseCaseId =
                    Convert.ToInt32(SelectedData.Rows[i][UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId].ToString());

				data.UseCaseActorId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId).ToString())
                    : int.Parse(SelectedData.Rows[i][UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorId].ToString());

				data.UseCaseId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId).ToString())
					: int.Parse(SelectedData.Rows[i][UseCaseActorXUseCaseDataModel.DataColumns.UseCaseId].ToString());

				data.UseCaseRelationshipId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId).ToString())
					: int.Parse(SelectedData.Rows[i][UseCaseActorXUseCaseDataModel.DataColumns.UseCaseRelationshipId].ToString());

                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.Update(data, SessionVariables.RequestProfile);
				data = new UseCaseActorXUseCaseDataModel();
				data.UseCaseActorXUseCaseId = Convert.ToInt32(SelectedData.Rows[i][UseCaseActorXUseCaseDataModel.DataColumns.UseCaseActorXUseCaseId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}			
			}
			return UpdatedData;
		}
        
		protected override DataTable GetEntityData(int? entityKey)
		{
			var useCaseActorXUseCasedata = new UseCaseActorXUseCaseDataModel();
			useCaseActorXUseCasedata.UseCaseActorXUseCaseId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.Search(useCaseActorXUseCasedata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseActorXUseCase;
			PrimaryEntityKey = "UseCaseActorXUseCase";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion	
	}
}