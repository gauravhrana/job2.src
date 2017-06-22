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

    public partial class HistoryList : UserControl
    {

        enum GridViewMode
        {
            Classic,
            Advanced
        }

        enum GridViewAdvancedModeGrouping
        {
            TimeInterval,
            AuditAction,
            ActionByAndAuditAction
        }

        public delegate DataTable GetDataDelegate();

        public delegate string[] GetColumnDelegate();

        private GetDataDelegate _getData;
        private GetColumnDelegate _getColumnDelegate;

        private DataTable dtGlobal = new DataTable();
        private GridPagiation oGridPagiation;
        private bool skipgridreload = false;

        #region

        /*
        public bool IsVisible
        {
            get
            {
                return chkVisible.Checked;
            }
            set
            {
                chkVisible.Checked = value;
                ToggleControls(value);
            }
        }
        */

        public bool HideData { get; set; }

        public int SystemEntityTypeId { get; set; }

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

        private GridViewMode ViewMode
        {
            get
            {
                if (ViewState[Prefix + "GridViewMode"] == null)
                {
                    ViewState[Prefix + "GridViewMode"] = GridViewMode.Advanced;
                }

                return (GridViewMode)ViewState[Prefix + "GridViewMode"];
            }
            set
            {
                ViewState[Prefix + "GridViewMode"] = value;
            }
        }

        private GridViewAdvancedModeGrouping AdvancedModeGrouping
        {
            get
            {
                if (ViewState[Prefix + "AdvancedModeGrouping"] == null)
                {
                    ViewState[Prefix + "AdvancedModeGrouping"] = GridViewAdvancedModeGrouping.ActionByAndAuditAction;
                }

                return (GridViewAdvancedModeGrouping)ViewState[Prefix + "AdvancedModeGrouping"];
            }
            set
            {
                ViewState[Prefix + "AdvancedModeGrouping"] = value;
            }
        }

        #endregion properties

        private void ToggleControls(bool isVisible)
        {
            MainGridView.Visible = isVisible;
            if (this.ViewMode == GridViewMode.Advanced)
            {
                if (isVisible)
                {
                    dynAdvancedMode.Visible = isVisible;
                }
            }
            dynModeHolder.Visible = isVisible;
            plcPaging.Visible = isVisible;
            litPagingSummary.Visible = isVisible;
            lblCacheStatus.Visible = isVisible;

            //if (isVisible)
            //{
            //    chkVisible.Text = "Hide";
            //}
            //else
            //{
            //    chkVisible.Text = "Show";
            //}
        }

        public HistoryList()
        {
            HideData = false;
        }

        public float ConvertTimeIntervalInMinute(int timeInterval)
        {
            float rInterval = 0;
            var intervalUnit = drpIntervalUnit.SelectedItem.Value;
            if (intervalUnit == "minute")
            {
                rInterval = timeInterval;
            }
            else if (intervalUnit == "second")
            {
                rInterval = ((float)timeInterval / 60);
            }
            else if (intervalUnit == "hour")
            {
                rInterval = timeInterval * 60;
            }
            else if (intervalUnit == "day")
            {
                rInterval = timeInterval * 60 * 24;
            }
            else if (intervalUnit == "week")
            {
                rInterval = timeInterval * 60 * 24 * 7;
            }
            else if (intervalUnit == "month")
            {
                rInterval = timeInterval * 60 * 24 * 30;
            }
            else if (intervalUnit == "quarter")
            {
                rInterval = timeInterval * 60 * 24 * 90;
            }
            else if (intervalUnit == "year")
            {
                rInterval = timeInterval * 60 * 24 * 365;
            }
            else
            {
                rInterval = timeInterval;
            }
            return rInterval;
        }

        private System.Data.DataTable GetData()
        {
            var data = new Framework.Components.Audit.AuditHistory.Data();
            data.EntityKey = EntityKey;
            data.SystemEntityId = SystemEntityTypeId;


            DataTable dt = null;
            if (ViewMode == GridViewMode.Advanced)
            {
                if (AdvancedModeGrouping == GridViewAdvancedModeGrouping.TimeInterval)
                {
                    data.TimeInterval = ConvertTimeIntervalInMinute(Convert.ToInt32(txtInterval.Text));
                    dt = Framework.Components.Audit.AuditHistory.SearchByTimeInterval(data, SessionVariables.AuditId);
                }
                else if (AdvancedModeGrouping == GridViewAdvancedModeGrouping.AuditAction)
                {
                    dt = Framework.Components.Audit.AuditHistory.SearchByAuditAction(data, SessionVariables.AuditId);
                }
                else if (AdvancedModeGrouping == GridViewAdvancedModeGrouping.ActionByAndAuditAction)
                {
                    dt = Framework.Components.Audit.AuditHistory.SearchByActionByAndAuditAction(data, SessionVariables.AuditId);
                }
            }
            else
            {
                dt = Framework.Components.Audit.AuditHistory.Find(data, SessionVariables.AuditId);
            }

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    lblHistory.Visible = true;
            //}

            return dt;
        }

        private string[] GetColumns()
        {
            string[] validColumns;
            if (ViewMode == GridViewMode.Advanced)
            {
                if (this.AdvancedModeGrouping == GridViewAdvancedModeGrouping.AuditAction)
                {
					validColumns = Framework.Components.ApplicationSecurity.GetAuditHistoryColumns("FindByAuditAction", SessionVariables.AuditId);
                }
                else
                {
					validColumns = Framework.Components.ApplicationSecurity.GetAuditHistoryColumns("Default", SessionVariables.AuditId);
                }
            }
            else
            {
				validColumns = Framework.Components.ApplicationSecurity.GetAuditHistoryColumns("Find", SessionVariables.AuditId);
            }
            return validColumns;
        }

        public void Setup(string tableName, string tableFolder, string primaryKey, bool pageLoad, bool isPaging, int systemEntityId, int entityKey, string UserPreferenceCategory = "")
        {
            this.SystemEntityTypeId = systemEntityId;
            this.EntityKey = entityKey;
            this.Prefix = entityKey.ToString();

            var userPreferenceCategory = UserPreferenceCategory;
            if (string.IsNullOrEmpty(userPreferenceCategory))
            {
                userPreferenceCategory = tableName;
            }

            ViewState["TableFolder"] = tableFolder;

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
            ViewState["IsTesting"] = SessionVariables.IsTesting;
            ViewState["PageLoad"] = pageLoad;

            string userGrouping = ApplicationCommon.GetUserPreferenceByKey(ApplicationCommon.HistoryAdvancedModeGroupingKey, userPreferenceCategory);
            if (userGrouping == "timeinterval")
            {
                AdvancedModeGrouping = GridViewAdvancedModeGrouping.TimeInterval;
                txtInterval.Text = Convert.ToString(ConvertTimeIntervalInMinute(ApplicationCommon.GetUserPreferenceByKeyAsInt(ApplicationCommon.HistoryAdvancedModeIntervalKey)));
                drpAdvancedModeGrouping.SelectedValue = "timeinterval";
                dynIntervalMode.Visible = true;
            }
            else
            {
                dynIntervalMode.Visible = false;
                if (userGrouping == "auditaction")
                {
                    AdvancedModeGrouping = GridViewAdvancedModeGrouping.AuditAction;
                    drpAdvancedModeGrouping.SelectedValue = "auditaction";
                }
                else if (userGrouping == "actionby")
                {
                    AdvancedModeGrouping = GridViewAdvancedModeGrouping.ActionByAndAuditAction;
                    drpAdvancedModeGrouping.SelectedValue = "actionby";
                }
            }


            dtGlobal = GetData();

            Sample(dtGlobal, primaryKey, HideData, SessionVariables.IsTesting);
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

        void oGridPagiation_Changed(object sender, EventArgs e)
        {
            SortGridView(string.Empty, SortDirection.Ascending.ToString());
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

            dtGlobal = GetData();

            oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.pageindexinsession = e.NewPageIndex;
            oGridPagiation.ManagePaging(dtGlobal);
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
            dtGlobal = GetData();
            oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.pageindexinsession = MainGridView.PageIndex;
            oGridPagiation.ManagePaging(dtGlobal);
            if (ViewState[Prefix + "SortExpression"] != null && ViewState[Prefix + "SortDirection"] != null)
                SortGridView(ViewState[Prefix + "SortExpression"].ToString(), ViewState[Prefix + "SortDirection"].ToString());

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

        private void Sample(DataTable dt, string primaryKeyId, bool hideData, bool isTesting)
        {

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
            var procedureHide = hideData;
            var fieldHide = false;

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
                    if (dataColumn.ToLower() != "count")
                    {

                        var boundField = new BoundField { DataField = dataColumn };
                        boundField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        boundField.HeaderText = dataColumn;
                        if (dataColumn == "SystemEntity" || dataColumn == "EntityKey")
                            boundField.Visible = isTesting;

                        if (dataColumn.ToLower().Contains("id"))
                            boundField.Visible = false;

                        // Add the newly created bound field to the GridView.
                        MainGridView.Columns.Add(boundField);
                    }
                    else
                    {
                        SetHyperLink(dt.Columns[t], false);
                    }
                }
            }
        }

        // reName to better 
        private void SetHyperLink(DataColumn dataColumn, bool enableLInk)
        {
            var hypField = new HyperLinkField();

            var split = " ";


            hypField.HeaderText = dataColumn.ColumnName;
            hypField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            hypField.DataTextField = dataColumn.ColumnName;
            hypField.Target = "_blank";

            if (!enableLInk)
            {
                hypField.SortExpression = dataColumn.ColumnName;
                StringBuilder strQueryString = new StringBuilder();
                if (AdvancedModeGrouping == GridViewAdvancedModeGrouping.TimeInterval)
                {
                    hypField.DataNavigateUrlFields = new string[] { "SystemEntityId", "EntityKey", "AuditActionId", "PersonId", "Start", "End" };
                    strQueryString.Append("SystemEntity={0}&");
                    strQueryString.Append("EntityKey={1}&");
                    strQueryString.Append("AuditAction={2}&");
                    strQueryString.Append("ActionBy={3}&");

                    strQueryString.Append("Start={4:yyyy-MM-dd hh-mm-ss tt}&");
                    strQueryString.Append("End={5:yyyy-MM-dd hh-mm-ss tt}&");
                    //strQueryString.Append("Start=" + HttpUtility.UrlEncode("{4}") + "&");
                    //strQueryString.Append("End=" + HttpUtility.UrlEncode("{5}") + "");
                }
                else if (AdvancedModeGrouping == GridViewAdvancedModeGrouping.AuditAction)
                {
                    hypField.DataNavigateUrlFields = new string[] { "SystemEntityId", "EntityKey", "AuditActionId" };
                    strQueryString.Append("SystemEntity={0}&");
                    strQueryString.Append("EntityKey={1}&");
                    strQueryString.Append("AuditAction={2}");
                }
                else if (AdvancedModeGrouping == GridViewAdvancedModeGrouping.ActionByAndAuditAction)
                {
                    hypField.DataNavigateUrlFields = new string[] { "SystemEntityId", "EntityKey", "AuditActionId", "PersonId" };
                    strQueryString.Append("SystemEntity={0}&");
                    strQueryString.Append("EntityKey={1}&");
                    strQueryString.Append("AuditAction={2}&");
                    strQueryString.Append("ActionBy={3}");
                }
                //hypField.DataNavigateUrlFields = str;
                hypField.DataNavigateUrlFormatString = "~/History/HistoryRecordDetails.aspx?" + strQueryString.ToString();
            }

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
            Sample(dtGlobal, primaryKeyId, HideData, isTesting);

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

        protected void MainGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpGridViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpGridViewMode.SelectedValue == "classic")
            {
                this.ViewMode = GridViewMode.Classic;
                dynAdvancedMode.Visible = false;
            }
            else
            {
                txtInterval.Text = ApplicationCommon.GetUserPreferenceByKey(ApplicationCommon.HistoryAdvancedModeIntervalKey);
                dynAdvancedMode.Visible = true;
                this.ViewMode = GridViewMode.Advanced;
            }

            var primaryKeyId = ViewState["PrimaryKey"].ToString();
            dtGlobal = GetData();

            Sample(dtGlobal, primaryKeyId, HideData, SessionVariables.IsTesting);
            MainGridView.DataSource = dtGlobal;
            MainGridView.DataBind();
        }

        protected void drpAdvancedModeGrouping_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (drpGridViewMode.SelectedValue == "classic")
            //{
            //    this.ViewMode = GridViewMode.Classic;
            //    dynAdvancedMode.Visible = true;
            //}
            //else
            //{
            //    txtInterval.Text = Convert.ToString(ApplicationCommon.GetAuditHistoryAdvancedModeInterval());
            //    dynAdvancedMode.Visible = true;
            //    this.ViewMode = GridViewMode.Advanced;
            //}

            if (drpAdvancedModeGrouping.SelectedValue == "timeinterval")
            {
                this.AdvancedModeGrouping = GridViewAdvancedModeGrouping.TimeInterval;
                if (string.IsNullOrEmpty(txtInterval.Text))
                {
                    txtInterval.Text = Convert.ToString(ConvertTimeIntervalInMinute(ApplicationCommon.GetUserPreferenceByKeyAsInt(ApplicationCommon.HistoryAdvancedModeIntervalKey)));
                }
                dynIntervalMode.Visible = true;
            }
            else
            {
                dynIntervalMode.Visible = false;
                if (drpAdvancedModeGrouping.SelectedValue == "auditaction")
                {
                    this.AdvancedModeGrouping = GridViewAdvancedModeGrouping.AuditAction;
                }
                else
                {
                    this.AdvancedModeGrouping = GridViewAdvancedModeGrouping.ActionByAndAuditAction;
                }
            }

            var primaryKeyId = ViewState["PrimaryKey"].ToString();
            dtGlobal = GetData();

            Sample(dtGlobal, primaryKeyId, HideData, SessionVariables.IsTesting);
            MainGridView.DataSource = dtGlobal;
            MainGridView.DataBind();
        }

        protected void drpdrpIntervalUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            var primaryKeyId = ViewState["PrimaryKey"].ToString();
            dtGlobal = GetData();

            Sample(dtGlobal, primaryKeyId, HideData, SessionVariables.IsTesting);
            MainGridView.DataSource = dtGlobal;
            MainGridView.DataBind();
        }

        protected void txtInterval_TextChanged(object sender, EventArgs e)
        {
            //plcPaging.Controls.Clear();
            //ViewState["AuditHistoryAdvancedModeInterval"]

            int _pagesize = 0;
            if (int.TryParse(txtInterval.Text, out _pagesize))
            {
                SessionVariables.AuditHistoryAdvancedModeInterval = txtInterval.Text;
            }

            dtGlobal = GetData();

            MainGridView.DataSource = dtGlobal;
            MainGridView.DataBind();
        }

        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            //IsVisible = chkVisible.Checked;
        }

    }
}