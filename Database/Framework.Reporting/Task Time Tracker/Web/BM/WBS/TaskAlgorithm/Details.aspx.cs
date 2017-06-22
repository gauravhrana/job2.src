using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithm
{
	public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskAlgorithm;
			PrimaryEntityKey = "TaskAlgorithm";
			DetailsControlPath = ApplicationCommon.GetControlPath("TaskAlgorithm", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}