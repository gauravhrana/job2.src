using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;

namespace Shared.UI.Web
{
    public class WebApplicationUser
    {

        public static void LogUserLogin(int auditId, bool userAuthorized)
        {
            var data = new Framework.Components.LogAndTrace.UserLoginDataModel();
            data.UserName = SessionVariables.ApplicationUserName;

            data.UserLoginStatus = userAuthorized ? "LOGGED_IN" : "LOGIN_FAIL";
            if (ApplicationCommon.ApplicationCache.ContainsKey(SessionVariables.SystemRequestProfile.ApplicationId))
            {
                data.Application = ApplicationCommon.ApplicationCache[SessionVariables.SystemRequestProfile.ApplicationId].Name;
            }

			Framework.Components.LogAndTrace.UserLoginDataManager.Create(data, SessionVariables.SystemRequestProfile);			
        }

        public static void LogUserLogin(int auditId, string  userLoginStatus)
        {
            var data = new Framework.Components.LogAndTrace.UserLoginDataModel();
            
			data.UserName = SessionVariables.ApplicationUserName;
            data.UserLoginStatus = userLoginStatus;

            if (ApplicationCommon.ApplicationCache.ContainsKey(SessionVariables.SystemRequestProfile.ApplicationId))
            {
                data.Application = ApplicationCommon.ApplicationCache[SessionVariables.SystemRequestProfile.ApplicationId].Name;
            }

			Framework.Components.LogAndTrace.UserLoginDataManager.Create(data, SessionVariables.SystemRequestProfile);
        }

		public static int GetCurrentUserId(int applicationId)
		{
			var aplicationUserEmail = string.Empty;  // set the PersonId of the user you want to log-in with
			var aplicationUserId = 0;
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
						foreach (XmlNode configChild in xConfiguration.ChildNodes)
						{
							if (configChild.Name == "ApplicationUserEmail")
							{
								aplicationUserEmail = configChild.InnerText;
								break;
							}
						}
					}
				}
			}
			catch { }

			if (!string.IsNullOrEmpty(aplicationUserEmail))
			{
				var applicationUserData           = new ApplicationUserDataModel();
				applicationUserData.EmailAddress  = aplicationUserEmail;
                applicationUserData.ApplicationId = applicationId;

                var obj = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(applicationUserData, SessionVariables.SystemRequestProfile);
                if (obj != null)
				{
                    aplicationUserId = obj.ApplicationUserId.Value;
				}
			}

			return aplicationUserId;
		}

        public static int GetApplicationInstanceId()
        {
            var aplicationInstanceId = 0;  // set the PersonId of the user you want to log-in with

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
						foreach (XmlNode configChild in xConfiguration.ChildNodes)
						{
							if (configChild.Name == "ApplicationInstanceId")
							{
								try
								{
									aplicationInstanceId = Convert.ToInt32(configChild.InnerText);
									break;
								}
								catch { }
							}
						}
                    }
                }
            }
            catch { }
            return aplicationInstanceId;
        }

        public static bool CheckIfUserIsValid(int applicationUserId)
        {
            var isValid = false;
            
			var data               = new ApplicationUserDataModel();
            data.ApplicationUserId = applicationUserId;

            var obj = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(data, SessionVariables.SystemRequestProfile);

            if (obj != null)
            {
                isValid = true;
                SessionVariables.ApplicationUserName = obj.ApplicationUserName;
            }
            
			LogUserLogin(applicationUserId, isValid);
            
			return isValid;
        }

        public static int GetStartupApplicationId()
        {
            var startupApplicationId = 0;  

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
                        foreach (XmlNode configChild in xConfiguration.ChildNodes)
                        {
                            if (configChild.Name == "StartupApplicationId")
                            {
                                try
                                {
                                    startupApplicationId = Convert.ToInt32(configChild.InnerText);
                                    break;
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            catch { }
            return startupApplicationId;
        }

    }

}