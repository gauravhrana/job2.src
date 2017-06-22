using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Admin.UserLoginStatus
{
	public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
	{

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLoginStatus;
            PrimaryEntityKey = "UserLoginStatus";
            BreadCrumbObject = Master.BreadCrumbObject;
            GenericControlPath = ApplicationCommon.GetControlPath("UserLoginStatus", ControlType.GenericControl);
            PrimaryGenericControl = myGenericControl;
        }

	}
}