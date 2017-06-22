using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;

namespace ApplicationContainer.UI.Web.Scheduling.Report
{
	public partial class WorkSummaryReport : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}

		
		protected override void OnPreRender(EventArgs e)
		{
			
			base.OnPreRender(e);

			var SettingCategory = "WorkSummaryReportDefaultView";
			var sbm = this.Master.SubMenuObject;

			sbm.SettingCategory = SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

			var bcControl = this.Master.BreadCrumbObject;
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup("");
			bcControl.GenerateMenu();
		}
	}
}