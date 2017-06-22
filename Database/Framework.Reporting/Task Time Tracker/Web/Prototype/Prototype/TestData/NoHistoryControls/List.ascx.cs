using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.WebCommon.UI.Web;
using System.Collections.Specialized;
using System.Collections;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.ApplicationManagement.Development.TestData.NoHistoryControls
{
    public partial class List : Shared.UI.WebFramework.BaseControl
    {
        private const string VIEW_STATE_KEY_DELETE = "Delete";
        private const string VIEW_STATE_KEY_UPDATE = "Update";
        private const string VIEW_STATE_KEY_DETAIL = "Details";

        public delegate DataTable GetDataDelegate();

        public delegate string[] GetColumnDelegate();

        private GetDataDelegate _getData;
        private GetColumnDelegate _getColumnDelegate;

        private DataTable dtGlobal = new DataTable();
        private GridPagiation oGridPagiation;
        private bool skipgridreload = false;
        private bool disabledelete = false;
        private ArrayList fkcolumnids;

        #region properties

        string[] adminLinks = new string[] { "BatchFile", "BatchFileStatus", "FileType", "SystemEntityType", "BatchFileSet" };
        string[] eventMonitorLinks = new string[] { "ApplicationMonitoredEvent", "ApplicationMonitoredEventEmail", "ApplicationMonitoredEventProcessingState", "ApplicationMonitoredEventSource" };
        string[] configurationLinks = new string[] { "ApplicationEntityFieldLabel", "ApplicationEntityParentalHierarchy", "UserPreference", "UserPreferenceCategory", "UserPreferenceDataType", "UserPreferenceKey" };
        string[] authLinks = new string[] { "ApplicationOperation", "ApplicationOperationRoleMapping", "ApplicationRole", "ApplicationRoleMapping", "ApplicationUser", "ApplicationUserRoleMapping", "ApplicationUserTitle" };
        string[] taskLinks = new string[] { "TaskEntity", "TaskEntityType", "TaskRun", "TaskSchedule", "TaskScheduleType" };
        string[] auditLinks = new string[] { "AuditAction", "AuditHistory" };

        public int? CurrentPageIndex
        {
            get
            {
                if (ViewState["CurrentPageIndex"] == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToInt32(ViewState["CurrentPageIndex"]);
                }
            }
            set
            {
                ViewState["CurrentPageIndex"] = value;
            }
        }

        public bool HideData { get; set; }

        public bool CustomizedSearch
        {
            get
            {
                if (ViewState["CustomizedSearch"] == null)
                {
                    ViewState["CustomizedSearch"] = false;
                }

                return (bool)ViewState["CustomizedSearch"];
            }
            set
            {
                ViewState["CustomizedSearch"] = value;
            }
        }

        public bool IsUpdateColumn
        {
            get
            {
                if (ViewState["IsUpdateColumn"] == null)
                {
                    ViewState["IsUpdateColumn"] = true;
                }

                return (bool)ViewState["IsUpdateColumn"];
            }
            set
            {
                ViewState["IsUpdateColumn"] = value;
            }
        }

        public bool IsDeleteColumn
        {
            get
            {
                if (ViewState["IsDeleteColumn"] == null)
                {
                    ViewState["IsDeleteColumn"] = true;
                }

                return (bool)ViewState["IsDeleteColumn"];
            }
            set
            {
                ViewState["IsDeleteColumn"] = value;
            }
        }

        public string UserPreferenceCategory
        {
            get;
            set;
        }

        public int UserPreferenceCategoryId
        {
            get;
            set;
        }

        #endregion properties

        public List()
        {
            HideData = false;
        }

        public void Setup(string tableName, string tableFolder, string primaryKey, bool pageLoad, GetDataDelegate getDataDelegate, GetColumnDelegate getColumnDelegate, string userPreferenceCategory = "")
        {
            oGridPagiation = new GridPagiation();
            _getData = getDataDelegate;
            _getColumnDelegate = getColumnDelegate;

            ViewState["TableFolder"] = tableFolder;
            if (SessionVariables.ActiveTableName == null)
                SessionVariables.ActiveTableName = tableName;

            if (!string.IsNullOrEmpty(SessionVariables.ActiveTableName) && !(SessionVariables.ActiveTableName.Equals(tableName)))
            {
                SessionVariables.ActiveTableName = tableName;
            }

            if (CurrentPageIndex == null)
            {
                CurrentPageIndex = 0;
            }

            ViewState["TableName"] = tableName;
            ViewState["TableFolder"] = tableFolder;
            ViewState["PrimaryKey"] = primaryKey;
            //ViewState["IsTesting"] = SessionVariables.IsTesting;
            ViewState["PageLoad"] = pageLoad;

            if (string.IsNullOrEmpty(userPreferenceCategory))
            {
                UserPreferenceCategory = tableName;
            }
            else
            {
                UserPreferenceCategory = userPreferenceCategory;
            }
            SetUserPreferenceCategory();

            MainGridView.PageSize = SessionVariables.DefaultRowCount;

            dtGlobal = _getData();
			oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            
			if (CurrentPageIndex != null)
                oGridPagiation.PageIndexInSession = CurrentPageIndex.Value;

            oGridPagiation.ManagePaging(dtGlobal);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableFolder"></param>
        /// <param name="primaryKey"></param>
        /// <param name="pageLoad"></param>
        /// <param name="getDataDelegate"></param>
        /// <param name="getColumnDelegate"></param>
        /// <param name="isPaging">flag for paging</param>
        public void Setup(string tableName, string tableFolder, string primaryKey, bool pageLoad, GetDataDelegate getDataDelegate, GetColumnDelegate getColumnDelegate, bool isPaging, bool isUpdateColumn = true, bool isDeleteColumn = true, string userPreferenceCategory = "")
        {
            _getData = getDataDelegate;
            _getColumnDelegate = getColumnDelegate;

            ViewState["TableFolder"] = tableFolder;
            if (SessionVariables.ActiveTableName == null)
                SessionVariables.ActiveTableName = tableName;

            if (!string.IsNullOrEmpty(SessionVariables.ActiveTableName) && !(SessionVariables.ActiveTableName.Equals(tableName)))
            {
                SessionVariables.ActiveTableName = tableName;
            }
            
			if (CurrentPageIndex == null)
            {
                CurrentPageIndex = 0;
            }

            ViewState["TableName"] = tableName;
            ViewState["PrimaryKey"] = primaryKey;
            //ViewState["IsTesting"] = SessionVariables.IsTesting;
            ViewState["PageLoad"] = pageLoad;
            MainGridView.AllowPaging = isPaging;

            if (string.IsNullOrEmpty(userPreferenceCategory))
            {
                UserPreferenceCategory = tableName;
            }
            else
            {
                UserPreferenceCategory = userPreferenceCategory;
            }

            SetUserPreferenceCategory();

            if (isPaging)
            {
                MainGridView.PageSize = SessionVariables.DefaultRowCount;

                oGridPagiation = new GridPagiation();
				oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page, SettingCategory);
                oGridPagiation.Changed += oGridPagiation_Changed;
                if (CurrentPageIndex != null)
                    oGridPagiation.PageIndexInSession = CurrentPageIndex.Value;
                oGridPagiation.ManagePaging(dtGlobal);

            }

            this.IsUpdateColumn = isUpdateColumn;
            this.IsDeleteColumn = isDeleteColumn;

        }

        void oGridPagiation_Changed(object sender, EventArgs e)
        {
            SortGridView(String.Empty, SortDirection.Ascending.ToString());
        }

        public void SetSession(string sessionUpdated)
        {
            ViewState["SessionUpdated"] = sessionUpdated;
        }

        public void SetUserPreferenceCategory()
        {
            if (!string.IsNullOrEmpty(UserPreferenceCategory))
            {
                var data = new UserPreferenceCategoryDataModel();
                data.Name = UserPreferenceCategory;
				var dt = Framework.Components.UserPreference.UserPreferenceCategoryDataManager.Search(data, SessionVariables.RequestProfile);
                if (dt != null && dt.Rows.Count > 0)
                {
                    UserPreferenceCategoryId = Convert.ToInt32(dt.Rows[0]["UserPreferenceCategoryId"]);
                }
            }
        }

        // string searchLastName, string searchFirstName, 
        public void ShowData(bool dataHide, bool search)
        {
            CustomizedSearch = search;
            HideData = dataHide;

            // sort by default ...
            SortGridView(String.Empty, SortDirection.Ascending.ToString());
        }

        protected void MainGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string tablename = ViewState["TableName"].ToString();
                var row = (DataRowView)e.Row.DataItem;
                var cells = e.Row.Cells;

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var row = (DataRowView)e.Row.DataItem;
                var cells = e.Row.Cells;

                for (int j = 0; j < e.Row.Cells.Count; j++)
                {
                    if (j > 0 && j < e.Row.Cells.Count - 2)
                    {
                        //e.Row.Cells[j].Style.Add("font-weight", "bold");
                        //e.Row.Cells[j].Style.Add("width", "auto");
                        //e.Row.Cells[j].Style.Add("text-align", "right");
                    }
                    else if (j == e.Row.Cells.Count - 2 || j == e.Row.Cells.Count - 1)
                    {
                        e.Row.Cells[j].Style.Add("width", "60px");
                    }

                    var strSystemEntityType = Convert.ToString(row[1]);
                    var entityKey = Convert.ToString(row[2]);
                    if (adminLinks.Contains(strSystemEntityType))
                    {
                        strSystemEntityType = "Admin/" + strSystemEntityType;
                    }
                    else if (eventMonitorLinks.Contains(strSystemEntityType))
                    {
                        strSystemEntityType = "EventMonitoring/" + strSystemEntityType;
                    }
                    else if (authLinks.Contains(strSystemEntityType))
                    {
                        strSystemEntityType = "AuthenticationAndAuthorization/" + strSystemEntityType;
                    }
                    else if (configurationLinks.Contains(strSystemEntityType))
                    {
                        strSystemEntityType = "Configuration/" + strSystemEntityType;
                    }
                    else if (taskLinks.Contains(strSystemEntityType))
                    {
                        strSystemEntityType = "TasksAndWorkflow/" + strSystemEntityType;
                    }
                    else if (auditLinks.Contains(strSystemEntityType))
                    {
                        strSystemEntityType = "Audit/" + strSystemEntityType;
                    }

                    foreach (TableCell tmpCell in e.Row.Cells)
                    {
                        try
                        {
                            var hLink = (HyperLink)tmpCell.Controls[0];
                            hLink.NavigateUrl = "~/" + strSystemEntityType + "/Details.aspx?DetailIds=" + entityKey;
                        }
                        catch { }
                    }
                }
            }
        }

        protected void GridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
			//if (e.Row.RowType == DataControlRowType.Header)
			//{
			//	var row = (DataRowView)e.Row.DataItem;
			//	var cells = e.Row.Cells;

			//	// var btn = (HyperLink)cells[0].FindControl("ButtonDelete");
			//	// btn.ImageUrl = Application["Branding"] + "/Images/delete.jpg";

			//}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //var rowView = (DataRowView)e.Row.DataItem;
                var myCells = e.Row.Cells;

                int _deleteIndex = 0, _updateIndex = 0;

                if (ViewState[VIEW_STATE_KEY_DELETE] != null && ViewState[VIEW_STATE_KEY_UPDATE] != null)
                {
                    int.TryParse(ViewState[VIEW_STATE_KEY_DELETE].ToString(), out _deleteIndex);
                    int.TryParse(ViewState[VIEW_STATE_KEY_UPDATE].ToString(), out _updateIndex);

                    if (_deleteIndex < myCells.Count && _updateIndex < myCells.Count && IsDeleteColumn)
                    {
                        int deleteIndex = 0;
                        int.TryParse(ViewState[VIEW_STATE_KEY_DELETE].ToString(), out deleteIndex);
                        if (myCells[_deleteIndex] != null && myCells[_deleteIndex].Controls.Count > 0)
                        {
                            var deleteLink = (HyperLink)myCells[_deleteIndex].Controls[0];
                            deleteLink.ImageUrl = Application["Branding"] + "/Images/delete.jpg";
                        }
                    }

                    if (IsUpdateColumn && _updateIndex < myCells.Count)
                    {
                        int updateIndex = 0;
                        int.TryParse(ViewState[VIEW_STATE_KEY_DELETE].ToString(), out updateIndex);
                        if (myCells[_updateIndex] != null && myCells[_updateIndex].Controls.Count > 0)
                        {
                            var updateLink = (HyperLink)myCells[_updateIndex].Controls[0];
                            updateLink.ImageUrl = Application["Branding"] + "/Images/untitled1.png";
                        }
                    }
                }
            }
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //In-built paging implementation
            MainGridView.PageIndex = e.NewPageIndex;
            MainGridView.DataBind();

            CurrentPageIndex = e.NewPageIndex;

            //Synchronize with custom paging

            oGridPagiation = new GridPagiation();
            dtGlobal = _getData();
			oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.PageIndexInSession = e.NewPageIndex;
            oGridPagiation.ManagePaging(dtGlobal);
            
			if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
                SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);
        }

        private void RefreshGrid()
        {
            if (CurrentPageIndex != null)
                MainGridView.PageIndex = CurrentPageIndex.Value;
            else
            {
                MainGridView.PageIndex = 0;
            }

            MainGridView.DataBind();

            oGridPagiation = new GridPagiation();
            dtGlobal = _getData();
			oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.PageIndexInSession = MainGridView.PageIndex;
            oGridPagiation.ManagePaging(dtGlobal);
            
			if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
                SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);

        }

        // OnPageLoad Event Handler
        protected void Page_Load(object sender, EventArgs e)
        {
            // only need to set initial condition, else every other time it shoulld
            // be thee via state 
            if (!IsPostBack)
            {
                if ((bool)ViewState["PageLoad"])
                {
                    // only go forward if settings are correct
                    if (ViewState["TableName"] != null || ViewState["PrimaryKey"] != null)
                    {
                        // can not have this, as keys above are all blank
                        SortGridView(String.Empty, SortDirection.Ascending.ToString());
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Missing info ... ");
                    }
                }
            }
        }

        private void Sample(DataTable dt, string tableName, string tableFolder, string primaryKeyId, bool hideData, bool isTesting)
        {
            if (IsPostBack) return;

            // Create Column 0 --> PrimaryKey

            // Declare the bound field and allocate memory for the bound field.
            // Initalize the DataField value.
            //var bfield = new BoundField { DataField = primaryKeyId };
            //bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            //bfield.HeaderText = dt.Columns[0].ColumnName.Replace("Id", " Id");
            //bfield.Visible = isTesting;
            //// ConvertStringArrayToString(dt.Columns[0].ColumnName.Split(new char[] { 'I' }, 1));            
            //// Add the newly created bound field to the GridView.
            //if(!dt.Columns[0].ColumnName.Equals("PersonId"))
            //MainGridView.Columns.Add(bfield);

            // boolean values set for the fields and procedures in the GridView            
            var procedureHide = hideData;
            var fieldHide = false;

            // Create a string array for DataNavigationUrlFields            
            var str = new string[2];
            str[0] = primaryKeyId;
            str[1] = dt.Columns[0].ColumnName;

            // Create rest of columns based on what user has access to ...            
            var key = "Default";
            var disableLink = true;
            var validColumns = _getColumnDelegate();

            DataColumn idColumn = new DataColumn(str[0]);

            if (idColumn != null)
            {
                // dynamically add hyperlink fields from the data table
                SetHyperLink(idColumn, str, tableName, tableFolder, disableLink);
            }

            foreach (var t in validColumns)
            {
                // get the column from the data table
                var dataColumn = dt.Columns[t];

                if (dataColumn != null)
                {

                    // dynamically add hyperlink fields from the data table
                    SetHyperLink(dataColumn, str, tableName, tableFolder, disableLink);
                }
            }


            if (SessionVariables.ApplicationUserRoles == null)
            {
                ApplicationCommon.SetApplicationUserRoles();
            }

            var roles = SessionVariables.ApplicationUserRoles;
            var role = roles.Find(item => item.ApplicationRole == "System Coordinator");

            if (role != null)
            {
                disabledelete = false;
            }
            else
            {
                disabledelete = true;
            }

            // based on another bool parameters indicating if action buttons should show            
            // add action button / links            
            if (IsUpdateColumn)
            {
                //var userDetailVisibility = ApplicationCommon.GetGridDetailLinkVisibility(this.UserPreferenceCategoryId);
                var userDetailVisibility = false;
                if (userDetailVisibility)
                {

                    var userAction = "update"; //ApplicationCommon.GetDefaultActionLink();
                    if (userAction == "update")
                        AddProcedures(str, VIEW_STATE_KEY_DETAIL, tableName, tableFolder, procedureHide);
                    else
                        AddProcedures(str, VIEW_STATE_KEY_UPDATE, tableName, tableFolder, procedureHide);
                }
            }

            if (IsDeleteColumn)
            {
                var userDeleteVisibility = false; // ApplicationCommon.GetGridDeleteLinkVisibility(this.UserPreferenceCategoryId);
                if (userDeleteVisibility)
                {
                    AddProcedures(str, VIEW_STATE_KEY_DELETE, tableName, tableFolder, procedureHide);
                }
            }
        }

        // reName to better 
        private void SetHyperLink(DataColumn dataColumn, string[] str, string entity, string tableFolder, bool enableLInk)
        {

            //var bField = new BoundField();
            //bField.DataField = dataColumn.ColumnName;
            //bField.SortExpression = dataColumn.Caption;
            //bField.HeaderText = dataColumn.ColumnName;
            //bField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            //MainGridView.Columns.Add(bField);

            var hypField = new HyperLinkField();
            //string imgArrowDown = Application["Branding"] + "/Images/arrow-down.jpg";
            //string imgArrowUp = Application["Branding"] + "/Images/arrow-up.jpg";
            var split = " ";
            //var strPath = "";

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
            hypField.DataNavigateUrlFields = new string[] { "SystemEntityType", "EntityKey" };

            if (!enableLInk)
            {
                hypField.SortExpression = dataColumn.ColumnName;
                hypField.NavigateUrl = "~/{0}/Details.aspx?DetailIds={1}";
            }

            MainGridView.Columns.Add(hypField);
        }

        private void AddProcedures(string[] str, string procedureName, string tableName, string tableFolder, bool procedureHide)
        {
            var hypUpdateField = new HyperLinkField { HeaderText = procedureName };

            hypUpdateField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            hypUpdateField.Text = procedureName;
            hypUpdateField.DataNavigateUrlFields = str;

            if (tableFolder == " ")
            {
                hypUpdateField.DataNavigateUrlFormatString = "~/" + tableName + "/" + procedureName + ".aspx?SetId={0}";
            }
            else
            {
                hypUpdateField.DataNavigateUrlFormatString = "~/" + tableFolder + "/" + tableName + "/" + procedureName + ".aspx?SetId={0}";
            }

            hypUpdateField.Visible = !procedureHide;

            MainGridView.Columns.Add(hypUpdateField);

            ViewState[procedureName] = MainGridView.Columns.Count - 1;

            // To put the Update and Delete buttons at the right Index during RowCreated Event han
            //ViewState[procedureName] = dt.Columns.IndexOf(procedureName);            
        }

        private DataTable SortDataTable(DataTable dt, string sort, string sortdirection)
        {
            DataTable newDT = dt.Clone();
            int rowCount = dt.Rows.Count;

            string sortstring = sort + " " + sortdirection;
            DataRow[] foundRows = dt.Select(null, sortstring);
            // Sort with Column name 
            for (var i = 0; i < rowCount; i++)
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

            for (var i = 0; i < newDT.Rows.Count; i++)
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
	        var isTesting = SessionVariables.IsTesting;
            var customizedSearch = CustomizedSearch;

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

            if (customizedSearch && ViewState["SessionUpdated"] != null)
            {
                dtGlobal = _getData();
            }
            else
            {
                dtGlobal = GetDataSet(tableName);
            }

            //Extract Sort Info from Session
            if (!string.IsNullOrEmpty(SessionVariables.SortExpression))
            {
                if (dtGlobal.Columns.Contains(SessionVariables.SortExpression))
                    sortExpression = SessionVariables.SortExpression;

            }
            if (!string.IsNullOrEmpty(SessionVariables.SortDirection))
            {
                sortDirection = SessionVariables.SortDirection;
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

            //Check which sort is selected
            //If View Sort is selected
            if (RadioButtonList1.SelectedItem.Value.Equals("VSort"))
            {
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

                for (var i = 0; i < columns.Length; i++)
                {
                    DataColumn dc = new DataColumn(columns[i], typeof(string));
                    sortedtable.Columns.Add(dc);
                    DataColumn dc2 = new DataColumn(columns[i], typeof(string));
                    dtlocal.Columns.Add(dc2);
                    DataColumn dc3 = new DataColumn(columns[i], typeof(string));
                    dtlocal2.Columns.Add(dc3);

                }

                //Load dtlocal with current rows in view 
                for (var i = floor; i <= ceil; i++)
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
                for (var i = 0; i < dtGlobal.Rows.Count; i++)
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

            }

            // fix this name ..
            Sample(dtGlobal, tableName, tableFolder, primaryKeyId, HideData, isTesting);

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
                if (RadioButtonList1.SelectedItem.Value.Equals("FTSort"))
                {
                    MainGridView.DataSource = dv;
                    MainGridView.DataBind();
                }
                else
                {
                    MainGridView.DataSource = sortedtable;
                    MainGridView.DataBind();
                }
                if (MainGridView.AllowPaging)
                {
                    if (oGridPagiation != null)
                    {
                        oGridPagiation.RefreshGrid = false;
                        oGridPagiation.ManagePaging(dtGlobal);
                    }
                }
                disablemultipledelete(ViewState["TableName"].ToString());
                //if (disabledelete)
                //    disabledeletefunctionality();

                stylefkcolumns();

            }

            if (RadioButtonList1.SelectedItem.Value.Equals("FTSort"))
            {
                return dv.ToTable(ViewState["TableName"].ToString());
            }
            else
            {
                return sortedtable;
            }
        }
        
		//private void disabledeletefunctionality()
		//{
		//	MainGridView.Columns[MainGridView.Columns.Count - 1].Visible = false;
		//	ButtonDelete.Visible = false;
		//}

        private void stylefkcolumns()
        {
            fkcolumnids = new ArrayList();
            
			for (var i = 1; i < MainGridView.Columns.Count; i++)
            {
                if (MainGridView.Columns[i].HeaderText.Contains("Id") ||
                    MainGridView.Columns[i].HeaderText.Contains("ID"))
                {
                    if (!MainGridView.Columns[i].HeaderText.Equals(ViewState["TableName"].ToString() + " Id"))
                        fkcolumnids.Add(i);
                }
                for (int j = 0; j < ApplicationCommon.SystemEntities.Length; j++)
                {
                    if (MainGridView.Columns[i].HeaderText.Equals(ApplicationCommon.SystemEntities[j]) ||
                        MainGridView.Columns[i].HeaderText.Contains(ApplicationCommon.SystemEntities[j]))
                    {
                        if (!MainGridView.Columns[i].HeaderText.Equals(ViewState["TableName"].ToString() + " Id"))
                            fkcolumnids.Add(i);
                    }
                }
            }

            if (fkcolumnids.Count > 0)
            {
                for (int j = 0; j < fkcolumnids.Count; j++)
                {
                    var cellid = int.Parse(fkcolumnids[j].ToString());
                    for (int k = 0; k < MainGridView.Rows.Count; k++)
                    {
                        MainGridView.Rows[k].Cells[cellid].Style.Add("text-align", "center");
                    }
                }
            }
        }
        private DataTable GetDataSet(string tableName)
        {
            //var ds = (DataTable)Cache["TaskTrackerTable"];

            var ds = (DataTable)Session[tableName];

            // Contact the database if necessary.
            if (ds == null || ViewState["SessionUpdated"] != null)
            {
                if (bool.Parse(ViewState["SessionUpdated"].ToString()))
                {
                    ds = _getData();
                    //Cache.Insert("TaskTrackerTable", ds, null, DateTime.MaxValue,
                    //TimeSpan.FromMinutes(2));

                    Session[tableName] = ds;
                    lblCacheStatus.Text = "Created and added to cache.";
                }
            }
            else
            {
                lblCacheStatus.Text = "Retrieved from cache.";
            }

            return ds;
        }

        protected override object SaveViewState()
        {
            var baseState = base.SaveViewState();

            //Add the synamic page selection value to Session
            if (oGridPagiation != null)
            {
                SessionVariables.DefaultRowCount = oGridPagiation.PageSize;
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

                dtGlobal = (DataTable)myState[1];

                MainGridView.DataSource = dtGlobal;
                MainGridView.DataBind();
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

            SessionVariables.SortExpression = e.SortExpression;

            // alternate direction););
            if (String.IsNullOrEmpty(SessionVariables.SortDirection))
            {
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    SessionVariables.SortDirection = "ASC";
                    SortGridView(sortExpression, "ASC");
                    GridViewSortDirection = SortDirection.Descending;

                }
                else
                {
                    SessionVariables.SortDirection = "DESC";
                    SortGridView(sortExpression, "DESC");
                    GridViewSortDirection = SortDirection.Ascending;

                }
            }
            else
            {
                if (SessionVariables.SortDirection.Equals("DESC"))
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    SessionVariables.SortDirection = "ASC";

                }
                else
                {
                    GridViewSortDirection = SortDirection.Descending;
                    SessionVariables.SortDirection = "DESC";
                }
                SortGridView(sortExpression, SessionVariables.SortDirection);
            }
        }

        protected void GridView_Sorted(object sender, EventArgs e)
        {
            string imgArrowDown = ApplicationVariables.Branding + "/Images/arrow-down.jpg";
            string imgArrowUp = ApplicationVariables.Branding + "/Images/arrow-up.jpg";
            var sortexpression = SessionVariables.SortExpression;
            if (sortexpression.Contains("Name"))
            {
                sortexpression = sortexpression.Replace("Name", " Name");

            }
            else if (sortexpression.Contains("Id"))
            {
                sortexpression = sortexpression.Replace("Id", " Id");
            }

            foreach (DataControlField field in MainGridView.Columns)
            {
                if (field.HeaderText.Contains(sortexpression) || field.HeaderText.Equals(sortexpression))
                {
                    // strip off the old ascending/descending icon
                    int iconPosition = field.HeaderText.IndexOf(@"<img ");
                    if (iconPosition > 0)
                        field.HeaderText = field.HeaderText.Substring(0, iconPosition);

                    // See where to add the sort ascending/descending icon

                    if (SessionVariables.SortDirection == "ASC")
                        field.HeaderText += "<img src='" + imgArrowUp + "' alt='' />";
                    else
                        field.HeaderText += "<img src='" + imgArrowDown + "' alt='' />";
                }
            }


        }
        #endregion

        protected void MainGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            int currentpage = MainGridView.PageIndex;
            int currentpagesize = MainGridView.PageSize;
            int totalnumofrows = MainGridView.Rows.Count;

            var floor = (currentpage * currentpagesize);
            var ceil = ((currentpage * currentpagesize) + currentpagesize) - 1;

            StringCollection sc = new StringCollection();
            string id = String.Empty;
            DataTable dt1 = new DataTable();
            CheckBox selectall = (CheckBox)MainGridView.HeaderRow.Cells[0].FindControl("chkSelectAll");
            
			bool chkchecked = selectall.Checked;
            
			if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
            {
                skipgridreload = true;
                dt1 = SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);
                skipgridreload = false;
            }
            else
            {
                dt1 = dtGlobal;
            }

            MainGridView.DataSource = dt1;
            MainGridView.DataBind();
            oGridPagiation = new GridPagiation();
			oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dt1, MainGridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.ManagePaging(dt1);
            selectall = (CheckBox)MainGridView.HeaderRow.Cells[0].FindControl("chkSelectAll");
            selectall.Checked = chkchecked;
            //loop the GridView Rows
            
			int j = 0;
            
			for (var i = floor; i <= ceil; i++)
            {
                if (j < MainGridView.Rows.Count)
                {
                    CheckBox cb = (CheckBox)MainGridView.Rows[j].Cells[0].FindControl("CheckBox1"); //find the CheckBox

                    if (cb != null && selectall != null)
                    {
                        if (selectall.Checked)
                        {
                            if (!cb.Checked)
                            {
                                cb.Checked = true; // add the id to be deleted in the StringCollection
                            }
                        }
                        else
                        {
                            if (cb.Checked)
                            {
                                cb.Checked = false;
                            }
                        }
                    }

                    j++;
                }
            }
        }

        private StringCollection GetSelectedRecordIDs()
        {
            int currentpage = MainGridView.PageIndex;
            int currentpagesize = MainGridView.PageSize;
            int totalnumofrows = MainGridView.Rows.Count;
            var floor = (currentpage * currentpagesize);
            var ceil = ((currentpage * currentpagesize) + currentpagesize) - 1;

            StringCollection sc = new StringCollection();
            string id = String.Empty;
            DataTable dt1 = new DataTable();
            
			if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
            {
                skipgridreload = true;
                dt1 = SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);
                skipgridreload = false;
            }
            else
            {
                dt1 = dtGlobal;
            }

            if (ceil > dt1.Rows.Count)
                ceil = dt1.Rows.Count - 1;
            //loop the GridView Rows
            int j = 0;

            for (var i = floor; i <= ceil; i++)
            {
                if (j < MainGridView.Rows.Count)
                {
                    CheckBox cb = (CheckBox)MainGridView.Rows[j].Cells[0].FindControl("CheckBox1"); //find the CheckBox
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            id = dt1.Rows[i][0].ToString(); // get the id of the field to be deleted
                            sc.Add(id); // add the id to be deleted in the StringCollection
                        }
                    }
                    j++;
                }
            }

            return sc;

        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                StringCollection sc = new StringCollection();
                sc = GetSelectedRecordIDs();
                RetrieveRecords(sc, Command.Delete);
            }
            catch (Exception ex)
            {
                string msg = "Deletion Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
            }
        }

        protected void ButtonDetails_Click(object sender, EventArgs e)
        {

            try
            {
                StringCollection sc = new StringCollection();
                sc = GetSelectedRecordIDs();
                RetrieveRecords(sc, Command.Details);

            }
            catch (Exception ex)
            {
                string msg = "Details Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {

            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                StringCollection sc = new StringCollection();
                sc = GetSelectedRecordIDs();
                RetrieveRecords(sc, Command.Update);
            }
            catch (Exception ex)
            {
                string msg = "Updation Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {

            }
        }

        protected void lnkfontsmall_Click(object sender, EventArgs e)
        {
            //griddiv.Style.Add("font-size", "12px");
            //MainGridView.CssClass = "smallfontgrid";
            MainGridView.DataBind();


            oGridPagiation = new GridPagiation();
            dtGlobal = _getData();
			oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.ManagePaging(dtGlobal);
            if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
                SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);
            //MainGridView.CssClass = "smallfontgrid";
        }

        protected void lnkfontmedium_Click(object sender, EventArgs e)
        {
            //griddiv.Style.Add("font-size", "14px");
            //MainGridView.CssClass = "mediumfontgrid";
            MainGridView.DataBind();

            oGridPagiation = new GridPagiation();
            dtGlobal = _getData();
            oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.ManagePaging(dtGlobal);
            if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
                SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);
            //MainGridView.CssClass = "mediumfontgrid";
        }

        protected void lnkfontlarger_Click(object sender, EventArgs e)
        {
            //griddiv.Style.Add("font-size", "16px");
            //MainGridView.CssClass = "largerfontgrid";
            MainGridView.DataBind();

            oGridPagiation = new GridPagiation();
            dtGlobal = _getData();
			oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dtGlobal, MainGridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.ManagePaging(dtGlobal);
            
			if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
                SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);
            
			//MainGridView.CssClass = "largerfontgrid";
        }

        private void RetrieveRecords(StringCollection sc, Command cmd)
        {

            try
            {
                ShowSelectedRecords(sc, cmd);
                DataTable dt1 = new DataTable();
                if (_getData != null)
                {
                    dt1 = _getData();
                    MainGridView.DataSource = dt1;
                    MainGridView.DataBind();
                }
                oGridPagiation = new GridPagiation();
                oGridPagiation.Setup(plcPaging, litPagingSummary, lblCacheStatus, dt1, MainGridView, Page, SettingCategory);
                oGridPagiation.Changed += oGridPagiation_Changed;
                oGridPagiation.ManagePaging(dt1);
            }
            catch (Exception ex)
            {
                string msg = "Deletion Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {

            }
        }

        private enum Command
        {
            Delete = 0,
            Details = 1,
            Update = 2
        } ;
        private void ShowSelectedRecords(StringCollection sc, Command cmd)
        {
            string _tableFolder = Convert.ToString(ViewState["TableFolder"]);
            string _tableName = Convert.ToString(ViewState["TableName"]);
            string redirecturl = "";

            string _tablePath = String.Empty;

            if (string.IsNullOrEmpty(_tableFolder.Trim()))
            {
                _tablePath = "~/" + _tableName;
            }
            else
            {
                _tablePath = "~/" + _tableFolder + "/" + _tableName;
            }

            if (cmd.Equals(Command.Delete))
            {
                redirecturl = _tablePath + "/Details.aspx?DeleteIds=";
            }
            else if (cmd.Equals(Command.Details))
            {
                redirecturl = _tablePath + "/Details.aspx?DetailIds=";
            }
            else
            {
                redirecturl = _tablePath + "/Update.aspx?UpdateIds=";
            }


            foreach (string _str in sc)
            {
                redirecturl += _str;
                redirecturl += ",";
            }

            redirecturl = redirecturl.Remove(redirecturl.Length - 1, 1);
            Response.Redirect(redirecturl, false);

        }

        private void disablemultipledelete(string tablename)
        {
            switch (tablename)
            {
                case "AuditAction":
                case "AuditHistory":
                case "TaskFormulation":
                case "Demo":
                    {
                        MainGridView.Columns[0].Visible = false;
                        break;
                    }
            }
        }
    }
}