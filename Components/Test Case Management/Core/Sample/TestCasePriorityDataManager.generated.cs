//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using Dapper;
//using Framework.Components.DataAccess;
//using DataModel.Framework.DataAccess;
//using DataModel.TestCaseManagement;

//namespace TestCaseManagement.Components.DataAccess
//{
//	public partial class TestCasePriorityDataManager : StandardDataManager
//	{

//		private static string DataStoreKey = string.Empty;

//		static TestCasePriorityDataManager()
//		{
//			DataStoreKey = SetupConfiguration.GetDataStoreKey("TestCasePriority");
//		}

//		#region ToSQLParameter

//		public static string ToSQLParameter(TestCasePriorityDataModel data, string dataColumnName)
//		{
//			var returnValue = "NULL";

//			switch (dataColumnName)
//			{
//				case TestCasePriorityDataModel.DataColumns.TestCasePriorityId:
//					if (data.TestCasePriorityId != null)
//					{
//						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TestCasePriorityDataModel.DataColumns.TestCasePriorityId, data.TestCasePriorityId);
//					}
//					else
//					{
//						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TestCasePriorityDataModel.DataColumns.TestCasePriorityId);
//					}
//					break;

//				default:
//					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
//					break;
//			}

//			return returnValue;

//		}

//		#endregion

//		#region Get Entity Details

//		public static List<TestCasePriorityDataModel> GetEntityDetails(TestCasePriorityDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
//		{
//			const string sql = @"dbo.TestCasePrioritySearch ";

//			var parameters =
//			new
//			{
//					AuditId                 = requestProfile.AuditId
//				 ,	ApplicationId           = requestProfile.ApplicationId
//				 ,	ApplicationMode         = requestProfile.ApplicationModeId
//				 ,	ReturnAuditInfo         = returnAuditInfo
//				 ,	TestCasePriorityId                  = dataQuery.TestCasePriorityId
//				 ,	Name                    = dataQuery.Name
//			};

//			List<TestCasePriorityDataModel> result;
//			using (var dataAccess = new DataAccessBase(DataStoreKey))
//			{
//				result = dataAccess.Connection.Query<TestCasePriorityDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
//			}

//			return result;

//		}

//		#endregion

//		#region Get List

//		public static DataTable GetList(RequestProfile requestProfile)
//		{
//			var list = GetEntityDetails(TestCasePriorityDataModel.Empty, requestProfile, 0);
//			return list.ToDataTable();
//		}

//		#endregion

//		#region Search

//		public static DataTable Search(TestCasePriorityDataModel data, RequestProfile requestProfile)
//		{
//			var list = GetEntityDetails(data, requestProfile, 0);
//			return list.ToDataTable();
//		}

//		#endregion

//		#region GetDetails

//		public static DataTable GetDetails(TestCasePriorityDataModel data, RequestProfile requestProfile)
//		{
//			var list = GetEntityDetails(data, requestProfile, 0);
//			return list.ToDataTable();
//		}

//		#endregion

//		#region Save

//		public static string Save(TestCasePriorityDataModel data, string action, RequestProfile requestProfile)
//		{

//			var sql = "EXEC ";

//			switch (action)
//			{
//				case "Create":
//					sql += "dbo.TestCasePriorityInsert  " +
//						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
//						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
//					break;

//				case "Update":
//					sql += "dbo.TestCasePriorityUpdate  " +
//						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
//					break;

//				default:
//					break;
//			}
//			sql = sql + ", " + ToSQLParameter(data, TestCasePriorityDataModel.DataColumns.TestCasePriorityId) +
//				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
//				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
//				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

//			return sql;
//		}

//		#endregion

//		#region Create

//		public static int Create(TestCasePriorityDataModel data, RequestProfile requestProfile)
//		{
//			var sql = Save(data, "Create", requestProfile);
//			var newId = DBDML.RunScalarSQL("TestCasePriority.Insert", sql, DataStoreKey);
//			return Convert.ToInt32(newId);
//		}

//		#endregion

//		#region Update

//		public static void Update(TestCasePriorityDataModel data, RequestProfile requestProfile)
//		{
//			var sql = Save(data, "Update", requestProfile);
//			DBDML.RunSQL("TestCasePriority.Update", sql, DataStoreKey);
//		}

//		#endregion

//		#region Delete

//		public static void Delete(TestCasePriorityDataModel data, RequestProfile requestProfile)
//		{

//			const string sql = @"dbo.TestCasePriorityDelete ";

//			var parameters =
//			new
//			{
//					AuditId = requestProfile.AuditId
//				,   TestCasePriorityId  = data.TestCasePriorityId
//			};

//			using (var dataAccess = new DataAccessBase(DataStoreKey))
//			{
//				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
//			}
//		}

//		#endregion

//		#region Does Exist

//		public static DataTable DoesExist(TestCasePriorityDataModel data, RequestProfile requestProfile)
//		{
//			var doesExistRequest = new TestCasePriorityDataModel();
//			doesExistRequest.Name = data.Name;
//			return Search(doesExistRequest, requestProfile);
//		}

//		#endregion

//		#region Renumber

//		public static void Renumber(int seed, int increment, RequestProfile requestProfile)
//		{
//			var sql = "EXEC dbo.TestCasePriorityRenumber" +
//				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
//				", @Seed = " + seed +
//				", @Increment = " + increment;
//			Framework.Components.DataAccess.DBDML.RunSQL("TestCasePriority.Renumber", sql, DataStoreKey);
//		}

//		#endregion

//		#region Get Children

//		public static DataSet GetChildren(TestCasePriorityDataModel data, RequestProfile requestProfile)
//		{
//			var sql = "EXEC dbo.TestCasePriorityChildrenGet" +
//				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
//				", " + ToSQLParameter(data, TestCasePriorityDataModel.DataColumns.TestCasePriorityId);
//			var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
//			return oDT.DBDataset;
//		}

//		#endregion

//		#region IsDeletable

//		public static bool IsDeletable(TestCasePriorityDataModel data, RequestProfile requestProfile)
//		{
//			var isDeletable = true;
//			var ds = GetChildren(data, requestProfile);
//			if (ds != null && ds.Tables.Count > 0)
//			{
//				foreach (DataTable dt in ds.Tables)
//				{
//					if (dt.Rows.Count > 0)
//					{
//						isDeletable = false;
//						break;
//					}
//				}
//			}
//			return isDeletable;
//		}

//		#endregion

//	}
//}
