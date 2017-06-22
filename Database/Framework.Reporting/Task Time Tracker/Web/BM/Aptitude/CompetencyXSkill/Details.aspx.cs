using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Aptitude.CompetencyXSkill
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.CompetencyXSkill;
			PrimaryEntityKey = "CompetencyXSkill";
			DetailsControlPath = ApplicationCommon.GetControlPath("CompetencyXSkill", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion 

    }
}