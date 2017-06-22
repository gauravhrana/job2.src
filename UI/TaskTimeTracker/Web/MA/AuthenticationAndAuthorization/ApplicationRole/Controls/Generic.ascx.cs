using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Framework.Components.ApplicationUser;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole.Controls
{
    public partial class Generic : ControlGenericStandard
    {
        #region properties

        public int? ApplicationRoleId
        {
            get
            {
                if (txtApplicationRoleId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationRoleId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtApplicationRoleId.Text);
                }
            }
            set
            {
                txtApplicationRoleId.Text = (value == null) ? String.Empty : value.ToString();
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

        public override void SetId(int setId, bool chkApplicationRoleId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationRoleId);
            txtApplicationRoleId.Enabled = chkApplicationRoleId;
            //txtDescription.Enabled = !chkApplicationRoleId;
            //txtName.Enabled = !chkApplicationRoleId;
            //txtSortOrder.Enabled = !chkApplicationRoleId;
        }

        public override int? Save(string action)
        {
            var data = new ApplicationRoleDataModel();

            data.ApplicationRoleId   = ApplicationRoleId;
            data.ApplicationId       = ApplicationId;           
            data.Name                = Name;
            data.Description         = Description;
            data.SortOrder           = SortOrder;

            if (action == "Insert")
            {
                if(!ApplicationRoleDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
                    ApplicationRoleDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                ApplicationRoleDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ApplicationRoleId;
        }

        public void LoadData(int applicationRoleId, bool showId)
        {
            Clear();

            var dataQuery = new ApplicationRoleDataModel();
            dataQuery.ApplicationRoleId = applicationRoleId;

            var items = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            ApplicationId        = item.ApplicationId;            
            Name                 = item.Name;
            Description          = item.Description;
            SortOrder            = item.SortOrder;

            if (!showId)
            {
                txtApplicationRoleId.Text = item.ApplicationRoleId.ToString();


                oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.ApplicationRole, applicationRoleId, "ApplicationRole");

            }
            else
            {
                txtApplicationRoleId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
        }    
          

        protected override void Clear()
        {
            base.Clear();
            var data = new ApplicationRoleDataModel();
            SetData(data);
        }

        public void SetData(ApplicationRoleDataModel data)
        {
            SystemKeyId = data.ApplicationRoleId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            SetupDropdown();
            CoreSystemKey.Visible = isTesting;
            lblApplicationRoleId.Visible = isTesting;
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

            PrimaryEntity            = SystemEntity.ApplicationRole;
            PrimaryEntityKey         = "ApplicationRole";
            FolderLocationFromRoot   = "ApplicationRole";

            PlaceHolderCore          = dynApplicationRoleId;
            PlaceHolderAuditHistory  = dynAuditHistory;
            BorderDiv                = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey            = txtApplicationRoleId;
            CoreControlName          = txtName;
            CoreControlDescription   = txtDescription;
            CoreControlSortOrder     = txtSortOrder;

            CoreUpdateInfo           = oUpdateInfo;
        }

        #endregion

    }
}