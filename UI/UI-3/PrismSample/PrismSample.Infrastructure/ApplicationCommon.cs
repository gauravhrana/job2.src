using Framework.Components.DataAccess;
using System.Xml;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.ApplicationUser;
using System.Configuration;
using System.IO;

namespace PrismSample.Infrastructure
{

    public static class AuditDetailsFlagExt
    {
        public static int Value(this AuditDetailsFlag val)
        {
            return (int)val;
        }
    }

    public enum AuditDetailsFlag
    {
            DoNotFetchDetails = 0
        ,   FetchDetails      = 1
    }

    public class ApplicationCommon
    {
        public static RequestProfile ReqProfile = null;
        public static RequestProfile SystemRequestProfile = null;


        static ApplicationCommon()
        {
            SystemRequestProfile = new RequestProfile();

            var startupApplicationId = int.Parse(ConfigurationManager.AppSettings["StartupApplicationId"]);
            SystemRequestProfile.ApplicationId = startupApplicationId;
            SystemRequestProfile.AuditId = int.Parse(ConfigurationManager.AppSettings["PMT.SystemAuditId"]); ;
            SystemRequestProfile.ApplicationModeId = 1;

            ReqProfile = new RequestProfile();

            ReqProfile.AuditId = GetCurrentUserId(startupApplicationId);
            ReqProfile.ApplicationId = startupApplicationId;
            ReqProfile.ApplicationModeId = 1;
        }

        #region static constant variable

        public const string FieldConfigurationMode = "FieldConfigurationMode";

        #endregion

        #region Methods

        public static int GetCurrentUserId(int applicationId)
        {
            var aplicationUserEmail = string.Empty;  // set the PersonId of the user you want to log-in with
            var aplicationUserId = 0;
            try
            {
                //var strFilePath = HttpContext.Current.Server.MapPath("~/");
                var strFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                var strFileName = "MyConfigurations.xml";

                var doc = new XmlDocument();
                doc.Load(strFilePath + "//" + strFileName);
                //doc.Load(strFileName);

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
                var applicationUserData = new ApplicationUserDataModel();
                applicationUserData.EmailAddress = aplicationUserEmail;
                applicationUserData.ApplicationId = applicationId;

                var lst = ApplicationUserDataManager.GetEntityDetails(applicationUserData, SystemRequestProfile);
                if (lst != null && lst.Count > 0)
                {
                    aplicationUserId = lst[0].ApplicationUserId.Value;
                }
            }

            return aplicationUserId;
        }

        public static RequestProfile GetRequestProfile()
        {
            return ReqProfile;
        }

        #endregion

    }
}
