using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.WBS.ActivityState
{
    public partial class Details : PageDetails
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ActivityState;
            PrimaryEntityKey = "ActivityState";
            DetailsControlPath = ApplicationCommon.GetControlPath("ActivityState", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}