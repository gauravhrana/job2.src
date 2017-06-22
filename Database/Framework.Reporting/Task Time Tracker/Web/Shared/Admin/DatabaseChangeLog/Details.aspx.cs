using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.DatabaseChangeLog
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey   = "DatabaseChangeLog";
            var detailsPath    = ApplicationCommon.GetControlPath("DatabaseChangeLog", ControlType.DetailsControl);
            DetailsControlPath = detailsPath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.DatabaseChangeLog;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion       

    }
}