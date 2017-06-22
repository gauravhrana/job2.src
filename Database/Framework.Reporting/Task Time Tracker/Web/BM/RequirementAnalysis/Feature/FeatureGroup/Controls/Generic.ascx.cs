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

namespace ApplicationContainer.UI.Web.FeatureGroup.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new FeatureGroupDataModel();

            data.FeatureGroupId  = SystemKeyId;
            data.Name            = Name;
            data.Description     = Description;
            data.SortOrder       = SortOrder;

            if (action == "Insert")
            {
                var dtFeatureGroup = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtFeatureGroup.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.FeatureGroupId;
        }

        public override void SetId(int setId, bool chkFeatureGroupId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFeatureGroupId);
            CoreSystemKey.Enabled = chkFeatureGroupId;            
        }

        public void LoadData(int featureGroupId, bool showId)
        {
            Clear();

            var data = new FeatureGroupDataModel();
			data.FeatureGroupId = featureGroupId;

            var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureGroupDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.FeatureGroupId;
                oHistoryList.Setup(PrimaryEntity, featureGroupId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FeatureGroupDataModel();

            SetData(data);
        }

        public void SetData(FeatureGroupDataModel data)
        {
            SystemKeyId = data.FeatureGroupId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting             = SessionVariables.IsTesting;
            CoreSystemKey.Visible     = isTesting;
            lblFeatureGroupId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity          = Framework.Components.DataAccess.SystemEntity.FeatureGroup;
            PrimaryEntityKey       = "FeatureGroup";
            FolderLocationFromRoot = "FeatureGroup";

            PlaceHolderCore         = dynFeatureGroupId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv               = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey          = txtFeatureGroupId;
            CoreControlName        = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder   = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}