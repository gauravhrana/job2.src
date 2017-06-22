using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin
{
	public partial class TestAndAuditDetails : Shared.UI.WebFramework.BasePage
    {

        #region Events

        protected void Page_Load(object sender, EventArgs e)
		{
			//if(!IsPostBack)
			//{
			//    TestAndAuditGrid.DataSource = Shared.Components.BusinessLayer.Admin.GetList(SessionVariables.AuditId);
			//    TestAndAuditGrid.DataBind();
			//}
		}

		protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			////In-built paging implementation
			//TestAndAuditGrid.DataSource = Shared.Components.BusinessLayer.Admin.GetList(SessionVariables.AuditId);
			//TestAndAuditGrid.PageIndex = e.NewPageIndex;
			//TestAndAuditGrid.DataBind();
		}

		protected void ddlEntity_SelectedIndexChanged(object sender, EventArgs e)
		{
			
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
			//var dataTable = Shared.Components.BusinessLayer.Admin.GetList(SessionVariables.AuditId);
            
			//if (!ddlEntity.SelectedValue.Equals("Select Entity"))
			//{
			//    for (int i = 0; i < dataTable.Rows.Count; i++)
			//    {
			//        if (!dataTable.Rows[i].ItemArray[0].ToString().Equals(ddlEntity.SelectedValue))
			//        {
			//            dataTable.Rows[i].Delete();
			//        }
			//    }
			//}

			//TestAndAuditGrid.DataSource = dataTable.DefaultView;
			//TestAndAuditGrid.DataBind();
        }

        #endregion

    }
}