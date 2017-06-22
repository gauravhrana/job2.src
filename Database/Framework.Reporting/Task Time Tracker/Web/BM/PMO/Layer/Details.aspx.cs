using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.Layer
{
    public partial class Details : PageDetails
        
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.Layer;
            PrimaryEntityKey = "Layer";
            DetailsControlPath = ApplicationCommon.GetControlPath("Layer", ControlType.DetailsControl);
            PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}