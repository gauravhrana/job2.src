using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.UserLoginHistory
{
	public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLoginHistory;
			PrimaryEntityKey = "UserLoginHistory";
			DetailsControlPath = ApplicationCommon.GetControlPath("UserLoginHistory", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

		//protected void Page_Load(object sender, EventArgs e)
		//{
		//	BreadCrumbObject = Master.BreadCrumbObject;
		//}

		#endregion

	}
}