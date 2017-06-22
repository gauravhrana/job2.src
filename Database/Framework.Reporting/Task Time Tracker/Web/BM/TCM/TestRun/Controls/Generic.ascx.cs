using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TestCaseManagement.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.TCM.TestRun.Controls
{
    public partial class Generic : ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new TestRunDataModel();

            data.TestRunId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtTestRun = TestCaseManagement.Components.DataAccess.TestRunDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTestRun.Rows.Count == 0)
                {
                    TestCaseManagement.Components.DataAccess.TestRunDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TestCaseManagement.Components.DataAccess.TestRunDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TestRunId;
        }

        public override void SetId(int setId, bool chkTestRunId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTestRunId);
            CoreSystemKey.Enabled = chkTestRunId;
            //txtDescription.Enabled = !chkTestRunId;
            //txtName.Enabled = !chkTestRunId;
            //txtSortOrder.Enabled = !chkTestRunId;
        }

        public void LoadData(int testRunId, bool showId)
        {
            Clear();

            var data = new TestRunDataModel();
			data.TestRunId = testRunId;

            var items = TestCaseManagement.Components.DataAccess.TestRunDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TestRunId;
                oHistoryList.Setup(PrimaryEntity, testRunId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TestRunDataModel();

            SetData(data);
        }

        public void SetData(TestRunDataModel data)
        {
            SystemKeyId = data.TestRunId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTestRunId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestRun;
            PrimaryEntityKey = "TestRun";
            FolderLocationFromRoot = "TestRun";

            PlaceHolderCore = dynTestRunId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTestRunId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}