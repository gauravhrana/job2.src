using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.MilestoneXFeatureArchive
{
    public partial class Details : PageDetails
    {
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.MilestoneXFeatureArchive;
			PrimaryEntityKey = "MilestoneXFeatureArchive";
			DetailsControlPath = ApplicationCommon.GetControlPath("MilestoneXFeatureArchive", ControlType.DetailsControl);			
			PrimaryPlaceHolder = plcDetailsList;			
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		
		#endregion
    }
}