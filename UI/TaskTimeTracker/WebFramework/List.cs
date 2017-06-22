using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.WebFramework
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:List runat=server></{0}:List>")]
	public class List : Panel
	{
		private GridView GridView;
		private PlaceHolder plcPaging = new PlaceHolder();
		private Label litPagingSummary = new Label();
		private Label lblCacheStatus = new Label();
		
		public delegate DataTable GetDataDelegate();

		public delegate string[] GetColumnDelegate();

		private bool _hideData = false;

		private GetDataDelegate _getData;
		private GetColumnDelegate _getColumnDelegate;

		// Global variables for pager settings
		const int pageSize = 5;
		const int pageDispCount = 10;

		private DataTable dtGlobal = new DataTable();

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				String s = (String)ViewState["Text"];
				return ((s == null) ? String.Empty : s);
			}
			set
			{
				ViewState["Text"] = value;
			}
		}


		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			
			GridView = new GridView();
			GridView.ID = ID + "_GridView";
			GridView.AllowPaging = true;
			GridView.PageSize = 5;
			//GridView.Width = "100%";
			GridView.AllowSorting = true;
			GridView.AutoGenerateColumns = false;

			GridView.Sorting += GridView_Sorting;
			GridView.RowCreated += GridView_RowCreated;
		}


		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "Execution")]
		protected override void CreateChildControls()
		{
			// Add a LiteralControl to the current ControlCollection.
			//Controls.Add(new LiteralControl("<h3>Value: "));			
			
			Controls.Add(GridView);

			Controls.Add(plcPaging);
            Controls.Add(litPagingSummary);
			Controls.Add(lblCacheStatus);

			// Create a text box control, set the default Text property, 
			// and add it to the ControlCollection.
			Controls.Add(new LiteralControl("</h3>"));

			var box = new TextBox();
			box.Text = "0";
			Controls.Add(box);

			Controls.Add(new LiteralControl("</h3>"));

			base.CreateChildControls();

		}

		public override void RenderControl(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			GridView.RenderControl(writer);
			writer.RenderEndTag();    // td
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("/");
			writer.RenderEndTag();    // td
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			plcPaging.RenderControl(writer);
			writer.RenderEndTag();    // td
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write("/");
			writer.RenderEndTag();    // td
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			litPagingSummary.RenderControl(writer);
			writer.RenderEndTag();    // td
			writer.RenderEndTag();    // tr
			writer.RenderEndTag();    // table
			lblCacheStatus.RenderControl(writer);
		}


		//protected override void RenderContents(HtmlTextWriter output)
		//{
		//    output.Write(Text);
		//}		

		//public void pgChange(object sender, GridViewPageEventArgs e)
		//{
		//    GridView.PageIndex = e.NewPageIndex;
		//    SortGridView(String.Empty, SortDirection.Ascending.ToString());                
		//}

		protected void GridView_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.Pager)
			{
				e.Row.Visible = false;
			}
		}

		public bool HideData
		{
			set
			{
				_hideData = value;
			}
			get
			{
				return _hideData;
			}
		}

		public void Setup(string tableName, string tableFolder, string primaryKey, bool pageLoad, GetDataDelegate getDataDelegate, GetColumnDelegate getColumnDelegate)
		{
			_getData = getDataDelegate;
			_getColumnDelegate = getColumnDelegate;

			ViewState["TableFolder"] = tableFolder;
			ViewState["TableName"] = tableName;
			ViewState["PrimaryKey"] = primaryKey;
			ViewState["IsTesting"] = HttpContext.Current.Session["IsTesting"];
			ViewState["PageLoad"] = pageLoad;
		}

		public void SetSession(string sessionUpdated)
		{
			ViewState["SessionUpdated"] = sessionUpdated;
		}

		// string searchLastName, string searchFirstName, 
		public void ShowData(bool dataHide, bool search)
		{
			CustomizedSearch = search;
			HideData = dataHide;

			// sort by default ...
			SortGridView(String.Empty, SortDirection.Ascending.ToString());
		}

		// OnPageLoad Event Handler
		protected void Page_Load(object sender, EventArgs e)
		{
			// only need to set initial condition, else every other time it shoulld
			// be thee via state 
			//if (!IsPostBack)
			//{
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
			//}
		}

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

		/// <summary>
		/// Grid OnSort EventHandler
		/// </summary>
		/// <param Name="sender"></param>
		/// <param Name="e"></param>
		protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
		{
			// get the expreson (Key)
			var sortExpression = e.SortExpression;

			// alternate direction
			if (GridViewSortDirection == SortDirection.Ascending)
			{
				GridViewSortDirection = SortDirection.Descending;
				SortGridView(sortExpression, "DESC");
			}
			else
			{
				GridViewSortDirection = SortDirection.Ascending;
				SortGridView(sortExpression, "ASC");
			}
		}

		private void Sample(DataTable dt, string tableName, string tableFolder, string primaryKeyId, bool hideData, bool isTesting)
		{
			//if (IsPostBack) return;

			// ***
			// Create Column 0 --> PrimaryKey
			// ***

			// Declare the bound field and allocate memory for the bound field.
			var bfield = new BoundField();

			// Initalize the DataField value.
			bfield.DataField = primaryKeyId;

			bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

			// Initialize the HeaderText field value.
			bfield.HeaderText = dt.Columns[0].ColumnName.Replace("Id", " Id");

			//ConvertStringArrayToString(dt.Columns[0].ColumnName.Split(new char[] { 'I' }, 1));

			// Add the newly created bound field to the GridView.
			GridView.Columns.Add(bfield);
			bfield.Visible = isTesting;

			// boolean values set for the fields and procedures in the GridView

			var procedureHide = hideData;
			var fieldHide = false;

			// Create a string array for DataNavigationUrlFields            
			var str = new string[2];
			str[0] = primaryKeyId;
			str[1] = dt.Columns[0].ColumnName;

			// ***
			// Create rest of columns based on what user has access to ...
			// ***
			//var key = "Default";
			var disableLink = hideData;
			var validColumns = _getColumnDelegate();

			for (var iAccessibleFields = 0; iAccessibleFields < validColumns.Length; iAccessibleFields++)
			{
				// get the column from the data table
				var dataColumn = dt.Columns[validColumns[iAccessibleFields]];

				if (dataColumn != null)
				{
					// dynamically add hyperlink fields from the data table
					funx(dataColumn, str, tableName, tableFolder, disableLink);
				}
			}

			// based on another bool parameters indicating if action buttons should show
			// ****
			// add action button / links
			// ****                                
			AddProcedures(dt, str, "Update", tableName, tableFolder, procedureHide);
			AddProcedures(dt, str, "Delete", tableName, tableFolder, procedureHide);
			//AddProcedures(dt, str, "Clone", tableName, tableFolder,  procedureHide);
		}

		// reName to better 
		private void funx(DataColumn dataColumn, string[] str, string entity, string tableFolder, bool enableLInk)
		{
			var hypField = new HyperLinkField();

			string split = " ";

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

			GridView.Columns.Add(hypField);
		}

		private void AddProcedures(DataTable dt, string[] str, string procedureName, string tableName, string tableFolder, bool procedureHide)
		{
			var hypUpdateField = new HyperLinkField();

			hypUpdateField.HeaderText = procedureName;
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
			
			GridView.Columns.Add(hypUpdateField);
			hypUpdateField.Visible = !procedureHide;
		}

		private void SortGridView(string sortExpression, string sortDirection)
		{
			//dtGlobal = _getData();

			var tableFolder = ViewState["TableFolder"].ToString();
			var tableName = ViewState["TableName"].ToString();
			var primaryKeyId = ViewState["PrimaryKey"].ToString();
			var isTesting = (bool)(ViewState["IsTesting"]);
			var customizedSearch = CustomizedSearch;

			if (customizedSearch == true || ViewState["SessionUpdated"] != null)
			{
				dtGlobal = _getData();
			}
			else
			{

				dtGlobal = GetDataSet(tableName);
			}

			// fix this name ..
			Sample(dtGlobal, tableName, tableFolder, primaryKeyId, HideData, isTesting);

			var dv = dtGlobal.DefaultView;

			// if blank, only should really be the first time
			// then we don't want to appened the sort instruction
			if (!string.IsNullOrEmpty(sortExpression))
			{
				dv.Sort = sortExpression + " " + sortDirection;
				//System.Diagnostics.Debug.WriteLine(dv.Sort);
			}

			GridView.DataSource = dv;
			GridView.DataBind();

			managePaging(dtGlobal);
		}

		private DataTable GetDataSet(string tableName)
		{
			//var ds = (DataTable)Cache["TaskTrackerTable"];

			var ds = (DataTable)HttpContext.Current.Session[tableName];

			// Contact the database if necessary.
			if (ds == null)
			{
				ds = _getData();
				//Cache.Insert("TaskTrackerTable", ds, null, DateTime.MaxValue,
				//TimeSpan.FromMinutes(2));

				HttpContext.Current.Session[tableName] = ds;
				lblCacheStatus.Text = "Created and added to cache.";
			}
			else
			{
				lblCacheStatus.Text = "Retrieved from cache.";
			}
			return ds;
		}

		private void objLb_Click(object sender, EventArgs e)
		{

			plcPaging.Controls.Clear();

			var objlb = (LinkButton)sender;

			GridView.PageIndex = (int.Parse(objlb.CommandArgument) - 1);

			SortGridView(String.Empty, SortDirection.Ascending.ToString());

			//managePaging(dtGlobal);
		}

		protected override object SaveViewState()
		{
			var baseState = base.SaveViewState();
			return new [] { baseState, dtGlobal };
		}

		protected override void LoadViewState(object savedState)
		{
			var myState = (object[])savedState;

			if (myState[0] != null)
				base.LoadViewState(myState[0]);

			if (myState[1] != null)
			{
				dtGlobal = (DataTable)myState[1];

				GridView.DataSource = dtGlobal;

				GridView.DataBind();

				managePaging(dtGlobal);
			}
		}
		
		protected void managePaging(DataTable _dt)
		{
			if (_dt.Rows.Count > 0)
			{
				// Variable declaration

				int numberOfPages;
				int numberOfRecords = dtGlobal.Rows.Count;
				int currentPage = (GridView.PageIndex);

				var strSummary = new StringBuilder();

				// If number of records is more then the page size (specified in global variable)
				// Just to check either gridview have enough records to implement paging

				if (numberOfRecords > pageSize)
				{
					// Calculating the total number of pages
					numberOfPages = (int)Math.Ceiling((double)numberOfRecords / (double)pageSize);
				}
				else
				{
					numberOfPages = 1;
				}

				// Creating a small summary for records.
				strSummary.Append("Displaying <b>");

				// Creating X f X Records
				int floor = (currentPage * pageSize) + 1;

				strSummary.Append(floor.ToString());

				strSummary.Append("</b>-<b>");

				var ceil = ((currentPage * pageSize) + pageSize);

				//let say you have 26 records and you specified 10 page size, 
				// On the third page it will return 30 instead of 25 as that is based on pageSize
				// So this check will see if the ceil value is increasing the number of records. Consider numberOfRecords

				if (ceil > numberOfRecords)
				{
					strSummary.Append(numberOfRecords.ToString());
				}
				else
				{
					strSummary.Append(ceil.ToString());
				}

				// Displaying Total number of records Creating X of X of About X records.
				strSummary.Append("</b> of About <b>");
				strSummary.Append(numberOfRecords.ToString());
				strSummary.Append("</b>Records</br>");

				litPagingSummary.Text = strSummary.ToString();

				//Variable declaration 
				//these variables will used to calculate page number display

				int pageShowLimitStart = 1;
				int pageShowLimitEnd = 1;

				// Just to check, either there is enough pages to implement page number display logic.
				if (pageDispCount > numberOfPages)
				{
					pageShowLimitEnd = numberOfPages; // Setting the end limit to the number of pages. Means show all page numbers
				}
				else
				{
					if (currentPage > 4) // If page index is more then 4 then need to less the page numbers from start and show more on end.
					{
						//Calculating end limit to show more page numbers
						pageShowLimitEnd = currentPage + (int)(Math.Floor((decimal)pageDispCount / 2));

						//Calculating Start limit to hide previous page numbers
						pageShowLimitStart = currentPage - (int)(Math.Floor((decimal)pageDispCount / 2));
					}
					else
					{
						//Simply Displaying the 10 pages. no need to remove / add page numbers
						pageShowLimitEnd = pageDispCount;
					}
				}

				// Since the pageDispCount can be changed and limit calculation can cause < 0 values 
				// Simply, set the limit start value to 1 if it is less

				if (pageShowLimitStart < 1)
					pageShowLimitStart = 1;


				//Dynamic creation of link buttons
				// First Link button to display with paging

				var objLbFirst = new LinkButton();
				objLbFirst.Click += objLb_Click;
				objLbFirst.Text = "First";
				objLbFirst.ID = "lb_FirstPage";
				objLbFirst.CommandName = "pgChange";
				objLbFirst.EnableViewState = true;
				objLbFirst.CommandArgument = "1";

				//Previous Link button to display with paging

				var objLbPrevious = new LinkButton();
				objLbPrevious.Click += objLb_Click;
				objLbPrevious.Text = "Previous";
				objLbPrevious.ID = "lb_PreviousPage";
				objLbPrevious.CommandName = "pgChange";
				objLbPrevious.EnableViewState = true;
				objLbPrevious.CommandArgument = currentPage.ToString();

				//of course if the page is the 1st page, then there is no need of First or Previous
				if (currentPage == 0)
				{
					objLbFirst.Enabled = false;
					objLbPrevious.Enabled = false;
				}
				else
				{
					objLbFirst.Enabled = true;
					objLbPrevious.Enabled = true;
				}

				//Adding control in a place holder
				plcPaging.Controls.Add(objLbFirst);
				plcPaging.Controls.Add(new LiteralControl("&nbsp; | &nbsp;")); // Just to give some space 
				plcPaging.Controls.Add(objLbPrevious);
				plcPaging.Controls.Add(new LiteralControl("&nbsp; | &nbsp;"));

				// Creatig page numbers based on the start and end limit variables.

				for (var i = pageShowLimitStart; i <= pageShowLimitEnd; i++)
				{
					if ((Page.FindControl("lb_" + i) == null) && i <= numberOfPages)
					{

						var objLb = new LinkButton();

						objLb.Click += objLb_Click;
						objLb.Text = i.ToString();
						objLb.ID = "lb_" + i.ToString();
						objLb.CommandName = "pgChange";
						objLb.EnableViewState = true;
						objLb.CommandArgument = i.ToString();

						if ((currentPage + 1) == i)
						{
							objLb.Enabled = false;
						}

						plcPaging.Controls.Add(objLb);
						plcPaging.Controls.Add(new LiteralControl("&nbsp; | &nbsp;"));
					}
				}

				// Last Link button to display with paging

				var objLbLast = new LinkButton();

				objLbLast.Click += objLb_Click;
				objLbLast.Text = "Last";
				objLbLast.ID = "lb_LastPage";
				objLbLast.CommandName = "pgChange";
				objLbLast.EnableViewState = true;
				objLbLast.CommandArgument = numberOfPages.ToString();

				// Next Link button to display with paging

				var objLbNext = new LinkButton();

				objLbNext.Click += objLb_Click;
				objLbNext.Text = "Next";
				objLbNext.ID = "lb_NextPage";
				objLbNext.CommandName = "pgChange";
				objLbNext.EnableViewState = true;
				objLbNext.CommandArgument = (currentPage + 2).ToString();


				// of course if the page is the last page, then there is no need of last or next
				if ((currentPage + 1) == numberOfPages)
				{
					objLbLast.Enabled = false;
					objLbNext.Enabled = false;
				}
				else
				{
					objLbLast.Enabled = true;
					objLbNext.Enabled = true;
				}

				// Adding Control to the place holder
				plcPaging.Controls.Add(objLbNext);
				plcPaging.Controls.Add(new LiteralControl("&nbsp; | &nbsp;"));
				plcPaging.Controls.Add(objLbLast);
				plcPaging.Controls.Add(new LiteralControl("&nbsp; | &nbsp;"));

			}

		}
	}
}
