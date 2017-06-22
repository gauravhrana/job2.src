using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.V45
{
	public class V45AreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "V45";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"V45_default",
				"V45/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
