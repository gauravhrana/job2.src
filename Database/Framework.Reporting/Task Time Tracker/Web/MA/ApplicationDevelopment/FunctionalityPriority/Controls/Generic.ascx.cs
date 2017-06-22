using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityPriority.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new FunctionalityPriorityDataModel();

            data.FunctionalityPriorityId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtFunctionalityPriority = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityPriorityDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtFunctionalityPriority.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityPriorityDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityPriorityDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.FunctionalityPriorityId;
        }

        public override void SetId(int setId, bool chkFunctionalityPriorityId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFunctionalityPriorityId);
            CoreSystemKey.Enabled = chkFunctionalityPriorityId;
            //txtDescription.Enabled = !chkFunctionalityPriorityId;
            //txtName.Enabled = !chkFunctionalityPriorityId;
            //txtSortOrder.Enabled = !chkFunctionalityPriorityId;
        }

        public void LoadData(int functionalityPriorityId, bool showId)
        {
            Clear();

            var data = new FunctionalityPriorityDataModel();
            data.FunctionalityPriorityId = functionalityPriorityId;

            var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityPriorityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.FunctionalityPriorityId;
                oHistoryList.Setup(PrimaryEntity, functionalityPriorityId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FunctionalityPriorityDataModel();

            SetData(data);
        }

        public void SetData(FunctionalityPriorityDataModel data)
        {
            SystemKeyId = data.FunctionalityPriorityId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblFunctionalityPriorityId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityPriority;
            PrimaryEntityKey = "FunctionalityPriority";
            FolderLocationFromRoot = "FunctionalityPriority";

            PlaceHolderCore = dynFunctionalityPriorityId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtFunctionalityPriorityId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}