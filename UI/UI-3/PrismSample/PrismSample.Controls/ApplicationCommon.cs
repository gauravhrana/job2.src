using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismSample.Controls
{
    public class ApplicationCommon
    {
        public static RequestProfile ReqProfile = null;

        static ApplicationCommon()
        {
            ReqProfile = new RequestProfile();

            ReqProfile.AuditId = 10;
            ReqProfile.ApplicationId = 100;
            ReqProfile.ApplicationModeId = 1;
        }

        public static RequestProfile GetRequestProfile()
        {
            return ReqProfile;
        }

        public static List<FieldConfigurationDataModel> GetFieldConfigurations(string entityName)
        {
            var fieldConfigurationDataModel = new FieldConfigurationDataModel();

            fieldConfigurationDataModel.SystemEntityTypeId = Helper.GetSystemEntityId(entityName);
            fieldConfigurationDataModel.FieldConfigurationModeId = FieldConfigurationModeDataManager.GetFCModeIdByName("Standard", ReqProfile);

            var result = FieldConfigurationDataManager.GetEntityDetails(fieldConfigurationDataModel, ReqProfile);
            return result;
        }

        public static List<FieldConfigurationModeDataModel> GetApplicationModes(string entityName)
        {
            var fcModes = FieldConfigurationModeDataManager.GetEntityDetails(FieldConfigurationModeDataModel.Empty, ReqProfile);
            return fcModes;
        }

    }
}
