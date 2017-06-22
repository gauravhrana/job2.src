using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.TCM.TestCase
{
    public partial class Update : PageUpdate
	{		
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

            PrimaryEntity       = SystemEntity.TestCase;

            GenericControlPath  = ApplicationCommon.GetControlPath("TestCase", ControlType.GenericControl);
			PrimaryPlaceHolder	= plcUpdateList;
            PrimaryEntityKey    = "TestCase";
			BreadCrumbObject	= Master.BreadCrumbObject;

            BtnUpdate			= btnUpdate;
            BtnClone			= btnClone;
            BtnCancel			= btnCancel;
		}
		
        #endregion		

	}
}