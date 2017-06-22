using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.TaskXDeliverableArtifact
{
    public partial class Clone : PageClone
    {
        #region Events    
        
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		  = Framework.Components.DataAccess.SystemEntity.TaskXDeliverableArtifact;
			PrimaryEntityKey	  = "TaskXDeliverableArtifact";
			BreadCrumbObject      = Master.BreadCrumbObject;
			GenericControlPath    = ApplicationCommon.GetControlPath("TaskXDeliverableArtifact", ControlType.GenericControl);
			PrimaryGenericControl = myGenericControl;
		}
        
        #endregion
    }
}