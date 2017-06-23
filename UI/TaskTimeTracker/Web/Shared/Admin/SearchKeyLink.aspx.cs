using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Admin
{
	public partial class SearchKeyLink : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            var entityName = Convert.ToString(Page.RouteData.Values["EntityName"]);
            var searchKey = Convert.ToString(Page.RouteData.Values["SetId"]);
            hypSearchLink.Text = Page.GetRouteUrl(entityName + "EntityRouteSearchKey", new { SearchKey = searchKey });
            hypSearchLink.NavigateUrl = Page.GetRouteUrl(entityName + "EntityRouteSearchKey", new { SearchKey = searchKey });
        }

        #endregion

    }
}