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

namespace ApplicationContainer.UI.Web.RequirementAnalysis.UseCaseXUseCaseStep
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();
            var data = new UseCaseXUseCaseStepDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.UseCaseXUseCaseStepId =
                    Convert.ToInt32(SelectedData.Rows[i][UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId].ToString());

                data.UseCaseStepId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStepId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStepId).ToString())
                    : int.Parse(SelectedData.Rows[i][UseCaseXUseCaseStepDataModel.DataColumns.UseCaseStepId].ToString());

                data.UseCaseId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(UseCaseXUseCaseStepDataModel.DataColumns.UseCaseId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(UseCaseXUseCaseStepDataModel.DataColumns.UseCaseId).ToString())
                    : int.Parse(SelectedData.Rows[i][UseCaseXUseCaseStepDataModel.DataColumns.UseCaseId].ToString());

                data.UseCaseWorkFlowCategoryId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategoryId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategoryId).ToString())
                    : int.Parse(SelectedData.Rows[i][UseCaseXUseCaseStepDataModel.DataColumns.UseCaseWorkFlowCategoryId].ToString());

                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.Update(data, SessionVariables.RequestProfile);
                data = new UseCaseXUseCaseStepDataModel();
                data.UseCaseXUseCaseStepId = Convert.ToInt32(SelectedData.Rows[i][UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }       
        
        protected override DataTable GetEntityData(int? entityKey)
        {
            var useCaseXUseCaseStepdata = new UseCaseXUseCaseStepDataModel();
            useCaseXUseCaseStepdata.UseCaseXUseCaseStepId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.Search(useCaseXUseCaseStepdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseXUseCaseStep;
            PrimaryEntityKey = "UseCaseXUseCaseStep";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}