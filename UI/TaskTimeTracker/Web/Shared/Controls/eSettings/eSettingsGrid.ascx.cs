using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{
    public partial class eSettingsGrid : BaseControl
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

        private int AEFLMode
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

        List<string> lstExcludeFCColumns = new List<string>() { "FieldConfigurationModeId", "FieldConfigurationId", "SystemEntityTypeId", "ApplicationId", "UpdatedDate", "UpdatedBy", "LastAction" };


        #endregion

        #region Methods

        public void SetUp(int systemEntityTypeId, string entityName)
        {
            SystemEntityTypeId = systemEntityTypeId;
            EntityName = entityName;
        }

        public void SetUp(int systemEntityTypeId, string entityName, int AEFLModeId, int applicationId)
        {
            SystemEntityTypeId = systemEntityTypeId;
            EntityName = entityName;
            AEFLMode = AEFLModeId;
            ApplicationId = applicationId;
            BindData(applicationId);
        }
        
        private void BindData(int applicationId)
        {
            
            //Get FC Data for selected Entity
            var obj = new FieldConfigurationDataModel();
            
            var odt = FieldConfigurationUtility.GetFieldConfigurations(SystemEntityTypeId, AEFLMode, string.Empty);

            //Add Template fields dynamically to Editable GridView
            EditableGridView.Columns.Clear();

            foreach (DataColumn col in odt.Columns)
            {
                var columnName = col.ColumnName;
                if (!lstExcludeFCColumns.Contains(columnName, StringComparer.InvariantCultureIgnoreCase))
                {
                    //Declare the bound field and allocate memory for the bound field.
                    var bfield = new TemplateField();

                    //Initalize the DataField value.
                    bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, columnName);

                    //Initialize the HeaderText field value.
                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, columnName);

                    //Add the newly created bound field to the GridView.
                    EditableGridView.Columns.Add(bfield);

                }
            }
            
            //Bind GridViews
            EditableGridView.DataSource = odt;
            EditableGridView.DataBind();
            ReadOnlyGridView.DataSource = odt;
            ReadOnlyGridView.DataBind();
            EditableGridView.Visible = true;
            ReadOnlyGridView.Visible = false;
            if(ReadOnlyGridView.Columns.Count > 1)
                ReadOnlyGridView.Columns[ReadOnlyGridView.Columns.Count - 1].Visible = false;
            if (!SessionVariables.IsTesting)
            {
                EditableGridView.Columns[0].Visible = false;
                if (ReadOnlyGridView.Columns.Count > 0)
                {
                    ReadOnlyGridView.Columns[0].Visible = false;
                }
            }
        }

        private void UpdateData()
        {
            var data = new FieldConfigurationDataModel();
            data.SystemEntityTypeId = SystemEntityTypeId;
            data.FieldConfigurationModeId = AEFLMode;
            var columns = eSettings.GetColumns(ApplicationId, SessionVariables.RequestProfile.AuditId);
            var label = new Label();
            var txtbox = new TextBox();
            try
            {
                for (var i = 0; i <= EditableGridView.Rows.Count; i++)
                {
                    for (var j = 0; j < EditableGridView.Columns.Count; j++)
                    {
                        foreach (DataRow dr in columns.Rows)
                        {
                            var colname = dr["Name"].ToString();
                            label = (Label)EditableGridView.HeaderRow.Cells[j].FindControl("lbl" + colname);
                            txtbox = (TextBox)EditableGridView.Rows[i].Cells[j].FindControl(colname);

                            if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.FieldConfigurationId))
                            {
                                data.FieldConfigurationId = int.Parse(txtbox.Text);
                            }

                            if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.Name))
                            {
                                data.Name = txtbox.Text;
                            }

                            if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.Value))
                            {
                                data.Value = txtbox.Text;
                            }

                            if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.Width))
                            {
                                data.Width = decimal.Parse(txtbox.Text);
                            }

                            if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.Formatting))
                            {
                                data.Formatting = txtbox.Text;
                            }

                            if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.ControlType))
                            {
                                data.ControlType = txtbox.Text;
                            }

                            if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.HorizontalAlignment))
                            {
                                data.HorizontalAlignment = txtbox.Text;
                            }

                            if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.GridViewPriority))
                            {
                                data.GridViewPriority = int.Parse(txtbox.Text);
                            }

                            if (label.Text.Equals(FieldConfigurationDataModel.DataColumns.DetailsViewPriority))
                            {
                                data.DetailsViewPriority = int.Parse(txtbox.Text);
                            }

                        }

                    }
					FieldConfigurationDataManager.Update(data, SessionVariables.RequestProfile);
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
				eSettings.UpdateGridTableInCache(SystemEntityTypeId, SessionVariables.RequestProfile);
                BindData(ApplicationId);
                btnEdit.Visible = true;
                btnUpdate.Visible = false;
                EditableGridView.Visible = false;
                ReadOnlyGridView.Visible = true;
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            BindData(ApplicationId);
            EditableGridView.Visible = true;
            ReadOnlyGridView.Visible = false;
            btnUpdate.Visible = true;
            btnEdit.Visible = false;
        }

        public void EditableGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var selectedrow = e.RowIndex.ToString();
            var lbl = (Label)EditableGridView.Rows[e.RowIndex].Cells[2].FindControl("lblFieldConfigurationId");
            var data = new FieldConfigurationDataModel();
            data.FieldConfigurationId = int.Parse(lbl.Text);
			FieldConfigurationDataManager.Delete(data, SessionVariables.RequestProfile);
            BindData(ApplicationId);
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