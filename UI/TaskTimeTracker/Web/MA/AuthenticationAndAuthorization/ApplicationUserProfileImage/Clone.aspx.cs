using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImage
{
	public partial class Clone : Framework.UI.Web.BaseClasses.PageClone
	{

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImage;
			PrimaryEntityKey = "ApplicationUserProfileImage";
			BreadCrumbObject = Master.BreadCrumbObject;
			GenericControlPath = ApplicationCommon.GetControlPath("ApplicationUserProfileImage", ControlType.GenericControl);
			PrimaryGenericControl = myGenericControl;
		}

	}
}