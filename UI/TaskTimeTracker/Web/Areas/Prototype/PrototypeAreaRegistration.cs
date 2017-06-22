using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.PrototypeArea
{
    public class PrototypeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Prototype";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AreaPrototype_default",
                "AreaPrototype/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}