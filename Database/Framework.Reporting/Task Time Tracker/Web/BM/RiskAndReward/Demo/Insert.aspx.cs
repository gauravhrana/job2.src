using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.RiskAndReward.Demo
{
	public partial class Insert : PageInsert
	{  
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.Risk;
			PrimaryEntityKey		= "Risk";		
			PrimaryGenericControl	= myGenericControl;		
			BreadCrumbObject		= Master.BreadCrumbObject;	
		}

		#endregion
	}
}
