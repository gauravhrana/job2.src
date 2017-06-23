using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Framework.Components.Core;
using System.Collections.Specialized;

namespace Shared.UI.Web.Admin.Controls
{
    public partial class FieldConfigurationSettings : Shared.UI.WebFramework.BaseControl
    {

        #region Properties

        string EntityName
        {
            get
            {
                return Convert.ToString(ViewState["EntityName"]);
            }
            set
            {
                ViewState["EntityName"] = value;
            }
        }

        int SystemEntityTypeId
        {
            get
            {
                return Convert.ToInt32(ViewState["SystemEntityTypeId"]);
            }
            set
            {
                ViewState["SystemEntityTypeId"] = value;
            }
        }

        #endregion

        #region Methods

        public List<SystemEntityTypeDataModel> GetApplicableSystemEntities(int ApplicationId)
        {
			var dt3 = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
            var entitiesdt = new List<SystemEntityTypeDataModel>();

            var dt = FieldConfigurationUtility.GetFieldConfigurations(null, null, string.Empty);

            var IsRequired = false;
            for (var i = 0; i < dt3.Count; i++)
            {
                var systementitytypeId = dt3[i].SystemEntityTypeId.Value;
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    if (systementitytypeId.ToString().Equals
                        (dt.Rows[j][FieldConfigurationDataModel.DataColumns.SystemEntityTypeId].ToString()))
                    {
                        IsRequired = true;
                    }
                }
                if (IsRequired)
                {
                    entitiesdt.Add(dt3[i]);
                    IsRequired = false;
                }

            }
            
            entitiesdt = entitiesdt.OrderBy(x => x.EntityName).ToList();
            return entitiesdt;
        }

        public static ListItemCollection GetListItemsFromEnum(Type enumType)
        {
            // container to be returned
            var items = new ListItemCollection();
            // break down the enumerator items into key/value pairs
            string[] names = Enum.GetNames(enumType);
            Array values = Enum.GetValues(enumType);
            // piece together the key/pairs into the listitem collection
            for (var i = 0; i <= names.Length - 1; i++)
            {
                items.Add(new ListItem(names[i], values.GetValue(i).ToString()));
            }
            // return it
            return items;
        }

        private int PopulateddlFCMode()
        {
            var systemEntity = Framework.Components.DataAccess.Helper.GetSystemEntity(ddlSystemEntityType.SelectedItem.Text);
            var dt = FieldConfigurationUtility.GetApplicableModesList(systemEntity);
            var modeselected = -1;
            if (dt.Rows.Count > 0)
            {
                ddlFCMode.DataSource = dt;
                ddlFCMode.DataTextField = StandardDataModel.StandardDataColumns.Name;
                ddlFCMode.DataValueField = FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId;
                ddlFCMode.DataBind();
                modeselected = int.Parse(ddlFCMode.SelectedValue);
            }
            else
            {
                ddlFCMode.Visible = false;
            }

            return modeselected;
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

        private void BindDropDowns()
        {
			var configScript = GetKendoComboBoxConfigScript("GetApplicationList", "Name", "ApplicationId", txtApplicationList);

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ApplicationId", configScript, true);

			configScript = GetKendoComboBoxConfigScript("GetSystemEntityList", "EntityName", "SystemEntityTypeId", txtEntity);

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "SystemEntityTypeId", configScript, true);
			
			//configScript = GetKendoComboBoxConfigScript("GetApplicationUserList", "FullName", "ApplicationUserId", txtApplicationUser);

			//ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ApplicationUserId", configScript, true);
			
			configScript = GetKendoComboBoxConfigScript("GetRelativeApplicationUserList", "FirstName", "ApplicationUserId", txtRelativeUser);

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ApplicationUserId", configScript, true);
			
			configScript = GetKendoComboBoxConfigScript("GetApplicationRoleList", "Name", "ApplicationRoleId", txtApplicationRole);

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ApplicationRoleId", configScript, true);
			
			configScript = GetKendoComboBoxConfigScript("GetFieldConfigurationModeList", "Name", "ApplicationModeId", txtMode);

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "ApplicationModeId", configScript, true);
			
			configScript = GetKendoComboBoxConfigScript("GetFieldConfigurationModeCategoryList", "Name", "FieldConfigurationModeCategoryId", txtModeCategory);

			ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "kendoScript" + "FieldConfigurationModeCategoryId", configScript, true);

			
			var dtApplication = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(dtApplication, ddlApplication, StandardDataModel.StandardDataColumns.Name,
                BaseDataModel.BaseDataColumns.ApplicationId);

            ddlApplication.SelectedValue = SessionVariables.RequestProfile.ApplicationId.ToString();

            var systemEntity = Framework.Components.DataAccess.Helper.GetSystemEntity(EntityName);
            var dtFCMode = FieldConfigurationUtility.GetApplicableModesList(systemEntity);
            UIHelper.LoadDropDown(dtFCMode, ddlFCMode, StandardDataModel.StandardDataColumns.Name,
                FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId);

			var dtApplicationUser = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(dtApplicationUser, ddlRelativeApplicationUser, ApplicationUserDataModel.DataColumns.ApplicationUserName,
                ApplicationUserDataModel.DataColumns.ApplicationUserId);

            UIHelper.LoadDropDown(dtApplicationUser, ddlApplicationUser, ApplicationUserDataModel.DataColumns.ApplicationUserName,
                ApplicationUserDataModel.DataColumns.ApplicationUserId);

			var dtApplicationRole = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(dtApplicationRole, ddlApplicationRole, StandardDataModel.StandardDataColumns.Name,
                ApplicationRoleDataModel.DataColumns.ApplicationRoleId);

			var dtFCModeCategory = FieldConfigurationModeCategoryDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(dtFCModeCategory, ddlFCModeCategory, StandardDataModel.StandardDataColumns.Name,
                FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId);

            var dtSystemEntities = GetApplicableSystemEntities(int.Parse(ddlApplication.SelectedValue));
            UIHelper.LoadDropDown(dtSystemEntities, ddlSystemEntityType, SystemEntityTypeDataModel.DataColumns.EntityName,
                SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

            ddlSystemEntityType.SelectedValue = Convert.ToString(ddlSystemEntityType.Items.FindByText(EntityName).Value);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (rbtnList.SelectedValue.Equals("GridView"))
                {
                    eSettingsGrid.Visible = true;
                    eSettingsRepeater.Visible = false;

                }
                else if (rbtnList.SelectedValue.Equals("Repeater"))
                {
                    eSettingsGrid.Visible = false;
                    eSettingsRepeater.Visible = true;
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            EntityName = Convert.ToString(Page.RouteData.Values["EntityName"]);
            SystemEntityTypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), EntityName);

            BindDropDowns();

            if (string.IsNullOrEmpty(ddlFCMode.SelectedValue) && string.IsNullOrEmpty(ddlApplication.SelectedValue))
            {
                ddlApplication.SelectedValue = SessionVariables.RequestProfile.ApplicationId.ToString();
                if (ViewState["ApplicationId"] != null)
                {
                    ddlApplication.SelectedValue = ViewState["ApplicationId"].ToString();
                }

                if (Session["FCMode"] != null)
                {
                    ddlFCMode.SelectedValue = Session["FCMode"].ToString();
                }
            }

            eSettingsRepeater.SetUp(SystemEntityTypeId, EntityName, int.Parse(ddlFCMode.SelectedValue),
                                int.Parse(ddlApplication.SelectedValue), ddlFCMode.SelectedItem.Text);

            eSettingsGrid.SetUp(SystemEntityTypeId, EntityName, int.Parse(ddlFCMode.SelectedValue),
                                int.Parse(ddlApplication.SelectedValue));
        }

        protected void rbtnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entityName = Convert.ToString(Page.RouteData.Values["EntityName"]);
            var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), entityName);
            if (rbtnList.SelectedValue.Equals("GridView"))
            {
                eSettingsGrid.Visible = true;
                eSettingsRepeater.Visible = false;

            }
            else if (rbtnList.SelectedValue.Equals("Repeater"))
            {
                eSettingsGrid.Visible = false;
                eSettingsRepeater.Visible = true;
            }
            eSettingsGrid.SetUp(systementitytypeId, entityName, int.Parse(ddlFCMode.SelectedValue),
                                        int.Parse(ddlApplication.SelectedValue));
            eSettingsRepeater.SetUp(systementitytypeId, entityName, int.Parse(ddlFCMode.SelectedValue),
                                int.Parse(ddlApplication.SelectedValue), ddlFCMode.SelectedItem.Text);
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void btnGetColumns_Click(object sender, EventArgs e)
        {
            var entityName = Convert.ToString(Page.RouteData.Values["EntityName"]);
            var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue);
            var modeselected = PopulateddlFCMode();
            eSettingsGrid.SetUp(systementitytypeId, entityName, modeselected, int.Parse(ddlApplication.SelectedValue));
            eSettingsRepeater.SetUp(systementitytypeId, entityName, modeselected, int.Parse(ddlApplication.SelectedValue), ddlFCMode.SelectedItem.Text);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            var entityName = ddlSystemEntityType.SelectedItem.Text;
            Response.Redirect(Page.GetRouteUrl(entityName + "EntityRoute", new { Action = "Default" }), false);
        }

        protected void lnkbtnAddRow_Click(object sender, EventArgs e)
        {
            AddRowPanel.Visible = true;
            genericList.FormatFields(true, int.Parse(ddlSystemEntityType.SelectedValue), int.Parse(ddlFCMode.SelectedValue));
        }

        protected void lnkbtnDeleteRow_Click(object sender, EventArgs e)
        {
            AddRowPanel.Visible = false;

        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {

            genericList.Save("Insert");

            var entityName = Convert.ToString(Page.RouteData.Values["EntityName"]);
            var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue);
            var modeselected = PopulateddlFCMode();
            eSettingsGrid.SetUp(systementitytypeId, entityName, modeselected,
                                int.Parse(ddlApplication.SelectedValue));
            eSettingsRepeater.SetUp(systementitytypeId, entityName, modeselected,
                                int.Parse(ddlApplication.SelectedValue), ddlFCMode.SelectedItem.Text);

            AddRowPanel.Visible = false;

        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            AddRowPanel.Visible = false;
        }

        protected void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            var applicationId = Convert.ToInt32(ddlApplication.SelectedValue);
            ddlSystemEntityType.DataSource = GetApplicableSystemEntities(applicationId);
            ddlSystemEntityType.DataTextField = "EntityName";
            ddlSystemEntityType.DataValueField = "SystemEntityTypeId";
            ddlSystemEntityType.DataBind();
            var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue);
            var modeselected = PopulateddlFCMode();
            eSettingsGrid.SetUp(
                    Convert.ToInt32(ddlSystemEntityType.SelectedValue),
                    ddlSystemEntityType.SelectedItem.Text, modeselected, applicationId);
            eSettingsRepeater.SetUp(
                    Convert.ToInt32(ddlSystemEntityType.SelectedValue),
                    ddlSystemEntityType.SelectedItem.Text, modeselected, applicationId, ddlFCMode.SelectedItem.Text);

        }

        protected void ddlSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entityFolder = "";
            var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue);
            var modeselected = PopulateddlFCMode();
            eSettingsGrid.SetUp(
                    (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue),
                    ddlSystemEntityType.SelectedItem.Text, modeselected, int.Parse(ddlApplication.SelectedValue));
            eSettingsRepeater.SetUp(
                    Convert.ToInt32(ddlSystemEntityType.SelectedValue),
                    ddlSystemEntityType.SelectedItem.Text, modeselected, int.Parse(ddlApplication.SelectedValue), ddlFCMode.SelectedItem.Text);

        }

        protected void ddlFCMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entityFolder = "";
            Session["FCMode"] = ddlFCMode.SelectedValue;
            eSettingsGrid.SetUp((int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue),
                ddlSystemEntityType.SelectedItem.Text, int.Parse(ddlFCMode.SelectedValue), int.Parse(ddlApplication.SelectedValue));
            eSettingsRepeater.SetUp((int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue),
                ddlSystemEntityType.SelectedItem.Text, int.Parse(ddlFCMode.SelectedValue), int.Parse(ddlApplication.SelectedValue), ddlFCMode.SelectedItem.Text);
        }

        protected void ddlRelativeApplicationUser_SelectedIndexChanged(object sender, EventArgs e) { }

        protected void ddlFCModeCategory_SelectedIndexChanged(object sender, EventArgs e) { }

        protected void ddlApplicationRole_SelectedIndexChanged(object sender, EventArgs e) { }

        protected void ddlApplicationUser_SelectedIndexChanged(object sender, EventArgs e) { }

        protected void lnkCommonUpdate_Click(object sender, EventArgs e)
        {
            var fcModedId = int.Parse(ddlFCMode.SelectedValue);
            
            // retrieve records for which SuperKey is to be generated.
            var dtFieldConfiguration = FieldConfigurationUtility.GetFieldConfigurations(SystemEntityTypeId, fcModedId, string.Empty);

            // collection of records of id for which SuperKey is to be generated.
            var sc = new StringCollection();
            foreach (DataRow dr in dtFieldConfiguration.Rows)
            {
                sc.Add(dr[FieldConfigurationDataModel.DataColumns.FieldConfigurationId].ToString());
            }

            // generate superkey
            var superKeyId = ApplicationCommon.GenerateSuperKey(sc, Framework.Components.DataAccess.SystemEntityExt.Value(Framework.Components.DataAccess.SystemEntity.FieldConfiguration));
            
            // Redirect to Common Update Page
            Response.Redirect(Page.GetRouteUrl("FieldConfigurationEntityRouteSuperKey", new { Action = "CommonUpdate", SuperKey = superKeyId }));
        }

        #endregion

    }
}