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

namespace ApplicationContainer.UI.Web.RiskAndReward.Risk.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new RiskDataModel();

            data.RiskId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtRisk = RiskDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtRisk.Rows.Count == 0)
                {
                    RiskDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                RiskDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.RiskId;
        }

        public override void SetId(int setId, bool chkRiskId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkRiskId);
            CoreSystemKey.Enabled = chkRiskId;
            //txtDescription.Enabled = !chkRiskId;
            //txtName.Enabled = !chkRiskId;
            //txtSortOrder.Enabled = !chkRiskId;
        }

        public void LoadData(int rewardId, bool showId)
        {
            Clear();

            var data = new RiskDataModel();
			data.RiskId = rewardId;

            var items = RiskDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.RiskId;
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

            var data = new RiskDataModel();

            SetData(data);
        }

        public void SetData(RiskDataModel data)
        {
            SystemKeyId = data.RiskId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblRiskId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Risk;
            PrimaryEntityKey = "Risk";
            FolderLocationFromRoot = "Risk";

            PlaceHolderCore = dynRiskId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtRiskId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}