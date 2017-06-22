using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.WBS.TaskXActivityInstance
{
	public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= Framework.Components.DataAccess.SystemEntity.TaskXActivityInstance;
			PrimaryEntityKey	= "TaskXActivityInstance";
			DetailsControlPath	= ApplicationCommon.GetControlPath("TaskXActivityInstance", ControlType.DetailsControl);
			PrimaryPlaceHolder	= oDetailsControl.PlaceHolderDetails;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}