using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.DeliverableArtifact.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new DeliverableArtifactDataModel();

            data.DeliverableArtifactId   = SystemKeyId;
            data.Name                    = Name;
            data.Description             = Description;
            data.SortOrder               = SortOrder;

            if (action == "Insert")
            {
                var dtDeliverableArtifact = DeliverableArtifactDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtDeliverableArtifact.Rows.Count == 0)
                {
                    DeliverableArtifactDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                DeliverableArtifactDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.DeliverableArtifactId;
        }

        public override void SetId(int setId, bool chkDeliverableArtifactId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkDeliverableArtifactId);
            CoreSystemKey.Enabled = chkDeliverableArtifactId;
            //txtDescription.Enabled = !chkDeliverableArtifactId;
            //txtName.Enabled = !chkDeliverableArtifactId;
            //txtSortOrder.Enabled = !chkDeliverableArtifactId;
        }

        public void LoadData(int deliverableArtifactId, bool showId)
        {
            Clear();

            var data = new DeliverableArtifactDataModel();
			data.DeliverableArtifactId = deliverableArtifactId;

            var items = DeliverableArtifactDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.DeliverableArtifactId;
                oHistoryList.Setup(PrimaryEntity, deliverableArtifactId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new DeliverableArtifactDataModel();

            SetData(data);
        }

        public void SetData(DeliverableArtifactDataModel data)
        {
            SystemKeyId = data.DeliverableArtifactId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblDeliverableArtifactId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity            = Framework.Components.DataAccess.SystemEntity.DeliverableArtifact;
            PrimaryEntityKey         = "DeliverableArtifact";
            FolderLocationFromRoot   = "DeliverableArtifact";

            PlaceHolderCore         = dynDeliverableArtifactId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv               = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey           = txtDeliverableArtifactId;
            CoreControlName         = txtName;
            CoreControlDescription  = txtDescription;
            CoreControlSortOrder    = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}