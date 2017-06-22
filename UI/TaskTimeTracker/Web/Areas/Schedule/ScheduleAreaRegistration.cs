using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.Schedule
{
	public class ScheduleAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
                return "Schedule";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
            //context.MapRoute(
            //    "Schedule_default",
            //    "AreaSchedule/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            //context.MapRoute(
            //    "Schedule_default2",
            //    "{applicationCode}/{applicationModule}/AreaSchedule/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                "Schedule",
                "Schedule/{controller}/{action}/{id}/{isMultiple}",
                new { action = "Index", id = UrlParameter.Optional, isMultiple = UrlParameter.Optional },
                new[] { "ApplicationContainer.UI.Web.Areas.Schedule.Controllers" }
            );
		}
	}
}
