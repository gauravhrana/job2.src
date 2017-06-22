using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.EntityDateRangeState
{
	public partial class Details : PageDetails
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "EntityDateRangeState";
			var detailsPath = ApplicationCommon.GetControlPath("EntityDateRangeState", ControlType.DetailsControl);
			DetailsControlPath = detailsPath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = SystemEntity.EntityDateRangeState;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}