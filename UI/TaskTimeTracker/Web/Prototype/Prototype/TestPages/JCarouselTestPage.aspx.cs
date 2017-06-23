using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.Development.TestPages
{
	public partial class JCarouselTestPage : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region private methods

        private void bindData()
        {
            var dt = TaskTimeTracker.Components.BusinessLayer.ClientDataManager.GetList(SessionVariables.RequestProfile);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindData();
            }
        }
    }
}