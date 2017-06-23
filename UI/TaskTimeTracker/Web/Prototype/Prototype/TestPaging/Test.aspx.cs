using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.Components.Core;
using System.Web.Routing;

namespace Shared.UI.Web.ApplicationManagement.Development.TestPaging
{
	public partial class Test : Framework.UI.Web.BaseClasses.PageBasePage
    {

        void bindGrid()
        {
            //var dt = Framework.Components.Core.MenuDataManager.GetList(SessionVariables.RequestProfile);
            //dt = dt.AsEnumerable().Take(10).CopyToDataTable();
            //dgvTest.DataSource = dt;
            //dgvTest.DataBind();
        }

        protected void NavigationMenu_MenuItemClick(Object sender, MenuEventArgs e)
        {
            // Display the text of the menu item selected by the user.
            //Message.Text = "You selected " +               e.Item.Text + ".";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                //bindGrid();
            //}
            
            //var dtRoutes = ApplicationRoute.GetList(SessionVariables.RequestProfile.AuditId);
            //var dtParameters = ApplicationRouteParameter.GetList(SessionVariables.RequestProfile.AuditId);
            //if(dtRoutes != null && dtRoutes.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dtRoutes.Rows)
            //    {
            //        var proposedRoute = Convert.ToString(dr[ApplicationRouteDataModel.DataColumns.ProposedRoute]);
            //        var relativeRoute = Convert.ToString(dr[ApplicationRouteDataModel.DataColumns.RelativeRoute]);
            //        var appRouteId = Convert.ToInt32(dr[ApplicationRouteDataModel.DataColumns.ApplicationRouteId]);
                                        
            //        var routeParameters = dtParameters.Select(ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteId + " = " + appRouteId);
            //        if (routeParameters.Length > 0)
            //        {
            //            var routeParamList = new RouteValueDictionary();
            //            foreach (DataRow drParam in routeParameters)
            //            {
            //                routeParamList.Add(Convert.ToString(drParam[ApplicationRouteParameterDataModel.DataColumns.ParameterName]), Convert.ToString(drParam[ApplicationRouteParameterDataModel.DataColumns.ParameterValue]));
            //            }
            //        }
            //    }
            //}

            //var lst = ApplicationConfiguration.GetRoutesViaXML();
            //foreach (var customRoute in lst)
            //{
            //    var appRouteData = new ApplicationRoute.Data();
            //    appRouteData.RouteName = customRoute.EntityName + "EntityRoute";
            //    appRouteData.EntityName = customRoute.EntityName;
            //    appRouteData.ProposedRoute = customRoute.ProposedRoute + "/{action}/{SetId}";
            //    appRouteData.RelativeRoute = customRoute.RelativePath + "/{action}.aspx";
            //    appRouteData.Description = customRoute.EntityName;

            //    var appRouteId = ApplicationRoute.Create(appRouteData, 5);

            //    var detailData = new ApplicationRouteParameter.Data();
            //    detailData.ApplicationRouteId = appRouteId;
            //    detailData.ParameterName = "EntityName";
            //    detailData.ParameterValue = customRoute.EntityName;
            //    ApplicationRouteParameter.Create(detailData, 5);

            //    detailData = new ApplicationRouteParameter.Data();
            //    detailData.ApplicationRouteId = appRouteId;
            //    detailData.ParameterName = "Action";
            //    detailData.ParameterValue = "Default";
            //    ApplicationRouteParameter.Create(detailData, 5);

            //    detailData = new ApplicationRouteParameter.Data();
            //    detailData.ApplicationRouteId = appRouteId;
            //    detailData.ParameterName = "SetId";
            //    detailData.ParameterValue = "null";
            //    ApplicationRouteParameter.Create(detailData, 5);

            //    appRouteData = new ApplicationRoute.Data();
            //    appRouteData.RouteName = customRoute.EntityName + "EntityRouteSuperKey";
            //    appRouteData.EntityName = customRoute.EntityName;
            //    appRouteData.ProposedRoute = customRoute.ProposedRoute + "/{action}/SuperKey/{SuperKey}";
            //    appRouteData.RelativeRoute = customRoute.RelativePath + "/{action}.aspx";
            //    appRouteData.Description = customRoute.EntityName;

            //    appRouteId = ApplicationRoute.Create(appRouteData, 5);

            //    detailData = new ApplicationRouteParameter.Data();
            //    detailData.ApplicationRouteId = appRouteId;
            //    detailData.ParameterName = "EntityName";
            //    detailData.ParameterValue = customRoute.EntityName;
            //    ApplicationRouteParameter.Create(detailData, 5);

            //    detailData = new ApplicationRouteParameter.Data();
            //    detailData.ApplicationRouteId = appRouteId;
            //    detailData.ParameterName = "Action";
            //    detailData.ParameterValue = "Default";
            //    ApplicationRouteParameter.Create(detailData, 5);

            //    break;
            //}

        }

        protected void dgvTest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTest.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void dgvTest_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                var btnDelete = new Button();
                btnDelete.Text = "Delete";
                btnDelete.Style.Add("background-color", "#B40404");
                btnDelete.Style.Add("font-weight", "bold");
                btnDelete.Style.Add("font-size", "small");
                btnDelete.Style.Add("color", "White");
                btnDelete.Click += new EventHandler(btnDelete_Click);
                //btnDelete.CommandName = "Delete";                
                //btnDelete.Command += new CommandEventHandler(btnDelete_Command);

                Table pagerTable = (e.Row.Cells[0].Controls[0] as Table);
                pagerTable.Width = Unit.Percentage(100);

                TableRow row = new TableRow();
                row = pagerTable.Rows[0];
                
                TableCell cell1 = new TableCell();
                cell1.Controls.Add(btnDelete);
                cell1.HorizontalAlign = HorizontalAlign.Left;

                row.Cells.AddAt(0, cell1);
                row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            Response.Write("Delete Click");
        }

        protected void dgvTest_DataBound(object sender, EventArgs e)
        {
            //DataPager pager = (DataPager)dgvTest.FindControl("DataPager1");            
            
            GridViewRow pagerRow = (GridViewRow)dgvTest.BottomPagerRow;
            if (pagerRow != null && pagerRow.Visible == false)
            {
                pagerRow.Visible = true;
            }
        }

        protected void dgvTest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "tested")
            {
                Response.Write("Grid Delete Command");
            }
        }
    }
}