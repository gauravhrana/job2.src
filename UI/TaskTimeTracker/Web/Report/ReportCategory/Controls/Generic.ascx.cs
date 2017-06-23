using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.ReportCategory.Controls
{
    public partial class Generic : ControlGenericStandard
    {
        #region properties

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

        #region methods

        public override int? Save(string action)
        {
            var data = new ReportCategoryDataModel();

            data.ReportCategoryId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;           
            data.SortOrder = SortOrder;
            data.ApplicationId = ApplicationId;


            if (action == "Insert")
            {
                if(!Framework.Components.Core.ReportCategoryDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
                    Framework.Components.Core.ReportCategoryDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                Framework.Components.Core.ReportCategoryDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ReportCategoryId;
        }

        public override void SetId(int setId, bool chkReportCategoryId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkReportCategoryId);
            CoreSystemKey.Enabled = chkReportCategoryId;
            //txtDescription.Enabled = !chkReportCategoryId;
            //txtName.Enabled = !chkReportCategoryId;
            //txtSortOrder.Enabled = !chkReportCategoryId;
        }

        public void LoadData(int ReportCategoryId, bool showId)
        {
            Clear();

            var data = new ReportCategoryDataModel();
            data.ReportCategoryId = ReportCategoryId;
            var items = Framework.Components.Core.ReportCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;
            var item = items[0];

            ApplicationId = item.ApplicationId;

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.ReportCategoryId;
                oHistoryList.Setup(PrimaryEntity, ReportCategoryId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ReportCategoryDataModel();

            SetData(data);

        }

        public void SetData(ReportCategoryDataModel data)
        {
            SystemKeyId = data.ReportCategoryId;

            base.SetData(data);
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


        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblReportCategoryId.Visible = isTesting;

            if (!IsPostBack)
            {
                SetupDropdown();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity            = Framework.Components.DataAccess.SystemEntity.ReportCategory;
            PrimaryEntityKey         = "ReportCategory";
            FolderLocationFromRoot   = "ReportCategory";

            PlaceHolderCore          = dynReportCategoryId;
            PlaceHolderAuditHistory  = dynAuditHistory;
            BorderDiv                = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey            = txtReportCategoryId;
            CoreControlName          = txtName;
            CoreControlDescription   = txtDescription;
            CoreControlSortOrder     = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}