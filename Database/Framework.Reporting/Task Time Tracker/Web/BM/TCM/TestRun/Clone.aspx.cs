using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.TCM.TestRun
{
	public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
    {
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestRun;
			PrimaryEntityKey = "TestRun";
			BreadCrumbObject = Master.BreadCrumbObject;
			GenericControlPath = ApplicationCommon.GetControlPath("TestRun", ControlType.GenericControl);
			PrimaryGenericControl = (ControlGeneric)myGenericControl;
		}

		#endregion

	}
}