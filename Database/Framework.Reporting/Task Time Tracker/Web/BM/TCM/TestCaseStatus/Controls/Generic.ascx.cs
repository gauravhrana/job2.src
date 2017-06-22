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

namespace ApplicationContainer.UI.Web.TCM.TestCaseStatus.Controls
{
    public partial class Generic : ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new TestCaseStatusDataModel();

            data.TestCaseStatusId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtTestCaseStatus = TestCaseManagement.Components.DataAccess.TestCaseStatusDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTestCaseStatus.Rows.Count == 0)
                {
                    TestCaseManagement.Components.DataAccess.TestCaseStatusDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TestCaseManagement.Components.DataAccess.TestCaseStatusDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TestCaseStatusId;
        }

        public override void SetId(int setId, bool chkTestCaseStatusId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTestCaseStatusId);
            CoreSystemKey.Enabled = chkTestCaseStatusId;
            //txtDescription.Enabled = !chkTestCaseStatusId;
            //txtName.Enabled = !chkTestCaseStatusId;
            //txtSortOrder.Enabled = !chkTestCaseStatusId;
        }

        public void LoadData(int testCaseStatusId, bool showId)
        {
            Clear();

            var data = new TestCaseStatusDataModel();
			data.TestCaseStatusId = testCaseStatusId;

            var items = TestCaseManagement.Components.DataAccess.TestCaseStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TestCaseStatusId;
                oHistoryList.Setup(PrimaryEntity, testCaseStatusId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TestCaseStatusDataModel();

            SetData(data);
        }

        public void SetData(TestCaseStatusDataModel data)
        {
            SystemKeyId = data.TestCaseStatusId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTestCaseStatusId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestCaseStatus;
            PrimaryEntityKey = "TestCaseStatus";
            FolderLocationFromRoot = "TestCaseStatus";

            PlaceHolderCore = dynTestCaseStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTestCaseStatusId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}