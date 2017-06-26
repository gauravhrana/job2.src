﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 using DataModel.Framework.AuthenticationAndAuthorization;
 using DataModel.Framework.Core;
 using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImage
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImage;

			GenericControlPath = ApplicationCommon.GetControlPath("ApplicationUserProfileImage", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey = "ApplicationUserProfileImage";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnClone = btnClone;
			BtnCancel = btnCancel;
		}

    }

}