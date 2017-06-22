using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.TaskNote.Controls
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

        #endregion

        #region private methods

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

        public void ShowData(string searchCondition, bool dataHide)
        {
            ViewState["SearchCondition"] = searchCondition;
            SortGridView(string.Empty, null);
            HideData = dataHide;
        }

        private DataTable GetData(string name)
        {
			var data = new DataModel.TaskTimeTracker.Task.TaskNoteDataModel();

            data.Name = name;

            var dt = TaskTimeTracker.Components.BusinessLayer.Task.TaskNoteDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SearchCondition"] = string.Empty;
                SortGridView(string.Empty, SortDirection.Ascending.ToString());
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

        #endregion

    }
}