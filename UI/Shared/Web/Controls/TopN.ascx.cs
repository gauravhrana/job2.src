using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Text;

using System.Collections.Specialized;

namespace Shared.UI.Web.Controls
{
    public partial class TopN : System.Web.UI.UserControl
    {

        #region private variables

        public delegate DataTable GetDataDelegate();

        public delegate string[] GetColumnDelegate();

        private GetDataDelegate _getData;
        private GetColumnDelegate _getColumnDelegate;

        private DataTable dtGlobal = new DataTable();
        private GridPagiation oGridPagiation;
        private bool skipgridreload = false;

        #endregion

        #region properties

        public bool HideData { get; set; }

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

        #region methods

        public TopN()
        {
            HideData = false;
        }

        public void Setup(string tableName, GetDataDelegate getDataDelegate, GetColumnDelegate getColumnDelegate, bool pageLoad, bool isPaging)
        {
            lblHeader.Text = "Top " + tableName;
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
            ViewState["IsTesting"] = SessionVariables.IsTesting;
            ViewState["PageLoad"] = pageLoad;

            _getColumnDelegate = getColumnDelegate;
            _getData = getDataDelegate;

            dtGlobal = _getData();

            Sample(dtGlobal, HideData, SessionVariables.IsTesting);
            MainGridView.DataSource = dtGlobal;

            if (isPaging)
            {
                MainGridView.PageSize = SessionVariables.DefaultRowCount;

                oGridPagiation = new GridPagiation();
                oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page);
                oGridPagiation.Changed += oGridPagiation_Changed;

                if (ViewState[Prefix + "CurrentPageIndex"] != null)
                {
                    oGridPagiation.pageindexinsession = int.Parse(ViewState[Prefix + "CurrentPageIndex"].ToString());
                }

                oGridPagiation.ManagePaging(dtGlobal);

            }
            else
            {
                MainGridView.AllowPaging = isPaging;
            }
            MainGridView.DataBind();
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
            SortGridView(string.Empty, SortDirection.Ascending.ToString());
        }

        private void RefreshGrid()
        {
            if (ViewState[Prefix + "CurrentPageIndex"] != null)
                MainGridView.PageIndex = int.Parse(ViewState[Prefix + "CurrentPageIndex"].ToString());
            else
            {
                MainGridView.PageIndex = 0;
            }
            MainGridView.DataBind();

            oGridPagiation = new GridPagiation();
            dtGlobal = _getData();
            oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.pageindexinsession = MainGridView.PageIndex;
            oGridPagiation.ManagePaging(dtGlobal);
            if (ViewState[Prefix + "SortExpression"] != null && ViewState["SortDirection"] != null)
                SortGridView(ViewState[Prefix + "SortExpression"].ToString(), ViewState[Prefix + "SortDirection"].ToString());

        }

        private void Sample(DataTable dt, bool hideData, bool isTesting)
        {

            if (MainGridView.Columns.Count > 0)
            {
                MainGridView.Columns.Clear();
                //   return;
            }

            // Create Column 0 --> PrimaryKey

            // Declare the bound field and allocate memory for the bound field.
            // Initalize the DataField value.

            // boolean values set for the fields and procedures in the GridView            
            var procedureHide = hideData;
            var fieldHide = false;

            // Create a string array for DataNavigationUrlFields            
            var str = new string[1];
            str[0] = dt.Columns[0].ColumnName;

            // Create rest of columns based on what user has access to ...            
            var key = "Default";
            var disableLink = hideData;
            var validColumns = _getColumnDelegate();

            foreach (var t in validColumns)
            {
                // get the column from the data table
                var dataColumn = dt.Columns[t].ColumnName;

                if (dataColumn != null)
                {
                    // dynamically add hyperlink fields from the data table
                    //SetHyperLink(dataColumn, str, tableName, tableFolder, disableLink);



                    var boundField = new BoundField { DataField = dataColumn };
                    boundField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    boundField.HeaderText = dataColumn;
                    // Add the newly created bound field to the GridView.
                    MainGridView.Columns.Add(boundField);

                }
            }
        }

        // reName to better 
        private void SetHyperLink(DataColumn dataColumn, string[] str, string entity, string tableFolder, bool enableLInk)
        {
            var hypField = new HyperLinkField();

            var split = " ";

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
            }

            hypField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            hypField.DataTextField = dataColumn.ColumnName;
            if (!enableLInk)
            {
                hypField.SortExpression = dataColumn.ColumnName;
                hypField.DataNavigateUrlFields = str;

                if (tableFolder == " ")
                {
                    hypField.DataNavigateUrlFormatString = "~/" + entity + "/Details.aspx?SetId={0}";
                }
                else
                {
                    hypField.DataNavigateUrlFormatString = "~/" + tableFolder + "/" + entity + "/Details.aspx?SetId={0}";
                }
            }
            //hypField.NavigateUrl = "~/" + tableName + "/Details.aspx?SetId=" + dt.Columns[primaryKeyId];

            MainGridView.Columns.Add(hypField);
        }

        private DataTable SortDataTable(DataTable dt, string sort, string sortdirection)
        {
            DataTable newDT = dt.Clone();
            int rowCount = dt.Rows.Count;

            string sortstring = sort + " " + sortdirection;
            DataRow[] foundRows = dt.Select(null, sortstring);
            // Sort with Column name 
            for (int i = 0; i < rowCount; i++)
            {
                object[] arr = new object[dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    arr[j] = foundRows[i][j];
                }
                DataRow data_row = newDT.NewRow();
                data_row.ItemArray = arr;
                newDT.Rows.Add(data_row);
            }

            //clear the incoming dt 
            dt.Rows.Clear();

            for (int i = 0; i < newDT.Rows.Count; i++)
            {

                object[] arr = new object[dt.Columns.Count];

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    arr[j] = newDT.Rows[i][j];
                }
                DataRow data_row = dt.NewRow();
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
            var isTesting = (bool)(ViewState["IsTesting"]);
            int currentpage = MainGridView.PageIndex;
            int currentpagesize = MainGridView.PageSize;
            int totalnumofrows = MainGridView.Rows.Count;
            DataTable dtlocal = new DataTable();
            DataTable sortedtable = new DataTable();
            DataTable dtlocal2 = new DataTable();

            var floor = (currentpage * currentpagesize);
            var ceil = ((currentpage * currentpagesize) + currentpagesize) - 1;

            if (ceil > dtGlobal.Rows.Count)
                ceil = dtGlobal.Rows.Count - 1;

            if (ViewState["SessionUpdated"] != null)
            {
                dtGlobal = _getData();
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


            string[] columns = new string[dtGlobal.Columns.Count];
            Type[] coltypes = new Type[dtGlobal.Columns.Count];
            int cnt = 0;

            //create Data Table objects
            foreach (DataColumn column in dtGlobal.Columns)
            {
                columns[cnt] = column.ColumnName;
                coltypes[cnt] = column.GetType();
                cnt++;
            }


            dtlocal.Clear();
            dtlocal.Reset();
            for (int i = 0; i < columns.Length; i++)
            {
                DataColumn dc = new DataColumn(columns[i], typeof(string));
                sortedtable.Columns.Add(dc);
                DataColumn dc2 = new DataColumn(columns[i], typeof(string));
                dtlocal.Columns.Add(dc2);
                DataColumn dc3 = new DataColumn(columns[i], typeof(string));
                dtlocal2.Columns.Add(dc3);

            }

            //Load dtlocal with current rows in view 
            for (int i = floor; i <= ceil; i++)
            {
                if (dtGlobal.Rows.Count >= ceil && i < dtGlobal.Rows.Count)
                    dtlocal.ImportRow(dtGlobal.Rows[i]);

            }

            //sort current rows in view 
            if (!string.IsNullOrEmpty(sortExpression))
            {
                dtlocal2 = SortDataTable(dtlocal, sortExpression, sortDirection);
            }

            //rebuild the datatable with sorted rows in view and rest of the rows
            for (int i = 0; i < dtGlobal.Rows.Count; i++)
            {

                if (i == floor)
                {
                    for (int j = 0; j <= dtlocal2.Rows.Count - 1; j++)
                    {
                        if (dtlocal2.Rows[j] != null)
                            sortedtable.ImportRow(dtlocal2.Rows[j]);
                        else
                        {
                            sortedtable.ImportRow(dtGlobal.Rows[j]);
                        }
                    }
                    i = ceil + 1;

                }
                if (i < dtGlobal.Rows.Count)
                    sortedtable.ImportRow(dtGlobal.Rows[i]);

            }

            // fix this name ..
            Sample(dtGlobal, HideData, isTesting);

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
                        oGridPagiation.pageindexinsession = int.Parse(ViewState[Prefix + "CurrentPageIndex"].ToString());
                    oGridPagiation.refreshgrid = false;
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

            ds = _getData();

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

        #endregion

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

        #region Events

        void oGridPagiation_Changed(object sender, EventArgs e)
        {
            SortGridView(string.Empty, SortDirection.Ascending.ToString());
        }

        protected void GridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                var row = (DataRowView)e.Row.DataItem;
                var cells = e.Row.Cells;

                // var btn = (HyperLink)cells[0].FindControl("ButtonDelete");
                // btn.ImageUrl = Application["Branding"] + "/Images/delete.jpg";

            }
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

            dtGlobal = _getData();

            oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.pageindexinsession = e.NewPageIndex;
            oGridPagiation.ManagePaging(dtGlobal);
            /*
            if (ViewState[Prefix + "SortExpression"] != null && ViewState[Prefix + "SortDirection"] != null)
            {
                SortGridView(ViewState[Prefix + "SortExpression"].ToString(), ViewState[Prefix + "SortDirection"].ToString());
            }
            */
        }

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
                //        SortGridView(string.Empty, SortDirection.Ascending.ToString());
                //    }
                //    else
                //    {
                //        System.Diagnostics.Debug.WriteLine("Missing info ... ");
                //    }
                //}
            }
        }

        protected override object SaveViewState()
        {
            var baseState = base.SaveViewState();

            //Add the synamic page selection value to Session
            if (oGridPagiation != null)
            {
                SessionVariables.DefaultRowCount = oGridPagiation.pageSize;            
            }
            return new[] { baseState, dtGlobal };
        }

        protected override void LoadViewState(object savedState)
        {
            var myState = (object[])savedState;

            if (myState[0] != null)
                base.LoadViewState(myState[0]);

            if (myState[1] != null)
            { }
        }

        protected void MainGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

    }
}