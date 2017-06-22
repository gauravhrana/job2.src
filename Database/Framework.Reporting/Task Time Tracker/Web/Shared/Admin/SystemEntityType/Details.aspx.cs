using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.SystemEntityType
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemEntityType;
            PrimaryEntityKey = "SystemEntityType";
            DetailsControlPath = ApplicationCommon.GetControlPath("SystemEntityType", ControlType.DetailsControl);
            PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject = Master.BreadCrumbObject;
        }

    }
}