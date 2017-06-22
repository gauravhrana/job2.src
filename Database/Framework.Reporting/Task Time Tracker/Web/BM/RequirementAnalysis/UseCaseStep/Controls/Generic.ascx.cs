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

namespace ApplicationContainer.UI.Web.UseCaseStep.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new UseCaseStepDataModel();

            data.UseCaseStepId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtUseCaseStep = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtUseCaseStep.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.UseCaseStepId;
        }

        public override void SetId(int setId, bool chkUseCaseStepId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkUseCaseStepId);
            CoreSystemKey.Enabled = chkUseCaseStepId;
            //txtDescription.Enabled = !chkUseCaseStepId;
            //txtName.Enabled = !chkUseCaseStepId;
            //txtSortOrder.Enabled = !chkUseCaseStepId;
        }

        public void LoadData(int UseCaseStepId, bool showId)
        {
            Clear();

            var data = new UseCaseStepDataModel();
            data.UseCaseStepId = UseCaseStepId;

            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.UseCaseStepId;
                oHistoryList.Setup(PrimaryEntity, UseCaseStepId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new UseCaseStepDataModel();

            SetData(data);
        }

        public void SetData(UseCaseStepDataModel data)
        {
            SystemKeyId = data.UseCaseStepId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblUseCaseStepId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseStep;
            PrimaryEntityKey = "UseCaseStep";
            FolderLocationFromRoot = "RequirementAnalysis/";

            PlaceHolderCore = dynUseCaseStepId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey            = txtUseCaseStepId;
            CoreControlName          = txtName;
            CoreControlDescription   = txtDescription;
            CoreControlSortOrder     = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}