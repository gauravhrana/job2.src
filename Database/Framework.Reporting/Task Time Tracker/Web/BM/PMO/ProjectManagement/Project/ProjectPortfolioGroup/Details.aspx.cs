using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ProjectPortfolioGroup
{
    public partial class Details : PageDetails
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ProjectPortfolioGroup;
            PrimaryEntityKey = "ProjectPortfolioGroup";
            DetailsControlPath = ApplicationCommon.GetControlPath("ProjectPortfolioGroup", ControlType.DetailsControl);
            PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject = Master.BreadCrumbObject;
        }


        #endregion

    }
}
