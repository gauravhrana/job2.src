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

namespace ApplicationContainer.UI.Web.RequirementAnalysis.UseCaseActor
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new UseCaseActorDataModel();
            UpdatedData = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.UseCaseActorId =
                    Convert.ToInt32(SelectedData.Rows[i][UseCaseActorDataModel.DataColumns.UseCaseActorId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.Update(data, SessionVariables.RequestProfile);
                data = new UseCaseActorDataModel();
                data.UseCaseActorId = Convert.ToInt32(SelectedData.Rows[i][UseCaseActorDataModel.DataColumns.UseCaseActorId].ToString());
                var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }       

        protected override DataTable GetEntityData(int? entityKey)
        {
            var useCaseActordata = new UseCaseActorDataModel();
            useCaseActordata.UseCaseActorId = entityKey;
            var results = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.Search(useCaseActordata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseActor;
            PrimaryEntityKey = "UseCaseActor";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}