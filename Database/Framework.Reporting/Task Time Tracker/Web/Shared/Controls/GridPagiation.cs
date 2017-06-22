using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Text;

namespace Shared.UI.Web.Controls
{
	public class GridPagiation
	{
		public string SettingCategory;

		public int DefaultRowCount
		{
			get
			{
				return PerferenceUtility.GetUserPreferenceByKeyAsInt(ApplicationCommon.DefaultRowCountKey, SettingCategory);
			}
			
		}

		// Global variables for pager settings
		public int PageSize = 5;

		const int PageDispCount = 10;

		public int PageIndexInSession = 0;
		public bool RefreshGrid = false;

		private PlaceHolder plcPaging;
		private Label litPagingSummary;
		private GridView MainGridView;
		private Page oPage;
		private TextBox textPageSize;

		private DataTable dtGlobal = new DataTable();

		// An event that clients can use to be notified whenever the
		// elements of the list change.
		public event EventHandler Changed;

		#region Public Methods

		public void Setup(PlaceHolder plc, Label lbl1, Label lbl2, DataTable dt, GridView mainGridView, Page pageObject, string settingCategory)
		{
			SettingCategory = settingCategory;

			plcPaging = plc;
			litPagingSummary = lbl1;
			//oPage = pageObject;

			MainGridView = mainGridView;

			dtGlobal = dt;

		}

		public void Setup(PlaceHolder plc, Label lbl1, DataTable dt, GridView mainGridView, Page pageObject, string settingCategory)
		{
			SettingCategory = settingCategory;

			plcPaging = plc;
			litPagingSummary = lbl1;
			//oPage = pageObject;

			MainGridView = mainGridView;

			dtGlobal = dt;
		}

		public void ManagePaging(DataTable _dt)
		{
			if (_dt == null)
				return;

			if (_dt.Rows.Count <= 0)
			{
				// Clear the label and controls from previous result cache
				litPagingSummary.Text = string.Empty;
				plcPaging.Controls.Clear();

				MainGridView.DataSource = _dt;
				MainGridView.DataBind();

				//return;
			}

			// Variable declaration

			int numberOfPages;
			var numberOfRecords = _dt.Rows.Count;

			var currentPage = (MainGridView.PageIndex);
			PageSize = MainGridView.PageSize;

			var strSummary = new StringBuilder();

			// If number of records is more then the page size (specified in global variable)
			// Just to check either gridview have enough records to implement paging

			if (numberOfRecords > PageSize)
			{
				// Calculating the total number of pages
				numberOfPages = (int)Math.Ceiling((double)numberOfRecords / (double)PageSize);
			}
			else
			{
				numberOfPages = 1;
			}

			// Creating a small summary for records.
			strSummary.Append("Displaying Records <b>");

			// Creating X f X Records

			var floor = (currentPage * PageSize) + 1;

			if (numberOfRecords == 0)
			{
				floor = 0;
			}

			strSummary.Append(floor.ToString());

			strSummary.Append("</b>-<b>");

			var ceil = ((currentPage * PageSize) + PageSize);

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
			//strSummary.Append("</b> of About <b>");
			strSummary.Append("</b> of <b>");
			strSummary.Append(numberOfRecords.ToString());
			//strSummary.Append("</b>Records</br>");

			litPagingSummary.Text = strSummary.ToString();

			// Variable declaration 
			// these variables will used to calculate page number display
			var pageShowLimitStart = 1;
			var pageShowLimitEnd = 1;

			// Just to check, either there is enough pages to implement page number display logic.
			if (PageDispCount > numberOfPages)
			{
				pageShowLimitEnd = numberOfPages; // Setting the end limit to the number of pages. Means show all page numbers
			}
			else
			{
				// If page index is more then 4 then need to less the page numbers from start and show more on end.
				if (currentPage > 4)
				{
					// Calculating end limit to show more page numbers
					pageShowLimitEnd = currentPage + (int)(Math.Floor((decimal)PageDispCount / 2));

					// Calculating Start limit to hide previous page numbers
					pageShowLimitStart = currentPage - (int)(Math.Floor((decimal)PageDispCount / 2));
				}
				else
				{
					// Simply Displaying the 10 pages. no need to remove / add page numbers
					pageShowLimitEnd = PageDispCount;
				}
			}

			// Since the pageDispCount can be changed and limit calculation can cause < 0 values 
			// Simply, set the limit start value to 1 if it is less
			if (pageShowLimitStart < 1)
				pageShowLimitStart = 1;

			//Dynamic Creation of Page Size Combo box
			var ddlPageSize = new DropDownList();
			var txtPageSize = new TextBox();

			//if (numberOfRecords <= 15)
			//{
			//    ddlPageSize = DdlPageSize(MainGridView.PageSize);
			//}
			//else
			//{
			txtPageSize = TxtPageSize(MainGridView.PageSize);
			textPageSize = txtPageSize;			
			//}

			// Dynamic creation of link buttons
			// First Link button to display with paging	
			var objLbFirst = ObjLbFirst();

			// Previous Link button to display with paging
			var objLbPrevious = ObjLbPrevious(currentPage);

			// of course if the page is the 1st page, then there is no need of First or Previous
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

			// Adding control in a place holder
			if (!(plcPaging.Controls.Count > 0 && plcPaging.Controls[0].FindControl("lb_FirstPage") != null))
			{
				plcPaging.Controls.Add(objLbFirst);
				plcPaging.Controls.Add(new LiteralControl("&nbsp; | &nbsp;")); // Just to give some space 
			}
			//enabling control based on the current page
			else
			{
				var objlbfirst = plcPaging.Controls[0].FindControl("lb_FirstPage");
				if (objlbfirst != null)
				{
					if (currentPage == 0)
						((LinkButton)objlbfirst).Enabled = false;
					else
						((LinkButton)objlbfirst).Enabled = true;
				}
			}
			if (!(plcPaging.Controls.Count > 2 && plcPaging.Controls[2].FindControl("lb_PreviousPage") != null))
			{
				plcPaging.Controls.Add(objLbPrevious);
				plcPaging.Controls.Add(new LiteralControl("&nbsp; | &nbsp;"));
			}
			//enabling control based on the current page
			else
			{
				var objlbprev = plcPaging.Controls[2].FindControl("lb_PreviousPage");
				if (objlbprev != null)
				{
					if (currentPage == 0)
						((LinkButton)objlbprev).Enabled = false;
					else
						((LinkButton)objlbprev).Enabled = true;
				}
			}
			// Creatig page numbers based on the start and end limit variables.
			CreateNumbers(currentPage, pageShowLimitEnd, numberOfPages, pageShowLimitStart);

			// Last Link button to display with paging

			var objLbLast = ObjLbLast(numberOfPages);

			// Next Link button to display with paging
			var objLbNext = ObjLbNext(currentPage);

			//of course if the page is the last page, then there is no need of last or next
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
			if (!(plcPaging.Controls.Count > 4 && plcPaging.Controls[4].FindControl("lb_NextPage") != null))
			{
				plcPaging.Controls.Add(objLbNext);
				plcPaging.Controls.Add(new LiteralControl("&nbsp; | &nbsp;"));
			}
			//enabling control based on the current page
			else
			{
				var objlbnext = plcPaging.Controls[4].FindControl("lb_NextPage");
				if (objlbnext != null)
				{
					if ((currentPage + 1) == numberOfPages)
						((LinkButton)objlbnext).Enabled = false;
					else
						((LinkButton)objlbnext).Enabled = true;
				}
			}
			// Adding Control to the place holder
			if (!(plcPaging.Controls.Count > 6 && plcPaging.Controls[6].FindControl("lb_LastPage") != null))
			{
				plcPaging.Controls.Add(objLbLast);
				plcPaging.Controls.Add(new LiteralControl("&nbsp; | &nbsp;"));

			}
			//enabling control based on the current page
			else
			{
				var objlblast = plcPaging.Controls[6].FindControl("lb_LastPage");
				if (objlblast != null)
				{
					if ((currentPage + 1) == numberOfPages)
						((LinkButton)objlblast).Enabled = false;
					else
						((LinkButton)objlblast).Enabled = true;
				}
			}
			//Adding Dynamic page size selection dropdown to PlaceHolder
			//if (numberOfRecords <= 15)
			//{
			//    if (!(plcPaging.Controls.Count > 8 && plcPaging.Controls[8].FindControl("lb_PageSize") != null))
			//    {

			//        plcPaging.Controls.Add(ddlPageSize);
			//    }
			//    else
			//    {

			//        var objlbpagesize = plcPaging.Controls[8].FindControl("lb_PageSize");
			//        plcPaging.Controls.Remove(objlbpagesize);
			//        plcPaging.Controls.Add(ddlPageSize);
			//        var objlbpagesize2 = plcPaging.Controls[8].FindControl("lb_PageSize");
			//        if (objlbpagesize2 != null)
			//            ((DropDownList)objlbpagesize2).SelectedValue = MainGridView.PageSize.ToString();
			//    }
			//}
			//else
			//{

			var btnGo = new Button();
			btnGo.Text = "Go";
			btnGo.CssClass = "btn";		
			btnGo.EnableViewState = true;
			btnGo.Click += btnGo_Click;

			if (!(plcPaging.Controls.Count > 8 && plcPaging.Controls[8].FindControl("lb_PageSize") != null))
			{
				plcPaging.Controls.Add(new LiteralControl("Page Size: &nbsp;"));
				plcPaging.Controls.Add(txtPageSize);
				plcPaging.Controls.Add(btnGo);
			}
			else
			{
				var objlbpagesize = plcPaging.Controls[8].FindControl("lb_PageSize");

				plcPaging.Controls.Remove(objlbpagesize);

				if (plcPaging.Controls[plcPaging.Controls.Count - 1] is Button)
				{
					if (((Button)plcPaging.Controls[plcPaging.Controls.Count - 1]).Text == "Go")
					{
						plcPaging.Controls.AddAt(plcPaging.Controls.Count - 1, txtPageSize);					
					}
				}
				else
				{
					plcPaging.Controls.Add(txtPageSize);
					plcPaging.Controls.Add(btnGo);
				}

				var objlbpagesize2 = plcPaging.Controls[8].FindControl("lb_PageSize");
				if (objlbpagesize2 != null)
					((TextBox)objlbpagesize2).Text = MainGridView.PageSize.ToString();
			}
			//}


			MainGridView.PageIndex = PageIndexInSession;
			//MainGridView.PageSize = PageSize;
		}
		
		#endregion

		#region Private Methods

		private LinkButton ObjLbLast(int numberOfPages)
		{
			var objLbLast = new LinkButton();

			objLbLast.Click += new EventHandler(objLb_Click);
			objLbLast.Text = "Last";
			objLbLast.ID = "lb_LastPage";
			objLbLast.CommandName = "pgChange";
			objLbLast.EnableViewState = true;
			objLbLast.CommandArgument = numberOfPages.ToString();
			
			return objLbLast;
		}

		private void CreateNumbers(int currentPage, int pageShowLimitEnd, int numberOfPages, int pageShowLimitStart)
		{
			if (oPage == null) return;

			for (var i = pageShowLimitStart; i <= pageShowLimitEnd; i++)
			{

				if ((oPage.FindControl("lb_" + i) == null) && i <= numberOfPages)
				{
					var objLb = new LinkButton();

					objLb.Click += new EventHandler(objLb_Click);
					objLb.Text = i.ToString();
					objLb.ID = "lb_" + i;
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
		}

		private LinkButton ObjLbPrevious(int currentPage)
		{
			var objLbPrevious = new LinkButton();

			objLbPrevious.Click += new EventHandler(objLb_Click);
			objLbPrevious.Text = "Previous";
			objLbPrevious.ID = "lb_PreviousPage";
			objLbPrevious.CommandName = "pgChange";
			objLbPrevious.EnableViewState = true;
			objLbPrevious.CommandArgument = (currentPage).ToString();
			return objLbPrevious;
		}

		private LinkButton ObjLbNext(int currentPage)
		{

			var objLbNext = new LinkButton();

			objLbNext.Click += new EventHandler(objLb_Click);
			objLbNext.Text = "Next";
			objLbNext.ID = "lb_NextPage";
			objLbNext.CommandName = "pgChange";
			objLbNext.EnableViewState = true;
			objLbNext.CommandArgument = (currentPage + 2).ToString();

			return objLbNext;
		}

		private TextBox TxtPageSize(int pagesize)
		{

			TextBox objPageSize;

			objPageSize = new TextBox();

			// Auto Post Back disabled temporarily
			//objPageSize.TextChanged += new EventHandler(txtPS_TextChanged);
			//objPageSize.AutoPostBack = true;

			objPageSize.ID = "lb_PageSize";
			objPageSize.Width = 50;
			objPageSize.Text = Convert.ToString(MainGridView.PageSize);
			
			objPageSize.EnableViewState = true;
			return objPageSize;
		}

		private DropDownList DdlPageSize(int pagesize)
		{

			DropDownList objPageSize;

			objPageSize = new DropDownList();
			objPageSize.SelectedIndexChanged += new EventHandler(objPageSize_selectedindexchanged);
			objPageSize.AutoPostBack = true;
			objPageSize.Items.Add("3");
			objPageSize.Items.Add("4");
			objPageSize.Items.Add("5");
			objPageSize.Items.Add("6");
			objPageSize.Items.Add("7");
			objPageSize.Items.Add("8");
			objPageSize.Items.Add("9");
			objPageSize.Items.Add("10");
			objPageSize.Items.Add("11");
			objPageSize.Items.Add("12");
			objPageSize.Items.Add("13");
			objPageSize.Items.Add("14");
			objPageSize.Items.Add("15");
			objPageSize.SelectedValue = pagesize.ToString();

			objPageSize.ID = "lb_PageSize";

			objPageSize.EnableViewState = true;


			return objPageSize;
		}

		private LinkButton ObjLbFirst()
		{
			var objLbFirst = new LinkButton();

			objLbFirst.Click += new EventHandler(objLb_Click);
			objLbFirst.Text = "First";
			objLbFirst.ID = "lb_FirstPage";
			objLbFirst.CommandName = "pgChange";
			objLbFirst.EnableViewState = true;
			objLbFirst.CommandArgument = "1";

			return objLbFirst;
		}
		
		#endregion

		#region Events

		void btnGo_Click(object sender, EventArgs e)
		{
			var pageSize = DefaultRowCount;

			if (textPageSize.Text == "ALL")
			{
				MainGridView.PageSize = dtGlobal.Rows.Count;
				pageSize = MainGridView.PageSize;
			}
			else
			{
				pageSize = int.Parse(textPageSize.Text);
				MainGridView.PageSize = pageSize;
			}

			PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.DefaultRowCountKey, pageSize.ToString());

			MainGridView.PageIndex = 0;
			MainGridView.DataBind();
			PageSize = MainGridView.PageSize;
			OnChanged(EventArgs.Empty);
			

			//SortGridView(String.Empty, SortDirection.Ascending.ToString());
		}

		void objLb_Click(object sender, EventArgs e)
		{
			plcPaging.Controls.Clear();

			var objlb = (LinkButton)sender;

			var pageindex = (int.Parse(objlb.CommandArgument) - 1);
			if (pageindex < 0)
				pageindex = 0;

			MainGridView.PageIndex = pageindex;

			OnChanged(EventArgs.Empty);

			//ManagePaging(dtGlobal);
		}

		void objPageSize_selectedindexchanged(object sender, EventArgs e)
		{

			//plcPaging.Controls.Clear();

			var objps = (DropDownList)sender;

			MainGridView.PageSize = DefaultRowCount;

			MainGridView.PageIndex = 0;

			PageSize = MainGridView.PageSize;

			OnChanged(EventArgs.Empty);

		}

		void txtPS_TextChanged(object sender, EventArgs e)
		{
			var objps = (TextBox)sender;
			var pageSize = DefaultRowCount;

			if (objps.Text == "ALL")
			{
				MainGridView.PageSize = dtGlobal.Rows.Count;
				pageSize = MainGridView.PageSize;
			}
			else
			{
				pageSize = int.Parse(objps.Text);
				MainGridView.PageSize = pageSize;
			}

			PerferenceUtility.UpdateUserPreference(SettingCategory, ApplicationCommon.DefaultRowCountKey, pageSize.ToString());

			MainGridView.PageIndex = 0;
			MainGridView.DataBind();
			PageSize = MainGridView.PageSize;
			OnChanged(EventArgs.Empty);

			//SortGridView(String.Empty, SortDirection.Ascending.ToString());

		}

		// Invoke the Changed event; called whenever list changes
		protected virtual void OnChanged(EventArgs e)
		{
			if (Changed != null)
				Changed(this, e);
		}

		#endregion


	}
}