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

namespace ApplicationContainer.UI.Web.UseCaseWorkFlowCategory.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new UseCaseWorkFlowCategoryDataModel();

            data.UseCaseWorkFlowCategoryId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtUseCaseWorkFlowCategory = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtUseCaseWorkFlowCategory.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.UseCaseWorkFlowCategoryId;
        }

        public override void SetId(int setId, bool chkUseCaseWorkFlowCategoryId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkUseCaseWorkFlowCategoryId);
            CoreSystemKey.Enabled = chkUseCaseWorkFlowCategoryId;
            //txtDescription.Enabled = !chkUseCaseWorkFlowCategoryId;
            //txtName.Enabled = !chkUseCaseWorkFlowCategoryId;
            //txtSortOrder.Enabled = !chkUseCaseWorkFlowCategoryId;
        }

        public void LoadData(int UseCaseWorkFlowCategoryId, bool showId)
        {
            Clear();

            var data = new UseCaseWorkFlowCategoryDataModel();
            data.UseCaseWorkFlowCategoryId = UseCaseWorkFlowCategoryId;

            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];                      

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.UseCaseWorkFlowCategoryId;
                oHistoryList.Setup(PrimaryEntity, UseCaseWorkFlowCategoryId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new UseCaseWorkFlowCategoryDataModel();

            SetData(data);
        }

        public void SetData(UseCaseWorkFlowCategoryDataModel data)
        {
            SystemKeyId = data.UseCaseWorkFlowCategoryId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblUseCaseWorkFlowCategoryId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseWorkFlowCategory;
            PrimaryEntityKey = "UseCaseWorkFlowCategory";
            FolderLocationFromRoot = "RequirementAnalysis/";

            PlaceHolderCore = dynUseCaseWorkFlowCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey            = txtUseCaseWorkFlowCategoryId;
            CoreControlName          = txtName;
            CoreControlDescription   = txtDescription;
            CoreControlSortOrder     = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}