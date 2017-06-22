using DataModel.Framework.Core;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using System.Reflection;

namespace Web.Api.Controllers
{
    public class DeployScriptsController : ApiController
    {

        #region Static Methods

        private static object CreateClassInstance(string className)
        {
            Type type = null;

            var currentAssembly = Assembly.GetExecutingAssembly();
            var assembliyNames = currentAssembly.GetReferencedAssemblies();

            foreach (var aName in assembliyNames)
            {
                try
                {
                    var assembly = Assembly.Load(aName);
                    type = assembly.GetTypes().First(t => t.Name == className);
                    return Activator.CreateInstance(type);
                }
                catch { }
            }

            return null;
        }

        #endregion

        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<SystemEntityTypeDataModel> ListEntities()
        {
            var data = new SystemEntityTypeDataModel();
            var listEntities = SystemEntityTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfo);
            return listEntities;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<string> GetProcedureText(string value)
        {
            var entityName = value;
            var lstProcTexts = new List<string>();

            var objDataManager = CreateClassInstance(entityName + "DataManager");
            var objDataModel = CreateClassInstance(entityName + "DataModel");

            if (objDataManager != null && objDataModel != null)
            {
                var obj = (Framework.Components.DataAccess.StandardDataManager)objDataManager;
                obj.EntityName = entityName;

                // Get Insert Procedure
                var scriptBuilder = new StringBuilder();
                scriptBuilder.Append(obj.GetCheckAndDropProcedureScript("Insert"));
                scriptBuilder.AppendLine();
                scriptBuilder.Append(obj.GetInsertProcedureScript(objDataModel.GetType()));
                lstProcTexts.Add(scriptBuilder.ToString());

                // Get Update Procedure
                scriptBuilder = new StringBuilder();
                scriptBuilder.Append(obj.GetCheckAndDropProcedureScript("Update"));
                scriptBuilder.AppendLine();
                scriptBuilder.Append(obj.GetUpdateProcedureScript(objDataModel.GetType()));
                lstProcTexts.Add(scriptBuilder.ToString());

                // Get Delete Procedure
                scriptBuilder = new StringBuilder();
                scriptBuilder.Append(obj.GetCheckAndDropProcedureScript("Delete"));
                scriptBuilder.AppendLine();
                scriptBuilder.Append(obj.GetDeleteProcedureScript());
                lstProcTexts.Add(scriptBuilder.ToString());

                var searchableColumns = new List<string>() { entityName + "Id", "ApplicationId", "Name", "Description" };

                // Get Search Procedure
                scriptBuilder = new StringBuilder();
                scriptBuilder.Append(obj.GetCheckAndDropProcedureScript("Search"));
                scriptBuilder.AppendLine();
                scriptBuilder.Append(obj.GetSearchProcedureScript(searchableColumns, objDataModel.GetType()));
                lstProcTexts.Add(scriptBuilder.ToString());

            }

            return lstProcTexts;

        }

        [System.Web.Http.AcceptVerbs("GET")]
        public bool DeployProcedureText(string value, string entityName)
        {
            string procedureType = value;
            var objDataManager = CreateClassInstance(entityName + "DataManager");
            var objDataModel = CreateClassInstance(entityName + "DataModel");

            if (objDataManager != null && objDataModel != null)
            {
                var obj = (Framework.Components.DataAccess.StandardDataManager)objDataManager;
                obj.EntityName = entityName;

                var lstProcedureTypes = new List<string>();
                if (procedureType == "All")
                {
                    lstProcedureTypes = new List<string>() { "Insert", "Update", "Delete", "Search" };
                }
                else
                {
                    lstProcedureTypes.Add(procedureType);
                }
                var sqlScript = string.Empty;
                foreach (var procType in lstProcedureTypes)
                {
                    // Check and Drop Procedure
                    sqlScript = obj.GetCheckAndDropProcedureScript(procType);
                    obj.DeployScript(sqlScript);

                    if (procType == "Insert")
                    {
                        // Get Insert Procedure & Deploy
                        sqlScript = obj.GetInsertProcedureScript(objDataModel.GetType());
                        obj.DeployScript(sqlScript);
                    }
                    else if (procType == "Update")
                    {
                        // Get Update Procedure & Deploy
                        sqlScript = obj.GetUpdateProcedureScript(objDataModel.GetType());
                        obj.DeployScript(sqlScript);
                    }
                    else if (procType == "Delete")
                    {
                        // Get Delete Procedure & Deploy
                        sqlScript = obj.GetDeleteProcedureScript();
                        obj.DeployScript(sqlScript);
                    }
                    else if (procType == "Search")
                    {

                        var searchableColumns = new List<string>() { entityName + "Id", "ApplicationId", "Name", "Description" };

                        // Get Insert Procedure & Deploy
                        sqlScript = obj.GetSearchProcedureScript(searchableColumns, objDataModel.GetType());
                        obj.DeployScript(sqlScript);
                    }
                }

            }
            return true;
        }

    }
}
