using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ActivityXDeliverableArtifact
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "ActivityXDeliverableArtifact";
			var detailsPath = ApplicationCommon.GetControlPath("ActivityXDeliverableArtifact", ControlType.DetailsControl);
			DetailsControlPath = detailsPath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ActivityXDeliverableArtifact;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}

        #endregion
    }
}