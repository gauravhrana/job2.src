using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace ApplicationContainer.UI.Web.BM.TCM
{
    public class Module
    {
        static Module()
        {

        }

        public static void Initalize()
        {
            SetupTCMRoutes();
        }

        private static void SetupTCMRoutes()
        {
            List<string> entityNamesTCM = new List<string>(new string[]	{
                "TestCase", "TestCaseOwner", "TestCasePriority", "TestCaseStatus",
				"TestRun", "TestSuite", "TestSuiteXTestCase",				
				"TestSuiteXTestCaseArchive"
			});

            foreach (string entity in entityNamesTCM)
            {
                var oApplicationRouteDataModel = new ApplicationRouteDataModel();

                oApplicationRouteDataModel.EntityName = entity;
                var route1 = string.Empty;

                var items = ApplicationRouteDataManager.GetEntityDetails(oApplicationRouteDataModel, Global.GetStartupProfile());
                switch (entity)
                {
                    case "TestCase":
                    case "TestCaseOwner":
                    case "TestCasePriority":
                    case "TestCaseStatus":
                    case "TestRun":
                    case "TestSuite":
                    case "TestSuiteXTestCase":
                    case "TestSuiteXTestCaseArchive":
                        route1 = "~/BM/TCM/";
                        break;

                }

                foreach (var dataItem in items)
                {
                    if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRoute"))
                    {
                        if (route1 != string.Empty)
                            dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
                        else
                            dataItem.RelativeRoute = string.Format("~/BM/TCM/{0}/{{action}}.aspx", dataItem.EntityName);
                    }
                    else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSuperKey"))
                    {
                        if (route1 != string.Empty)
                            dataItem.RelativeRoute = string.Format(route1 + "{0}/{{action}}.aspx", dataItem.EntityName);
                        else
							dataItem.RelativeRoute = string.Format("~/BM/TCM/{0}/{{action}}.aspx", dataItem.EntityName);
                    }
                    else if (dataItem.RouteName.Equals(dataItem.EntityName + "EntityRouteSearch"))
                    {
                        if (route1 != string.Empty)
                            dataItem.RelativeRoute = string.Format(route1 + "{0}/Default.aspx", dataItem.EntityName);
                        else
							dataItem.RelativeRoute = string.Format("~/BM/TCM/{0}/Default.aspx", dataItem.EntityName);
                    }

                    ApplicationRouteDataManager.Update(dataItem, Global.GetStartupProfile());
                }
            }
        }
    }
        
 }