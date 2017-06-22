using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.LogAndTrace;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web;
using Shared.UI.Web.Controls.BreadCrumb;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.MasterPages.Legal
{
	public partial class SiteMaster : PageSiteMaster
	{

		#region Properties

		override public BreadCrumb BreadCrumbObject
		{
			get
			{
				return oBreadCrumb;
			}
		}


		#endregion

		#region Methods

		#endregion

		#region events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!SessionVariables.UserAuthorized)
			{
				Response.Redirect("~/UnauthorizedAccess.htm", true);
			}

			Page.Header.DataBind();

			//StartupCheck();

			//var isTesting = SessionVariables.IsTesting;

			lblPerson.Text = "Logged in User: " + ApplicationCommon.GetApplicationUserName() + " (" + SessionVariables.RequestProfile.AuditId + ")";

			if (!IsPostBack)
			{
				lblStatus.ToolTip = "ApplicationMode = " + oSliderMenu.ApplicationMode + " \n" + " MenuCategory = " + oSliderMenu.MenuCategory;
			}

			var strURL = String.Empty;

			if (ApplicationCommon.GetSetId() != 0)
			{
				strURL = Page.AppRelativeVirtualPath.Substring(0, Page.AppRelativeVirtualPath.LastIndexOf(".", System.StringComparison.Ordinal)) + "/" + ApplicationCommon.GetSetId();
			}
			else
			{
				strURL = Page.AppRelativeVirtualPath;
			}

            AddUserLoginHistoryRecord();


		}

		#endregion
	}
}