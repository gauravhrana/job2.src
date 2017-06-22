using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.Configuration.Application
{
    public partial class Details : PageDetails
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity        = SystemEntity.Application;
            PrimaryEntityKey     = "Application";
            DetailsControlPath   = ApplicationCommon.GetControlPath("Application", ControlType.DetailsControl);
            PrimaryPlaceHolder   = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject     = Master.BreadCrumbObject;
        }  

        #endregion

    }
}
