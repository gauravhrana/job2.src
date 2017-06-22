using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.ReleasePublishCategory.Controls
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

        private DataTable GetData(string name)
        {
            var data = new ReleasePublishCategoryDataModel();
            data.Name = name;
			var dt = Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.Search(data, AuditId);
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