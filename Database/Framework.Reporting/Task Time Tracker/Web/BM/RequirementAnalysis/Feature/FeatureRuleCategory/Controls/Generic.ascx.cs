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

namespace ApplicationContainer.UI.Web.FeatureRuleCategory.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new FeatureRuleCategoryDataModel();

            data.FeatureRuleCategoryId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtFeatureRuleCategory = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtFeatureRuleCategory.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.FeatureRuleCategoryId;
        }

        public override void SetId(int setId, bool chkFeatureRuleCategoryId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFeatureRuleCategoryId);
            CoreSystemKey.Enabled = chkFeatureRuleCategoryId;
            //txtDescription.Enabled = !chkFeatureRuleCategoryId;
            //txtName.Enabled = !chkFeatureRuleCategoryId;
            //txtSortOrder.Enabled = !chkFeatureRuleCategoryId;
        }

        public void LoadData(int featureRuleCategoryId, bool showId)
        {
            Clear();

            var data = new FeatureRuleCategoryDataModel();
			data.FeatureRuleCategoryId = featureRuleCategoryId;

            var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.FeatureRuleCategoryId;
                oHistoryList.Setup(PrimaryEntity, featureRuleCategoryId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FeatureRuleCategoryDataModel();

            SetData(data);
        }

        public void SetData(FeatureRuleCategoryDataModel data)
        {
            SystemKeyId = data.FeatureRuleCategoryId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblFeatureRuleCategoryId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRuleCategory;
            PrimaryEntityKey = "FeatureRuleCategory";
            FolderLocationFromRoot = "FeatureRuleCategory";

            PlaceHolderCore = dynFeatureRuleCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtFeatureRuleCategoryId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}