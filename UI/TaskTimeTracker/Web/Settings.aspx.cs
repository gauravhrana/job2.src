using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Application;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;

namespace TaskTimeTracker.UI.Web
{
	public partial class Settings : Shared.UI.WebFramework.BasePage
    {

        #region Variables

        private DataTable Modes;

        #endregion

        #region Methods

        private void PopulateDropDown(DropDownList ddl, DataTable dt, string datatextfield, string datavaluefield)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = datatextfield;
            ddl.DataValueField = datavaluefield;
            ddl.DataBind();
        }

        public DataTable GetApplicableSystemEntities(int ApplicationId)
        {
            var dt3 = Framework.Components.Core.SystemEntityTypeDataManager.GetList(AuditId);
            var entitiesdt = dt3.Clone();

            var dt = FieldConfigurationUtility.GetFieldConfigurations(null, null, string.Empty);

            var IsRequired = false;
            for (var i = 0; i < dt3.Rows.Count; i++)
            {
                var systementitytypeId = int.Parse(dt3.Rows[i][SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId].ToString());
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
                    entitiesdt.ImportRow(dt3.Rows[i]);
                    IsRequired = false;
                }

            }
            var dv = entitiesdt.DefaultView;
            dv.Sort = "EntityName ASC";
            return dv.ToTable();
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

        private DataTable GetApplicableModesList(int systemEntityTypeId)
        {
            var columns = FieldConfigurationUtility.GetFieldConfigurations(systemEntityTypeId, null, string.Empty);
            var modes = FieldConfigurationModeDataManager.GetList(SessionVariables.AuditId, Convert.ToInt32(ddlApplication.SelectedValue));
            var modeapplicable = false;
            var validmodes = new DataTable();
            validmodes = modes.Clone();

            for (int j = 0; j < modes.Rows.Count; j++)
            {
                for (var i = 0; i < columns.Rows.Count; i++)
                {

                    if (
                        int.Parse(
                            columns.Rows[i][
                                FieldConfigurationDataModel.DataColumns.FieldConfigurationModeId].
                                ToString()) ==
                        int.Parse(
                            modes.Rows[j][
                                FieldConfigurationModeDataModel.DataColumns.
                                    FieldConfigurationModeId].ToString())
                        )
                    {
                        var temp = validmodes.Select(FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId + " = " + int.Parse(
                            modes.Rows[j][
                                FieldConfigurationModeDataModel.DataColumns.
                                    FieldConfigurationModeId].ToString()));
                        if (temp.Length == 0)
                            validmodes.ImportRow(modes.Rows[j]);


                    }
                }

            }
            return validmodes;

        }

        private int BindFCModes()
        {
            var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue);
            var dt = GetApplicableModesList(systementitytypeId);
            var modeselected = -1;
            if (dt.Rows.Count > 0)
            {
                ddlAEFLMode.DataSource = dt;
                ddlAEFLMode.DataTextField = FieldConfigurationModeDataModel.DataColumns.Name;
                ddlAEFLMode.DataValueField = FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId;
                ddlAEFLMode.DataBind();
                modeselected = int.Parse(ddlAEFLMode.SelectedValue);

            }
            else
            {
                ddlAEFLMode.Visible = false;
            }

            return modeselected;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
		{
			try
			{ 
				if (!IsPostBack)
				{
					var entityName = Request.QueryString["EN"].ToString();
					var entityFolder = Request.QueryString["EF"].ToString();
					var systementitytypeId = (int) Enum.Parse(typeof (Framework.Components.DataAccess.SystemEntity), entityName);
					var dt2 = Framework.Components.ApplicationUser.Application.GetList(AuditId);
					PopulateDropDown(ddlApplication, dt2, ApplicationDataModel.DataColumns.Name,
						ApplicationDataModel.DataColumns.ApplicationId);
					ddlApplication.SelectedValue = Framework.Components.DataAccess.SetupConfiguration.ApplicationId.ToString();
					var dt = GetApplicableModesList(systementitytypeId);
					PopulateDropDown(ddlAEFLMode, dt, FieldConfigurationModeDataModel.DataColumns.Name,
						FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId);
					
                    //eSettingsGrid.SetUp(systementitytypeId, entityName, int.Parse(ddlAEFLMode.SelectedValue),
                    //                    int.Parse(ddlApplication.SelectedValue));
                    eSettingsRepeater.SetUp(systementitytypeId, entityName, int.Parse(ddlAEFLMode.SelectedValue),
                                        int.Parse(ddlApplication.SelectedValue));
					var dt1 = GetApplicableSystemEntities(int.Parse(ddlApplication.SelectedValue));
					PopulateDropDown(ddlSystemEntityType, dt1, SystemEntityTypeDataModel.DataColumns.EntityName, 
						SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
					ddlSystemEntityType.SelectedValue = Convert.ToString(ddlSystemEntityType.Items.FindByText(entityName).Value);
					if (System.Diagnostics.Debugger.IsAttached)
					{
						ApplicationRow.Visible = false;
					}

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

			}
			catch (Exception ex)
			{
				
			}
			
		}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var entityName = Request.QueryString["EN"].ToString();
            var entityFolder = Request.QueryString["EF"].ToString();
            var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), entityName);

            if (string.IsNullOrEmpty(ddlAEFLMode.SelectedValue) && string.IsNullOrEmpty(ddlApplication.SelectedValue))
            {
                var dt2 = Framework.Components.ApplicationUser.Application.GetList(AuditId);
				PopulateDropDown(ddlApplication, dt2, ApplicationDataModel.DataColumns.Name,
					ApplicationDataModel.DataColumns.ApplicationId);
                ddlApplication.SelectedValue = Framework.Components.DataAccess.SetupConfiguration.ApplicationId.ToString();
                if (ViewState["ApplicationId"] != null)
                    ddlApplication.SelectedValue = ViewState["ApplicationId"].ToString();
                var dt = GetApplicableModesList(systementitytypeId);
                PopulateDropDown(ddlAEFLMode, dt, FieldConfigurationModeDataModel.DataColumns.Name,
                    FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId);
                if (Session["AEFLMode"] != null)
                    ddlAEFLMode.SelectedValue = Session["AEFLMode"].ToString();
            }
            eSettingsGrid.SetUp(systementitytypeId, entityName, int.Parse(ddlAEFLMode.SelectedValue),
                                int.Parse(ddlApplication.SelectedValue));
        }

        protected void rbtnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entityName = Request.QueryString["EN"].ToString();
            var entityFolder = Request.QueryString["EF"].ToString();
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
            eSettingsGrid.SetUp(systementitytypeId, entityName, int.Parse(ddlAEFLMode.SelectedValue),
                                        int.Parse(ddlApplication.SelectedValue));
            eSettingsRepeater.SetUp(systementitytypeId, entityName, int.Parse(ddlAEFLMode.SelectedValue),
                                int.Parse(ddlApplication.SelectedValue));
        }

		protected void btnHome_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Default.aspx");
		}

		protected void btnGetColumns_Click(object sender, EventArgs e)
		{
			var entityName = Request.QueryString["EN"].ToString();
			var entityFolder = Request.QueryString["EF"].ToString(); 
			var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue);
			var modeselected = BindFCModes();
			eSettingsGrid.SetUp(systementitytypeId, entityName, modeselected, int.Parse(ddlApplication.SelectedValue));
            eSettingsRepeater.SetUp(systementitytypeId, entityName, modeselected, int.Parse(ddlApplication.SelectedValue));
		}

		protected void btnReturn_Click(object sender, EventArgs e)
		{
			var data = new MenuDataModel();
			data.Value = ddlSystemEntityType.SelectedItem.Text;
			var dt = Framework.Components.Core.MenuDataManager.Search(data, AuditId);
			Response.Redirect(dt.Rows[0][MenuDataModel.DataColumns.NavigateURL].ToString());
		}

		protected void lnkbtnAddRow_Click(object sender, EventArgs e)
		{
			AddRowPanel.Visible = true;
			genericList.FormatFields(true, int.Parse(ddlSystemEntityType.SelectedValue), int.Parse(ddlAEFLMode.SelectedValue));
		}

		protected void lnkbtnDeleteRow_Click(object sender, EventArgs e)
		{
			AddRowPanel.Visible = false;
		
		}

		protected void lnkbtnAdd_Click(object sender, EventArgs e)
		{
			var data = new FieldConfigurationDataModel();
            data.FieldConfigurationId = (int?)genericList.FieldConfigurationId;
			data.Name = genericList.Name;
			data.Value = genericList.Value;
			data.SystemEntityTypeId = genericList.SystemEntityTypeId;
			data.Width = genericList.Width;
			data.Formatting = genericList.Formatting;
			data.ControlType = genericList.ControlType;
			data.HorizontalAlignment = genericList.HorizontalAlignment;
			data.GridViewPriority = genericList.GridViewPriority;
			data.DetailsViewPriority = genericList.DetailsViewPriority;
            data.FieldConfigurationModeId = genericList.FieldConfigurationModeId;
            FieldConfigurationDataManager.Create(data, AuditId);

			var entityName   = Request.QueryString["EN"].ToString();
			var entityFolder = Request.QueryString["EF"].ToString();
			var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue);
			var modeselected = BindFCModes();
			eSettingsGrid.SetUp(systementitytypeId, entityName, modeselected,
			                    int.Parse(ddlApplication.SelectedValue));
            eSettingsRepeater.SetUp(systementitytypeId, entityName, modeselected,
                                int.Parse(ddlApplication.SelectedValue));

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
			var entityFolder = "";
			lblEntityName.Text = ddlSystemEntityType.SelectedValue;
			var applicationId = Convert.ToInt32(ddlApplication.SelectedValue.ToString());
			ddlSystemEntityType.DataSource = GetApplicableSystemEntities(applicationId);
			ddlSystemEntityType.DataTextField = "EntityName";
			ddlSystemEntityType.DataValueField = "SystemEntityTypeId";
			ddlSystemEntityType.DataBind();
			var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue);
			var modeselected = BindFCModes();
			eSettingsGrid.SetUp(
					Convert.ToInt32(ddlSystemEntityType.SelectedValue),
					ddlSystemEntityType.SelectedItem.Text, modeselected, applicationId);
            eSettingsRepeater.SetUp(
                    Convert.ToInt32(ddlSystemEntityType.SelectedValue),
                    ddlSystemEntityType.SelectedItem.Text, modeselected, applicationId);

		}

		protected void ddlSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
		{
			var entityFolder = "";
			lblEntityName.Text = ddlSystemEntityType.SelectedValue;
			var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue);
			var modeselected = BindFCModes();
			eSettingsGrid.SetUp(
					(int) Enum.Parse(typeof (Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue),
					ddlSystemEntityType.SelectedItem.Text, modeselected, int.Parse(ddlApplication.SelectedValue));
            eSettingsRepeater.SetUp(
                    Convert.ToInt32(ddlSystemEntityType.SelectedValue),
                    ddlSystemEntityType.SelectedItem.Text, modeselected, int.Parse(ddlApplication.SelectedValue));
			
		}

		protected void ddlAEFLMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			var entityFolder = "";
            Session["AEFLMode"] = ddlAEFLMode.SelectedValue;
			lblEntityName.Text = ddlSystemEntityType.SelectedValue;
			eSettingsGrid.SetUp((int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue), 
                ddlSystemEntityType.SelectedItem.Text, int.Parse(ddlAEFLMode.SelectedValue), int.Parse(ddlApplication.SelectedValue));
            eSettingsRepeater.SetUp((int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ddlSystemEntityType.SelectedValue),
                ddlSystemEntityType.SelectedItem.Text, int.Parse(ddlAEFLMode.SelectedValue), int.Parse(ddlApplication.SelectedValue));

        }

        #endregion

	}
}