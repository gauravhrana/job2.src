using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser
{
	public partial class Insert : Framework.UI.Web.BaseClasses.PageInsert
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.ApplicationUser;
			PrimaryEntityKey		= "ApplicationUser";
			PrimaryGenericControl	= myGenericControl;
			BreadCrumbObject		= Master.BreadCrumbObject;
		}

		#endregion
	}
}