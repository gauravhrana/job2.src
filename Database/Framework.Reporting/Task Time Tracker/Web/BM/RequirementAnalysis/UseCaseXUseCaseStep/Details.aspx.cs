using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.UseCaseXUseCaseStep
{
	public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
	{
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseXUseCaseStep;
			PrimaryEntityKey = "UseCaseXUseCaseStep";
			DetailsControlPath = ApplicationCommon.GetControlPath("UseCaseXUseCaseStep", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}