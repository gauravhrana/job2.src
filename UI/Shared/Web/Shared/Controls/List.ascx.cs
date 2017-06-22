using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using System.Collections.Specialized;
using System.Collections;

namespace Shared.UI.Web.Controls
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
		private DataTable gridFormatdt = new DataTable();
		private double totalGridColumnWidth = 0;
		private GridPagiation oGridPagiation;
		private bool skipgridreload = false;
		private bool disabledelete = false;

		private ArrayList fkcolumnids;
		
		public List()
		{
			HideData = false;
		}

		public void SetSession(string sessionUpdated)
		{
			ViewState["SessionUpdated"] = sessionUpdated;
		}

		public void SetUserPreferenceCategory()
		{
			if (!string.IsNullOrEmpty(UserPreferenceCategory))
			{
				var data = new UserPreferenceCategory.Data();
				data.Name = UserPreferenceCategory;

				var dt = Framework.Components.UserPreference.UserPreferenceCategory.Search(data, SessionVariables.AuditId);

				if (dt != null && dt.Rows.Count > 0)
				{
					var id = dt.Rows[0][Framework.Components.UserPreference.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId];
					UserPreferenceCategoryId = Convert.ToInt32(id);
				}
			}
		}

        private DataTable GetGroupedRecords()
        {
            DataTable dt = null;
            if (!string.IsNullOrEmpty(GroupByField))
            {
                dt = (DataTable)Session[CoreTableName];
                if (dt != null)
                {
                    var dv = dt.DefaultView;
                    dv.RowFilter = GroupByField + " = '" + GroupByFieldValue + "'";
                    dt = dv.ToTable();
                }
            }
            return dt;
        }
		
		// string searchLastName, string searchFirstName, 
		public void ShowData(bool dataHide, bool search)
		{
			CustomizedSearch = search;
			HideData = dataHide;

			// sort by default ...
			SortGridView(string.Empty, SortDirection.Ascending.ToString());
		}

		private DataTable GetGridFormatSettings()
		{
			var dtGridFormat = new FieldConfiguration.Data();
			dtGridFormat.SystemEntityTypeId = GetSystemEntityTypeId();
            DataTable gridFormatdt = null;
            if (Session[CoreTableName + "_GridFormatSettings"] != null)
            {
                gridFormatdt = (DataTable)Session[CoreTableName + "_GridFormatSettings"];
            }
            else
            {
                gridFormatdt = FieldConfiguration.Search(dtGridFormat, SessionVariables.AuditId);
                Session[CoreTableName + "_GridFormatSettings"] = gridFormatdt;
            }

			return gridFormatdt;
		}

		private int GetSystemEntityTypeId()
		{
			var entityName = CoreTableName;
			var entityId = 0;
			
			try
			{
				entityId = (int)System.Enum.Parse(typeof(SystemEntity), entityName);
			}
			catch { }
			
			return entityId;
		}		

		private void oGridPagiation_Changed(object sender, EventArgs e)
		{
			SortGridView(string.Empty, SortDirection.Ascending.ToString());
		}        
		
		// OnPageLoad Event Handler
		protected void Page_Load(object sender, EventArgs e)
		{
			// only need to set initial condition, else every other time it shoulld
			// be thee via state 

            if (IsPostBack)
            {
                return;
            }

			if (!((bool)ViewState["PageLoad"])) return;

			// only go forward if settings are correct
			if (ViewState["TableName"] != null || ViewState["PrimaryKey"] != null)
			{
				// can not have this, as keys above are all blank
				SortGridView(string.Empty, SortDirection.Ascending.ToString());
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Missing info ... ");
			}

            SessionVariables.SearchControlColumnsModeId = 10019;
			GridActionBarBackgroundColor = ApplicationCommon.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarBackgroundColor);
			GridActionBarForegroundColor = ApplicationCommon.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarForegroundColor);
			GridActionBarFontFamily = ApplicationCommon.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarFontFamily);
			GridActionBarFontSize = ApplicationCommon.GetApplicationUserPreferenceByKey(ApplicationCommon.GridActionBarFontSize);
			divGridActionBar.Attributes.Add("style", "border: 2px none; border-top-left-radius: 15px;border-top-right-radius: 15px;");
			divGridActionBar.Style.Add("background-color", GridActionBarBackgroundColor);
			divGridActionBar.Style.Add("color", GridActionBarForegroundColor);
			divGridActionBar.Style.Add("font-family", GridActionBarFontFamily);
			divGridActionBar.Style.Add("font-size", GridActionBarFontSize);
            SetFontForGrid("12px", "smallfontgrid");
			//AddCheckBox();

            var isButtonPanelVisible = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, this.SettingCategory + "ButtonPanel");
            var isAdvancedButtonPanelVisible = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, this.SettingCategory + "AdvancedButtonPanel");

            buttonPanel.Visible = isButtonPanelVisible;
            advancedButtonpanel.Visible = isAdvancedButtonPanelVisible;

		}

		private void BindColumns(DataTable dt, string tableName, string primaryKeyId, bool hideData, bool isTesting)
		{
			gridFormatdt = GetGridFormatSettings();

			// if (IsPostBack) return;

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
			var disableLink = hideData;
			var validColumns = _getColumnDelegate();

            //if (!string.IsNullOrEmpty(GroupByField) && validColumns.Contains(GroupByField))
            //{
            //    //validColumns.d
            //}

			var idColumn = new DataColumn(str[0]);

			if (idColumn != null)
			{
				for (var i = 0; i < validColumns.Length; i++)
				{
					if (validColumns[i].Equals(idColumn.ColumnName))
					{
                        if (!string.IsNullOrEmpty(GroupByField) || idColumn.ColumnName != GroupByField)
                        {
                            // dynamically add hyperlink fields from the data table
                            SetHyperLink(idColumn, str, tableName, disableLink);
                        }
					}
				}
			}

			foreach (DataRow row in gridFormatdt.Rows)
			{
				//Considering all the columns have their width fixed from the Database
				totalGridColumnWidth += Double.Parse(row[FieldConfiguration.DataColumns.Width].ToString());
			}

			foreach (var t in validColumns)
			{
				// get the column from the data table
				if (!t.Equals(idColumn.ColumnName))
				{
					var dataColumn = dt.Columns[t];
					//var i = 0;
					//str[0] = t;
					//str[1] = t;
					if (dataColumn != null)
                    {
                        if (string.IsNullOrEmpty(GroupByField)  || dataColumn.ColumnName != GroupByField)
                        {
                            //if (dataColumn.ColumnName.ToLower() != idColumn.ColumnName.ToLower())
                            // dynamically add hyperlink fields from the data table
                            SetHyperLink(dataColumn, str, tableName, disableLink);
                        }
					}
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
				var userDetailVisibility = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.GridDetailLinkVisibleKey, this.UserPreferenceCategory);

				if (userDetailVisibility)
				{
					var userAction = ApplicationCommon.GetUserPreferenceByKey(ApplicationCommon.GridDefaultClickActionKey);
					//var userAction = ApplicationCommon.GetDefaultActionLink();
					if (userAction == "update")
						ListHelper.AddProcedures(MainGridView, ViewState, str, VIEW_STATE_KEY_DETAIL, tableName, procedureHide);
					else
						ListHelper.AddProcedures(MainGridView, ViewState, str, VIEW_STATE_KEY_UPDATE, tableName, procedureHide);
				}
			}

			if (IsDeleteColumn)
			{
				var userDeleteVisibility = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.GridDeleteLinkVisibleKey, this.UserPreferenceCategory);
				if (userDeleteVisibility)
				{
					ListHelper.AddProcedures(MainGridView, ViewState, str, VIEW_STATE_KEY_DELETE, tableName, procedureHide);
				}
			}
		}

        private void BindColumns(DataTable dt, string tableName, string primaryKeyId, bool hideData, bool isTesting, string[] validColumns, string userPreferenceCategory)
        {
            gridFormatdt = GetGridFormatSettings();            

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

            var idColumn = new DataColumn(str[0]);

            if (idColumn != null)
            {
                for (var i = 0; i < validColumns.Length; i++)
                {
                    if (validColumns[i].Equals(idColumn.ColumnName))
                    {
                        if (!string.IsNullOrEmpty(GroupByField) || idColumn.ColumnName != GroupByField)
                        {
                            // dynamically add hyperlink fields from the data table
                            SetHyperLink(idColumn, str, tableName, disableLink);
                        }
                    }
                }
            }

            foreach (DataRow row in gridFormatdt.Rows)
            {
                //Considering all the columns have their width fixed from the Database
                totalGridColumnWidth += Double.Parse(row[FieldConfiguration.DataColumns.Width].ToString());
            }

            foreach (var t in validColumns)
            {
                // get the column from the data table
                if (!t.Equals(idColumn.ColumnName))
                {
                    var dataColumn = dt.Columns[t];
                    //var i = 0;
                    //str[0] = t;
                    //str[1] = t;
                    if (dataColumn != null)
                    {
                        if (string.IsNullOrEmpty(GroupByField) || dataColumn.ColumnName != GroupByField)
                        {
                            //if (dataColumn.ColumnName.ToLower() != idColumn.ColumnName.ToLower())
                            // dynamically add hyperlink fields from the data table
                            SetHyperLink(dataColumn, str, tableName, disableLink);
                        }
                    }
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
                var userDetailVisibility = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.GridDetailLinkVisibleKey, userPreferenceCategory);

                if (userDetailVisibility)
                {
                    var userAction = ApplicationCommon.GetUserPreferenceByKey(ApplicationCommon.GridDefaultClickActionKey);
                    //var userAction = ApplicationCommon.GetDefaultActionLink();
                    if (userAction == "update")
                        ListHelper.AddProcedures(MainGridView, ViewState, str, VIEW_STATE_KEY_DETAIL, tableName, procedureHide);
                    else
                        ListHelper.AddProcedures(MainGridView, ViewState, str, VIEW_STATE_KEY_UPDATE, tableName, procedureHide);
                }
            }

            if (IsDeleteColumn)
            {
                var userDeleteVisibility = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.GridDeleteLinkVisibleKey, userPreferenceCategory);
                if (userDeleteVisibility)
                {
                    ListHelper.AddProcedures(MainGridView, ViewState, str, VIEW_STATE_KEY_DELETE, tableName, procedureHide);
                }
            }
        }

		// reName to better 
		private void SetHyperLink(DataColumn dataColumn, string[] str, string entity, bool enableLInk)
		{
			var hypField = new HyperLinkField();
			var flagGridFormat = false;

			var totalColumnWidth = 0;
			var headerWidth = "100%";
			var headerAlignment = "Center";
			var headerTextValue = "";
			var columnsExists = false;
			//ConsoleColor headerColor	;	

			//var imgArrowDown = Application["Branding"] + "/Images/arrow-down.jpg";
			//var imgArrowUp = Application["Branding"] + "/Images/arrow-up.jpg";
			var split = " ";

			if (dataColumn.ColumnName == "ApplicationId" && entity != "Application")
			{
				hypField.Visible = false;
			}

			//bool flagContainsDescriptionColumn = false;

			//no need to call on each dataColumn
			//var gridFormatdt = GetGridFormatSettings();

			foreach (DataRow row in gridFormatdt.Rows)
			{
				var headerTextName = row[FieldConfiguration.DataColumns.Name].ToString();

				headerTextValue = row[FieldConfiguration.DataColumns.Value].ToString();

				if (dataColumn.ColumnName == headerTextName)
				{

					//Set the remaining width of the column to Description, Set 100 as width for Checkbox column
					if (headerTextName == "Description" && (1000 - (totalGridColumnWidth + 100)) > 0)
					{
						headerWidth = (1000 - (totalGridColumnWidth + 100)).ToString();
					}
					else
					{
						headerWidth = row[FieldConfiguration.DataColumns.Width].ToString();
					}

					headerAlignment = row[FieldConfiguration.DataColumns.HorizontalAlignment].
					ToString();

					//headerColor		= row[Framework.Components.UserPreference.FieldConfiguration.DataColumns.Formatting].
					//ToString();

					flagGridFormat = true;

					//totalColumnWidth += int.Parse(headerWidth);
					break;

				}

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
				//flagGridFormat = true;
			}
			else
			{
				hypField.HeaderText = dataColumn.ColumnName;
				//hypField.HeaderStyle.ForeColor = 
			}


			//if (flagGridFormat)
			//{
			if (!headerWidth.Contains('-'))
				hypField.HeaderStyle.Width = Unit.Parse(headerWidth);
			else
				hypField.HeaderStyle.Width = '*';

			//hypField.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
			hypField.ItemStyle.HorizontalAlign = ListHelperAlignment.GetHeaderAlignment(headerAlignment);
			//hypField.HeaderStyle.BackColor = System.Drawing.Color.Black;

			if (!string.IsNullOrEmpty(headerTextValue))
			{
				hypField.HeaderText = headerTextValue;
			}
			hypField.HeaderStyle.BorderColor = System.Drawing.Color.Black;
			hypField.HeaderStyle.BorderWidth=1;			
			//hypField.ItemStyle.HorizontalAlign = GetHeaderAlignment(headerAlignment);
			hypField.DataTextField = dataColumn.ColumnName;

			if (!enableLInk)
			{
				hypField.SortExpression = dataColumn.ColumnName;
				//hypField.HeaderText += "<img src='"+imgArrowDown+"' alt='' />";
				hypField.DataNavigateUrlFields = str;

				var userClickAction = string.Empty;
				userClickAction = ApplicationCommon.GetUserPreferenceByKey(ApplicationCommon.GridDefaultClickActionKey);
				userClickAction = userClickAction.ToLower();
				var actionPage = string.Empty;

				if (userClickAction == "detail")
				{
					actionPage = "Details";
				}
				else if (userClickAction == "update")
				{
					actionPage = "Update";
				}
				else if (userClickAction == "inlineupdate")
				{
					actionPage = "Update";
				}
				else
				{
					actionPage = "Details";
				}

				var userMode = string.Empty;

				if (!string.IsNullOrEmpty(Request.QueryString["user"]))
				{
					userMode = "&user=" + Request.QueryString["user"];
				}				                  
				
                hypField.DataNavigateUrlFormatString = Page.GetRouteUrl(entity + "EntityRoute", new { Action = actionPage }) + "/{0}" + userMode; 
				
			}

			foreach (DataControlField column in MainGridView.Columns)
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
				MainGridView.Columns.Add(hypField);
			}
		}
				
		private DataTable SortGridView(string sortExpression, string sortDirection)
		{
			var tableName			= ViewState["TableName"].ToString();
			var primaryKeyId		= ViewState["PrimaryKey"].ToString();
			var isTesting			= (bool)(ViewState["IsTesting"]);
			var customizedSearch	= CustomizedSearch;

			var currentpage = MainGridView.PageIndex;
			var currentpagesize = MainGridView.PageSize;
			var totalnumofrows = MainGridView.Rows.Count;

			var dtlocal = new DataTable();
			var sortedtable = new DataTable();
			var dtlocal2 = new DataTable();

			var floor = (currentpage * currentpagesize);
			var ceil = ((currentpage * currentpagesize) + currentpagesize) - 1;

			if (ceil > dtGlobal.Rows.Count)
				ceil = dtGlobal.Rows.Count - 1;

			if (customizedSearch && ViewState["SessionUpdated"] != null)
			{
                if (_getData != null)
                {
                    dtGlobal = _getData();
                }
                else
                {
                    dtGlobal = GetGroupedRecords();
                }
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
				var columns = new string[dtGlobal.Columns.Count];
				var coltypes = new Type[dtGlobal.Columns.Count];

				var cnt = 0;

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
					var dc = new DataColumn(columns[i], typeof(string));
					sortedtable.Columns.Add(dc);

					var dc2 = new DataColumn(columns[i], typeof(string));
					dtlocal.Columns.Add(dc2);

					var dc3 = new DataColumn(columns[i], typeof(string));
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
					dtlocal2 = ListHelperSort.SortDataTable(dtlocal, sortExpression, sortDirection);
				}

				//rebuild the datatable with sorted rows in view and rest of the rows
				for (var i = 0; i < dtGlobal.Rows.Count; i++)
				{
					if (i == floor)
					{
						for (var j = 0; j <= dtlocal2.Rows.Count - 1; j++)
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
			BindColumns(dtGlobal, tableName, primaryKeyId, HideData, isTesting);

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

                CurrentPageIndex = MainGridView.PageIndex;
				if (MainGridView.AllowPaging)
				{
                    numberedPager.Setup(dtGlobal.Rows.Count, MainGridView);
                    numberedPager.CurrentIndex = CurrentPageIndex.Value;
                    numberedPager.CreateNumberedPagingControl();
					if (oGridPagiation != null)
					{
						oGridPagiation.refreshgrid = false;
						oGridPagiation.ManagePaging(dtGlobal);
					}
				}
				DisableMultipleDelete(ViewState["TableName"].ToString());
				//if (disabledelete)
				//    disabledeletefunctionality();

				//stylefkcolumns();
                if(!string.IsNullOrEmpty(FieldConfigurationMode))
                    ListHelper.StyleDataRows(MainGridView, ViewState, int.Parse(FieldConfigurationMode));
                else
                    ListHelper.StyleDataRows(MainGridView, ViewState, -1);
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
		
		private DataTable GetDataSet(string tableName)
		{
			//var ds = (DataTable)Cache["TaskTrackerTable"];

			var ds = (DataTable)Session[tableName];

			// Contact the database if necessary.
			if (ds == null || ViewState["SessionUpdated"] != null)
			{
                //if ( ViewState["SessionUpdated"] != null && bool.Parse(ViewState["SessionUpdated"].ToString()))
                //{
                if (_getData != null)
                {
                    ds = _getData();
                    //Cache.Insert("TaskTrackerTable", ds, null, DateTime.MaxValue,
                    //TimeSpan.FromMinutes(2));

                    Session[tableName] = ds;
                }
                else
                {
                    ds = GetGroupedRecords();
                }
				//}
			}

			return ds;
		}

		protected override object SaveViewState()
		{
			var baseState = base.SaveViewState();

			//Add the dynamic page selection value to Session
			if (oGridPagiation != null)
			{
				//SessionVariables.DefaultRowCount = oGridPagiation.pageSize;
			}
            
            var validColumns = _getColumnDelegate();

            return new[] { baseState, dtGlobal, validColumns, UserPreferenceCategory };
		}

		protected override void LoadViewState(object savedState)
		{
			var myState = (object[])savedState;

			if (myState[0] != null)
				base.LoadViewState(myState[0]);

			if (myState[1] != null && myState[2] != null && myState[3] != null)
			{
				dtGlobal = (DataTable)myState[1];
                var validColumns = (string[]) myState[2];
                var upCategory = Convert.ToString(myState[3]);

                var tableName = Convert.ToString(ViewState["TableName"]);
                var primaryKeyId = Convert.ToString(ViewState["PrimaryKey"]);
                var isTesting = SessionVariables.IsTesting;

                BindColumns(dtGlobal, tableName, primaryKeyId, HideData, isTesting, validColumns, upCategory);

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
		protected void MainGridView_Sorting(object sender, GridViewSortEventArgs e)
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

		protected void MainGridView_Sorted(object sender, EventArgs e)
		{
			//string imgArrowDown = ApplicationVariables.Branding + "/Images/arrow-down.jpg";
			//string imgArrowUp = ApplicationVariables.Branding + "/Images/arrow-up.jpg";
			
			var imgArrowDown = string.Empty;
			var imgArrowUp = string.Empty;

			var userSortArrowStyle = ApplicationCommon.GetApplicationUserPreferenceByKey(ApplicationCommon.SortArrowStyle);
			
			if (userSortArrowStyle == "SortArrowStyle1")
			{
				imgArrowDown = "&nbsp; &darr;";
				imgArrowUp = "&nbsp; &uarr;";
			}
			else if (userSortArrowStyle == "SortArrowStyle2")
			{
				imgArrowDown = "&nbsp; &#x25BC;";
				imgArrowUp = "&nbsp; &#x25B2;";

			}
			else
			{
				imgArrowDown = "&nbsp; &#x25BE;";
				imgArrowUp = "&nbsp; &#x25B4";
			}

			var sortexpression = SessionVariables.SortExpression;
			if (sortexpression.Contains("Order"))
			{
				sortexpression = sortexpression.Replace("Order", " Order");

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
					//int iconPosition = field.HeaderText.IndexOf(@"<img ");
					var iconPosition = field.HeaderText.IndexOf("&nbsp;");
					if (iconPosition > 0)
						field.HeaderText = field.HeaderText.Substring(0, iconPosition);

					// See where to add the sort ascending/descending icon

					if (SessionVariables.SortDirection == "ASC")
						//field.HeaderText += "<img src='" + imgArrowUp + "' alt='' />";
						field.HeaderText += imgArrowUp;
					else
						//field.HeaderText += "<img src='" + imgArrowDown + "' alt='' />";
						field.HeaderText += imgArrowDown;
				}
			}
		}
		#endregion

		
		protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			
			var currentpage = int.Parse(CurrentPageIndex.ToString());
			var currentpagesize = MainGridView.PageSize;
			//var totalnumofrows = MainGridView.Rows.Count;
			
			var floor = (currentpage * currentpagesize);
			var ceil = ((currentpage * currentpagesize) + currentpagesize) - 1;
			
			//var sc = new StringCollection();
			//var id = string.Empty;

			var dt1 = new DataTable();

			var selectall = SelectAllCheckBox;
			var chkchecked = selectall.Checked;

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
			oGridPagiation.Setup(plcPaging, litPagingSummary, dt1, MainGridView, Page);
			oGridPagiation.Changed += oGridPagiation_Changed;
			oGridPagiation.ManagePaging(dt1);

            numberedPager.Setup(dt1.Rows.Count, MainGridView);
            numberedPager.Changed += oGridPagiation_Changed;

			ListHelper.AddCheckBox(MainGridView);
			
			SelectAllCheckBox.Checked = chkchecked;

			

			//loop the GridView Rows
			var j = 0;

			for (var i = floor; i <= ceil; i++)
			{
				if (j < MainGridView.Rows.Count)
				{
					var cb = (CheckBox)MainGridView.Rows[j].Cells[0].FindControl("CheckBox1"); //find the CheckBox
					
					if (cb == null)
					{
						for (var k = 0; k < MainGridView.Rows[j].Cells[0].Controls.Count; k++)
						{
							if (MainGridView.Rows[j].Cells[0].Controls[k].ID.Equals("CheckBox1"))
								cb = (CheckBox)MainGridView.Rows[j].Cells[0].Controls[k];
						}
					}

					if (cb != null && SelectAllCheckBox != null)
					{
						if (SelectAllCheckBox.Checked)
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

			if (SelectAllCheckBox.Checked)
			{
				ButtonDelete.Enabled = true;
				ButtonUpdate.Enabled = true;
				ButtonDetails.Enabled = true;
				ButtonCommonUpdate.Enabled = true;
				ButtonInlineUpdate.Enabled = true;

				ButtonDetails.Style.Add("background-color", "#B40404");
				ButtonDelete.Style.Add("background-color", "#B40404");
				ButtonUpdate.Style.Add("background-color", "#B40404");
				ButtonCommonUpdate.Style.Add("background-color", "#B40404");
				ButtonInlineUpdate.Style.Add("background-color", "#B40404");
				
				//ButtonDetails.Style.Add("display",""); //below 3 lines added to make the Delete, Details, Update buttons visible.
				//ButtonUpdate.Style.Add("display", "");
				//ButtonDelete.Style.Add("display", "");
			}
			else
			{
				ButtonDelete.Enabled = false;
				ButtonUpdate.Enabled = false;
				ButtonDetails.Enabled = false;
				ButtonCommonUpdate.Enabled = false;
				ButtonInlineUpdate.Enabled = false;

				ButtonDetails.Style.Add("background-color", "#808080");
				ButtonDelete.Style.Add("background-color", "#808080");
				ButtonUpdate.Style.Add("background-color", "#808080");
				ButtonCommonUpdate.Style.Add("background-color", "#808080");
				ButtonInlineUpdate.Style.Add("background-color", "#808080");
				

				//ButtonDetails.Style.Add("display", "none");//below 3 lines added to make the Delete, Details, Update buttons invisible.
				//ButtonUpdate.Style.Add("display", "none");
				//ButtonDelete.Style.Add("display", "none");
			}
		}

		private StringCollection GetSelectedRecordIDs()
		{
			var currentpage = MainGridView.PageIndex;
			var currentpagesize = MainGridView.PageSize;
			var totalnumofrows = MainGridView.Rows.Count;

			var floor = (currentpage * currentpagesize);
			var ceil = ((currentpage * currentpagesize) + currentpagesize) - 1;

			var sc = new StringCollection();
			var id = string.Empty;
			var dt1 = new DataTable();

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
			var j = 0;

			for (var i = floor; i <= ceil; i++)
			{
				if (j < MainGridView.Rows.Count)
				{
					var cb = (CheckBox)MainGridView.Rows[j].Cells[0].FindControl("CheckBox1"); //find the CheckBox

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
		
		private void RetrieveRecords(StringCollection sc, ActionCommand cmd)
		{
			try
			{
				ShowSelectedRecords(sc, cmd);

				var dt1 = new DataTable();

                if (_getData != null)
                {
                    dt1 = _getData();
                    
                }
                else
                {
                    dt1 = GetGroupedRecords();
                }

                MainGridView.DataSource = dt1;
                MainGridView.DataBind();

				oGridPagiation = new GridPagiation();
				oGridPagiation.Setup(plcPaging, litPagingSummary, dt1, MainGridView, Page);
				oGridPagiation.Changed += oGridPagiation_Changed;
				oGridPagiation.ManagePaging(dt1);

                numberedPager.Setup(dt1.Rows.Count, MainGridView);
                numberedPager.Changed += oGridPagiation_Changed;
			}
			catch (Exception ex)
			{
				var msg = "Deletion Error:";
				msg += ex.Message;
				throw new Exception(msg);
			}
		}		

		private void ShowSelectedRecords(StringCollection sc, ActionCommand cmd)
		{
			var _tableFolder = Convert.ToString(ViewState["TableFolder"]);
			var _tableName = CoreTableName;
			var redirecturl = "";
			
			if (sc.Count > 1)
			{
                var systemEntityTypeId = GetSystemEntityTypeId();
                var superKeyId = ApplicationCommon.GenerateSuperKey(sc, systemEntityTypeId);

                redirecturl = Page.GetRouteUrl(_tableName + "EntityRouteSuperKey", new { Action = cmd.ToString(), SuperKey = superKeyId });
			}
			else if (sc.Count == 1)
			{
                redirecturl = Page.GetRouteUrl(_tableName + "EntityRoute", new { Action = cmd.ToString(), SetId = sc[0] });                
			}

			if (!string.IsNullOrEmpty(Request.QueryString["user"]))
			{
				redirecturl += "&user=" + Request.QueryString["user"];
			}

			if (cmd.Equals(ActionCommand.TestData))
			{
				redirecturl += "&Mode=Test";
			}
			else if (cmd.Equals(ActionCommand.RealData))
			{
				redirecturl += "&Mode=Real";
			}
			else if (cmd.Equals(ActionCommand.Renumber))
			{
				if (string.IsNullOrEmpty(txtSeed.Text) || string.IsNullOrEmpty(txtIncrement.Text))
					return;

				redirecturl += Page.AppRelativeTemplateSourceDirectory + "RenumberData.aspx?Mode=Renumber&Seed=" + txtSeed.Text + "&Increment=" + txtIncrement.Text;
			}

			Response.Redirect(redirecturl, false);
		}

		private void DisableMultipleDelete(string tablename)
		{
			switch (tablename)
			{
				case "AuditAction":
				//case "TaskFormulation":
				case "Demo":
					{
						MainGridView.Columns[0].Visible = false;
						break;
					}
			}
		}

		protected void lnkColumnChooser_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Shared/Admin/ColumnChooser.aspx?entity=" + Convert.ToString(ViewState["TableName"]), true);
		}

		private string CoreTableName
		{
			get { return Convert.ToString(ViewState["TableName"]); }
		}

		private string GridActionBarBackgroundColor
		{
			get
			{
				if (ViewState["GridActionBarBackgroundColor"] != null)
				{
					return Convert.ToString(ViewState["GridActionBarBackgroundColor"]);
				}
				return string.Empty;
			}
			set
			{
				ViewState["GridActionBarBackgroundColor"] = value;
			}
		}

		private string GridActionBarForegroundColor
		{
			get
			{
				if (ViewState["GridActionBarForegroundColor"] != null)
				{
					return Convert.ToString(ViewState["GridActionBarForegroundColor"]);
				}
				return string.Empty;
			}
			set
			{
				ViewState["GridActionBarForegroundColor"] = value;
			}
		}

		private string GridActionBarFontFamily
		{
			get
			{
				if (ViewState["GridActionBarFontFamily"] != null)
				{
					return Convert.ToString(ViewState["GridActionBarFontFamily"]);
				}
				return string.Empty;
			}
			set
			{
				ViewState["GridActionBarFontFamily"] = value;
			}
		}

		private string GridActionBarFontSize
		{
			get
			{
				if (ViewState["GridActionBarFontSize"] != null)
				{
					return Convert.ToString(ViewState["GridActionBarFontSize"]);
				}
				return string.Empty;
			}
			set
			{
				ViewState["GridActionBarFontSize"] = value;
			}
		}

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }
	}
}