using System;
using System.Collections.Generic;
using System.IO;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.DataAccess;
using Generators.Generator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskTimeTracker.Components.BusinessLayer.Task;

namespace UnitTestCommon
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var t = typeof (DataModel.TaskTimeTracker.ClientDataModel);

			var o = Demo.GenerateInsertStoredProcedure(t);

			using (var outputFile = new StreamWriter(@"C:\temp\" + System.DateTime.Now.ToFileTimeUtc() + "." + t.Name + ".sql"))
			{
				outputFile.Write(o);
			}
		}

		[TestMethod]
		public void TestDynamicTableScripts()
		{
			var systemAuditId = 100006;

			SetupConfiguration.SetConnectionList(systemAuditId);

			var objDataManager = new TaskNoteDataManager();
			objDataManager.EntityName = "TaskNote";

			var sql = objDataManager.GetCheckAndDropTableScript();

			sql = objDataManager.GetCreateTableScript(typeof(TaskNoteDataModel));

			sql = objDataManager.GetCreatePrimaryKeyScript();

			var uniqueColumns = new List<string>() { "Name", "ApplicationId" };

			sql = objDataManager.GetCheckAndDropUniqueIndex(uniqueColumns);
			sql = objDataManager.GetCreateUniqueIndex(uniqueColumns);

		}

		[TestMethod]
		public void TestDynamicProcedureScripts()
		{
			var systemAuditId = 100006;

			SetupConfiguration.SetConnectionList(systemAuditId);

			var objDataManager = new TaskNoteDataManager();

			// Drop Create Procedure
			var sql = objDataManager.GetCheckAndDropProcedureScript("Insert");
			objDataManager.DeployScript(sql);

			// Add Create Procedure
			sql = objDataManager.GetInsertProcedureScript();
			objDataManager.DeployScript(sql);

			// Drop Update Procedure
			sql = objDataManager.GetCheckAndDropProcedureScript("Update");
			objDataManager.DeployScript(sql);

			// Add Update Procedure
			sql = objDataManager.GetUpdateProcedureScript();
			objDataManager.DeployScript(sql);

			// Drop Delete Procedure
			sql = objDataManager.GetCheckAndDropProcedureScript("Delete");
			objDataManager.DeployScript(sql);

			// Add Delete Procedure
			sql = objDataManager.GetDeleteProcedureScript();
			objDataManager.DeployScript(sql);

			// Drop Search Procedure
			sql = objDataManager.GetCheckAndDropProcedureScript("Search");
			objDataManager.DeployScript(sql);

			var searchableColumns = new List<string>() { "TaskNoteId", "ApplicationId", "Name", "Description" };

			// Add Search Procedure
			sql = objDataManager.GetSearchProcedureScript(searchableColumns);
			objDataManager.DeployScript(sql);

		}

		[TestMethod]
		public void TestSendLastErrorInEmail()
		{
			try
			{
				var i = 0;

				// casuse error 

				var l= 7/i;
			}
			catch (Exception)
			{
				Shared.WebCommon.UI.Web.ApplicationCommon.SendLastErrorInEmail(System.Environment.MachineName);

				throw;
			}			
		}

		[TestMethod]
		public void TestTaskNoteCrud()
		{
			//
		}

	}
}
