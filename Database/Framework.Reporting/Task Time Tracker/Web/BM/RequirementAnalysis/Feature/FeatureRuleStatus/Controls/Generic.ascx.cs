using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.FeatureRuleStatus.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new FeatureRuleStatusDataModel();

            data.FeatureRuleStatusId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtFeatureRuleStatus = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtFeatureRuleStatus.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.FeatureRuleStatusId;
        }

        public override void SetId(int setId, bool chkFeatureRuleStatusId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFeatureRuleStatusId);
            CoreSystemKey.Enabled = chkFeatureRuleStatusId;
            //txtDescription.Enabled = !chkFeatureRuleStatusId;
            //txtName.Enabled = !chkFeatureRuleStatusId;
            //txtSortOrder.Enabled = !chkFeatureRuleStatusId;
        }

        public void LoadData(int featureRuleStatusId, bool showId)
        {
            Clear();

            var data = new FeatureRuleStatusDataModel();
			data.FeatureRuleStatusId = featureRuleStatusId;

            var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.FeatureRuleStatusId;
                oHistoryList.Setup(PrimaryEntity, featureRuleStatusId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FeatureRuleStatusDataModel();

            SetData(data);
        }

        public void SetData(FeatureRuleStatusDataModel data)
        {
            SystemKeyId = data.FeatureRuleStatusId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblFeatureRuleStatusId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRuleStatus;
            PrimaryEntityKey = "FeatureRuleStatus";
            FolderLocationFromRoot = "FeatureRuleStatus";

            PlaceHolderCore = dynFeatureRuleStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtFeatureRuleStatusId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}