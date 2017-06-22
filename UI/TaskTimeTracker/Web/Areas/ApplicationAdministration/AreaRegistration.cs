using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.ApplicationAdministrationArea
{
	public class AreaRegistration : System.Web.Mvc.AreaRegistration
	{
		public override string AreaName
		{
			get
			{
                return "ApplicationAdministration";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
            context.MapRoute(
                "ApplicationAdministration_default",				 
                "ApplicationAdministration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            // Route override to work with Angularjs and HTML5 routing
            context.MapRoute(
                name: "ApplicationAdministration2Override",
                url: "ApplicationAdministration/app/{*.}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );

		}
	}
}
