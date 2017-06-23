using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Specialized;
using System.Collections;
using System.Drawing;
using AjaxControlToolkit;
 
namespace Shared.UI.Web.Controls
{
    public partial class ListControl
    {

		private int GetCellByBoundFieldName(GridView grd, string fieldName)
		{
			foreach (DataControlField col in grd.Columns)
			{
				if (col.HeaderText == fieldName)
					return grd.Columns.IndexOf(col);
			}
			return -1;
		}

        protected void MainGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {			
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var chkBox = e.Row.FindControl("CheckBox1");
                if (chkBox != null)
                {
                    ((CheckBox)chkBox).Attributes.Add("onclick", "CheckChanged(this, '" + MainGridView.ClientID + "', '" + buttonPanel.ClientID + "');");
                }

				if (CoreTableName == "Functionality")
				{
					int index = 0;
					var str = DataBinder.Eval(e.Row.DataItem, "FunctionalityImageId");
					if (!string.IsNullOrEmpty(str.ToString()))
					{
						List<string> imagedIds = str.ToString().Split(',').ToList<string>();

						index = GetCellByBoundFieldName(MainGridView, "Image");
						if (index != -1)
						{
							e.Row.Cells[index].Text = string.Empty;

							var hypImageLink = new HyperLink();
							hypImageLink.ID = "btnPopUpTarget";
							for (int i = 0; i < imagedIds.Count; i++)
							{
								var entityPath = ApplicationCommon.GetControlPath("FunctionalityImage", ControlType.ImageHandler);

								var imgField = new ImageButton();
								var imageId = "imageId" + i.ToString();
								imgField.ID = imageId;
								imgField.ImageUrl = entityPath + "ShowImage.aspx?ImageId=" + imagedIds[i];
								imgField.ControlStyle.Width = 50;
								imgField.ControlStyle.Height = 50;
								imgField.ControlStyle.BorderStyle = BorderStyle.Solid;
								imgField.ControlStyle.BorderColor = Color.Black;
								imgField.ControlStyle.BorderWidth = 1;
								e.Row.Cells[index].Controls.Add(imgField);

								// formulate actual image URL
								var absUrl = "http://" + Request.Url.Host + ":" + Request.Url.Port;
								absUrl += imgField.ImageUrl.Replace("~", "");

								imgField.Attributes.Add("onclick", "javascript:OpenPopup('" + absUrl + "'); return false;");

							}
						}
					}
				}
            }
        }		

        protected void MainGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //var row = (DataRowView)e.Row.DataItem;
                //var cells = e.Row.Cells;
                // var btn = (HyperLink)cells[0].FindControl("ButtonDelete");
                // btn.ImageUrl = Application["Branding"] + "/Images/delete.jpg";
            }            
			else if (e.Row.RowType == DataControlRowType.Pager)
            {
                var space = new LiteralControl("     ");

                var lb = new Label();
                lb.ID = "lblsummary";
                lb.Text = "";

                // Pager is rendered in a single cell as a table;   
                // each page # is in a cell by it's own  
                var table = e.Row.Cells[0].Controls[0] as Table;

                // Add ViewAll linkbutton to the last cell  
                var parentCell = table.Rows[0].Cells[table.Rows[0].Cells.Count - 1];
                parentCell.Controls.Add(space);
                parentCell.Controls.Add(lb);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //var rowView = (DataRowView)e.Row.DataItem;
                var myCells = e.Row.Cells;

                var _deleteIndex = 0;
                var _updateIndex = 0;

                if (ViewState[VIEW_STATE_KEY_DELETE] != null && ViewState[VIEW_STATE_KEY_UPDATE] != null)
                {
                    int.TryParse(ViewState[VIEW_STATE_KEY_DELETE].ToString(), out _deleteIndex);
                    int.TryParse(ViewState[VIEW_STATE_KEY_UPDATE].ToString(), out _updateIndex);

                    if (_deleteIndex < myCells.Count && _updateIndex < myCells.Count && IsDeleteColumn)
                    {
                        var deleteIndex = 0;
                        int.TryParse(ViewState[VIEW_STATE_KEY_DELETE].ToString(), out deleteIndex);

                        if (myCells[_deleteIndex].Controls.Count > 0)
                        {
                            var deleteLink = (HyperLink)myCells[_deleteIndex].Controls[0];
                            deleteLink.ImageUrl = Application["Branding"] + "/Images/delete.jpg";
                        }
                    }

                    if (IsUpdateColumn && _updateIndex < myCells.Count)
                    {
                        var updateIndex = 0;
                        int.TryParse(ViewState[VIEW_STATE_KEY_DELETE].ToString(), out updateIndex);

                        if (myCells[_updateIndex].Controls.Count > 0)
                        {
                            var updateLink = (HyperLink)myCells[_updateIndex].Controls[0];
                            updateLink.ImageUrl = Application["Branding"] + "/Images/untitled1.png";
                        }
                    }
                }
            }
        }

        protected void MainGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //In-built paging implementation
            MainGridView.PageIndex = e.NewPageIndex;
            CurrentPageIndex = e.NewPageIndex;
            CurrentPageIndexInSession = e.NewPageIndex;
            MainGridView.DataBind();

            oGridPagiation.PageIndexInSession = e.NewPageIndex;

            //Synchronize with custom paging

            oGridPagiation = new GridPagiation();

            if (_getData != null)
            {
                dtGlobal = _getData();
            }
            else
            {
                dtGlobal = GetGroupedRecords();
            }

            oGridPagiation.Setup(plcPaging, litPagingSummary, dtGlobal, MainGridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.PageIndexInSession = e.NewPageIndex;

            if (CurrentPageIndex != null)
            {
                oGridPagiation.PageIndexInSession = CurrentPageIndex.Value;
                numberedPager.CurrentIndex = CurrentPageIndex.Value;
            }

			numberedPager.Setup(dtGlobal.Rows.Count, MainGridView, DefaultRowCount);
            numberedPager.Changed += oGridPagiation_Changed;

            ListHelper.AddCheckBox(MainGridView);

            oGridPagiation.ManagePaging(dtGlobal);

			// not sure if this should be session attribute
            if
            (
                    !string.IsNullOrEmpty(SessionVariables.SortExpression)
                && !string.IsNullOrEmpty(SessionVariables.SortDirection)
            )
            {
                SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);
            }
        }

    }
}