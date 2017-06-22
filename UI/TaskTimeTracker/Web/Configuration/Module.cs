using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web.Configuration
{
    public class Module
    {
        static Module()
        {

        }

        public static void Initalize()
        {
            //SetupClientRoute();
            SetupConfigurationRoutes();
        }

        private static void SetupConfigurationRoutes()
        {
            var entityNames = new List<string>(new string[]	{
				"Application",  "ApplicationMode",	"ApplicationModeXFieldConfigurationMode", "ApplicationRoute", "ApplicationRouteParameter",
				"DateRangeTitle", "FieldConfiguration", "FieldConfigurationMode", "FieldConfigurationAccessMode",
                "FieldConfigurationModeCategory", "FieldConfigurationModeCategoryXFCMode", "FieldConfigurationModeXApplicationRole",
				"FieldConfigurationModeXApplicationRole", "Language", "Menu", "MenuCategory", "MenuCategoryXMenu", 
                "SystemForeignRelationship", "SystemForeignRelationshipDatabase", "SystemForeignRelationshipType", 
                "TabChildStructure", "TabParentStructure", "Theme", "ThemeCategory", "ThemeDetail", "ThemeKey",
                "UserPreference", "UserPreferenceCategory", "UserPreferenceKey", "UserPreferenceDataType", "UserPreferenceSelectedItem"
			});

            foreach (string entity in entityNames)
            {
                var oApplicationRouteDataModel = new ApplicationRouteDataModel();

                oApplicationRouteDataModel.EntityName = entity;

                var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());                

                foreach (var dataItem in items)
                {
                    if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
                    {
                        dataItem.RelativeRoute = string.Format("~/Configuration/{0}/{{action}}.aspx", dataItem.EntityName);
                    }
                    else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
                    {
                        dataItem.RelativeRoute = string.Format("~/Configuration/{0}/{{action}}.aspx", dataItem.EntityName);
                    }
                    else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
                    {
                        dataItem.RelativeRoute = string.Format("~/Configuration/{0}/Default.aspx", dataItem.EntityName);
                    }

                    ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
                    //break;
                }
                //break;
            }
        }


        private static void SetupClientRoute()
        {
            var oApplicationRouteDataModel = new ApplicationRouteDataModel();

            oApplicationRouteDataModel.EntityName = "Client";

            var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());

            foreach (var dataItem in items)
            {
                if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
                {
                    dataItem.RelativeRoute = string.Format("~/PMO/{0}/{{action}}.aspx", dataItem.EntityName);
                }
                else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
                {
                    dataItem.RelativeRoute = string.Format("~/PMO/{0}/{{action}}.aspx", dataItem.EntityName);
                }
                else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
                {
                    dataItem.RelativeRoute = string.Format("~/PMO/{0}/Default.aspx", dataItem.EntityName);
                }

                ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
            }
        }
    }
}