using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.MilestoneFeatureState
{
    public partial class Details : PageDetails
    {
        #region Events
         
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "MilestoneFeatureState";
			var detailsPath = ApplicationCommon.GetControlPath("MilestoneFeatureState", ControlType.DetailsControl);
			DetailsControlPath = detailsPath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = SystemEntity.MilestoneFeatureState;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}    

		
        #endregion
    }
}