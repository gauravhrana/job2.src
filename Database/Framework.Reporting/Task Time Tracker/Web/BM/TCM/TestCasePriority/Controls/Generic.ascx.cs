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

namespace ApplicationContainer.UI.Web.TCM.TestCasePriority.Controls
{
    public partial class Generic : ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new TestCasePriorityDataModel();

            data.TestCasePriorityId      = SystemKeyId;
            data.Name                    = Name;
            data.Description             = Description;
            data.SortOrder               = SortOrder;

            if (action == "Insert")
            {
                var dtTestCasePriority = TestCaseManagement.Components.DataAccess.TestCasePriorityDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTestCasePriority.Rows.Count == 0)
                {
                    TestCaseManagement.Components.DataAccess.TestCasePriorityDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TestCaseManagement.Components.DataAccess.TestCasePriorityDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TestCasePriorityId;
        }

        public override void SetId(int setId, bool chkTestCasePriorityId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTestCasePriorityId);
            CoreSystemKey.Enabled = chkTestCasePriorityId;
            //txtDescription.Enabled = !chkTestCasePriorityId;
            //txtName.Enabled = !chkTestCasePriorityId;
            //txtSortOrder.Enabled = !chkTestCasePriorityId;
        }

        public void LoadData(int testCasePriorityId, bool showId)
        {
            Clear();

            var data = new TestCasePriorityDataModel();
			data.TestCasePriorityId = testCasePriorityId;

            var items = TestCaseManagement.Components.DataAccess.TestCasePriorityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TestCasePriorityId;
                oHistoryList.Setup(PrimaryEntity, testCasePriorityId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TestCasePriorityDataModel();

            SetData(data);
        }

        public void SetData(TestCasePriorityDataModel data)
        {
            SystemKeyId = data.TestCasePriorityId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTestCasePriorityId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestCasePriority;
            PrimaryEntityKey = "TestCasePriority";
            FolderLocationFromRoot = "TestCasePriority";

            PlaceHolderCore = dynTestCasePriorityId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTestCasePriorityId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}