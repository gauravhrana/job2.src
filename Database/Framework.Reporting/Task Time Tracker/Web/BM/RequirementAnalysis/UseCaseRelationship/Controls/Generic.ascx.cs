using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.UseCaseRelationship.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new UseCaseRelationshipDataModel();

            data.UseCaseRelationshipId   = SystemKeyId;
            data.Name                    = Name;
            data.Description             = Description;
            data.SortOrder               = SortOrder;

            if (action == "Insert")
            {
                var dtUseCaseRelationship = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseRelationshipDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtUseCaseRelationship.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseRelationshipDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseRelationshipDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.UseCaseRelationshipId;
        }

        public override void SetId(int setId, bool chkUseCaseRelationshipId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkUseCaseRelationshipId);
            CoreSystemKey.Enabled = chkUseCaseRelationshipId;
            //txtDescription.Enabled = !chkUseCaseRelationshipId;
            //txtName.Enabled = !chkUseCaseRelationshipId;
            //txtSortOrder.Enabled = !chkUseCaseRelationshipId;
        }

        public void LoadData(int UseCaseRelationshipId, bool showId)
        {
            Clear();

            var data = new UseCaseRelationshipDataModel();
            data.UseCaseRelationshipId = UseCaseRelationshipId;

            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseRelationshipDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.UseCaseRelationshipId;
                oHistoryList.Setup(PrimaryEntity, UseCaseRelationshipId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new UseCaseRelationshipDataModel();

            SetData(data);
        }

        public void SetData(UseCaseRelationshipDataModel data)
        {
            SystemKeyId = data.UseCaseRelationshipId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblUseCaseRelationshipId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseRelationship;
            PrimaryEntityKey = "UseCaseRelationship";
            FolderLocationFromRoot = "RequirementAnalysis/";

            PlaceHolderCore = dynUseCaseRelationshipId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey            = txtUseCaseRelationshipId;
            CoreControlName          = txtName;
            CoreControlDescription   = txtDescription;
            CoreControlSortOrder     = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}