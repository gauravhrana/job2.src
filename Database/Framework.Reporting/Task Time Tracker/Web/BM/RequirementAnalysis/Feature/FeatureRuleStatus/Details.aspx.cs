using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.FeatureRuleStatus
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
		#region Events

		protected override void OnInit(EventArgs e) 
		{
			base.OnInit(e);
			PrimaryEntityKey = "FeatureRuleStatus";
			var detailsPath = ApplicationCommon.GetControlPath("FeatureRuleStatus", ControlType.DetailsControl);
			DetailsControlPath = detailsPath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRuleStatus;
			BreadCrumbObject = Master.BreadCrumbObject;
		}		

		#endregion
    }
}