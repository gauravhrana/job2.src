using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Scheduling.CustomTimeLog
{
	public partial class Details : PageDetails
	{

		#region Methods

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.CustomTimeLog;
			PrimaryEntityKey = "CustomTimeLog";
			DetailsControlPath = ApplicationCommon.GetControlPath("CustomTimeLog", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	} 
}