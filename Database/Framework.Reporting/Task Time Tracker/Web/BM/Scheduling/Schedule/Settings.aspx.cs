using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule
{
	public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			eSettingsList.SetUp((int)SystemEntity.Schedule, "Schedule");
		}

		override protected void btnBack_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Shared/Configuration/Schedule/Default.aspx");
		}
	}
}