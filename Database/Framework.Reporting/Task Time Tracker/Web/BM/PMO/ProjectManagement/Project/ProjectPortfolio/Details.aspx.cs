using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ProjectPortfolio
{
    public partial class Details : PageDetails
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ProjectPortfolio;
            PrimaryEntityKey = "ProjectPortfolio";
            DetailsControlPath = ApplicationCommon.GetControlPath("ProjectPortfolio", ControlType.DetailsControl);
            PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject = Master.BreadCrumbObject;
        }


        #endregion

    }
}
