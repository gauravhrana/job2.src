using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.SystemAdminArea
{
    public class SystemAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SystemAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SystemAdmin_default",
                "SystemAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            // Route override to work with Angularjs and HTML5 routing
            context.MapRoute(
                name: "SystemAdmin2Override",
                url: "SystemAdmin/app/{*.}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}