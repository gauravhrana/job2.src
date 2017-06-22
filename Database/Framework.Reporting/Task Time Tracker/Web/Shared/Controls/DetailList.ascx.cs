using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.UserPreference;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace Shared.UI.Web.Controls
{
    public partial class DetailListControl : BaseControl
    {
        public delegate DataTable GetDataDelegate();

        public delegate string[] GetColumnDelegate();

        private GetDataDelegate _getData;
        private GetColumnDelegate _getColumnDelegate;

        private DataTable dtGlobal = new DataTable();
        private GridPagiation oGridPagiation;
        private bool skipgridreload = false;

		#region properties

		public int DefaultRowCount
		{
			get
			{
				return PerferenceUtility.GetUserPreferenceByKeyAsInt(ApplicationCommon.DefaultRowCountKey, SettingCategory);
			}
		}

		public bool HideData { get; set; }

        public int EntityKey { get; set; }

        private string Prefix
        {
            get
            {
                if (ViewState["Prefix"] == null)
                {
                    ViewState["Prefix"] = "_";
                }

                return (string)ViewState["Prefix"];
            }
            set
            {
                ViewState["Prefix"] = value + "_";
            }
        }

        #endregion properties

        public DetailListControl()
        {
            HideData = false;
        }

        private DataTable GetData()
        {
            var dt = _getData();

            return dt;
        }

        private string[] GetColumns()
        {
            string[] validColumns;
            validColumns = _getColumnDelegate();
            return validColumns;
        }

        public void Setup(
			string tableName, string tableFolder, string primaryKey, bool pageLoad
			, GetDataDelegate getDataDelegate, GetColumnDelegate getColumnDelegate
			, bool isPaging, int entityKey, string UserPreferenceCategory = "")
        {
            EntityKey = entityKey;
            Prefix = entityKey.ToString();

            _getData = getDataDelegate;
            _getColumnDelegate = getColumnDelegate;
            ViewState["TableFolder"] = tableFolder;
            ViewState["TableName"] = tableName;

            var userPreferenceCategory = UserPreferenceCategory;
            if (string.IsNullOrEmpty(userPreferenceCategory))
            {
                userPreferenceCategory = tableName;
            }

            if (ViewState[Prefix + "TableName"] == null)
            {
                ViewState.Add(Prefix + "TableName", tableName);
            }

            if (ViewState[Prefix + "TableName"] != null && !(ViewState[Prefix + "TableName"].ToString().Equals(tableName)))
            {
                ViewState[Prefix + "TableName"] = tableName;
                ViewState[Prefix + "CurrentPageIndex"] = 0;
            }

            ViewState["TableName"] = tableName;
            ViewState["PrimaryKey"] = primaryKey;
            //ViewState["IsTesting"] = SessionVariables.IsTesting;
            ViewState["PageLoad"] = pageLoad;

            dtGlobal = GetData();

            Sample(dtGlobal, primaryKey, HideData, SessionVariables.IsTesting);
            MainGridView.DataSource = dtGlobal;

            if (isPaging)
            {
                MainGridView.PageSize = DefaultRowCount;

                oGridPagiation = new GridPagiation();
                oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page, SettingCategory);
                oGridPagiation.Changed += oGridPagiation_Changed;

                if (ViewState[Prefix + "CurrentPageIndex"] != null)
                {
                    oGridPagiation.PageIndexInSession = int.Parse(ViewState[Prefix + "CurrentPageIndex"].ToString());
                }

                oGridPagiation.ManagePaging(dtGlobal);

            }
            else
            {
                MainGridView.AllowPaging = isPaging;
            }

            MainGridView.DataBind();
        }

        void oGridPagiation_Changed(object sender, EventArgs e)
        {
            SortGridView(String.Empty, SortDirection.Ascending.ToString());
        }

        public void SetSession(string sessionUpdated)
        {
            ViewState["SessionUpdated"] = sessionUpdated;
        }

        // string searchLastName, string searchFirstName, 
        public void ShowData(bool dataHide, bool search)
        {
            HideData = dataHide;

            // sort by default ...
            SortGridView(String.Empty, SortDirection.Ascending.ToString());
        }

        protected void GridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
			//if (e.Row.RowType == DataControlRowType.Header)
			//{
				//var row = (DataRowView)e.Row.DataItem;
				//var cells = e.Row.Cells;

                // var btn = (HyperLink)cells[0].FindControl("ButtonDelete");
                // btn.ImageUrl = Application["Branding"] + "/Images/delete.jpg";
            //}
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //In-built paging implementation
            MainGridView.PageIndex = e.NewPageIndex;
            MainGridView.DataBind();

            if (ViewState[Prefix + "CurrentPageIndex"] == null)
                ViewState.Add(Prefix + "CurrentPageIndex", e.NewPageIndex);
            else
                ViewState[Prefix + "CurrentPageIndex"] = e.NewPageIndex;

            //Synchronize with custom paging
            oGridPagiation = new GridPagiation();

            dtGlobal = GetData();

            oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.PageIndexInSession = e.NewPageIndex;
            oGridPagiation.ManagePaging(dtGlobal);
        }

		//private void RefreshGrid()
		//{
		//    if (ViewState[Prefix + "CurrentPageIndex"] != null)
		//        MainGridView.PageIndex = int.Parse(ViewState[Prefix + "CurrentPageIndex"].ToString());
		//    else
		//    {
		//        MainGridView.PageIndex = 0;
		//    }
		//    MainGridView.DataBind();

		//    oGridPagiation = new GridPagiation();
		//    dtGlobal = GetData();
		//    oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page, SettingCategory);
		//    oGridPagiation.Changed += oGridPagiation_Changed;
		//    oGridPagiation.pageindexinsession = MainGridView.PageIndex;
		//    oGridPagiation.ManagePaging(dtGlobal);
		//    if (ViewState[Prefix + "SortExpression"] != null && ViewState[Prefix + "SortDirection"] != null)
		//        SortGridView(ViewState[Prefix + "SortExpression"].ToString(), ViewState[Prefix + "SortDirection"].ToString());

		//}

        // OnPageLoad Event Handler
        protected void Page_Load(object sender, EventArgs e)
        {
            // only need to set initial condition, else every other time it shoulld
            // be thee via state 
            if (!IsPostBack)
            {
                //if ((bool)ViewState["PageLoad"])
                //{
                //    // only go forward if settings are correct
                //    if (ViewState["TableName"] != null || ViewState["PrimaryKey"] != null)
                //    {
                //        // can not have this, as keys above are all blank
                //        SortGridView(String.Empty, SortDirection.Ascending.ToString());
                //    }
                //    else
                //    {
                //        System.Diagnostics.Debug.WriteLine("Missing info ... ");
                //    }
                //}
            }
        }

		// reName to better 
        private void Sample(DataTable dt, string primaryKeyId, bool hideData, bool isTesting)
        {
            var tableName = Convert.ToString(ViewState["TableName"]);

            if (MainGridView.Columns.Count > 0)
            {
                MainGridView.Columns.Clear();
                //   return;
            }

            // Create Column 0 --> PrimaryKey

            // Declare the bound field and allocate memory for the bound field.
            // Initalize the DataField value.

            if (dt.Columns[0].ColumnName.ToLower().Equals(primaryKeyId.ToLower()))
            {
                var bfield = new BoundField { DataField = primaryKeyId };
                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                bfield.HeaderText = dt.Columns[0].ColumnName.Replace("Id", " Id");
                bfield.Visible = isTesting;
                // ConvertStringArrayToString(dt.Columns[0].ColumnName.Split(new char[] { 'I' }, 1));            
                // Add the newly created bound field to the GridView.
                MainGridView.Columns.Add(bfield);
            }

            // boolean values set for the fields and procedures in the GridView            
            //var procedureHide = hideData;
            //var fieldHide = false;

            // Create a string array for DataNavigationUrlFields            
            var str = new string[2];
            str[0] = primaryKeyId;
            str[1] = dt.Columns[0].ColumnName;

            // Create rest of columns based on what user has access to ...            
            var key = "Default";
            var disableLink = hideData;
            var validColumns = GetColumns();

            foreach (var t in validColumns)
            {
                // get the column from the data table
                var dataColumn = dt.Columns[t].ColumnName;

                if (dataColumn != null)
                {
                    //if (dataColumn.ToLower() != "count")
                    //{

                    //    var boundField = new BoundField { DataField = dataColumn };
                    //    boundField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    //    boundField.HeaderText = dataColumn;
                    //    if (dataColumn == "SystemEntity" || dataColumn == "EntityKey")
                    //        boundField.Visible = isTesting;

                    //    if (dataColumn.ToLower().Contains("id"))
                    //        boundField.Visible = false;

                    //    // Add the newly created bound field to the GridView.
                    //    MainGridView.Columns.Add(boundField);
                    //}
                    //else
                    //{
                    SetHyperLink(dt.Columns[t], str, tableName, false);
                    //}
                }
            }
        }

        // reName to better 
        private void SetHyperLink(DataColumn dataColumn, string[] str, string entity, bool enableLInk)
        {
            var tableFolder = Convert.ToString(ViewState["TableFolder"]);
            //var bField = new BoundField();
            //bField.DataField = dataColumn.ColumnName;
            //bField.SortExpression = dataColumn.Caption;
            //bField.HeaderText = dataColumn.ColumnName;
            //bField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            //MainGridView.Columns.Add(bField);

            var hypField = new HyperLinkField();
            var flagGridFormat = false;

            var headerWidth = "100";
            var headerAlignment = "Center";
            var headerTextValue = "";
            //ConsoleColor headerColor	;	

			//string imgArrowDown = Application["Branding"] + "/Images/arrow-down.jpg";
			//string imgArrowUp = Application["Branding"] + "/Images/arrow-up.jpg";

            var split = " ";

            if (dataColumn.ColumnName == "ApplicationId")
            {
                hypField.Visible = false;
            }

            var gridFormatdt = GetGridFormatSettings();

            foreach (DataRow row in gridFormatdt.Rows)
            {
                var headerTextName = row[FieldConfigurationDataModel.DataColumns.Name].
                    ToString();

                headerTextValue = row[FieldConfigurationDataModel.DataColumns.Value].
                    ToString();

                if (dataColumn.ColumnName == headerTextName)
                {
                    headerWidth = row[FieldConfigurationDataModel.DataColumns.Width].ToString();

                    headerAlignment = row[FieldConfigurationDataModel.DataColumns.HorizontalAlignment].
                    ToString();

                    //headerColor		= row[FieldConfiguration.DataColumns.Formatting].
                    //ToString();

                    flagGridFormat = true;
                    break;

                }

                //else
                //{
                //    hypField.HeaderText = dataColumn.ColumnName;
                //    hypField.HeaderStyle.BackColor = System.Drawing.Color.Cyan;
                //}
                //if (e.Row.Cells[i].Text == headerText)
                //{


                //    e.Row.Cells[i].Style.Add("width", headerWidth);
                //    //foreach (DataColumn column in MainGridView.Columns)
                //    //{
                //    //    if (column.ColumnName == headerText)
                //    //    {
                //    //        column.MaxLength = headerWidth;
                //    //        break;
                //    //    }
                //    //}
                //    //MainGridView.Columns[1].HeaderStyle.Width = headerWidth;
                //    e.Row.Cells[i].Style.Add("text-align", headerAlignment);
                //    //e.Row.Cells[i].Width = headerWidth;
                //    //e.Row.Cells[i].BackColor = headerColor;
                //    e.Row.Cells[i].Style.Add("text-color", headerColor);
                //    break;
                //}
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
            }
            else
            {
                hypField.HeaderText = dataColumn.ColumnName;

                //hypField.HeaderStyle.ForeColor = 
            }


            if (flagGridFormat)
            {
                hypField.HeaderStyle.Width = Unit.Parse(headerWidth);
                hypField.HeaderStyle.HorizontalAlign = GetHeaderAlignment(headerAlignment);
                hypField.HeaderStyle.BackColor = Color.Black;
                hypField.HeaderText = headerTextValue;
            }

            //if (dataColumn.ColumnName.Contains("Name"))
            //{
            //    split = dataColumn.ColumnName.Replace("Name", " Name");
            //    hypField.HeaderText = split;
            //}
            //else if (dataColumn.ColumnName.Contains("Id"))
            //{
            //    split = dataColumn.ColumnName.Replace("Id", " Id");
            //    hypField.HeaderText = split;
            //}
            //else
            //{
            //    hypField.HeaderText = dataColumn.ColumnName;
            //    hypField.HeaderStyle.Width = 1000;
            //    hypField.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
            //    //hypField.HeaderStyle.ForeColor = 
            //}

            hypField.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            hypField.DataTextField = dataColumn.ColumnName;

            if (!enableLInk)
            {
                hypField.SortExpression = dataColumn.ColumnName;
                //hypField.HeaderText += "<img src='"+imgArrowDown+"' alt='' />";
                hypField.DataNavigateUrlFields = str;

                var userClickAction = String.Empty;
                //userClickAction = ApplicationCommon.GetDefaultActionLink();
                userClickAction = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.GridDefaultClickActionKey);
                userClickAction = userClickAction.ToLower();
                var actionPage = String.Empty;

                if (userClickAction == "detail")
                {
                    actionPage = "Details";
                }
                else if (userClickAction == "update")
                {
                    actionPage = "Update";
                }
                else
                {
                    actionPage = "Details";
                }

                if (tableFolder == " ")
                {
                    hypField.DataNavigateUrlFormatString = "~/" + entity + "/" + actionPage + ".aspx?SetId={0}";
                }
                else
                {
                    hypField.DataNavigateUrlFormatString = "~/" + tableFolder + "/" + entity + "/" + actionPage + ".aspx?SetId={0}";
                }
            }
            //hypField.NavigateUrl = "~/" + tableName + "/Details.aspx?SetId=" + dt.Columns[primaryKeyId];

            MainGridView.Columns.Add(hypField);
        }

        private int GetSystemEntityTypeId()
        {
            var data = new SystemEntityTypeDataModel();
            var auditId = SessionVariables.RequestProfile.AuditId;
            var entityName = ViewState["TableName"].ToString();
            data.EntityName = entityName;

			var dtEntityRow = SystemEntityTypeDataManager.Search(data, SessionVariables.RequestProfile).Rows[0];
            var entityId = int.Parse(dtEntityRow[SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId].ToString());

            return entityId;
        }

        private DataTable GetGridFormatSettings()
        {
            var entityId = GetSystemEntityTypeId();
            var auditId = SessionVariables.RequestProfile.AuditId;
            var dtGridFormat = new FieldConfigurationDataModel();

            dtGridFormat.SystemEntityTypeId = entityId;

			var gridFormatdt = FieldConfigurationDataManager.Search(dtGridFormat, SessionVariables.RequestProfile);

            return gridFormatdt;

            //var validColumns = _getColumnDelegate();
        }

        private static HorizontalAlign GetHeaderAlignment(string alignment)
        {
            switch (alignment)
            {
                case "Right":
                    return HorizontalAlign.Right;
	            case "Left":
                    return HorizontalAlign.Left;
	            case "Center":
                    return HorizontalAlign.Center;
	            case "Justify":
                    return HorizontalAlign.Justify;
	            default:
                    return HorizontalAlign.Center;
            }
        }

        private static DataTable SortDataTable(DataTable dt, string sort, string sortdirection)
        {
            var newDT = dt.Clone();
			var rowCount = dt.Rows.Count;

			var sortstring = sort + " " + sortdirection;
			var foundRows = dt.Select(null, sortstring);
            
			// Sort with Column name 
            for (var i = 0; i < rowCount; i++)
            {
				var arr = new object[dt.Columns.Count];
				for (var j = 0; j < dt.Columns.Count; j++)
                {
                    arr[j] = foundRows[i][j];
                }
				var data_row = newDT.NewRow();
                data_row.ItemArray = arr;
                newDT.Rows.Add(data_row);
            }

            //clear the incoming dt 
            dt.Rows.Clear();

            for (var i = 0; i < newDT.Rows.Count; i++)
            {
                var arr = new object[dt.Columns.Count];

                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    arr[j] = newDT.Rows[i][j];
                }

                var data_row = dt.NewRow();

                data_row.ItemArray = arr;
                dt.Rows.Add(data_row);
            }

            return dt;
        }

        private DataTable SortGridView(string sortExpression, string sortDirection)
        {
            var tableFolder = ViewState["TableFolder"].ToString();

            var tableName = ViewState["TableName"].ToString();
            var primaryKeyId = ViewState["PrimaryKey"].ToString();

	        //var isTesting = SessionVariables.IsTesting; //(bool)(ViewState["IsTesting"]);

			var currentPage = MainGridView.PageIndex;
			var currentPageSize = MainGridView.PageSize;

			var totalnumofrows = MainGridView.Rows.Count;

			var dtLocal = new DataTable();
			var sortedTable = new DataTable();
			var dtLocal2 = new DataTable();

            var floor = (currentPage * currentPageSize);
            var ceil = ((currentPage * currentPageSize) + currentPageSize) - 1;

            if (ceil > dtGlobal.Rows.Count)
                ceil = dtGlobal.Rows.Count - 1;

            if (ViewState["SessionUpdated"] != null)
            {
                dtGlobal = GetData();
            }
            else
            {
                dtGlobal = GetDataSet(tableName);
            }

            //Extract Sort Info from Session
            if (ViewState[Prefix + "SortExpression"] != null)
            {
                if (dtGlobal.Columns.Contains(ViewState[Prefix + "SortExpression"].ToString()))
                    sortExpression = ViewState[Prefix + "SortExpression"].ToString();

            }
            
			if (ViewState[Prefix + "SortDirection"] != null)
            {

                sortDirection = ViewState[Prefix + "SortDirection"].ToString();

            }
            else
            {
                if (sortDirection.Equals("Ascending"))
                    sortDirection = "ASC";
                else
                {
                    sortDirection = "DESC";
                }
            }

			var columns = new string[dtGlobal.Columns.Count];
			var colTypes = new Type[dtGlobal.Columns.Count];
			var cnt = 0;

            //create Data Table objects
            foreach (DataColumn column in dtGlobal.Columns)
            {
                columns[cnt] = column.ColumnName;
                colTypes[cnt] = column.GetType();
                cnt++;
            }

            dtLocal.Clear();
            dtLocal.Reset();

            for (var i = 0; i < columns.Length; i++)
            {
				var dc = new DataColumn(columns[i], typeof(string));
                sortedTable.Columns.Add(dc);
				
				var dc2 = new DataColumn(columns[i], typeof(string));
                dtLocal.Columns.Add(dc2);
				
				var dc3 = new DataColumn(columns[i], typeof(string));
                dtLocal2.Columns.Add(dc3);
            }

            //Load dtlocal with current rows in view 
			for (var i = floor; i <= ceil; i++)
            {
                if (dtGlobal.Rows.Count >= ceil && i < dtGlobal.Rows.Count)
                    dtLocal.ImportRow(dtGlobal.Rows[i]);
            }

            //sort current rows in view 
            if (!string.IsNullOrEmpty(sortExpression))
            {
                dtLocal2 = SortDataTable(dtLocal, sortExpression, sortDirection);
            }

            //rebuild the datatable with sorted rows in view and rest of the rows
			for (var i = 0; i < dtGlobal.Rows.Count; i++)
            {
                if (i == floor)
                {
					for (var j = 0; j <= dtLocal2.Rows.Count - 1; j++)
                    {
                        if (dtLocal2.Rows[j] != null)
                            sortedTable.ImportRow(dtLocal2.Rows[j]);
                        else
                        {
                            sortedTable.ImportRow(dtGlobal.Rows[j]);
                        }
                    }
                    i = ceil + 1;

                }

                if (i < dtGlobal.Rows.Count)
                    sortedTable.ImportRow(dtGlobal.Rows[i]);

            }

            // fix this name ..
            Sample(dtGlobal, primaryKeyId, HideData, SessionVariables.IsTesting);

            var dv = dtGlobal.DefaultView;

            // if blank, only should really be the first time
            // then we don't want to appened the sort instruction
            if (!string.IsNullOrEmpty(sortExpression) && dtGlobal.Columns.Contains(sortExpression))
            {
                dv.Sort = sortExpression + " " + sortDirection;
                //System.Diagnostics.Debug.WriteLine(dv.Sort);
            }

            //Bind data based on the sort selection
            if (!skipgridreload)
            {
                //if (RadioButtonList1.SelectedItem.Value.Equals("FTSort"))
                //{
                MainGridView.DataSource = dv;
                MainGridView.DataBind();
                //}
                //else
                //{
                //    MainGridView.DataSource = sortedtable;
                //    MainGridView.DataBind();
                //}
                if (MainGridView.AllowPaging)
                {
                    if (ViewState[Prefix + "CurrentPageIndex"] != null)
                        oGridPagiation.PageIndexInSession = int.Parse(ViewState[Prefix + "CurrentPageIndex"].ToString());
                    oGridPagiation.RefreshGrid = false;
                    oGridPagiation.ManagePaging(dtGlobal);
                }
            }
            //if (RadioButtonList1.SelectedItem.Value.Equals("FTSort"))
            //{
            return dv.ToTable(ViewState["TableName"].ToString());
            //}
            //else
            //{
            //    return sortedtable;
            //}
        }

        private DataTable GetDataSet(string tableName)
        {
            //var ds = (DataTable)Cache["TaskTrackerTable"];

            var ds = (DataTable)ViewState[Prefix + tableName];

            // Contact the database if necessary.
            //if (ds == null)
            //{

            ds = GetData();

            //Cache.Insert("TaskTrackerTable", ds, null, DateTime.MaxValue,
            //TimeSpan.FromMinutes(2));

            //    ViewState[tableName] = ds;
            //    lblCacheStatus.Text = "Created and added to cache.";
            //}
            //else
            //{
            //    lblCacheStatus.Text = "Retrieved from cache.";
            //}
            return ds;
        }

        protected override object SaveViewState()
        {
            var baseState = base.SaveViewState();
            
            return new[] { baseState, dtGlobal };
        }

        protected override void LoadViewState(object savedState)
        {
            var myState = (object[])savedState;

            if (myState[0] != null)
                base.LoadViewState(myState[0]);

            if (myState[1] != null)
            {

                //dtGlobal = (DataTable)myState[1];
                //if (ViewState[Prefix + "CurrentPageIndex"] != null)
                //    MainGridView.PageIndex = int.Parse(ViewState[Prefix + "CurrentPageIndex"].ToString());
                //else
                //    MainGridView.PageIndex = 0;

                //MainGridView.DataSource = dtGlobal;
                //MainGridView.DataBind();

                //if (ViewState[Prefix + "CurrentPageIndex"] != null)
                //{
                //    oGridPagiation.pageindexinsession = int.Parse(ViewState[Prefix + "CurrentPageIndex"].ToString());
                //}

                //if (oGridPagiation != null)
                //{
                //    oGridPagiation.ManagePaging(dtGlobal);
                //}
            }
        }

        #region sort

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                {
                    ViewState["SortDirection"] = SortDirection.Ascending;
                }

                return (SortDirection)ViewState["SortDirection"];
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        /// <summary>
        /// Grid OnSort EventHandler
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the expreson (Key)
            var sortExpression = e.SortExpression;

            if (ViewState[Prefix + "SortExpression"] == null)
                ViewState.Add(Prefix + "SortExpression", e.SortExpression);
            else
                ViewState[Prefix + "SortExpression"] = e.SortExpression;

            if (ViewState[Prefix + "SortDirection"] == null)
                ViewState.Add(Prefix + "SortDirection", "");
            // alternate direction););

            if (String.IsNullOrEmpty(ViewState[Prefix + "SortDirection"].ToString()))
            {
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    ViewState[Prefix + "SortDirection"] = "ASC";
                    SortGridView(sortExpression, "ASC");
                    GridViewSortDirection = SortDirection.Descending;

                }
                else
                {
                    ViewState[Prefix + "SortDirection"] = "DESC";
                    SortGridView(sortExpression, "DESC");
                    GridViewSortDirection = SortDirection.Ascending;

                }
            }
            else
            {
                if (ViewState[Prefix + "SortDirection"].ToString().Equals("DESC"))
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    ViewState[Prefix + "SortDirection"] = "ASC";

                }
                else
                {
                    GridViewSortDirection = SortDirection.Descending;
                    ViewState[Prefix + "SortDirection"] = "DESC";
                }
                SortGridView(sortExpression, ViewState[Prefix + "SortDirection"].ToString());
            }
        }

        #endregion

		//protected void MainGridView_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//}

    }
}