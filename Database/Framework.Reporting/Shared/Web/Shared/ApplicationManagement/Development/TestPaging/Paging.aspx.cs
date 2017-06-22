using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Application;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.TestPaging
{
	public partial class Paging : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
			if (!Page.IsPostBack)
			{
				PagingGridView1_DataBind();
			}
		}

		private void PagingGridView1_DataBind()
		{

            var data = new ApplicationRoleDataModel();
            var dt = Framework.Components.ApplicationUser.ApplicationRole.Search(data, 5);

			//dt.Columns.Add("ID", Type.GetType("System.Int32"));
			//dt.Columns.Add("Name", Type.GetType("System.String"));

			//for (var i = 1; i <= 13; i++)
			//{
			//    dt.Rows.Add(new object[] {i,"Name " + i.ToString()});
			//}

			PagingGridView1.DataSource = dt;
			PagingGridView1.DataBind();
		}

		protected void PagingGridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
		{
			PagingGridView1.PageIndex = e.NewPageIndex;

			PagingGridView1_DataBind();
		}
	}
}