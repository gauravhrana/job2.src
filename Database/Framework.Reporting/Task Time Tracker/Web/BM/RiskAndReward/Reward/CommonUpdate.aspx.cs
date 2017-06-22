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

namespace ApplicationContainer.UI.Web.RiskAndReward.Reward
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new RewardDataModel();
            UpdatedData = RewardDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.RewardId =
                    Convert.ToInt32(SelectedData.Rows[i][RewardDataModel.DataColumns.RewardId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                RewardDataManager.Update(data, SessionVariables.RequestProfile);
                data = new RewardDataModel();
                data.RewardId = Convert.ToInt32(SelectedData.Rows[i][RewardDataModel.DataColumns.RewardId].ToString());
                var dt = RewardDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var rewarddata = new RewardDataModel();
            rewarddata.RewardId = entityKey;
            var results = RewardDataManager.Search(rewarddata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Reward;
            PrimaryEntityKey = "Reward";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}