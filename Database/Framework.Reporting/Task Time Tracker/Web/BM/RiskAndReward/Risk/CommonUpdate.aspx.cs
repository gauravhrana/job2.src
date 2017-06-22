using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RiskReward;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using System.Data;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.RiskAndReward.Risk
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new RiskDataModel();
            UpdatedData = RiskDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.RiskId =
                    Convert.ToInt32(SelectedData.Rows[i][RiskDataModel.DataColumns.RiskId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                RiskDataManager.Update(data, SessionVariables.RequestProfile);
                data = new RiskDataModel();
                data.RiskId = Convert.ToInt32(SelectedData.Rows[i][RiskDataModel.DataColumns.RiskId].ToString());
                var dt = RiskDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var riskdata = new RiskDataModel();
            riskdata.RiskId = entityKey;
            var results = RiskDataManager.Search(riskdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Risk;
            PrimaryEntityKey = "Risk";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}