using System;
using System.Data;
using System.Web.UI;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using DataModel.TaskTimeTracker.RiskReward;

namespace ApplicationContainer.UI.Web.RiskAndReward.Demo
{

	public partial class Details : PageDetails
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		   = SystemEntity.Risk;
			PrimaryEntityKey	   = "Risk";
			DetailsControlPath	   = ApplicationCommon.GetControlPath("Risk", ControlType.DetailsControl);
			PrimaryPlaceHolder     = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject	   = Master.BreadCrumbObject;
		}

		#endregion

	}

}
