using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.MilestoneFeatureState.Controls
{
    public partial class Generic : ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new MilestoneFeatureStateDataModel();

            data.MilestoneFeatureStateId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtMilestoneFeatureState = MilestoneFeatureStateDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtMilestoneFeatureState.Rows.Count == 0)
                {
                    MilestoneFeatureStateDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                MilestoneFeatureStateDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.MilestoneFeatureStateId;
        }

        public override void SetId(int setId, bool chkMilestoneFeatureStateId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkMilestoneFeatureStateId);
            CoreSystemKey.Enabled = chkMilestoneFeatureStateId;
            //txtDescription.Enabled = !chkMilestoneFeatureStateId;
            //txtName.Enabled = !chkMilestoneFeatureStateId;
            //txtSortOrder.Enabled = !chkMilestoneFeatureStateId;
        }

        public void LoadData(int milestoneFeatureStateId, bool showId)
        {
            Clear();

            var data = new MilestoneFeatureStateDataModel();
			data.MilestoneFeatureStateId = milestoneFeatureStateId;

            var items = MilestoneFeatureStateDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.MilestoneFeatureStateId;
                oHistoryList.Setup(PrimaryEntity, milestoneFeatureStateId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new MilestoneFeatureStateDataModel();

            SetData(data);
        }

        public void SetData(MilestoneFeatureStateDataModel data)
        {
            SystemKeyId = data.MilestoneFeatureStateId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblMilestoneFeatureStateId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.MilestoneFeatureState;
            PrimaryEntityKey = "MilestoneFeatureState";
            FolderLocationFromRoot = "MilestoneFeatureState";

            PlaceHolderCore = dynMilestoneFeatureStateId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtMilestoneFeatureStateId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}