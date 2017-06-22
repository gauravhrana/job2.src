using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.TimeEntryArea
{
	public class AreaRegistration : System.Web.Mvc.AreaRegistration
	{
		public override string AreaName
		{
			get
			{
                return "TimeEntry";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
            context.MapRoute(
                "TimeEntry_default",
                "TimeEntry/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            // Route override to work with Angularjs and HTML5 routing
            context.MapRoute(
                name: "TimeEntry2Override",
                url: "TimeEntry/app/{*.}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );

		}
	}
}
