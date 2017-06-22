
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleDetail
{
	public partial class TwoDimesionalStatistics : PageCommon
    {
		protected override void OnInit(EventArgs e)
		{
			SettingCategory = "TwoDimesionalStatisticsDefaultView";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

    }
}