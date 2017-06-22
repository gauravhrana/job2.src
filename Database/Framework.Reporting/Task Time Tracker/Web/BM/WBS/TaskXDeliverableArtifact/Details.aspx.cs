using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.TaskXDeliverableArtifact
{
    public partial class Details : PageDetails
    {
        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "TaskXDeliverableArtifact";
			var detailsPath = ApplicationCommon.GetControlPath("TaskXDeliverableArtifact", ControlType.DetailsControl);
			DetailsControlPath = detailsPath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = SystemEntity.TaskXDeliverableArtifact;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

        
        #endregion
    }
}