using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.TestPaging.Class
{
	public class PagingGridView : GridView
	{
		public enum PagerTypes
		{
			Regular = 0,
			Custom = 1
		}

		[DefaultValue(PagerTypes.Custom), Category("Paging"), Description("Indicates whether to use the built-in custom pager or not.")]
		public PagerTypes PagerType
		{
			get
			{
				if ((ViewState["PagerType"]!= null)) 
				{ 
					return (PagerTypes)ViewState["PagerType"]; 
				} 
				else 
				{ 
					return PagerTypes.Custom; 
				}
			} 
			set 
			{ 
				ViewState["PagerType"]= value; 
			}
		}

		protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
		{
			switch (PagerType)
			{
				case PagerTypes.Custom: InitCustomPager(row, columnSpan, pagedDataSource);
					break; 
				default: base.InitializePager(row, columnSpan, pagedDataSource); 
					break;
			}
		}

		private void InitCustomPager(System.Web.UI.WebControls.GridViewRow row, int columnSpan, System.Web.UI.WebControls.PagedDataSource pagedDataSource)
		{
			var pnlPager = new Panel();
			var _with1 = pnlPager;
			_with1.ID = "pnlPager";
			_with1.CssClass = PagerStyle.CssClass;

			var tblPager = new Table();
			var _with2 = tblPager;
			_with2.ID = "tblPager";
			_with2.CellPadding = 3;
			_with2.CellSpacing = 0;
			_with2.Style.Add("width", "100%");
			_with2.Style.Add("height", "100%");
			_with2.BorderStyle = BorderStyle.None;
			_with2.GridLines = GridLines.None;

			var trPager = new TableRow();
			trPager.ID = "trPager";

			var ltlPageIndex = new Literal();
			ltlPageIndex.ID = "ltlPageIndex";
			ltlPageIndex.Text = (PageIndex + 1).ToString();

			var ltlPageCount = new Literal();
			ltlPageCount.ID = "ltlPageCount";
			ltlPageCount.Text = PageCount.ToString();

			var tcPageXofY = new TableCell();
			var _with3 = tcPageXofY;
			_with3.ID = "tcPageXofY";
			_with3.Controls.Add(new LiteralControl("Page "));
			_with3.Controls.Add(ltlPageIndex);
			_with3.Controls.Add(new LiteralControl(" of "));
			_with3.Controls.Add(ltlPageCount);

			var ibtnFirst = new ImageButton();
			var _with4 = ibtnFirst;
			_with4.ID = "ibtnFirst";
			_with4.CommandName = "First";
			_with4.ToolTip = "First Page";
			_with4.Style.Add("cursor", "pointer");
			_with4.CausesValidation = false;
			_with4.Command += PagerCommand;

			var ibtnPrevious = new ImageButton();
			var _with5 = ibtnPrevious;
			_with5.ID = "ibtnPrevious";
			_with5.CommandName = "Previous";
			_with5.ToolTip = "Previous Page";
			_with5.ImageAlign = ImageAlign.AbsMiddle;
			_with5.Style.Add("cursor", "pointer");
			_with5.CausesValidation = false;
			_with5.Command += PagerCommand;

			var ibtnNext = new ImageButton();
			var _with6 = ibtnNext;
			_with6.ID = "ibtnNext";
			_with6.CommandName = "Next";
			_with6.ToolTip = "Next Page";
			_with6.ImageAlign = ImageAlign.AbsMiddle;
			_with6.Style.Add("cursor", "pointer");
			_with6.CausesValidation = false;
			_with6.Command += PagerCommand;

			var ibtnLast = new ImageButton();
			var _with7 = ibtnLast;
			_with7.ID = "ibtnLast";
			_with7.CommandName = "Last";
			_with7.ToolTip = "Last Page";
			_with7.ImageAlign = ImageAlign.AbsMiddle;
			_with7.Style.Add("cursor", "pointer");
			_with7.CausesValidation = false;
			_with7.Command += PagerCommand;

			if (PageIndex > 0)
			{
				ibtnFirst.Enabled = true;
				ibtnPrevious.Enabled = true;
			}
			else
			{
				ibtnFirst.Enabled = false;
				ibtnPrevious.Enabled = false;
				ibtnFirst.Style.Add("cursor", "default");
				ibtnPrevious.Style.Add("cursor", "default");
			}

			if (PageIndex < PageCount - 1)
			{
				ibtnNext.Enabled = true;
				ibtnLast.Enabled = true;
			}
			else
			{
				ibtnNext.Enabled = false;
				ibtnLast.Enabled = false;
				ibtnNext.Style.Add("cursor", "default");
				ibtnLast.Style.Add("cursor", "default");
			}

			var tcPagerBtns = new TableCell();
			var _with8 = tcPagerBtns;
			_with8.ID = "tcPagerBtns";
			_with8.Controls.Add(ibtnFirst);
			_with8.Controls.Add(ibtnPrevious);
			_with8.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
			_with8.Controls.Add(ibtnNext);
			_with8.Controls.Add(ibtnLast);

			var ddlPages = new DropDownList();
			var _with9 = ddlPages;
			_with9.AutoPostBack = true;

			for (var i = 1; i <= PageCount; i += +1)
			{
				_with9.Items.Add(new ListItem(i.ToString(), i.ToString()));
			}
			_with9.SelectedIndex = PageIndex;
			_with9.CausesValidation = false;
			_with9.SelectedIndexChanged += ddlPages_SelectedIndexChanged;

			var tcPagerDDL = new TableCell();
			var _with10 = tcPagerDDL;
			_with10.ID = "tcPagerDDL";
			_with10.Controls.Add(new LiteralControl("<table cellpadding=\"0\" cellspacing=\"0\" frame=\"void\" rules=\"none\"><tr><td style=\"padding-right: 5px;border: 0px;\">Page:</td><td style=\"border: 0px;\">"));
			_with10.Controls.Add(ddlPages);
			_with10.Controls.Add(new LiteralControl("</td></tr></table>"));

			//add cells to row
			trPager.Cells.Add(tcPageXofY);
			trPager.Cells.Add(tcPagerBtns);
			trPager.Cells.Add(tcPagerDDL);

			//add row to table
			tblPager.Rows.Add(trPager);

			//add table to div
			pnlPager.Controls.Add(tblPager);

			//add div to pager row
			row.Controls.AddAt(0, new TableCell());
			row.Cells[0].ColumnSpan = columnSpan;
			row.Cells[0].Controls.Add(pnlPager);
		}

		protected void ddlPages_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			var newPageIndex = ((DropDownList)sender).SelectedIndex;

			OnPageIndexChanging(new GridViewPageEventArgs(newPageIndex));
			
		}

		protected void PagerCommand(object sender, System.Web.UI.WebControls.CommandEventArgs e) 
		{ 
			var curPageIndex = PageIndex; 
			var newPageIndex = 0; 
			
			switch (e.CommandName)
			{
				case "First": newPageIndex = 0; break;
				case "Previous": if (curPageIndex > 0)
				{ newPageIndex = curPageIndex - 1; } 
				break; 
				case "Next": if (!(curPageIndex == PageCount)) { newPageIndex = curPageIndex + 1; } 
					break; 
				case "Last": newPageIndex = PageCount; 
					break;
			} 
			
			OnPageIndexChanging(new GridViewPageEventArgs(newPageIndex));
		}

	}
}