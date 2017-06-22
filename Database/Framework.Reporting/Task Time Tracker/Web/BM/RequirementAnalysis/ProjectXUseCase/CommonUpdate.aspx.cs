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

namespace ApplicationContainer.UI.Web.RequirementAnalysis.ProjectXUseCase
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

		protected override DataTable UpdateData()
		{
            var UpdatedData = new DataTable();

            var data = new ProjectXUseCaseDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ProjectXUseCaseId =
                    Convert.ToInt32(SelectedData.Rows[i][ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId].ToString());
                data.ProjectId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ProjectXUseCaseDataModel.DataColumns.ProjectId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(ProjectXUseCaseDataModel.DataColumns.ProjectId).ToString())
                    : int.Parse(SelectedData.Rows[i][ProjectXUseCaseDataModel.DataColumns.ProjectId].ToString());

				data.UseCaseId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ProjectXUseCaseDataModel.DataColumns.UseCaseId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ProjectXUseCaseDataModel.DataColumns.UseCaseId).ToString())
					: int.Parse(SelectedData.Rows[i][ProjectXUseCaseDataModel.DataColumns.UseCaseId].ToString());

				data.ProjectUseCaseStatusId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId).ToString())
					: int.Parse(SelectedData.Rows[i][ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId].ToString());


                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ProjectXUseCaseDataModel();
                data.ProjectXUseCaseId = Convert.ToInt32(SelectedData.Rows[i][ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}			
			}
			return UpdatedData;
		}		

        protected override DataTable GetEntityData(int? entityKey)
        {
            var projectXUseCasedata = new ProjectXUseCaseDataModel();
            projectXUseCasedata.ProjectXUseCaseId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.Search(projectXUseCasedata, SessionVariables.RequestProfile);
            return results;
        }

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectXUseCase;
			PrimaryEntityKey = "ProjectXUseCase";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion	
        
    }
}