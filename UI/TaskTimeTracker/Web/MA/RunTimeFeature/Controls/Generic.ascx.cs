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

namespace ApplicationContainer.UI.Web.RunTimeFeature.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new RunTimeFeatureDataModel();

            data.RunTimeFeatureId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                if (!TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
                    TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.RunTimeFeatureId;
        }

        public override void SetId(int setId, bool chkRunTimeFeatureId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkRunTimeFeatureId);
            CoreSystemKey.Enabled = chkRunTimeFeatureId;
            //txtDescription.Enabled = !chkRunTimeFeatureId;
            //txtName.Enabled = !chkRunTimeFeatureId;
            //txtSortOrder.Enabled = !chkRunTimeFeatureId;
        }

        public void LoadData(int runTimeFeatureId, bool showId)
        {
            Clear();

            var data = new RunTimeFeatureDataModel();
			data.RunTimeFeatureId = runTimeFeatureId;

            var items = TaskTimeTracker.Components.BusinessLayer.Feature.RunTimeFeatureDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.RunTimeFeatureId;
                oHistoryList.Setup(PrimaryEntity, runTimeFeatureId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new RunTimeFeatureDataModel();

            SetData(data);
        }

        public void SetData(RunTimeFeatureDataModel data)
        {
            SystemKeyId = data.RunTimeFeatureId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblRunTimeFeatureId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.RunTimeFeature;
            PrimaryEntityKey = "RunTimeFeature";
            FolderLocationFromRoot = "RunTimeFeature";

            PlaceHolderCore = dynRunTimeFeatureId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtRunTimeFeatureId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}