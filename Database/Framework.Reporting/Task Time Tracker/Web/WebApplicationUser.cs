using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;

namespace TaskTimeTracker.UI.Web
{
	public class WebApplicationUser
	{
		public static int GetCurrentUserId()
		{
			var aplicationUserId = 300;  // set the PersonId of the user you want to log-in with
			try
			{
				var strFilePath = HttpContext.Current.Server.MapPath("~/");
				var strFileName = "MyConfigurations.xml";
				var doc = new XmlDocument();

				doc.Load(strFilePath + "//" + strFileName);
				var documentElement = doc.DocumentElement;

				if (documentElement != null)
				{

					var xConfiguration = documentElement.FirstChild;
					if (xConfiguration.Name == "Configuration")
					{
						var xPersonId = xConfiguration.FirstChild;
                        if (xPersonId.Name == "ApplicationUserId")
						{
							try
							{
								aplicationUserId = Convert.ToInt32(xPersonId.InnerText);
							}
							catch { }
						}
					}
				}
			}
			catch { }
			return aplicationUserId;
		}
	}
}