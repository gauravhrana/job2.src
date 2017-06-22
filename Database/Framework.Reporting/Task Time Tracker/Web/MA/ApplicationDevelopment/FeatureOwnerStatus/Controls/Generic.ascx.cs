using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.ApplicationDevelopment;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FeatureOwnerStatus.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new FeatureOwnerStatusDataModel();

            data.FeatureOwnerStatusId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
				var dtFeatureOwnerStatus = TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtFeatureOwnerStatus.Rows.Count == 0)
                {
					TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.FeatureOwnerStatusId;
        }

        public override void SetId(int setId, bool chkFeatureOwnerStatusId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFeatureOwnerStatusId);
            CoreSystemKey.Enabled = chkFeatureOwnerStatusId;
            //txtDescription.Enabled = !chkFeatureOwnerStatusId;
            //txtName.Enabled = !chkFeatureOwnerStatusId;
            //txtSortOrder.Enabled = !chkFeatureOwnerStatusId;
        }

        public void LoadData(int featureOwnerStatusId, bool showId)
        {
            Clear();

            var data = new FeatureOwnerStatusDataModel();
            data.FeatureOwnerStatusId = SystemKeyId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FeatureOwnerStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.FeatureOwnerStatusId;
                oHistoryList.Setup(PrimaryEntity, featureOwnerStatusId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FeatureOwnerStatusDataModel();

            SetData(data);
        }

        public void SetData(FeatureOwnerStatusDataModel data)
        {
            SystemKeyId = data.FeatureOwnerStatusId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblFeatureOwnerStatusId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureOwnerStatus;
            PrimaryEntityKey = "FeatureOwnerStatus";
            FolderLocationFromRoot = "FeatureOwnerStatus";

            PlaceHolderCore = dynFeatureOwnerStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtFeatureOwnerStatusId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}