using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.Components.DataAccess;

namespace Shared.WebCommon.UI.Web
{
	public class ApplicationVariables
	{

		public static RequestProfile SystemRequestProfile
		{
			get
			{
				if (HttpContext.Current.Application != null)
					return (RequestProfile)HttpContext.Current.Application[SessionVariables.SessionKeys.SystemRequestProfile];
				else
					return null;
			}
			set
			{
				HttpContext.Current.Application[SessionVariables.SessionKeys.SystemRequestProfile] = value;
			}
		}
		
		public static string Branding
		{
			get
			{
				return (string) HttpContext.Current.Application["Branding"];
			}
			set
			{
				HttpContext.Current.Application["Branding"] = value;
			}
		}

		public static int Seed
		{
			get
			{
				return Convert.ToInt32( HttpContext.Current.Application["Seed"].ToString());
			}
			set
			{
				HttpContext.Current.Application["Seed"] = value;
			}
		}

		public static int Increment
		{
			get
			{
				return Convert.ToInt32(HttpContext.Current.Application["Increment"].ToString());
			}
			set
			{
				HttpContext.Current.Application["Increment"] = value;
			}
		}

		public static string WildCardPrefix = "%";
		public static string WildCardPostfix = "%";		

        public static List<UPreference> ApplicationInstancePreferences
        {
            get
            {
                if (HttpContext.Current.Application["ApplicationInstancePreferences"] != null)
                {
                    return (List<UPreference>)(HttpContext.Current.Application["ApplicationInstancePreferences"]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Application["ApplicationInstancePreferences"] = value;
            }
        }

	}
}