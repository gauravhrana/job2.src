using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin
{
	public partial class ColumnChooser : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region variables

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

        #endregion

        #region private methods

        private DataTable CreateAEFLTable(DataTable dtSource)
        {
            var dt = new DataTable();

            dt.Columns.Add("Name");
            dt.Columns.Add("Value");
            dt.Columns.Add("Width");
            dt.Columns.Add("Formatting");
            dt.Columns.Add("ControlType");
            dt.Columns.Add("HorizontalAlignment");
            dt.Columns.Add("GridViewPriority");
            dt.Columns.Add("DetailsViewPriority");

            var iCount = 1;
            foreach (DataColumn column in dtSource.Columns)
            {      
                if(column.DataType.Name.ToLower() == "string")
                {
                    dt.Rows.Add(new Object[] { column.ColumnName, column.Caption, 150, String.Empty, "TextBox", "Right", iCount, iCount });
                }
                else
                {
                    dt.Rows.Add(new Object[] { column.ColumnName, column.Caption, 20, String.Empty, "TextBox", "Left", iCount, iCount });
                }
                iCount++;
            }

            return dt;
        }

        private void BindGrid()
        {
            if (string.IsNullOrEmpty(EntityName))
            {
                EntityName = Request.QueryString["entity"];
            }
            var dtSource = (DataTable)Session[EntityName];
            var dt = CreateAEFLTable(dtSource);
            if (dtSource.Columns.Count > 0)
            {
                dgvAEFL.DataSource = dt;
                dgvAEFL.DataBind();
            }
            //Session[Request.QueryString["entity"]]
        }

        private void UpdateGridTableInCache(int? systemEntityTypeId)
        {
            var obj = new FieldConfigurationDataModel();
            var data = new DataTable();
            obj.SystemEntityTypeId = systemEntityTypeId;
			data = FieldConfigurationDataManager.Search(obj, SessionVariables.RequestProfile);
            var dtInSession = (DataTable)SessionVariables.GridColumnsTable;
            if (dtInSession != null)
            {
                var odt = dtInSession.Clone();
                var datarows = dtInSession.Select("SystemEntityTypeId = " + systemEntityTypeId);
                if (datarows.Length > 0)
                {
                    foreach (DataRow dr in datarows)
                    {
                        dtInSession.Rows.Remove(dr);
                    }
                }

                dtInSession.Merge(data);
                SessionVariables.GridColumnsTable = dtInSession;
            }
            else
            {
                SessionVariables.GridColumnsTable = data;
            }


        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvAEFL.Rows.Count > 0)
            {
                var strAEFLModeName = EntityName + "_" + txtAEFLModeName.Text.Trim();
                var data = new FieldConfigurationModeDataModel();
                data.Name = strAEFLModeName;
				
                if(!FieldConfigurationModeDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
                    Response.Write("This AEFL Mode already exists, Please choose different name for AEFL Mode");
                    txtAEFLModeName.Focus();
                }
                else
                {
                    data.Description = data.Name;
                    data.SortOrder = 1;
					var fcModeId = FieldConfigurationModeDataManager.Create(data, SessionVariables.RequestProfile);
                    var SystemEntityTypeId = ApplicationCommon.GetSystemEntityTypeId(EntityName);
                    foreach (GridViewRow row in dgvAEFL.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            var dataAEFL = new FieldConfigurationDataModel();

                            var chkSelected = (CheckBox)row.FindControl("chkSelected");
                            if (chkSelected.Checked)
                            {                               

                                try
                                {
                                    var txtName                  = (TextBox)row.FindControl("txtName");
                                    var txtValue                 = (TextBox)row.FindControl("txtValue");
                                    var txtWidth                 = (TextBox)row.FindControl("txtWidth");
                                    var txtHorizontalAlignment   = (TextBox)row.FindControl("txtHorizontalAlignment");
                                    var txtFormatting            = (TextBox)row.FindControl("txtFormatting");
                                    var txtControlType           = (TextBox)row.FindControl("txtControlType");
                                    var txtGridViewPriority      = (TextBox)row.FindControl("txtGridViewPriority");
                                    var txtDetailsViewPriority   = (TextBox)row.FindControl("txtDetailsViewPriority");

                                    dataAEFL.Name                              = txtName.Text;
                                    dataAEFL.Value                             = txtValue.Text;
                                    //dataAEFL.Width                             = decimal.Parse(txtWidth.Text);
                                    dataAEFL.SystemEntityTypeId                = SystemEntityTypeId;
                                    dataAEFL.Formatting                        = txtFormatting.Text;
                                    dataAEFL.ControlType                       = txtControlType.Text;
                                    dataAEFL.HorizontalAlignment               = txtHorizontalAlignment.Text;
                                    dataAEFL.GridViewPriority                  = int.Parse(txtGridViewPriority.Text);
                                    dataAEFL.DetailsViewPriority               = int.Parse(txtDetailsViewPriority.Text);
                                    dataAEFL.FieldConfigurationModeId = fcModeId;

									FieldConfigurationDataManager.Create(dataAEFL, SessionVariables.RequestProfile);
                                }
                                catch { }
                            }

                        }
                    }
                    UpdateGridTableInCache(SystemEntityTypeId);
                    Response.Redirect("~/" + EntityName, true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}