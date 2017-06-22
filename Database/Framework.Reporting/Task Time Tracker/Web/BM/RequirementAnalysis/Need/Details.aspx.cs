using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Need
{
    public partial class Details : PageDetails
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.Need;
            PrimaryEntityKey = "Need";
            DetailsControlPath = ApplicationCommon.GetControlPath("Need", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BreadCrumbObject = Master.BreadCrumbObject;
        }
              

        #endregion

    }
}
