using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TestCaseManagement.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.TCM.TestCase.Controls
{
    public partial class Generic : ControlGenericStandard
    {
        #region properties

        public int? TestCaseId
        {
            get
            {
                if (txtTestCaseId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtTestCaseId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtTestCaseId.Text);
                }
            }
            set
            {
                txtTestCaseId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? ApplicationId
        {
            get
            {
                return int.Parse(txtApplicationList.Text.Trim());
            }
            set
            {
                txtApplicationList.Text = txtApplicationId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        #endregion

        #region private methods

        public override void SetId(int setId, bool chkTestCaseId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTestCaseId);
            txtTestCaseId.Enabled = chkTestCaseId;
            //txtDescription.Enabled = !chkTestCaseId;
            //txtName.Enabled = !chkTestCaseId;
            //txtSortOrder.Enabled = !chkTestCaseId;
        }

        public override int? Save(string action)
        {
            var data = new TestCaseDataModel();

            data.TestCaseId = TestCaseId;
            data.ApplicationId = ApplicationId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtTestCase = TestCaseDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTestCase.Rows.Count == 0)
                {
                    TestCaseDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TestCaseDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TestCaseId;
        }

        public void LoadData(int testCaseId, bool showId)
        {
            Clear();

            var dataQuery = new TestCaseDataModel();
            dataQuery.TestCaseId = testCaseId;

            var items = TestCaseManagement.Components.DataAccess.TestCaseDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            ApplicationId = item.ApplicationId;
            Name = item.Name;
            Description = item.Description;
            SortOrder = item.SortOrder;

            if (!showId)
            {
                txtTestCaseId.Text = item.TestCaseId.ToString();


                oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.TestCase, testCaseId, "TestCase");

            }
            else
            {
                txtTestCaseId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
        }


        protected override void Clear()
        {
            base.Clear();
            var data = new TestCaseDataModel();
            SetData(data);
        }

        public void SetData(TestCaseDataModel data)
        {
            SystemKeyId = data.TestCaseId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            SetupDropdown();
            CoreSystemKey.Visible = isTesting;
            lblTestCaseId.Visible = isTesting;
        }

        protected void drpApplicationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationId.Text = drpApplicationList.SelectedItem.Value;
        }

        private string GetKendoComboBoxConfigScript(string methodName, string dataTextField, string dataValueField, TextBox txtBoxList, bool addAllItem = true)
        {
            const string stringA = @"
			$(document).ready(function ()
			{{
				var local_url = '{0}';
				var local_element = '#{1}';
				var local_dataTextField = '{2}';
				var local_dataValueField = '{3}';
				var local_addElement = {4};
				libary_kendo_getData(local_url, local_element, local_dataTextField, local_dataValueField, local_addElement);

			}});
			";

            var a = string.Format(stringA
                , ("http://localhost:53331/API/AutoComplete.asmx/" + methodName)
                , txtBoxList.ClientID
                , dataTextField
                , dataValueField
                , addAllItem.ToString().ToLower());

            return a;
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

            var configScript = GetKendoComboBoxConfigScript("GetApplicationList", "Name", "ApplicationId", txtApplicationList);

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ApplicationId", configScript, true);

            if (isTesting)
            {
                drpApplicationList.AutoPostBack = true;
                if (drpApplicationList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
                    {
                        drpApplicationList.SelectedValue = txtApplicationId.Text;
                    }
                    else
                    {
                        txtApplicationId.Text = drpApplicationList.SelectedItem.Value;
                    }
                }
                txtApplicationId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplicationId.Text.Trim()))
                {
                    drpApplicationList.SelectedValue = txtApplicationId.Text;
                }
                txtApplicationId.Visible = false;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.TestCase;
            PrimaryEntityKey = "TestCase";
            FolderLocationFromRoot = "TestCase";

            PlaceHolderCore = dynTestCaseId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTestCaseId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}