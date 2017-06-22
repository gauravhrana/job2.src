using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.WebCommon.UI.Web
{
	public class ApplicationVariables
	{
		public static string Branding
		{
			get
			{
				return Convert.ToString(HttpContext.Current.Application["Branding"]);
			}
			set
			{
				HttpContext.Current.Application["Branding"] = value;
			}
		}
	}
}