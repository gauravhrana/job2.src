//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Framework.Components.DataAccess;

//namespace ApplicationContainer.UI.Web.Areas
//{

//	public class SessionVariables
//	{
//		public static class SessionKeys
//		{
//			public static string ApplicationUserRoles = "ApplicationUserRoles";			
//			public static string RequestProfile = "RequestProfile";		
//		}

//		public static RequestProfile RequestProfile
//		{
//			get
//			{
//				return (RequestProfile)HttpContext.Current.Session[SessionKeys.RequestProfile];
//			}
//			set
//			{
//				HttpContext.Current.Session[SessionKeys.RequestProfile] = value;
//			}
//		}
//	}
//}