using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.Aptitude.Skill
{
    public partial class Details : PageDetails
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity          = SystemEntity.Skill;
            PrimaryEntityKey       = "Skill";
            DetailsControlPath     = ApplicationCommon.GetControlPath("Skill", ControlType.DetailsControl);
            PrimaryPlaceHolder     = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject	   = Master.BreadCrumbObject;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}