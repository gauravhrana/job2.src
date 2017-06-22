using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RiskReward;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.RiskAndReward.Reward.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new RewardDataModel();

            data.RewardId       = SystemKeyId;
            data.Name           = Name;
            data.Description    = Description;
            data.SortOrder      = SortOrder;

            if (action == "Insert")
            {
                var dtReward = RewardDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtReward.Rows.Count == 0)
                {
                    RewardDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                RewardDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.RewardId;
        }

        public override void SetId(int setId, bool chkRewardId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkRewardId);
            CoreSystemKey.Enabled = chkRewardId;
            //txtDescription.Enabled = !chkRewardId;
            //txtName.Enabled = !chkRewardId;
            //txtSortOrder.Enabled = !chkRewardId;
        }

        public void LoadData(int rewardId, bool showId)
        {
            Clear();

            var data = new RewardDataModel();
            data.RewardId = rewardId;

            var items = RewardDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.RewardId;

                oHistoryList.Setup(PrimaryEntity, rewardId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();
            var data = new RewardDataModel();
            SetData(data);
        }

		public void SetData(RewardDataModel data)
        {
            SystemKeyId = data.RewardId;
            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblRewardId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity               = Framework.Components.DataAccess.SystemEntity.Reward;
            PrimaryEntityKey            = "Reward";
            FolderLocationFromRoot      = "Reward";

            PlaceHolderCore = dynRewardId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey           = txtRewardId;
            CoreControlName         = txtName;
            CoreControlDescription  = txtDescription;
            CoreControlSortOrder    = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}