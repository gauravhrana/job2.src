using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectPortfolio
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new ProjectPortfolioDataModel();
            UpdatedData = ProjectPortfolioDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.ProjectPortfolioId =
                    Convert.ToInt32(SelectedData.Rows[i][ProjectPortfolioDataModel.DataColumns.ProjectPortfolioId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                ProjectPortfolioDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ProjectPortfolioDataModel();
                data.ProjectPortfolioId = Convert.ToInt32(SelectedData.Rows[i][ProjectPortfolioDataModel.DataColumns.ProjectPortfolioId].ToString());
                var dt = ProjectPortfolioDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var projectPortfoliodata = new ProjectPortfolioDataModel();
            projectPortfoliodata.ProjectPortfolioId = entityKey;
            var results = ProjectPortfolioDataManager.Search(projectPortfoliodata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = SystemEntity.ProjectPortfolio;
            PrimaryEntityKey = "ProjectPortfolio";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}