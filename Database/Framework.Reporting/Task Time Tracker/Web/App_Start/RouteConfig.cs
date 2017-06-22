using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApplication3
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			//routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			
			// Web API routes
			//routes.MapRoute(
			//	name: "ActionApi",
			//	url: "/apiV2/{controller}/{action}/{id}",
			//	defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
			//);

			// MapHttpRoute is meant for Web API controllers.			

			
			routes.MapHttpRoute(
				name: "RestApi",
				routeTemplate: "apiV2/rest/{controller}/{id}",
				defaults:   new 
							{ 
									id = RouteParameter.Optional
							}
			);


			routes.MapHttpRoute(
				name: "ActionApi",
				routeTemplate: "apiV2/{controller}/{action}/{value}",
				defaults:   new 
							{ 
								value = RouteParameter.Optional, 
								action = RouteParameter.Optional 
							}
			);

			routes.MapHttpRoute(
				name: "ActionApi2",
				routeTemplate: "apiV2/{controller}/{action}/{value}/{value1}",
				defaults:   new 
							{ 
								value = RouteParameter.Optional,
								value1 = RouteParameter.Optional, 
								action = RouteParameter.Optional 
							}
			);

			routes.MapHttpRoute(
				name: "ActionApi3",
				routeTemplate: "apiV2/{controller}/{action}/{value1}/{value2}/{value3}/",
				defaults:   new
							{
								value1 = RouteParameter.Optional,
								value2 = RouteParameter.Optional,
								value3 = RouteParameter.Optional,
								action = RouteParameter.Optional 
							}
			);

			//routes.MapRoute(
			//	name: "Default",
			//	url: "{controller}/{action}/{id}",
			//	defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
			//	namespaces: new[] { "Areas.Controllers" }
			//);

			// routes.MapRoute(
			//	name: "Default1",
			//	url: "NewView/{controller}/{action}/{id}",
			//	defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			//);
		}
	}
}