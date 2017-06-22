using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Project.Controls
{
	public partial class List : BaseControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				ViewState["SearchCondition"] = string.Empty;
				SortGridView(string.Empty, SortDirection.Ascending.ToString());
			}
		}

		private bool _hideData;

		public bool HideData
		{
			set
			{
				_hideData = value;

				// more clear its hiding / showing sections
				// you do not want to put also here get data
				// it confuses the message on what HideData is doing

				GridView1.Columns[3].Visible = !value;
				GridView1.Columns[4].Visible = !value;
				GridView1.Columns[5].Visible = !value;
			}
			get
			{
				return _hideData;
			}
		}

		public void ShowData(string searchCondition, bool dataHide)
		{
			ViewState["SearchCondition"] = searchCondition;
			SortGridView(string.Empty, null);
			HideData = dataHide;
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

		protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
		{
			var sortExpression = e.SortExpression;
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
		private void SortGridView(string sortExpression, string sortDirection)
		{
			var dt = GetData((ViewState["SearchCondition"]).ToString());
			var dv = dt.DefaultView;

			if (!string.IsNullOrEmpty(sortExpression))
			{
				dv.Sort = sortExpression + " " + sortDirection;
			}

			GridView1.DataSource = dv;
			GridView1.DataBind();
		}

		private DataTable GetData(string project)
		{
			//var data = new Components.BusinessLayer.Project.Data();

			//data.Name = project;

			//var dt = TaskTimeTracker.Components.BusinessLayer.Project.Search(data, AuditId);
			//return dt;
			return  new DataTable();
		}
	}
}