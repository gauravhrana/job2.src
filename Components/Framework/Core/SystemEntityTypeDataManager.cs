using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Reflection;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class SystemEntityTypeDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static SystemEntityTypeDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("SystemEntityType");
		}

		#region ToSqlParameter

		public static string ToSQLParameter(SystemEntityTypeDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{

				case SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId:
					if (data.SystemEntityTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId, data.SystemEntityTypeId);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);

					}
					break;

				case SystemEntityTypeDataModel.DataColumns.EntityName:
					if (!string.IsNullOrEmpty(data.EntityName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemEntityTypeDataModel.DataColumns.EntityName, data.EntityName.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityTypeDataModel.DataColumns.EntityName);

					}
					break;

				case SystemEntityTypeDataModel.DataColumns.EntityDescription:
					if (!string.IsNullOrEmpty(data.EntityDescription))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemEntityTypeDataModel.DataColumns.EntityDescription, data.EntityDescription.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityTypeDataModel.DataColumns.EntityDescription);

					}
					break;

				case SystemEntityTypeDataModel.DataColumns.PrimaryDatabase:
					if (!string.IsNullOrEmpty(data.PrimaryDatabase))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, SystemEntityTypeDataModel.DataColumns.PrimaryDatabase, data.PrimaryDatabase.Trim());

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityTypeDataModel.DataColumns.PrimaryDatabase);

					}
					break;

				case SystemEntityTypeDataModel.DataColumns.NextValue:
					if (data.NextValue != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemEntityTypeDataModel.DataColumns.NextValue, data.NextValue);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityTypeDataModel.DataColumns.NextValue);

					}
					break;

				case SystemEntityTypeDataModel.DataColumns.IncreaseBy:
					if (data.IncreaseBy != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, SystemEntityTypeDataModel.DataColumns.IncreaseBy, data.IncreaseBy);

					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, SystemEntityTypeDataModel.DataColumns.IncreaseBy);

					}
					break;

				default:
					returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}
			return returnValue;
		}

		#endregion

		#region GetList

        public static List<SystemEntityTypeDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(SystemEntityTypeDataModel.Empty, requestProfile, 0);
        }	

		public static List<SystemEntityTypeDataModel> GetEntityList(RequestProfile requestProfile)
		{
			var list = GetEntityDetails(SystemEntityTypeDataModel.Empty, requestProfile);

			var result = list.Select(item => new SystemEntityTypeDataModel()
			{
					EntityName			= item.EntityName
				,	ApplicationId		= item.ApplicationId
				,	SystemEntityTypeId	= item.SystemEntityTypeId
			}).ToList();


			return result;
		}

		#endregion

		#region GetDetails

        public static SystemEntityTypeDataModel GetDetails(SystemEntityTypeDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<SystemEntityTypeDataModel> GetEntityDetails(SystemEntityTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.SystemEntityTypeSearch ";

			var parameters =
			new
			{
					AuditId					= requestProfile.AuditId				
				,	SystemEntityTypeId		= dataQuery.SystemEntityTypeId
				,	ApplicationMode			= requestProfile.ApplicationModeId
				,	EntityName				= dataQuery.EntityName				
				,	CreatedDate				= dataQuery.CreatedDate
				,	ReturnAuditInfo			= returnAuditInfo
			};

			List<SystemEntityTypeDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<SystemEntityTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(SystemEntityTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			DBDML.RunSQL("SystemEntityType.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(SystemEntityTypeDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("SystemEntityType.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(SystemEntityTypeDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.SystemEntityTypeDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				SystemEntityTypeId = dataQuery.SystemEntityTypeId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(SystemEntityTypeDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

		#region Save

		private static string Save(SystemEntityTypeDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.SystemEntityTypeInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					//" " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
					break;

				case "Update":
					sql += "dbo.SystemEntityTypeUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					//" " + BaseColumns.ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
					break;

				default:
					break;

			}

			sql = sql + ",  " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId) +
						", " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.EntityName) +
						", " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.EntityDescription) +
						", " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.NextValue) +
						", " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.PrimaryDatabase) +
						", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.CreatedDate);
			//", " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.CreatedDate);
			//", " + ToSQLParameter(data, DataModel.Framework.Core.SystemEntityType.DataColumns.IncreaseBy);
			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(SystemEntityTypeDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new SystemEntityTypeDataModel();
			doesExistRequest.EntityName = data.EntityName;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		public static string GetFullName(int systemEntityTypeId, RequestProfile requestProfile)
		{
			var data = new SystemEntityTypeDataModel();
			data.SystemEntityTypeId = systemEntityTypeId;
			var item = GetDetails(data, requestProfile);
			var value = item.EntityName + " " + item.EntityDescription;

			return value;
		}

		public static int GetNextSequence(string entityName, int systemEntityTypeId, RequestProfile requestProfile)
		{
			var data = new SystemEntityTypeDataModel();
			data.SystemEntityTypeId = systemEntityTypeId;
			data.EntityName = entityName;

			var sql = "EXEC dbo.SystemEntityTypeGetNextSequenceTemp " +
				 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				 ", " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId) +
				 ", " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.EntityName) +
				 ", " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.NextValue);

			var newId = DBDML.RunScalarSQL("SystemEntityTypeGetNextSequence", sql, DataStoreKey).ToString();
			//var newId = int.Parse(DBDML.RunScalarSQL("SystemEntityTypeGetNextSequence", sql, DataStoreKey).ToString());
			return int.Parse(newId);

		}

		public static int GetNextSequenceId(string entityName, int systemEntityTypeId, RequestProfile requestProfile)
		{
			var data = new SystemEntityTypeDataModel();
			data.SystemEntityTypeId = systemEntityTypeId;
			data.EntityName = entityName;
			var sql = "EXEC dbo.SystemEntityTypeGetNextSequence " +
				 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				 ", " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId) +
				 ", " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.EntityName) +
				 ", " + ToSQLParameter(data, SystemEntityTypeDataModel.DataColumns.NextValue);

			var newId = new DBDataTable("SystemEntityTypeGetNextSequence", sql, DataStoreKey);

			//var newId = int.Parse(DBDML.RunScalarSQL("SystemEntityTypeGetNextSequence", sql, DataStoreKey).ToString());
			return int.Parse(newId.DBTable.Rows[0][0].ToString());

		}

		public static int GetEntityMinId(string entityName)
		{
			var minId = 0;

			var SQLKey = ".SQL.GetEntityMinId.sql";

			// Get SQL Template and replace parameters
			var assembly = Assembly.GetExecutingAssembly();
			var scriptTemplate = GetResourceText(assembly, SQLKey);

			var replacementFieldSet = new Dictionary<string, string>();
			var replacementOtherSet = new Dictionary<string, string>();

			replacementOtherSet.Add("@EntityName@", entityName);

			var sql = GetSQL(replacementFieldSet, replacementOtherSet, scriptTemplate, assembly.GetName().Name + SQLKey);

			var id = DBDML.RunScalarSQL("EntityName.GetMinId", sql, DataStoreKey);
			minId = Convert.ToInt32(id);

			return minId;
		}

		public static int GetEntitySuperKeyForRandomIds(string entityName, int systemEntityTypeId, RequestProfile requestProfile)
		{
			var SQLKey = ".SQL.GetEntitySuperKeyForRandomIds.sql";

			// Get SQL Template and replace parameters
			var assembly = Assembly.GetExecutingAssembly();
			var scriptTemplate = GetResourceText(assembly, SQLKey);

			var replacementFieldSet = new Dictionary<string, string>();
			var replacementOtherSet = new Dictionary<string, string>();

			replacementOtherSet.Add("@ApplicationId@", requestProfile.ApplicationId.ToString());
			replacementOtherSet.Add("@AuditId@", requestProfile.AuditId.ToString());
			replacementOtherSet.Add("@EntityName@", entityName);
			replacementOtherSet.Add("@SystemEntityTypeId@", systemEntityTypeId.ToString());
			replacementOtherSet.Add("@ExpirationDate@", DateTime.Now.AddDays(30).ToString("yyyyMMdd"));
			replacementOtherSet.Add("@Description@", "'" + Convert.ToString(systemEntityTypeId + " : " + DateTime.Now.AddDays(30).ToString("yyyyMMdd")) + "'");
			replacementOtherSet.Add("@Name@", "'" + Convert.ToString(systemEntityTypeId + " : " + " : " + DateTime.Now.ToLongTimeString() + DateTime.Now.Ticks) + "'");

			var sql = GetSQL(replacementFieldSet, replacementOtherSet, scriptTemplate, assembly.GetName().Name + SQLKey);

			var id = DBDML.RunScalarSQL("EntityName.GetEntitySuperKeyForRandomIds", sql, DataStoreKey);
			var superKeyId = Convert.ToInt32(id);

			return superKeyId;
		}
		
		public static DataTable EntityTestData(RequestProfile requestProfile)
		{
			// add columns as entityname, application id, entity id, test data count, audit data count		
			DataTable result = new DataTable();
			
			result.Columns.Add("Entity Id");
			result.Columns.Add("Application Id");
			result.Columns.Add("Entity Name");
			result.Columns.Add("Test Data Count");
			result.Columns.Add("Audit Data Count");

			DataRow tempRow = null;
			
			var dtEntities = GetList(requestProfile);
			// i think u'll have to check this if it is using some application id or not for fetching data,, if not plz modify it
			var dtSetupConfiguration = SetupConfiguration.GetList();

			foreach (DataRow row in dtSetupConfiguration.Rows)
			{

				var entityName = row["EntityName"].ToString();
				var entityId = 0;

                if (dtEntities.Where(x => x.EntityName == entityName).Any())
				{
					entityId = dtEntities.Where(x => x.EntityName == entityName).First().SystemEntityTypeId.Value;
				}

				// Execute step2 

				var count = 0;

				var SQLKey = ".SQL.GetEntityTestDataCount.sql";

				// Get SQL Template and replace parameters
				var assembly = Assembly.GetExecutingAssembly();
				var scriptTemplate = GetResourceText(assembly, SQLKey);

				var replacementFieldSet = new Dictionary<string, string>();
				var replacementOtherSet = new Dictionary<string, string>();

				replacementOtherSet.Add("@EntityName@", entityName);

				var sql = GetSQL(replacementFieldSet, replacementOtherSet, scriptTemplate, assembly.GetName().Name + SQLKey);

				var id = DBDML.RunScalarSQL("EntityName.GetCount", sql, DataStoreKey);
				count = Convert.ToInt32(id);
				
				//execute step 3

				var noOfAudit = 0;

				var SQLAuditCount = ".SQL.GetEntityDetails.sql";

				var scriptAuditTemplate = GetResourceText(assembly, SQLAuditCount);

				var replacementFieldSetAudit = new Dictionary<string, string>();
				var replacementOtherSetAudit = new Dictionary<string, string>();

				replacementOtherSetAudit.Add("@EntityName@", entityName);
				replacementOtherSetAudit.Add("@SystemEntityTypeId@", entityId.ToString());

				var sqlAudit = GetSQL(replacementFieldSetAudit, replacementOtherSetAudit, scriptAuditTemplate, assembly.GetName().Name + SQLAuditCount);

				var noOfAuditRecords = DBDML.RunScalarSQL("EntityName.GetAuditCount", sqlAudit, DataStoreKey);
				noOfAudit = Convert.ToInt32(noOfAuditRecords);
				// create a row in result table
				 tempRow = result.NewRow();
				 // add the data in row
				 tempRow["Entity Id"] = entityId.ToString();
				 tempRow["Application Id"] = requestProfile.ApplicationId.ToString();
				 tempRow["Entity Name"] = entityName;
				 tempRow["Test Data Count"] = count;
				 tempRow["Audit Data Count"] = noOfAudit;

				 result.Rows.Add(tempRow);

			}

			return result;

		}

		public static DataTable EntityIncorrectNextValueData(RequestProfile requestProfile,int applicationId)
		{
			// add columns as entityname, application id, entity id, test data count, audit data count		
			DataTable result = new DataTable();

			result.Columns.Add("Entity Id");
			result.Columns.Add("Application Id");
			result.Columns.Add("Entity Name");
			result.Columns.Add("Next PrimaryKey Value");
			result.Columns.Add("Next Value");

			DataRow tempRow = null;

			var dtEntities = GetList(requestProfile);
			// i think u'll have to check this if it is using some application id or not for fetching data,, if not plz modify it
			var dtSetupConfiguration = SetupConfiguration.GetList(applicationId);

			foreach (DataRow row in dtSetupConfiguration.Rows)
			{
				var entityName = row["EntityName"].ToString();
				var appId = row["ApplicationId"].ToString();
				var entityId = 0;

                if (dtEntities.Where(x => x.EntityName == entityName).Any())
                {
                    entityId = dtEntities.Where(x => x.EntityName == entityName).First().SystemEntityTypeId.Value;
                }

				// Execute step2 

				var maxValue = 0;

				var SQLKey = ".SQL.GetEntityMaxId.sql";

				// Get SQL Template and replace parameters
				var assembly = Assembly.GetExecutingAssembly();
				var scriptTemplate = GetResourceText(assembly, SQLKey);

				var replacementFieldSet = new Dictionary<string, string>();
				var replacementOtherSet = new Dictionary<string, string>();

				replacementOtherSet.Add("@EntityName@", entityName);

				var sql = GetSQL(replacementFieldSet, replacementOtherSet, scriptTemplate, assembly.GetName().Name + SQLKey);

				var maxId = DBDML.RunScalarSQL("EntityName.GetEntityMaxId", sql, DataStoreKey);
				if (maxId != System.DBNull.Value)
					maxValue = Convert.ToInt32(maxId);							
				else
					maxValue = 0;

				//execute step 3

				var nextIdValue = 0;

				var SQLAuditCount = ".SQL.GetNextValue.sql";

				var scriptAuditTemplate = GetResourceText(assembly, SQLAuditCount);

				var replacementFieldSetAudit = new Dictionary<string, string>();
				var replacementOtherSetAudit = new Dictionary<string, string>();

				replacementOtherSetAudit.Add("@EntityName@", entityName);				

				var sqlAudit = GetSQL(replacementFieldSetAudit, replacementOtherSetAudit, scriptAuditTemplate, assembly.GetName().Name + SQLAuditCount);

				var nextValue = DBDML.RunScalarSQL("EntityName.GetNextValue", sqlAudit, DataStoreKey);
				nextIdValue = Convert.ToInt32(nextValue);
				// create a row in result table
				if (nextIdValue != (maxValue + 1))
				{
					tempRow = result.NewRow();
					// add the data in row
					tempRow["Entity Id"] = entityId.ToString();
					if (applicationId != -1)
						tempRow["Application Id"] = applicationId.ToString();
					else
						tempRow["Application Id"] = appId;
					tempRow["Entity Name"] = entityName;
					tempRow["Next PrimaryKey Value"] = maxValue+1;
					tempRow["Next Value"] = nextIdValue;

					result.Rows.Add(tempRow);
				}

			}

			return result;

		}

		public static DataTable EntityTestData(RequestProfile requestProfile,int applicationId,string appName,string entityName)
		{
			// add columns as entityname, application id, entity id, test data count, audit data count		
			DataTable result = new DataTable();

			result.Columns.Add("Entity Id");
			result.Columns.Add("Application Id");
			result.Columns.Add("Entity Name");
			result.Columns.Add("Test Data Count");
			result.Columns.Add("Audit Data Count");

			DataRow tempRow = null;

			//if (applicationId != -1)
			var	dtEntities = GetList(requestProfile);
			//else
			//{
				//RequestProfile rp=new RequestProfile();
				//rp.ApplicationId = applicationId;
				//dtEntities = GetList(null);
			//}

			DataTable dtSetupConfiguration=null;

			// i think u'll have to check this if it is using some application id or not for fetching data,, if not plz modify it
			if(applicationId != -1)
				 dtSetupConfiguration = SetupConfiguration.GetList(-1, appName);
			else
				dtSetupConfiguration = SetupConfiguration.GetList(-1, null);

			if (entityName != "All")
			{
				var entityId = 0;

                var tmpRows = dtEntities.Where(x => x.EntityName == entityName).ToList();

                if (tmpRows.Count > 0)
                {
                    entityId = tmpRows[0].SystemEntityTypeId.Value;
                }

				// Execute step2 

				var count = 0;

				var SQLKey = ".SQL.GetEntityTestDataCount.sql";

				// Get SQL Template and replace parameters
				var assembly = Assembly.GetExecutingAssembly();
				var scriptTemplate = GetResourceText(assembly, SQLKey);

				var replacementFieldSet = new Dictionary<string, string>();
				var replacementOtherSet = new Dictionary<string, string>();

				replacementOtherSet.Add("@EntityName@", entityName);
				if (applicationId != -1)
					replacementOtherSet.Add("@ApplicationId@", applicationId.ToString());
				else
					replacementOtherSet.Add("@ApplicationId@", requestProfile.ApplicationId.ToString());
				var sql = GetSQL(replacementFieldSet, replacementOtherSet, scriptTemplate, assembly.GetName().Name + SQLKey);

				var id = DBDML.RunScalarSQL("EntityName.GetCount", sql, DataStoreKey);
				count = Convert.ToInt32(id);

				//execute step 3

				var noOfAudit = 0;

				var SQLAuditCount = ".SQL.GetEntityDetails.sql";

				var scriptAuditTemplate = GetResourceText(assembly, SQLAuditCount);

				var replacementFieldSetAudit = new Dictionary<string, string>();
				var replacementOtherSetAudit = new Dictionary<string, string>();

				replacementOtherSetAudit.Add("@EntityName@", entityName);
				replacementOtherSetAudit.Add("@SystemEntityTypeId@", entityId.ToString());

				var sqlAudit = GetSQL(replacementFieldSetAudit, replacementOtherSetAudit, scriptAuditTemplate, assembly.GetName().Name + SQLAuditCount);

				var noOfAuditRecords = DBDML.RunScalarSQL("EntityName.GetAuditCount", sqlAudit, DataStoreKey);
				noOfAudit = Convert.ToInt32(noOfAuditRecords);
				// create a row in result table
				tempRow = result.NewRow();
				// add the data in row
				tempRow["Entity Id"] = entityId.ToString();
				if (applicationId !=-1)
					tempRow["Application Id"] = applicationId.ToString();
				else
					tempRow["Application Id"] = requestProfile.ApplicationId;
				//tempRow["Application Id"] = appId;
				tempRow["Entity Name"] = entityName;
				tempRow["Test Data Count"] = count;
				tempRow["Audit Data Count"] = noOfAudit;

				result.Rows.Add(tempRow);
			}
			else
			{
				foreach (DataRow row in dtSetupConfiguration.Rows)
				{
					 entityName = row["EntityName"].ToString();
					 var appId = row["ConnectionKeyName"].ToString();

					var entityId = 0;

                    if (dtEntities.Where(x => x.EntityName == entityName).Any())
                    {
                        entityId = dtEntities.Where(x => x.EntityName == entityName).First().SystemEntityTypeId.Value;
                    }

					// Execute step2 

					var count = 0;

					var SQLKey = ".SQL.GetEntityTestDataCount.sql";

					// Get SQL Template and replace parameters
					var assembly = Assembly.GetExecutingAssembly();
					var scriptTemplate = GetResourceText(assembly, SQLKey);

					var replacementFieldSet = new Dictionary<string, string>();
					var replacementOtherSet = new Dictionary<string, string>();

					replacementOtherSet.Add("@EntityName@", entityName);

					if (applicationId != -1)
						replacementOtherSet.Add("@ApplicationId@", applicationId.ToString());
					else
						replacementOtherSet.Add("@ApplicationId@", requestProfile.ApplicationId.ToString());

					var sql = GetSQL(replacementFieldSet, replacementOtherSet, scriptTemplate, assembly.GetName().Name + SQLKey);

					var id = DBDML.RunScalarSQL("EntityName.GetCount", sql, DataStoreKey);
					count = Convert.ToInt32(id);

					//execute step 3

					var noOfAudit = 0;

					var SQLAuditCount = ".SQL.GetEntityDetails.sql";

					var scriptAuditTemplate = GetResourceText(assembly, SQLAuditCount);

					var replacementFieldSetAudit = new Dictionary<string, string>();
					var replacementOtherSetAudit = new Dictionary<string, string>();

					replacementOtherSetAudit.Add("@EntityName@", entityName);
					replacementOtherSetAudit.Add("@SystemEntityTypeId@", entityId.ToString());

					var sqlAudit = GetSQL(replacementFieldSetAudit, replacementOtherSetAudit, scriptAuditTemplate, assembly.GetName().Name + SQLAuditCount);

					var noOfAuditRecords = DBDML.RunScalarSQL("EntityName.GetAuditCount", sqlAudit, DataStoreKey);
					noOfAudit = Convert.ToInt32(noOfAuditRecords);
					// create a row in result table
					tempRow = result.NewRow();
					// add the data in row
					tempRow["Entity Id"] = entityId.ToString();
					if (applicationId !=-1)
						tempRow["Application Id"] = applicationId.ToString();
					else
						tempRow["Application Id"] = appId;
					tempRow["Entity Name"] = entityName;
					tempRow["Test Data Count"] = count;
					tempRow["Audit Data Count"] = noOfAudit;

					result.Rows.Add(tempRow);
				}
			
			}

			return result;

		}
	}
}
