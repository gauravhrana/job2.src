using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.RiskAndReward.Demo
{

	public partial class Update : PageUpdate
	{

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= SystemEntity.Risk;

			GenericControlPath	= ApplicationCommon.GetControlPath("Risk", ControlType.GenericControl);
			PrimaryPlaceHolder	= plcUpdateList;
			PrimaryEntityKey	= "Risk";
			BreadCrumbObject	= Master.BreadCrumbObject;

			BtnUpdate			= btnUpdate;
			BtnClone				= btnClone;
			BtnCancel			= btnCancel;

		}

	}

}
