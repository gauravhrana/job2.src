using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Feature.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new FeatureDataModel();

            data.FeatureId      = SystemKeyId;
            data.Name           = Name;
            data.Description    = Description;
            data.SortOrder      = SortOrder;

            if (action == "Insert")
            {
                var dtFeature = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtFeature.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.FeatureId;
        }

        public override void SetId(int setId, bool chkFeatureId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFeatureId);
            CoreSystemKey.Enabled = chkFeatureId;            
        }

        public void LoadData(int featureId, bool showId)
        {
            Clear();

            var data = new FeatureDataModel();
			data.FeatureId = featureId;

            var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.FeatureId;
                oHistoryList.Setup(PrimaryEntity, featureId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FeatureDataModel();

            SetData(data);
        }

        public void SetData(FeatureDataModel data)
        {
            SystemKeyId = data.FeatureId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblFeatureId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity						= Framework.Components.DataAccess.SystemEntity.Feature;
            PrimaryEntityKey					= "Feature";
            FolderLocationFromRoot				= "Feature";

            PlaceHolderCore						= dynFeatureId;
            PlaceHolderAuditHistory				= dynAuditHistory;
            BorderDiv							= borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey						= txtFeatureId;
            CoreControlName						= txtName;
            CoreControlDescription				= txtDescription;
            CoreControlSortOrder				= txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}