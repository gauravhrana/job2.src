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

namespace ApplicationContainer.UI.Web.TCM.TestSuite.Controls
{
    public partial class Generic : ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new TestSuiteDataModel();

            data.TestSuiteId    = SystemKeyId;
            data.Name           = Name;
            data.Description    = Description;
            data.SortOrder      = SortOrder;

            if (action == "Insert")
            {
                var dtTestSuite = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTestSuite.Rows.Count == 0)
                {
                    TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TestSuiteId;
        }

        public override void SetId(int setId, bool chkTestSuiteId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTestSuiteId);
            CoreSystemKey.Enabled = chkTestSuiteId;
            //txtDescription.Enabled = !chkTestSuiteId;
            //txtName.Enabled = !chkTestSuiteId;
            //txtSortOrder.Enabled = !chkTestSuiteId;
        }

        public void LoadData(int testSuiteId, bool showId)
        {
            Clear();

            var data = new TestSuiteDataModel();
			data.TestSuiteId = testSuiteId;

            var items = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TestSuiteId;
                oHistoryList.Setup(PrimaryEntity, testSuiteId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TestSuiteDataModel();

            SetData(data);
        }

        public void SetData(TestSuiteDataModel data)
        {
            SystemKeyId = data.TestSuiteId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTestSuiteId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestSuite;
            PrimaryEntityKey = "TestSuite";
            FolderLocationFromRoot = "TestSuite";

            PlaceHolderCore = dynTestSuiteId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTestSuiteId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}