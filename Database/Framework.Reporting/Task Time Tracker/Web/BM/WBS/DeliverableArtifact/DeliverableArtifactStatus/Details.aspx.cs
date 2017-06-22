using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.DeliverableArtifactStatus
{
    public partial class Details : PageDetails
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.DeliverableArtifactStatus;
            PrimaryEntityKey = "DeliverableArtifactStatus";
            DetailsControlPath = ApplicationCommon.GetControlPath("DeliverableArtifactStatus", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion
    }
}