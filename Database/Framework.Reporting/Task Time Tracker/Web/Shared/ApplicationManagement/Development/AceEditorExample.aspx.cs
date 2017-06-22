using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer.Task;

namespace Shared.UI.Web.Development
{
	public partial class AceEditorExample : System.Web.UI.Page
	{

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

		[WebMethod]
		public static Dictionary<string, string> GetProcedureText(string entityName)
		{
			var procedures = new Dictionary<string, string>();

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
				procedures.Add("InsertProcedure", scriptBuilder.ToString());

				// Get Update Procedure
				scriptBuilder = new StringBuilder();
				scriptBuilder.Append(obj.GetCheckAndDropProcedureScript("Update"));
				scriptBuilder.AppendLine();
				scriptBuilder.Append(obj.GetUpdateProcedureScript(objDataModel.GetType()));
				procedures.Add("UpdateProcedure", scriptBuilder.ToString());

				// Get Delete Procedure
				scriptBuilder = new StringBuilder();
				scriptBuilder.Append(obj.GetCheckAndDropProcedureScript("Delete"));
				scriptBuilder.AppendLine();
				scriptBuilder.Append(obj.GetDeleteProcedureScript());
				procedures.Add("DeleteProcedure", scriptBuilder.ToString());

				var searchableColumns = new List<string>() { entityName + "Id", "ApplicationId", "Name", "Description" };

				// Get Search Procedure
				scriptBuilder = new StringBuilder();
				scriptBuilder.Append(obj.GetCheckAndDropProcedureScript("Search"));
				scriptBuilder.AppendLine();
				scriptBuilder.Append(obj.GetSearchProcedureScript(searchableColumns, objDataModel.GetType()));
				procedures.Add("SearchProcedure", scriptBuilder.ToString());

			}

			return procedures;

		}

		[WebMethod]
		public static void DeployProcedureText(string entityName, string procedureType)
		{

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
		}

		[WebMethod]
		public static string GetAceEditorNoOfLines()
		{
			string aceEditorNoOfLines = string.Empty;
			aceEditorNoOfLines = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.AceEditorNoOfLines);
			return aceEditorNoOfLines;
		}

		[WebMethod]
		public static void UpdateAceEditorNoOfLines(string aceEditorNoOfLines)
		{
			PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.AceEditorNoOfLines, aceEditorNoOfLines);
		}

		private void BindEntity()
		{
			var entityData = SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);

			UIHelper.LoadDropDown(entityData, drpSystemEntityType,
					SystemEntityTypeDataModel.DataColumns.EntityName,
					SystemEntityTypeDataModel.DataColumns.EntityName);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			//var obj = new TaskNoteDataManager();
			//var sql = obj.GetInsertProcedureScript();
			//editorServer.InnerHtml = sql;

			if (!IsPostBack)
			{
				BindEntity();
			}
			
		}

	}
}