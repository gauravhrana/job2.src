using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.RunTimeFeature
{
	public partial class Details : PageDetails
	{

		#region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity        = SystemEntity.RunTimeFeature;
            PrimaryEntityKey     = "RunTimeFeature";
            DetailsControlPath   = ApplicationCommon.GetControlPath("RunTimeFeature", ControlType.DetailsControl);
            PrimaryPlaceHolder   = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject     = Master.BreadCrumbObject;
        }

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}