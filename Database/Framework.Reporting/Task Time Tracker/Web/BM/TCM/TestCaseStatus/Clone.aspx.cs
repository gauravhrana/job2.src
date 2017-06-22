using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.TCM.TestCaseStatus
{
	public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
    {

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestCaseStatus;
			PrimaryEntityKey = "TestCaseStatus";
			BreadCrumbObject = Master.BreadCrumbObject;
			GenericControlPath = ApplicationCommon.GetControlPath("TestCaseStatus", ControlType.GenericControl);
			PrimaryGenericControl = (ControlGeneric)myGenericControl;
		}

		#endregion
	}
}