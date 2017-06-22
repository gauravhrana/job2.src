using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Controls
{
    public partial class eSettingsRepeater : BaseControl
    {

        #region Properties

        private int SystemEntityTypeId
        {

            get
            {
                if (ViewState["SystemEntityTypeId"] != null)
                    return int.Parse(ViewState["SystemEntityTypeId"].ToString());
                else
                    return 0;

            }
            set { ViewState["SystemEntityTypeId"] = value; }
        }

        private string EntityName
        {

            get
            {
                if (ViewState["EntityName"] != null)
                    return ViewState["EntityName"].ToString();
                else
                    return String.Empty;
            }
            set { ViewState["EntityName"] = value; }
        }

        private int ApplicationId
        {

            get
            {
                if (ViewState["ApplicationId"] != null)
                    return int.Parse(ViewState["ApplicationId"].ToString());
                else
                    return 100;
            }
            set { ViewState["ApplicationId"] = value; }
        }

        private int FCMode
        {

            get
            {
                if (ViewState["FCMode"] != null)
                    return int.Parse(ViewState["FCMode"].ToString());
                else
                    return 3;
            }
            set { ViewState["FCMode"] = value; }
        }

        private string FCModeName
        {

            get
            {
                if (ViewState["FCModeName"] != null)
                {
                    return ViewState["FCModeName"].ToString();
                }
                return string.Empty;
            }
            set { ViewState["FCModeName"] = value; }
        }

        List<string> lstExcludeFCColumns = new List<string>() { "FieldConfigurationModeId", "FieldConfigurationMode", "FieldConfigurationId", "SystemEntityTypeId", "ApplicationId", "UpdatedDate", "UpdatedBy", "LastAction" };

        #endregion

        #region Methods

        public void SetUp(int systemEntityTypeId, string entityName)
        {
            SystemEntityTypeId = systemEntityTypeId;
            EntityName = entityName;
        }

        public void SetUp(int systemEntityTypeId, string entityName, int fcModeId, int applicationId, string fcModeName = "")
        {
            SystemEntityTypeId = systemEntityTypeId;
            EntityName = entityName;
            FCMode = fcModeId;
            FCModeName = fcModeName;
            ApplicationId = applicationId;
            BindData(applicationId);
        }

        private DataTable FilterData(DataTable dtSource, bool isVisibleColumns)
        {
            var filterExpression = string.Empty;

            if (isVisibleColumns)
            {
                filterExpression = FieldConfigurationDataModel.DataColumns.GridViewPriority + " > -1 ";
            }
            else
            {
                filterExpression = FieldConfigurationDataModel.DataColumns.GridViewPriority + " = -1 ";
            }

            var dv = dtSource.DefaultView;
            dv.RowFilter = filterExpression;
            dv.Sort = FieldConfigurationDataModel.DataColumns.GridViewPriority + " ASC";

            var dtResults = dv.ToTable();

            return dtResults;
        }

        private void BindData(int applicationId, bool ReadSeassionSubject = false)
        {

            lblFCModeName.Text = Convert.ToString(FCModeName);

            //Get FieldConfig data for selected entity
            var obj = new FieldConfigurationDataModel();
            var odt = new DataTable();
            if (!ReadSeassionSubject)
            {
                odt = FieldConfigurationUtility.GetFieldConfigurations(SystemEntityTypeId, FCMode, string.Empty);
            }
            divTabContainer.Controls.Clear();

            //Bind Repeaters

            if (rbtnList.SelectedValue.Equals("Grid"))
            {
                ReadOnlyGridPanel.DataSource = odt;
                ReadOnlyGridPanel.DataBind();

                //filter for visible non-visible columns
                EditableGridPanel.DataSource = FilterData(odt, true);
                EditableGridPanel.DataBind();

                EditableGridPanelHiddenColumns.DataSource = FilterData(odt, false);
                EditableGridPanelHiddenColumns.DataBind();

                EditableGridPanel.Visible = true;
                EditableGridPanelHiddenColumns.Visible = true;
                ReadOnlyGridPanel.Visible = false;

                var tabControlVertical = ApplicationCommon.GetNewDetailTabControl();

                tabControlVertical.Setup(EntityName + "ListSettings");

                tabControlVertical.AddTab("VisibleColumns", EditableGridPanel, "Visible Columns", true);
                tabControlVertical.AddTab("Hidden", EditableGridPanelHiddenColumns);

                divTabContainer.Controls.Add(tabControlVertical);

                EditableRepeater.Visible = false;
                EditableRepeaterHiddenColuumns.Visible = false;
                ReadOnlyRepeater.Visible = false;

                ReadOnlyCarouselContainer.Visible = false;
                EditableCarouselContainer.Visible = false;
            }
            else if (rbtnList.SelectedValue.Equals("Stack"))
            {
                ReadOnlyRepeater.DataSource = odt;
                ReadOnlyRepeater.DataBind();

                //filter for visible non-visible columns
                EditableRepeater.DataSource = FilterData(odt, true);
                EditableRepeater.DataBind();

                EditableRepeaterHiddenColuumns.DataSource = FilterData(odt, false);
                EditableRepeaterHiddenColuumns.DataBind();

                EditableGridPanel.Visible = false;
                EditableGridPanelHiddenColumns.Visible = false;
                ReadOnlyGridPanel.Visible = false;

                EditableRepeater.Visible = true;
                EditableRepeaterHiddenColuumns.Visible = true;
                ReadOnlyRepeater.Visible = false;

                var tabControlHorizontal = ApplicationCommon.GetNewDetailTabControl();

                tabControlHorizontal.Setup(EntityName + "ListSettings");

                tabControlHorizontal.AddTab("VisibleColumns", EditableRepeater, "Visible Columns", true);
                tabControlHorizontal.AddTab("Hidden", EditableRepeaterHiddenColuumns);

                divTabContainer.Controls.Add(tabControlHorizontal);

                ReadOnlyCarouselContainer.Visible = false;
                EditableCarouselContainer.Visible = false;
            }
            else if (rbtnList.SelectedValue.Equals("Carousel"))
            {

                ReadOnlyCarouselRepeater.DataSource = odt;
                ReadOnlyCarouselRepeater.DataBind();
                EditableCarouselRepeater.DataSource = odt;
                EditableCarouselRepeater.DataBind();

                ReadOnlyCarouselContainer.Visible = false;                
                EditableCarouselContainer.Visible = true;

                EditableGridPanel.Visible = false;
                EditableGridPanelHiddenColumns.Visible = false;
                ReadOnlyGridPanel.Visible = false;

                EditableRepeater.Visible = false;
                EditableRepeaterHiddenColuumns.Visible = false;
                ReadOnlyRepeater.Visible = false;
            }
        }

        private void AssignPropertyValue(FieldConfigurationDataModel data, Label label, TextBox txtbox)
        {
            if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.Name))
            {
                data.Name = txtbox.Text;
            }
            else if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.Value))
            {
                data.Value = txtbox.Text;
            }
            else if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.Width))
            {
                data.Width = decimal.Parse(txtbox.Text);
            }
            else if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.Formatting))
            {
                data.Formatting = txtbox.Text;
            }
            else if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.ControlType))
            {
                data.ControlType = txtbox.Text;
            }
            else if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.HorizontalAlignment))
            {
                data.HorizontalAlignment = txtbox.Text;
            }
            else if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.GridViewPriority))
            {
                data.GridViewPriority = int.Parse(txtbox.Text);
            }
            else if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.DetailsViewPriority))
            {
                data.DetailsViewPriority = int.Parse(txtbox.Text);
            }
            else if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.DisplayColumn))
            {
                data.DisplayColumn = int.Parse(txtbox.Text);
            }
        }

        private void UpdateData()
        {
            if (rbtnList.SelectedValue.Equals("Stack"))
            {
                foreach (RepeaterItem item in EditableRepeater.Items)
                {
                    //Define data object
                    var data = new FieldConfigurationDataModel();

                    //Find controls inside Repeater
                    var hdnFieldConfigurationId = (HiddenField)item.FindControl("hdnfcid");
                    var hdnFieldConfigurationModeId = (HiddenField)item.FindControl("hdnfcmid");
                    var innerrepeater = (DataList)item.FindControl("InnerRepeater");
                    var hdnname = (HiddenField)item.FindControl("hdncolname");

                    try
                    {
                        data.FieldConfigurationId = int.Parse(hdnFieldConfigurationId.Value);
                        data.FieldConfigurationModeId = int.Parse(hdnFieldConfigurationModeId.Value);

                        //loop through inner reperater items to set data object properties based on Column name
                        foreach (DataListItem row in innerrepeater.Items)
                        {
                            var label = (Label)row.FindControl("lblColumnName");
                            var txtbox = (TextBox)row.FindControl("txt");

                            AssignPropertyValue(data, label, txtbox);

                            data.SystemEntityTypeId = SystemEntityTypeId;

                        }
						FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);

                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                // update hidden grid records
                foreach (RepeaterItem item in EditableRepeaterHiddenColuumns.Items)
                {
                    //Define data object
                    var data = new FieldConfigurationDataModel();

                    //Find controls inside Repeater
                    var hdnFieldConfigurationId = (HiddenField)item.FindControl("hdnfcid");
                    var hdnFieldConfigurationModeId = (HiddenField)item.FindControl("hdnfcmid");
                    var innerrepeater = (DataList)item.FindControl("InnerRepeater");
                    var hdnname = (HiddenField)item.FindControl("hdncolname");

                    try
                    {
                        data.FieldConfigurationId = int.Parse(hdnFieldConfigurationId.Value);
                        data.FieldConfigurationModeId = int.Parse(hdnFieldConfigurationModeId.Value);

                        //loop through inner reperater items to set data object properties based on Column name
                        foreach (DataListItem row in innerrepeater.Items)
                        {
                            var label = (Label)row.FindControl("lblColumnName");
                            var txtbox = (TextBox)row.FindControl("txt");

                            AssignPropertyValue(data, label, txtbox);

                            data.SystemEntityTypeId = SystemEntityTypeId;

                        }
						FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);

                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            else if (rbtnList.SelectedValue.Equals("Grid"))
            {
                foreach (DataListItem item in EditableGridPanel.Items)
                {
                    //Define data object
                    var data = new FieldConfigurationDataModel();

                    //Find controls inside Repeater
                    var hdnFieldConfigurationId = (HiddenField)item.FindControl("hdnfcid");
                    var hdnFieldConfigurationModeId = (HiddenField)item.FindControl("hdnfcmid");
                    var innerrepeater = (DataList)item.FindControl("InnerRepeater");
                    var hdnname = (HiddenField)item.FindControl("hdncolname");

                    try
                    {
                        data.FieldConfigurationId = int.Parse(hdnFieldConfigurationId.Value);
                        data.FieldConfigurationModeId = int.Parse(hdnFieldConfigurationModeId.Value);

                        //loop through inner reperater items to set data object properties based on Column name
                        foreach (DataListItem row in innerrepeater.Items)
                        {
                            var label = (Label)row.FindControl("lblColumnName");
                            var txtbox = (TextBox)row.FindControl("txt");

                            AssignPropertyValue(data, label, txtbox);

                            data.SystemEntityTypeId = SystemEntityTypeId;

                        }

                        FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);

                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                // update hidden column Records
                foreach (DataListItem item in EditableGridPanelHiddenColumns.Items)
                {
                    //Define data object
                    var data = new FieldConfigurationDataModel();

                    //Find controls inside Repeater
                    var hdnFieldConfigurationId = (HiddenField)item.FindControl("hdnfcid");
                    var hdnFieldConfigurationModeId = (HiddenField)item.FindControl("hdnfcmid");
                    var innerrepeater = (DataList)item.FindControl("InnerRepeater");
                    var hdnname = (HiddenField)item.FindControl("hdncolname");

                    try
                    {
                        data.FieldConfigurationId = int.Parse(hdnFieldConfigurationId.Value);
                        data.FieldConfigurationModeId = int.Parse(hdnFieldConfigurationModeId.Value);

                        //loop through inner reperater items to set data object properties based on Column name
                        foreach (DataListItem row in innerrepeater.Items)
                        {
                            var label = (Label)row.FindControl("lblColumnName");
                            var txtbox = (TextBox)row.FindControl("txt");

                            AssignPropertyValue(data, label, txtbox);

                            data.SystemEntityTypeId = SystemEntityTypeId;

                        }

                        FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);

                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            else if (rbtnList.SelectedValue.Equals("Carousel"))
            {
                foreach (RepeaterItem item in EditableCarouselRepeater.Items)
                {
                    //Define data object
                    var data = new FieldConfigurationDataModel();

                    //Find controls inside Repeater
                    var hdnFieldConfigurationId = (HiddenField)item.FindControl("hdnfcid");
                    var hdnFieldConfigurationModeId = (HiddenField)item.FindControl("hdnfcmid");
                    var innerRepeater = (Repeater)item.FindControl("InnerRepeater");
                    var hdnname = (HiddenField)item.FindControl("hdncolname");

                    try
                    {
                        data.FieldConfigurationId = int.Parse(hdnFieldConfigurationId.Value);
                        data.FieldConfigurationModeId = int.Parse(hdnFieldConfigurationModeId.Value);

                        //loop through inner reperater items to set data object properties based on Column name
                        foreach (RepeaterItem row in innerRepeater.Items)
                        {
                            var label = (Label)row.FindControl("lblColumnName");
                            var txtbox = (TextBox)row.FindControl("txt");

                            AssignPropertyValue(data, label, txtbox);

                            data.SystemEntityTypeId = SystemEntityTypeId;

                        }
                        FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);

                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }

            FieldConfigurationUtility.SetFieldConfigurations();

			eSettings.UpdateGridTableInCache(SystemEntityTypeId, SessionVariables.RequestProfile);
            BindData(ApplicationId);
            btnEdit.Visible = true;
            btnUpdate.Visible = false;

            if (rbtnList.SelectedValue.Equals("Stack"))
            {
                EditableRepeater.Visible = false;
                ReadOnlyRepeater.Visible = true;
            }
            else if (rbtnList.SelectedValue.Equals("Grid"))
            {
                EditableGridPanel.Visible = false;
                ReadOnlyGridPanel.Visible = true;
            }
            else if (rbtnList.SelectedValue.Equals("Carousel"))
            {
                ReadOnlyCarouselContainer.Visible = true;
                EditableCarouselContainer.Visible = false;
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnEdit.Visible = false;
                btnUpdate.Visible = true;
            }
        }

        protected void rbtnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData(ApplicationId);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            BindData(ApplicationId);
            if (rbtnList.SelectedValue.Equals("Stack"))
            {
                EditableRepeater.Visible = true;
                EditableRepeaterHiddenColuumns.Visible = true;
                ReadOnlyRepeater.Visible = false;
            }
            else if (rbtnList.SelectedValue.Equals("Grid"))
            {
                EditableGridPanel.Visible = true;
                EditableGridPanelHiddenColumns.Visible = true;
                ReadOnlyGridPanel.Visible = false;
            }
            else if (rbtnList.SelectedValue.Equals("Carousel"))
            {
                ReadOnlyCarouselContainer.Visible = false;
                EditableCarouselContainer.Visible = true;
            }

            btnUpdate.Visible = true;
            btnEdit.Visible = false;
        }

        protected void ItemBound(object sender, RepeaterItemEventArgs args)
        {
            var applicationId = SessionVariables.RequestProfile.ApplicationId;

            //Get FC columns to determine columns to be displayed in Repeater
            var dtc = eSettings.GetColumns(ApplicationId, SessionVariables.RequestProfile.AuditId);

            //Build temp data table to bind InnerRepeater
            var finalodt = new DataTable();

            finalodt.Columns.Add("Name");
            finalodt.Columns.Add("Value");
            finalodt.Columns.Add("ColumnName");
            finalodt.Columns.Add("ColumnOrder", Type.GetType("System.Int32"));

            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                var childRepeater = (DataList)args.Item.FindControl("InnerRepeater");
                var hdnfield = (HiddenField)args.Item.FindControl("hdncol");
                if (hdnfield != null && childRepeater != null)
                {
                    //Get FC Data for selected Entity
                    var odt = FieldConfigurationUtility.GetFieldConfigurations(SystemEntityTypeId, FCMode, hdnfield.Value);

                    //Fill Temp table by looping through FC data of selected entity
                    if (odt.Rows.Count >= 1)
                    {
                        foreach (DataColumn dcol in dtc.Columns)
                        {
                            var columnName = dcol.ColumnName;
                            if (!lstExcludeFCColumns.Contains(columnName, StringComparer.InvariantCultureIgnoreCase))
                            {
                                var row = finalodt.NewRow();
                                row["ColumnName"] = columnName;
                                row["Name"] = FieldConfigurationUtility.GetFieldConfigurationColumnDisplayName(columnName) + ": ";
                                row["ColumnOrder"] = FieldConfigurationUtility.GetFieldConfigurationColumnDisplayOrder(columnName);
                                if (odt.Columns.Contains(columnName))
                                {
                                    row["Value"] = odt.Rows[0][columnName];
                                }
                                finalodt.Rows.Add(row);
                            }
                        }
                    }

                }

                finalodt.DefaultView.Sort = "ColumnOrder ASC";
                finalodt = finalodt.DefaultView.ToTable();

                //Bind InnerRepeater
                childRepeater.DataSource = finalodt;
                childRepeater.DataBind();
            }
        }

        protected void ItemBound(object sender, DataListItemEventArgs args)
        {
            var applicationId = SessionVariables.RequestProfile.ApplicationId;

            //Get FC columns to determine columns to be displayed in Repeater
            var dtc = eSettings.GetColumns(ApplicationId, SessionVariables.RequestProfile.AuditId);

            //Build temp data table to bind InnerRepeater
            var finalodt = new DataTable();
            finalodt.Columns.Add("Name");
            finalodt.Columns.Add("Value");
            finalodt.Columns.Add("ColumnName");
            finalodt.Columns.Add("ColumnOrder", Type.GetType("System.Int32"));

            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                var childRepeater = (DataList)args.Item.FindControl("InnerRepeater");
                var hdnfield = (HiddenField)args.Item.FindControl("hdncol");
                if (hdnfield != null && childRepeater != null)
                {
                    //Get FC Data for selected Entity
                    var odt = FieldConfigurationUtility.GetFieldConfigurations(SystemEntityTypeId, FCMode, hdnfield.Value);

                    //Fill Temp table by looping through FC data of selected entity
                    if (odt.Rows.Count >= 1)
                    {
                        lblFCModeName.Text = Convert.ToString(dtc.Rows[0]["FieldConfigurationMode"]);
                        foreach (DataColumn dcol in dtc.Columns)
                        {
                            var columnName = dcol.ColumnName;
                            if (!lstExcludeFCColumns.Contains(columnName, StringComparer.InvariantCultureIgnoreCase))
                            {
                                var row = finalodt.NewRow();

                                row["ColumnName"] = columnName;
                                row["Name"] = FieldConfigurationUtility.GetFieldConfigurationColumnDisplayName(columnName) + ": ";
                                row["ColumnOrder"] = FieldConfigurationUtility.GetFieldConfigurationColumnDisplayOrder(columnName);
                                if (odt.Columns.Contains(columnName))
                                {
                                    row["Value"] = odt.Rows[0][columnName];
                                }
                                finalodt.Rows.Add(row);
                            }
                        }
                    }

                }

                finalodt.DefaultView.Sort = "ColumnOrder ASC";
                finalodt = finalodt.DefaultView.ToTable();
                //Bind InnerRepeater
                childRepeater.DataSource = finalodt;
                childRepeater.DataBind();
            }
        }

        protected void ItemBoundCarosel(object sender, RepeaterItemEventArgs args)
        {
            var applicationId = SessionVariables.RequestProfile.ApplicationId;

            //Get FC columns to determine columns to be displayed in Repeater
            var dtc = eSettings.GetColumns(ApplicationId, SessionVariables.RequestProfile.AuditId);

            //Build temp data table to bind InnerRepeater
            var finalodt = new DataTable();

            finalodt.Columns.Add("Name");
            finalodt.Columns.Add("Value");
            finalodt.Columns.Add("ColumnName");
            finalodt.Columns.Add("ColumnOrder", Type.GetType("System.Int32"));

            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                var childRepeater = (Repeater)args.Item.FindControl("InnerRepeater");
                var hdnfield = (HiddenField)args.Item.FindControl("hdncol");
                if (hdnfield != null && childRepeater != null)
                {
                    //Get FC Data for selected Entity
                    var odt = FieldConfigurationUtility.GetFieldConfigurations(SystemEntityTypeId, FCMode, hdnfield.Value);

                    //Fill Temp table by looping through FC data of selected entity
                    if (odt.Rows.Count >= 1)
                    {
                        lblFCModeName.Text = Convert.ToString(dtc.Rows[0]["FieldConfigurationMode"]);

                        foreach (DataColumn dcol in dtc.Columns)
                        {
                            var columnName = dcol.ColumnName;
                            if (!lstExcludeFCColumns.Contains(columnName, StringComparer.InvariantCultureIgnoreCase))
                            {
                                var row = finalodt.NewRow();
                                row["ColumnName"] = columnName;
                                row["Name"] = FieldConfigurationUtility.GetFieldConfigurationColumnDisplayName(columnName) + ": ";
                                row["ColumnOrder"] = FieldConfigurationUtility.GetFieldConfigurationColumnDisplayOrder(columnName);
                                if (odt.Columns.Contains(columnName))
                                {
                                    row["Value"] = odt.Rows[0][columnName];
                                }
                                finalodt.Rows.Add(row);
                            }
                        }
                    }

                }

                finalodt.DefaultView.Sort = "ColumnOrder ASC";
                finalodt = finalodt.DefaultView.ToTable();

                //Bind InnerRepeater
                childRepeater.DataSource = finalodt;
                childRepeater.DataBind();
            }
        }

        protected void btnUpdateReturn_Click(object sender, EventArgs e)
        {
            UpdateData();
            var entityName = EntityName;
            Response.Redirect(Page.GetRouteUrl(entityName + "EntityRoute", new { Action = "Default" }), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        #endregion

    }
}