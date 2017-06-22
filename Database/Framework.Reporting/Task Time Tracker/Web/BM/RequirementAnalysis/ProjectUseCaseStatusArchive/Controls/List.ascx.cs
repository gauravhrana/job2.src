﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer.DataModel.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.ProjectUseCaseStatusArchive.Controls
{
	public partial class List : Shared.UI.WebFramework.BaseControl
	{
		#region variables

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

		#endregion

		#region private methods

		private void SortGridView(string sortExpression, string sortDirection)
		{
			var dt = GetData();
			var dv = dt.DefaultView;

			if (!string.IsNullOrEmpty(sortExpression))
			{
				dv.Sort = sortExpression + " " + sortDirection;
			}

			GridView1.DataSource = dv;
			GridView1.DataBind();
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

		private DataTable GetData()
		{
			var data = new ProjectUseCaseStatusArchiveDataModel();
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusArchiveDataManager.Search(data, SessionVariables.RequestProfile);
			return dt;
		}

		#endregion

		#region Events

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

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				ViewState["SearchCondition"] = String.Empty;
				SortGridView(String.Empty, SortDirection.Ascending.ToString());
			}
		}

		#endregion
	}
}