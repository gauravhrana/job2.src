using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.FieldConfiguration.Controls
{
    public partial class List : Shared.UI.WebFramework.BaseControl
    {
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
                //tblMain.Visible                = !value;
                // todo: rename this LinkButton1, LinkBUtton2 ... no idea from code what they are for
                //LinkButton1.Visible            = !value;
                //LinkButton2.Visible            = !value;                
            }
            get
            {
                return _hideData;
            }
        }

        public void ShowData(string search, bool dataHide)
        {
            // 
            ViewState["SearchCondition"] = search;
            SortGridView(String.Empty, null);
            HideData = dataHide;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // intialize
                ViewState["SearchCondition"] = String.Empty;
                SortGridView(String.Empty, SortDirection.Ascending.ToString());
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
            // not sure, if this is right to save it in here everytime
            // data is sorted or 
            //ViewState["SearchCondition"] = sortExpression;
            //ViewState["SortDirection"] = sortDirection;

            //
            var dt = GetData((ViewState["SearchCondition"]).ToString());

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

            GridView1.DataSource = dv;
            GridView1.DataBind();
        }

        private System.Data.DataTable GetData(string name)
        {
            // basic list -- no longer needed as Search will be more robust and powerfull
            //var dt = Shared.Components.BusinessLayer.Project.GetList(SessionVariables.RequestProfile);

            // List given search parameters
            var data = new FieldConfigurationDataModel();

			var dt = FieldConfigurationDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }
    }
}