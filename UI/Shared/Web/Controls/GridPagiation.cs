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
        // Global variables for pager settings
        public int pageSize = 5;
        const int pageDispCount = 10;
        public int pageindexinsession = 0;
        public bool refreshgrid = false;


        private PlaceHolder plcPaging;
        private Label litPagingSummary;
        private Label lblCacheStatus;
        private GridView MainGridView;
        private Page oPage;

        private DataTable dtGlobal = new DataTable();

        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event EventHandler Changed;

        public void Setup(PlaceHolder plc, Label lbl1, Label lbl2, DataTable dt, GridView mainGridView, Page oPage)
        {
            plcPaging = plc;
            litPagingSummary = lbl1;
            lblCacheStatus = lbl2;
            oPage = oPage;

            MainGridView = mainGridView;
			
            dtGlobal = dt;

        }

        #region paging
        void objLb_Click(object sender, EventArgs e)
        {
            plcPaging.Controls.Clear();

            var objlb = (LinkButton)sender;

            var pageindex = (int.Parse(objlb.CommandArgument) - 1);
            if (pageindex < 0)
                pageindex = 0;

            MainGridView.PageIndex = pageindex;

            OnChanged(EventArgs.Empty);


            //managePaging(dtGlobal);
        }
        void objPageSize_selectedindexchanged(object sender, EventArgs e)
        {

            //plcPaging.Controls.Clear();

            var objps = (DropDownList)sender;

            MainGridView.PageSize = int.Parse(objps.SelectedItem.Value);

            MainGridView.PageIndex = 0;

            pageSize = MainGridView.PageSize;

            OnChanged(EventArgs.Empty);

        }
        void txtPS_TextChanged(object sender, EventArgs e)
        {

            //plcPaging.Controls.Clear();

            var objps = (TextBox)sender;
            int _pagesize = 0;
            if (int.TryParse(objps.Text, out _pagesize))
                MainGridView.PageSize = int.Parse(objps.Text);
            else if (objps.Text == "ALL")
                MainGridView.PageSize = dtGlobal.Rows.Count;

            MainGridView.PageIndex = 0;

            pageSize = MainGridView.PageSize;
            SessionVariables.DefaultRowCount = pageSize;
            OnChanged(EventArgs.Empty);

        }
        //public void pgChange(object sender, GridViewPageEventArgs e)
        //{
        //    GridView.PageIndex = e.NewPageIndex;
        //    SortGridView(string.Empty, SortDirection.Ascending.ToString());                
        //}

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public void ManagePaging(DataTable _dt)
        {
            if (_dt.Rows.Count <= 0)
            {
                MainGridView.DataSource = _dt;
                MainGridView.DataBind();
                return;
            }

            // Variable declaration

            int numberOfPages;
            var numberOfRecords = dtGlobal.Rows.Count;
            var currentPage = (MainGridView.PageIndex);
            pageSize = MainGridView.PageSize;

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
            strSummary.Append("Displaying Records <b>");

            // Creating X f X Records

            var floor = (currentPage * pageSize) + 1;

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
            if (pageDispCount > numberOfPages)
            {
                pageShowLimitEnd = numberOfPages; // Setting the end limit to the number of pages. Means show all page numbers
            }
            else
            {
                // If page index is more then 4 then need to less the page numbers from start and show more on end.
                if (currentPage > 4)
                {
                    // Calculating end limit to show more page numbers
                    pageShowLimitEnd = currentPage + (int)(Math.Floor((decimal)pageDispCount / 2));

                    // Calculating Start limit to hide previous page numbers
                    pageShowLimitStart = currentPage - (int)(Math.Floor((decimal)pageDispCount / 2));
                }
                else
                {
                    // Simply Displaying the 10 pages. no need to remove / add page numbers
                    pageShowLimitEnd = pageDispCount;
                }
            }

            // Since the pageDispCount can be changed and limit calculation can cause < 0 values 
            // Simply, set the limit start value to 1 if it is less
            if (pageShowLimitStart < 1)
                pageShowLimitStart = 1;

            //Dynamic Creation of Page Size Combo box
            DropDownList ddlPageSize = new DropDownList();
            TextBox txtPageSize = new TextBox();

            if (numberOfRecords <= 15)
                ddlPageSize = DdlPageSize(MainGridView.PageSize);
            else
            {
                txtPageSize = TxtPageSize(MainGridView.PageSize);

            }

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
            if (numberOfRecords <= 15)
            {
                if (!(plcPaging.Controls.Count > 8 && plcPaging.Controls[8].FindControl("lb_PageSize") != null))
                {

                    plcPaging.Controls.Add(ddlPageSize);
                }
                else
                {

                    var objlbpagesize = plcPaging.Controls[8].FindControl("lb_PageSize");
                    plcPaging.Controls.Remove(objlbpagesize);
                    plcPaging.Controls.Add(ddlPageSize);
                    var objlbpagesize2 = plcPaging.Controls[8].FindControl("lb_PageSize");
                    if (objlbpagesize2 != null)
                        ((DropDownList)objlbpagesize2).SelectedValue = MainGridView.PageSize.ToString();
                }
            }
            else
            {
                if (!(plcPaging.Controls.Count > 8 && plcPaging.Controls[8].FindControl("lb_PageSize") != null))
                {
                    plcPaging.Controls.Add(new LiteralControl("Page Size: &nbsp;"));
                    plcPaging.Controls.Add(txtPageSize);
                }
                else
                {

                    var objlbpagesize = plcPaging.Controls[8].FindControl("lb_PageSize");
                    plcPaging.Controls.Remove(objlbpagesize);
                    plcPaging.Controls.Add(txtPageSize);
                    var objlbpagesize2 = plcPaging.Controls[8].FindControl("lb_PageSize");
                    if (objlbpagesize2 != null)
                        ((TextBox)objlbpagesize2).Text = MainGridView.PageSize.ToString();
                }
            }


            MainGridView.PageIndex = pageindexinsession;

        }
        #endregion

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

                if ((oPage.FindControl("lb_" + i.ToString()) == null) && i <= numberOfPages)
                {
                    var objLb = new LinkButton();

                    objLb.Click += new EventHandler(objLb_Click);
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
            objPageSize.TextChanged += new EventHandler(txtPS_TextChanged);
            objPageSize.AutoPostBack = true;
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


    }
}