using System;
using System.Text;
using System.Web;

namespace ApplicationContainer.UI.Web.Styles
{
	public class StyleMenuHandler : IHttpHandler
	{
		public void ProcessRequest (System.Web.HttpContext context)
		{
			// Comment out these lines first:
			 context.Response.ContentType = "text/plain";
			 context.Response.Write("Hello World");

			//context.Response.ContentType = "image/png";
			//context.Response.WriteFile("~/Flower1.png");

			//context.Response.Clear();
			//context.Response.ContentType = "text/xml";
			//context.Response.ContentEncoding = Encoding.UTF8;
			//context.Response.ContentEncoding = Encoding.UTF32;
			//context.Response.ContentType = "text/xml; charset=utf-8";
			//context.Response.ContentType = "text/xml; charset=utf-32";
			//context.Response.End();

		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}

}
