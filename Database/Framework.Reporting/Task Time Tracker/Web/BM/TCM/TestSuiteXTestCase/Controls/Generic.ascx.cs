using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;

namespace ApplicationContainer.UI.Web.TCM.TestSuiteXTestCase.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? TestSuiteXTestCaseId
        {
            get
            {
                if (txtTestSuiteXTestCaseId.Enabled)
                {
					// review
	                //return -1;//Framework.Components.DefaultDataRules.CheckAndGetApplicationId(txtTestSuiteXTestCaseId.Text);
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTestSuiteXTestCaseId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtTestSuiteXTestCaseId.Text);
                }
            }
			set
			{
				txtTestSuiteXTestCaseId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? TestCaseId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtTestCaseId.Text.Trim());
                else
                    return int.Parse(drpTestCaseList.SelectedItem.Value);
            }
			set
			{
				txtTestCaseId.Text = (value == null) ? String.Empty : value.ToString();
			}

        }

        public int? TestSuiteId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtTestSuiteId.Text.Trim());
                else
                    return int.Parse(drpTestSuiteList.SelectedItem.Value);
            }
			set
			{
				txtTestSuiteId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }
        public int? TestCaseStatusId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtTestCaseStatusId.Text.Trim());
                else
                    return int.Parse(drpTestCaseStatusList.SelectedItem.Value);
            }
			set
			{
				txtTestCaseStatusId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? TestCasePriorityId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtTestCasePriorityId.Text.Trim());
                else
                    return int.Parse(drpTestCasePriorityList.SelectedItem.Value);
            }
			set
			{
				txtTestCasePriorityId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }
   
        #endregion properties

		#region methods

		public override int? Save(string action)
		{
			var data = new TestSuiteXTestCaseDataModel();

			data.TestSuiteXTestCaseId	= TestSuiteXTestCaseId;
			data.TestSuiteId			= TestSuiteId;
			data.TestCaseId				= TestCaseId;
			data.TestCasePriorityId		= TestCasePriorityId;
			data.TestCaseStatusId		= TestCaseStatusId;

			if (action == "Insert")
			{
				var dtTestSuiteXTestCase = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtTestSuiteXTestCase.Rows.Count == 0)
				{
					TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of TestSuiteXTestCaseID ?
			return TestSuiteXTestCaseId;
		}

		public override void SetId(int setId, bool chkTestSuiteXTestCaseId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkTestSuiteXTestCaseId);
			txtTestSuiteXTestCaseId.Enabled = chkTestSuiteXTestCaseId;
			//txtDescription.Enabled = !chkTestSuiteXTestCaseId;
			//txtName.Enabled = !chkTestSuiteXTestCaseId;
			//txtSortOrder.Enabled = !chkTestSuiteXTestCaseId;
		}

		public void LoadData(int testSuiteXTestCaseId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new TestSuiteXTestCaseDataModel();
			data.TestSuiteXTestCaseId = testSuiteXTestCaseId;

			// get data
			var items = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.GetEntityList(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			TestSuiteXTestCaseId	= item.TestSuiteXTestCaseId;
			TestSuiteId				= item.TestSuiteId;
			TestCaseId				= item.TestCaseId;
			TestCaseStatusId		= item.TestCaseStatusId;
			TestCasePriorityId		= item.TestCasePriorityId;

			if (!showId)
			{
				txtTestSuiteXTestCaseId.Text = item.TestSuiteXTestCaseId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, testSuiteXTestCaseId, PrimaryEntityKey);
			}
			else
			{
				txtTestSuiteXTestCaseId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new TestSuiteXTestCaseDataModel();

			TestSuiteXTestCaseId	= data.TestSuiteXTestCaseId;
			TestSuiteId				= data.TestSuiteId;
			TestCaseId				= data.TestCaseId;
			TestCaseStatusId		= data.TestCaseStatusId;
			TestCasePriorityId		= data.TestCasePriorityId;
		}	

		
        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			var TestSuitePriorityTypeData = TestCaseManagement.Components.DataAccess.TestCaseDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(TestSuitePriorityTypeData, drpTestCaseList, StandardDataModel.StandardDataColumns.Name, TestCaseDataModel.DataColumns.TestCaseId);

            var TestSuiteData = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(TestSuiteData, drpTestSuiteList, StandardDataModel.StandardDataColumns.Name, TestSuiteDataModel.DataColumns.TestSuiteId);

            var testCaseStatusData = TestCaseManagement.Components.DataAccess.TestCaseStatusDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(testCaseStatusData, drpTestCaseStatusList,
                StandardDataModel.StandardDataColumns.Name,
                TestCaseStatusDataModel.DataColumns.TestCaseStatusId);

            var testCasePriorityData = TestCaseManagement.Components.DataAccess.TestCasePriorityDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(testCasePriorityData, drpTestCasePriorityList,
                StandardDataModel.StandardDataColumns.Name,
                TestCasePriorityDataModel.DataColumns.TestCasePriorityId);

            if (isTesting)
            {
                drpTestSuiteList.AutoPostBack = true;
                drpTestCaseList.AutoPostBack = true;
                drpTestCasePriorityList.AutoPostBack = true;
                drpTestCaseStatusList.AutoPostBack = true;
                if (drpTestCaseList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTestCaseId.Text.Trim()))
                    {
                        drpTestCaseList.SelectedValue = txtTestCaseId.Text;
                    }
                    else
                    {
                        txtTestCaseId.Text = drpTestCaseList.SelectedItem.Value;
                    }
                }
                if (drpTestSuiteList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTestSuiteId.Text.Trim()))
                    {
                        drpTestSuiteList.SelectedValue = txtTestSuiteId.Text;
                    }
                    else
                    {
                        txtTestSuiteId.Text = drpTestSuiteList.SelectedItem.Value;
                    }
                }
                if (drpTestCaseStatusList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTestCaseStatusId.Text.Trim()))
                    {
                        drpTestCaseStatusList.SelectedValue = txtTestCaseStatusId.Text;
                    }
                    else
                    {
                        txtTestCaseStatusId.Text = drpTestCaseStatusList.SelectedItem.Value;
                    }
                }
                if (drpTestCasePriorityList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtTestCasePriorityId.Text.Trim()))
                    {
                        drpTestCasePriorityList.SelectedValue = txtTestCasePriorityId.Text;
                    }
                    else
                    {
                        txtTestCasePriorityId.Text = drpTestCasePriorityList.SelectedItem.Value;
                    }
                }
                txtTestCaseId.Visible = true;
                txtTestSuiteId.Visible = true;
                txtTestCasePriorityId.Visible = true;
                txtTestCaseStatusId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTestCaseId.Text.Trim()))
                {
                    drpTestCaseList.SelectedValue = txtTestCaseId.Text;
                }
                if (!string.IsNullOrEmpty(txtTestSuiteId.Text.Trim()))
                {
                    drpTestSuiteList.SelectedValue = txtTestSuiteId.Text;
                }
                if (!string.IsNullOrEmpty(txtTestCasePriorityId.Text.Trim()))
                {
                    drpTestCasePriorityList.SelectedValue = txtTestCasePriorityId.Text;
                }
                if (!string.IsNullOrEmpty(txtTestCaseStatusId.Text.Trim()))
                {
                    drpTestCaseStatusList.SelectedValue = txtTestCaseStatusId.Text;
                }
            }
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                txtTestSuiteXTestCaseId.Visible = isTesting;
                lblTestSuiteXTestCaseId.Visible = isTesting;
                SetupDropdown();
            }
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "TestSuiteXTestCase";
			FolderLocationFromRoot = "TCM/TestSuiteXTestCase";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCase;

			// set object variable reference            
			PlaceHolderCore = dynTestSuiteXTestCaseId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

        protected void drpTestCaseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTestCaseId.Text = drpTestCaseList.SelectedItem.Value;
        }

        protected void drpTestSuiteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTestSuiteId.Text = drpTestSuiteList.SelectedItem.Value;
        }
        protected void drpTestCasePriorityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTestCasePriorityId.Text = drpTestCasePriorityList.SelectedItem.Value;
        }
        protected void drpTestCaseStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTestCaseStatusId.Text = drpTestCaseStatusList.SelectedItem.Value;
        }

        #endregion

    }
}