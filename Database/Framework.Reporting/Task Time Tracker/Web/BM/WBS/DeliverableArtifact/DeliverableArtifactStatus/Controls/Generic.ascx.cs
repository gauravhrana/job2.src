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

namespace ApplicationContainer.UI.Web.DeliverableArtifactStatus.Controls
{
    public partial class Generic : ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new DeliverableArtifactStatusDataModel();

            data.DeliverableArtifactStatusId     = SystemKeyId;
            data.Name                            = Name;
            data.Description                     = Description;
            data.SortOrder                       = SortOrder;

            if (action == "Insert")
            {
                var dtDeliverableArtifactStatus = DeliverableArtifactStatusDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtDeliverableArtifactStatus.Rows.Count == 0)
                {
                    DeliverableArtifactStatusDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                DeliverableArtifactStatusDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.DeliverableArtifactStatusId;
        }

        public override void SetId(int setId, bool chkDeliverableArtifactStatusId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkDeliverableArtifactStatusId);
            CoreSystemKey.Enabled = chkDeliverableArtifactStatusId;
            //txtDescription.Enabled = !chkDeliverableArtifactStatusId;
            //txtName.Enabled = !chkDeliverableArtifactStatusId;
            //txtSortOrder.Enabled = !chkDeliverableArtifactStatusId;
        }

        public void LoadData(int deliverableArtifactStatusId, bool showId)
        {
            Clear();

            var data = new DeliverableArtifactStatusDataModel();
			data.DeliverableArtifactStatusId = deliverableArtifactStatusId;

            var items = DeliverableArtifactStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.DeliverableArtifactStatusId;
                oHistoryList.Setup(PrimaryEntity, deliverableArtifactStatusId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new DeliverableArtifactStatusDataModel();

            SetData(data);
        }

        public void SetData(DeliverableArtifactStatusDataModel data)
        {
            SystemKeyId = data.DeliverableArtifactStatusId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblDeliverableArtifactStatusId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.DeliverableArtifactStatus;
            PrimaryEntityKey = "DeliverableArtifactStatus";
            FolderLocationFromRoot = "DeliverableArtifactStatus";

            PlaceHolderCore = dynDeliverableArtifactStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtDeliverableArtifactStatusId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}