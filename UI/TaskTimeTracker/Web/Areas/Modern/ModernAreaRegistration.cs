using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.Modern
{
	public class ModernAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Modern";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Modern_default",
				"Modern/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
