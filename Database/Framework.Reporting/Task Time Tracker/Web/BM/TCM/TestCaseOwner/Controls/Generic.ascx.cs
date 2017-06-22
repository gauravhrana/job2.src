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

namespace ApplicationContainer.UI.Web.TCM.TestCaseOwner.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {
        #region methods

        public override int? Save(string action)
        {
            var data = new TestCaseOwnerDataModel();

            data.TestCaseOwnerId     = SystemKeyId;
            data.Name                = Name;
            data.Description         = Description;
            data.SortOrder           = SortOrder;

            if (action == "Insert")
            {
                var dtTestCaseOwner = TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTestCaseOwner.Rows.Count == 0)
                {
                    TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TestCaseOwnerId;
        }

        public override void SetId(int setId, bool chkTestCaseOwnerId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTestCaseOwnerId);
            CoreSystemKey.Enabled = chkTestCaseOwnerId;
            //txtDescription.Enabled = !chkTestCaseOwnerId;
            //txtName.Enabled = !chkTestCaseOwnerId;
            //txtSortOrder.Enabled = !chkTestCaseOwnerId;
        }

        public void LoadData(int testCaseOwnerId, bool showId)
        {
            Clear();

            var data = new TestCaseOwnerDataModel();
			data.TestCaseOwnerId = testCaseOwnerId;

            var items = TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];
			
            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TestCaseOwnerId;
                oHistoryList.Setup(PrimaryEntity, testCaseOwnerId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TestCaseOwnerDataModel();

            SetData(data);
        }

        public void SetData(TestCaseOwnerDataModel data)
        {
            SystemKeyId = data.TestCaseOwnerId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTestCaseOwnerId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestCaseOwner;
            PrimaryEntityKey = "TestCaseOwner";
            FolderLocationFromRoot = "TestCaseOwner";

            PlaceHolderCore = dynTestCaseOwnerId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTestCaseOwnerId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}