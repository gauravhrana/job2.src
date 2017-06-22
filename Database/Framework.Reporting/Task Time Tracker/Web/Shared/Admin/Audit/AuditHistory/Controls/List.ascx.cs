using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.Audit.AuditHistory.Controls
{
    public partial class List : Shared.UI.WebFramework.BaseControl
    {

        #region properties

        private bool _hideData;

        public bool HideData
        {
            set
            {
                _hideData = value;

                // more clear its hiding / showing sections
                // you do not want to put also here get data
                // it confuses the message on what HideData is doing
                //this.tblMain.Visible                = !value;
                // todo: rename this LinkButton1, LinkBUtton2 ... no idea from code what they are for
                //this.LinkButton1.Visible            = !value;
                //this.LinkButton2.Visible            = !value;                
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

        public void ShowData(string search, bool dataHide)
        {
            // 
            ViewState["SearchCondition"] = search;
            SortGridView(String.Empty, null);
            this.HideData = dataHide;
        }

        private void SortGridView(string sortExpression, string sortDirection)
        {
            // not sure, if this is right to save it in here everytime
            // data is sorted or 
            //ViewState["SearchCondition"] = sortExpression;
            //ViewState["SortDirection"] = sortDirection;

            //
            var dt = GetData(null, null, null, null, null, null);

            if (!IsPostBack)
            {
                lblCount.Text = "";
            }
            else
            {
                lblCount.Text = "Number of results found: " + dt.Rows.Count.ToString();
            }

            var dv = dt.DefaultView;

            if (!string.IsNullOrEmpty(sortExpression))
            {
                dv.Sort = sortExpression + " " + sortDirection;
            }

            this.GridView1.DataSource = dv;
            this.GridView1.DataBind();
        }

        private System.Data.DataTable GetData(int? systemEntityId, int? entityKey, int? auditActionId, DateTime? fromDate, DateTime? toDate, int? createdByPersonId)
        {
            // basic list -- no longer needed as Search will be more robust and powerfull

            // List given search parameters
            var data = new DataModel.Framework.Audit.AuditHistory();

            data.SystemEntityId = systemEntityId;
            data.EntityKey = entityKey;
            data.AuditActionId = auditActionId;
            data.FromSearchDate = fromDate;
            data.ToSearchDate = toDate;
            data.CreatedByPersonId = createdByPersonId;

			var dt = Framework.Components.Audit.AuditHistoryDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // intialize
                ViewState["SearchCondition"] = String.Empty;
                SortGridView(String.Empty, SortDirection.Ascending.ToString());
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