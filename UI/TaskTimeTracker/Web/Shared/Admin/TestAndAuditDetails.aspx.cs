using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;


namespace Shared.UI.Web.Admin
{
	public partial class TestAndAuditDetails : Framework.UI.Web.BaseClasses.PageBasePage
    {
		
        #region Events

        protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				TestAndAuditGrid.DataSource = TaskTimeTracker.Components.BusinessLayer.Admin.GetList(SessionVariables.RequestProfile);
				TestAndAuditGrid.DataBind();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "TestAndAuditDetailsaDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;
		}


		protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			////In-built paging implementation
			TestAndAuditGrid.DataSource = TaskTimeTracker.Components.BusinessLayer.Admin.GetList(SessionVariables.RequestProfile);
			TestAndAuditGrid.PageIndex = e.NewPageIndex;
			TestAndAuditGrid.DataBind();
		}

		protected void ddlEntity_SelectedIndexChanged(object sender, EventArgs e)
		{
			
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
			var dataTable = TaskTimeTracker.Components.BusinessLayer.Admin.GetList(SessionVariables.RequestProfile);

			if (!ddlEntity.SelectedValue.Equals("Select Entity"))
			{
				for (var i = 0; i < dataTable.Rows.Count; i++)
				{
					if (!dataTable.Rows[i].ItemArray[0].ToString().Equals(ddlEntity.SelectedValue))
					{
						dataTable.Rows[i].Delete();
					}
				}
			}

			TestAndAuditGrid.DataSource = dataTable.DefaultView;
			TestAndAuditGrid.DataBind();
        }

        #endregion

    }
}