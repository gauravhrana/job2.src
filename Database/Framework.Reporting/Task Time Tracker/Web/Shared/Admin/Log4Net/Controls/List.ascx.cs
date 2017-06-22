using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;
using Shared.UI.Web.Controls;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.Admin.Log4Net.Controls
{
    public partial class List : Shared.UI.WebFramework.BaseControl
    {

        #region public properties

		public int DefaultRowCount
		{
			get
			{
				return PerferenceUtility.GetUserPreferenceByKeyAsInt(ApplicationCommon.DefaultRowCountKey, SettingCategory);
			}
		}

        public ExportMenu ExportMenu
        {
            get { return myExportMenu; }
        }

        public string FieldConfigurationMode
        {
            get
            {
                if (ddlFieldConfigurationMode.SelectedValue != null)
                    return ddlFieldConfigurationMode.SelectedValue;
                else
                    return String.Empty;
            }
        }

        public string UserPreferenceCategory
        {
            get
            {
                if (ViewState["UserPreferenceCategory"] != null)
                {
                    return Convert.ToString(ViewState["UserPreferenceCategory"]);
                }
                return String.Empty;
            }
            set
            {
                ViewState["UserPreferenceCategory"] = value;
            }
        }

        #endregion

        #region Variables

        int CurrentPageIndex
        {
            get
            {
                if (ViewState["CurrentPageIndex"] != null)
                {
                    return Convert.ToInt32(ViewState["CurrentPageIndex"]);
                }
                return 1;
            }
            set
            {
                ViewState["CurrentPageIndex"] = value;
            }
        }

        int TotalNoOfPages
        {
            get
            {
                if (ViewState["TotalNoOfPages"] != null)
                {
                    return Convert.ToInt32(ViewState["TotalNoOfPages"]);
                }
                return 1;
            }
            set
            {
                ViewState["TotalNoOfPages"] = value;
            }
        }

        string CurrentOrderBy
        {
            get
            {
                if (ViewState["CurrentOrderBy"] != null)
                {
                    return Convert.ToString(ViewState["CurrentOrderBy"]);
                }
                return PrimaryKeyColumn;
            }
            set
            {
                ViewState["CurrentOrderBy"] = value;
            }
        }

        string CurrentOrderByDirection
        {
            get
            {
                if (ViewState["CurrentOrderByDirection"] != null)
                {
                    return Convert.ToString(ViewState["CurrentOrderByDirection"]);
                }
                return String.Empty;
            }
            set
            {
                ViewState["CurrentOrderByDirection"] = value;
            }
        }

        string TableName
        {
            get
            {
                if (ViewState["TableName"] != null)
                {
                    return Convert.ToString(ViewState["TableName"]);
                }
                return String.Empty;
            }
            set
            {
                ViewState["TableName"] = value;
            }
        }

        string PrimaryKeyColumn
        {
            get
            {
                if (ViewState["PrimaryKeyColumn"] != null)
                {
                    return Convert.ToString(ViewState["PrimaryKeyColumn"]);
                }
                return String.Empty;
            }
            set
            {
                ViewState["PrimaryKeyColumn"] = value;
            }
        }

        private double totalGridColumnWidth = 0;

        public delegate DataTable GetDataDelegate(int pageIndex, int pageSize, string orderBy, string orderByDirection);
        private GetDataDelegate _getData;

        public delegate string[] GetColumnDelegate();
        private GetColumnDelegate _getColumnDelegate;

        #endregion

        #region Methods

        private HorizontalAlign GetHeaderAlignment(string alignment)
        {
            switch (alignment)
            {
                case "Right":
                    return HorizontalAlign.Right;
                    break;
                case "Left":
                    return HorizontalAlign.Left;
                    break;
                case "Centre":
                    return HorizontalAlign.Center;
                    break;
                case "Justify":
                    return HorizontalAlign.Justify;
                    break;
                default:
                    return HorizontalAlign.Center;
                    break;
            }

        }

        private int GetSystemEntityTypeId()
        {
            var entityName = Convert.ToString(ViewState["TableName"]);
            var entityId = 0;
            try
            {
                entityId = (int)System.Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), entityName);
            }
            catch { }
            return entityId;
        }

        private DataTable GetGridFormatSettings()
        {
            var entityId = GetSystemEntityTypeId();
            var auditId = SessionVariables.RequestProfile.AuditId;
            var dtGridFormat = new FieldConfigurationDataModel();
            dtGridFormat.SystemEntityTypeId = entityId;

			var gridFormatdt = FieldConfigurationDataManager.Search(dtGridFormat, SessionVariables.RequestProfile);
            Session.Add("GridFormatSettings", gridFormatdt);

            return gridFormatdt;
        }

        public void Setup(string tableName, string primaryKeyColumn, GetDataDelegate getDataDelegate, GetColumnDelegate getColumnDelegate, string userPreferenceCategory = "")
        {
            TableName = tableName;
            PrimaryKeyColumn = primaryKeyColumn;

            _getData = getDataDelegate;
            _getColumnDelegate = getColumnDelegate;

            if (!IsPostBack)
            {
                SetUpDropDownAEFLMode();
            }

            //ExportMenu.Setup(tableName, String.Empty, getDataDelegate, getColumnDelegate, String.Empty);

            bindGrid();
        }

        private void bindGrid()
        {
            var dt = _getData(CurrentPageIndex, DefaultRowCount, CurrentOrderBy, CurrentOrderByDirection);
            GenerateColumns(dt);

            dgvRecords.DataSource = dt;
            dgvRecords.DataBind();

            var totalRecords = 0;
            if (dt.Rows.Count > 0)
            {
                totalRecords = Convert.ToInt32(dt.Rows[0]["TotalCount"]);
                TotalNoOfPages = (totalRecords / DefaultRowCount) + 1;
            }

            lb_FirstPage.Enabled = true;
            lb_NextPage.Enabled = true;
            lb_PreviousPage.Enabled = true;
            lb_LastPage.Enabled = true;

            if (CurrentPageIndex == 1)
            {
                lb_PreviousPage.Enabled = false;
                lb_FirstPage.Enabled = false;
            }
            if (CurrentPageIndex == TotalNoOfPages)
            {
                lb_LastPage.Enabled = false;
                lb_NextPage.Enabled = false;
            }
            if (CurrentPageIndex != TotalNoOfPages)
            {
                litPagingSummary.Text = "Displaying Records " +
                    (((CurrentPageIndex - 1) * DefaultRowCount) + 1) + "-" + (CurrentPageIndex * DefaultRowCount) + " of " + totalRecords;
            }
            else
            {
                litPagingSummary.Text = "Displaying Records " +
                       (((CurrentPageIndex - 1) * DefaultRowCount) + 1) + "-" + totalRecords + " of " + totalRecords;
            }
        }

        private void GenerateColumns(DataTable dt)
        {
            var split = " ";
            var lstColumns = _getColumnDelegate();
            var gridFormatdt = GetGridFormatSettings();
            foreach (DataRow row in gridFormatdt.Rows)
            {
                //Considering all the columns have their width fixed from the Database
                totalGridColumnWidth += Double.Parse(row[FieldConfigurationDataModel.DataColumns.Width].ToString());
            }

            foreach (string strColumn in lstColumns)
            {
                var dataColumn = dt.Columns[strColumn];
                if (dataColumn != null)
                {
                    var hypField = new HyperLinkField();

                    var selectCriteria = FieldConfigurationDataModel.DataColumns.Name + " = '" + dataColumn.ColumnName + "'";
                    var rows = gridFormatdt.Select(selectCriteria);
                    if (rows.Length > 0)
                    {
                        var flagGridFormat = false;
                        int totalColumnWidth = 0;
                        string headerWidth = "100";
                        string headerAlignment = "Center";
                        string headerTextValue = "";
                        var headerTextName = rows[0][FieldConfigurationDataModel.DataColumns.Name].ToString();
                        headerTextValue = rows[0][FieldConfigurationDataModel.DataColumns.FieldConfigurationDisplayName].ToString();

                        if (headerTextName == "Description" && (1000 - (totalGridColumnWidth + 100)) > 0)
                        {
                            headerWidth = (1000 - (totalGridColumnWidth + 100)).ToString();
                        }

                        else
                        {
                            headerWidth = rows[0][FieldConfigurationDataModel.DataColumns.Width].ToString();
                        }

                        headerAlignment = rows[0][FieldConfigurationDataModel.DataColumns.HorizontalAlignment].ToString();

                        flagGridFormat = true;

                        if (dataColumn.ColumnName == "ApplicationId")
                        {
                            hypField.Visible = false;
                        }

                        if (dataColumn.ColumnName.Contains("Name"))
                        {
                            split = dataColumn.ColumnName.Replace("Name", " Name");
                            hypField.HeaderText = split;
                        }
                        else if (dataColumn.ColumnName.Contains("Id"))
                        {
                            split = dataColumn.ColumnName.Replace("Id", " Id");
                            hypField.HeaderText = split;
                            flagGridFormat = true;
                        }
                        else
                        {
                            hypField.HeaderText = dataColumn.ColumnName;
                        }

                        if (!headerWidth.Contains('-'))
                        {
                            hypField.HeaderStyle.Width = Unit.Parse(headerWidth);
                        }
                        else
                        {
                            hypField.HeaderStyle.Width = '*';
                        }

                        hypField.ItemStyle.HorizontalAlign = GetHeaderAlignment(headerAlignment);
                        hypField.HeaderStyle.BackColor = System.Drawing.Color.Black;
                        if (!string.IsNullOrEmpty(headerTextValue))
                        {
                            hypField.HeaderText = headerTextValue;
                        }
                        hypField.DataTextField = dataColumn.ColumnName;

                        var columnsExists = false;

                        foreach (DataControlField column in dgvRecords.Columns)
                        {
                            var headertext = column.HeaderText;
                            if (headertext.EndsWith("&nbsp; &uarr;"))
                                headertext = headertext.Replace("&nbsp; &uarr;", "");
                            else if (headertext.EndsWith("&nbsp; &darr;"))
                                headertext = headertext.Replace("&nbsp; &darr;", "");
                            if (headertext.Equals(hypField.HeaderText))
                            {
                                columnsExists = true;
                                break;
                            }
                        }

                        if (!columnsExists)
                        {
                            dgvRecords.Columns.Add(hypField);
                        }

                    }


                }
            }
        }

        private DataTable GetApplicableModesList(int systemEntityTypeId)
        {
            var data = new FieldConfigurationDataModel();
            data.SystemEntityTypeId = systemEntityTypeId;
			var columns = FieldConfigurationDataManager.Search(data, SessionVariables.RequestProfile);
            var modes = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile);
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
                        var temp = validmodes.Select("FieldConfigurationModeId = " + int.Parse(
                            modes.Rows[j][
                                FieldConfigurationModeDataModel.DataColumns.
                                    FieldConfigurationModeId].ToString()));
                        if (temp.Length == 0)
                            validmodes.ImportRow(modes.Rows[j]);


                    }
                }

            }
            var dv = validmodes.DefaultView;
            dv.Sort = "SortOrder ASC";
            var sortedvalidmodes = dv.ToTable();
            return sortedvalidmodes;

        }

        private int GetSessionInstanceAEFLMode(string currententity)
        {
            if (Session[ViewState["TableName"] + "SelectedMode"] != null)
                return Convert.ToInt32(Session[currententity + "SelectedMode"].ToString());
            else
                return -1;
        }

        private int SetUpDropDownAEFLMode()
        {
            var systementitytypeId = (int)Enum.Parse(typeof(Framework.Components.DataAccess.SystemEntity), ViewState["TableName"].ToString());
            var dt = GetApplicableModesList(systementitytypeId);
            var modeselected = GetSessionInstanceAEFLMode(ViewState["TableName"].ToString());

            if (dt.Rows.Count > 0)
            {
                ddlFieldConfigurationMode.DataSource = dt;
                ddlFieldConfigurationMode.DataTextField = "Name";
                ddlFieldConfigurationMode.DataValueField = "FieldConfigurationModeId";
                ddlFieldConfigurationMode.DataBind();
                if (modeselected != -1)
                {
                    ddlFieldConfigurationMode.SelectedValue = modeselected.ToString();
                }
                modeselected = int.Parse(ddlFieldConfigurationMode.SelectedValue);

            }
            else
            {
                ddlFieldConfigurationMode.Visible = false;
            }

            return modeselected;
        }

        private void SaveSessionInstanceAEFLMode(int selectedmode)
        {
            if (Session[ViewState["TableName"] + "SelectedMode"] == null)
                Session.Add(ViewState["TableName"] + "SelectedMode", selectedmode);
            else
                Session[ViewState["TableName"] + "SelectedMode"] = selectedmode;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void lb_FirstPage_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = 1;
            bindGrid();
        }

        protected void lb_PreviousPage_Click(object sender, EventArgs e)
        {
            CurrentPageIndex -= 1;
            bindGrid();
        }

        protected void lb_NextPage_Click(object sender, EventArgs e)
        {
            CurrentPageIndex += 1;
            bindGrid();
        }

        protected void lb_LastPage_Click(object sender, EventArgs e)
        {
            //CurrentIndex = TotalPages - 1;
            CurrentPageIndex = TotalNoOfPages;
            bindGrid();
        }

        protected void dgvRecords_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (CurrentOrderBy == e.SortExpression)
            {
                if (CurrentOrderByDirection == "ASC")
                {
                    CurrentOrderByDirection = "DESC";
                }
                else
                {
                    CurrentOrderByDirection = "ASC";
                }
            }
            else
            {
                CurrentOrderBy = e.SortExpression;
                CurrentOrderByDirection = "ASC";
            }
            CurrentPageIndex = 1;
            bindGrid();
        }

        protected void ddlFieldConfigurationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionVariables.FieldConfigurationMode = Convert.ToInt32(ddlFieldConfigurationMode.SelectedValue);
            while (dgvRecords.Columns.Count > 1)
            {
                if (!dgvRecords.Columns[dgvRecords.Columns.Count - 1].HeaderText.Equals("All"))
                    dgvRecords.Columns.RemoveAt(dgvRecords.Columns.Count - 1);

            }
            SaveSessionInstanceAEFLMode(Convert.ToInt32(ddlFieldConfigurationMode.SelectedValue));
            //Setup(ViewState["TableName"].ToString(), ViewState["TableFolder"].ToString(), ViewState["PrimaryKey"].ToString(), true, _getData, _getColumnDelegate);
            bindGrid();
            //SortGridView(String.Empty, SortDirection.Ascending.ToString());
            //AddCheckBox();
        }

        #endregion

    }
}