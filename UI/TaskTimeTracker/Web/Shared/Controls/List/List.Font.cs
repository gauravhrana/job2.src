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
	public partial class ListControl
	{

		protected void lnkfontsmall_Click(object sender, EventArgs e)
		{
            //SetFontForGrid("12px", "smallfontgrid");
		}

		protected void lnkfontmedium_Click(object sender, EventArgs e)
		{
            //SetFontForGrid("14px", "mediumfontgrid");
		}

		protected void LnkfontlargerClick(object sender, EventArgs e)
		{
            //SetFontForGrid("16px", "largerfontgrid");
		}

        private void SetFontForGrid(string fontsize, string cssclass)
        {
            //griddiv.Style.Add("font-size", fontsize);
            //MainGridView.CssClass = cssclass;

	        return;

			// not sure if all this is applicable ...
            MainGridView.DataBind();

            oGridPagiation = new GridPagiation();

            if (_getData != null)
            {
                dtGlobal = (DataTable)Session[CoreTableName];
            }
            else
            {
                dtGlobal = GetGroupedRecords();
            }

            if (dtGlobal == null) return;

            oGridPagiation.Setup(plcPaging, litPagingSummary, dtGlobal, MainGridView, Page, SettingCategory);
            oGridPagiation.Changed += oGridPagiation_Changed;
            oGridPagiation.ManagePaging(dtGlobal);

            numberedPager.Setup(dtGlobal.Rows.Count, MainGridView, DefaultRowCount); 
            numberedPager.Changed += oGridPagiation_Changed;

	        if (!string.IsNullOrEmpty(SessionVariables.SortExpression) && !string.IsNullOrEmpty(SessionVariables.SortDirection))
	        {
				SortGridView(SessionVariables.SortExpression, SessionVariables.SortDirection);
	        }
	        
            MainGridView.CssClass = cssclass;
        }

	}
}