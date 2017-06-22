using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser.Controls
{
	public partial class List : Shared.UI.WebFramework.BaseControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				ViewState["SearchCondition"] = String.Empty;
				SortGridView(String.Empty, SortDirection.Ascending.ToString());
			}
		}

		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.Header)
			{
				var row = (DataRowView)e.Row.DataItem;
				var cells = e.Row.Cells;

				for (int j = 1; j < e.Row.Cells.Count - 2; j++)
				{
					//e.Row.Cells[j].Style.Add("font-weight", "bold");
					//e.Row.Cells[j].Style.Add("color", "Red");
				}
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

				GridView1.Columns[2].Visible = !value;
				GridView1.Columns[3].Visible = !value;
				GridView1.Columns[4].Visible = !value;
			}
			get
			{
				return _hideData;
			}
		}

		public void ShowData(string searchCondition, bool dataHide)
		{
			ViewState["SearchCondition"] = searchCondition;
			SortGridView(String.Empty, null);
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

		private DataTable GetData(string name)
		{
			var data = new ApplicationUserDataModel();

			data.LastName = name;

			var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.Search(data, AuditId, SessionVariables.ApplicationMode);
			return dt;
		}
	}
}